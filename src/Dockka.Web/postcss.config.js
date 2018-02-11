const reactToolboxVariables = {
  // 'color-primary': "#093A4A",
  'unit': "10px",
  'preferred-font': "'Noto Sans', 'Helvetica', 'Arial', sans-serif",
  'input-line-height': '18px',
  // 'color-divider': 
  // 'color-background': 
  'color-text': '#1F1F1F',
  'color-text-secondary': '#3A3A3A',
  'color-primary': '#1A6774',
  'color-primary-dark': '#093A4A',
  'color-accent': '#FF6600',
  'color-accent-dark': '#CFDE51' // This is actually lighter
  // 'color-primary-contrast': 
  // 'color-accent-contrast': 
}

module.exports = {
  plugins: {
    // 'postcss-import': {
    //   root: __dirname,
    // },
    'postcss-mixins': {},
    // 'postcss-each': {},
    'postcss-cssnext': {
      features: {
        customProperties: {
          variables: reactToolboxVariables
        }
      }
    }
  }
};
