const path = require("path");
const webpack = require("webpack");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const CheckerPlugin = require("awesome-typescript-loader").CheckerPlugin;
const merge = require("webpack-merge");
const UglifyJsPlugin = require('uglifyjs-webpack-plugin')

module.exports = env => {
  const isDevBuild = !(env && env.prod);
  console.log(isDevBuild ? "Dev Build" : "");

  // Configuration in common to both client-side and server-side bundles
  const sharedConfig = () => ({
    stats: { modules: false },
    resolve: { extensions: [".js", ".jsx", ".ts", ".tsx"] },
    output: {
      filename: "[name].js",
      publicPath: "dist/" // Webpack dev middleware, if enabled, handles requests for this URL prefix
    },
    module: {
      rules: [
        {
          test: /\.tsx?$/,
          include: /ClientApp/,
          use: "awesome-typescript-loader?silent=true"
        },
        { test: /\.(png|jpg|jpeg|gif|svg)$/, use: "url-loader?limit=25000" }
      ]
    },
    plugins: [new CheckerPlugin()]
  });

  // Configuration for client-side bundle suitable for running in browsers
  const clientBundleOutputDir = "./wwwroot/dist";
  const clientBundleConfig = merge(sharedConfig(), {
    entry: { "main-client": "./ClientApp/boot-client.tsx" },
    module: {
      rules: [
        {
          test: /\.s?css$/,
          use: ExtractTextPlugin.extract({
            use: [
              {
                loader: "css-loader",
                options: {
                  modules: true,
                  sourceMap: true,
                  importLoaders: 1,
                  localIdentName: "[name]--[local]--[hash:base64:8]",
                  minimize: isDevBuild
                }
              },
              {
                loader: "postcss-loader",
                options: {
                  sourceMap: isDevBuild
                }
              },
              {
                loader: "sass-loader",
                options: {
                  sourceMap: isDevBuild,
                  outputStyle: "nested"
                }
              }
            ]
          })
        }
      ]
    },
    output: { path: path.join(__dirname, clientBundleOutputDir) },
    plugins: [
      new ExtractTextPlugin("site.css"),
      new webpack.DllReferencePlugin({
        context: __dirname,
        manifest: require("./wwwroot/dist/vendor-manifest.json")
      })
    ].concat(
      isDevBuild
        ? [
            // Plugins that apply in development builds only
            new webpack.SourceMapDevToolPlugin({
              filename: "[file].map", // Remove this line if you prefer inline source maps
              moduleFilenameTemplate: path.relative(
                clientBundleOutputDir,
                "[resourcePath]"
              ) // Point sourcemap entries to the original file locations on disk
            })
          ]
        : [
            // Plugins that apply in production builds only
            new UglifyJsPlugin()
          ]
    )
  });

  // Configuration for server-side (prerendering) bundle suitable for running in Node
  const serverBundleConfig = merge(sharedConfig(), {
    resolve: { mainFields: ["main"] },
    entry: { "main-server": "./ClientApp/boot-server.tsx" },
    module: {
      rules: [
        {
          test: /\.s?css$/,
          use: [
            {
              loader: "css-loader",
              options: {
                modules: true, // default is false
                sourceMap: true,
                importLoaders: 1,
                localIdentName: "[name]--[local]--[hash:base64:8]"
              }
            },
            "postcss-loader",
            "sass-loader"
          ]
        }
      ]
    },
    plugins: [
      new webpack.DllReferencePlugin({
        context: __dirname,
        manifest: require("./ClientApp/dist/vendor-manifest.json"),
        sourceType: "commonjs2",
        name: "./vendor"
      })
    ],
    output: {
      libraryTarget: "commonjs",
      path: path.join(__dirname, "./ClientApp/dist")
    },
    target: "node",
    devtool: "inline-source-map"
  });

  return [clientBundleConfig, serverBundleConfig];
};
