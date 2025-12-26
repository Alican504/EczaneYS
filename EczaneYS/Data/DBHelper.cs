using Npgsql;
using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EczaneYS.Data
{
    public static class DBHelper
    {
        private static readonly string connectionString;

        static DBHelper()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            connectionString = config.GetConnectionString("Postgres");
        }

        // Yeni connection üretir
        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }

        // ============================
        // SELECT → DataTable
        // ============================
        public static DataTable GetDataTable(string query, params object[] parameters)
        {
            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(query, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
            {
                for (int i = 0; i < parameters.Length; i += 2)
                {
                    cmd.Parameters.AddWithValue(
                        parameters[i].ToString(),
                        parameters[i + 1] ?? DBNull.Value
                    );
                }

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // ============================
        // INSERT / UPDATE / DELETE
        // ============================
        public static int ExecuteNonQuery(string query, params object[] parameters)
        {
            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                conn.Open();

                for (int i = 0; i < parameters.Length; i += 2)
                {
                    cmd.Parameters.AddWithValue(
                        parameters[i].ToString(),
                        parameters[i + 1] ?? DBNull.Value
                    );
                }

                return cmd.ExecuteNonQuery();
            }
        }

        // ============================
        // SCALAR (tek değer)
        // ============================
        public static object ExecuteScalar(string query, params object[] parameters)
        {
            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                conn.Open();

                for (int i = 0; i < parameters.Length; i += 2)
                {
                    cmd.Parameters.AddWithValue(
                        parameters[i].ToString(),
                        parameters[i + 1] ?? DBNull.Value
                    );
                }

                return cmd.ExecuteScalar();
            }
        }

        // ============================
        // PROCEDURE ÇAĞIR (LOG UYUMLU)
        // ============================
        public static void ExecuteProcedure(
            string procedureCall,
            int currentUserId,
            params object[] parameters
        )
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                // Log trigger için kullanıcıyı DB session’a yaz
                using (var setCmd = new NpgsqlCommand("SET app.current_user = @uid", conn))
                {
                    setCmd.Parameters.AddWithValue("@uid", currentUserId);
                    setCmd.ExecuteNonQuery();
                }

                using (var cmd = new NpgsqlCommand(procedureCall, conn))
                {
                    for (int i = 0; i < parameters.Length; i += 2)
                    {
                        cmd.Parameters.AddWithValue(
                            parameters[i].ToString(),
                            parameters[i + 1] ?? DBNull.Value
                        );
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
