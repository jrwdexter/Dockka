using System;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Serilog;

namespace Dockka.Data.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Wait for the SQL server connection to be active before continuing.
        /// </summary>
        public static DbContextOptionsBuilder<T> WaitForActiveServer<T>(this DbContextOptionsBuilder<T> options, TimeSpan timeout) where T : DbContext
        {
            return WaitForActiveServer(options as DbContextOptionsBuilder, timeout) as DbContextOptionsBuilder<T>;
        }
        /// <summary>
        /// Wait for the SQL server connection to be active before continuing.
        /// </summary>
        public static DbContextOptionsBuilder WaitForActiveServer(this DbContextOptionsBuilder options, TimeSpan timeout)
        {
            var cancellationToken = new CancellationTokenSource(timeout);

            var sqlExtension = options.Options.GetExtension<SqlServerOptionsExtension>();
            if (sqlExtension == null)
                throw new NullReferenceException("No SqlServerOptionsExtension found in DbContextOptionsBuilder options.");

            TestSqlConnection(sqlExtension.ConnectionString, cancellationToken.Token);
            return options;
        }

        private static void TestSqlConnection(string connectionString, CancellationToken token)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (SqlException e)
                {
                    if (token.IsCancellationRequested)
                    {
                        Log.Warning(e, $"Could not access database.");
                        return;
                    }

                    Thread.Sleep(100);
                }
            }
        }
    }
}
