using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AZepher.Postgres
{
    public class postgres
    {
        private string _connstr;
        private NpgsqlConnection _conn;
        public postgres(IConfiguration config)
        {
            _connstr = config["ConnectionString"];
        }

        private void openConnection()
        {
            _conn = new NpgsqlConnection(_connstr);
            _conn.Open();
        }

        public void execute(string command)
        {
            if (_conn == null || _conn.State != ConnectionState.Open)
                openConnection();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = _conn;
                cmd.CommandText = "INSERT INTO data (some_field) VALUES (@p)";
                cmd.Parameters.AddWithValue("p", "Hello world");
                cmd.ExecuteNonQuery();
            }

        }

        public string read( string sql)
        {
            StringBuilder data = new StringBuilder();
            if (_conn == null || _conn.State != ConnectionState.Open)
                openConnection();
            using (var cmd = new NpgsqlCommand(sql, _conn))
            using (var reader = cmd.ExecuteReader())
            {
                if (!reader.HasRows)
                    return "[]";
                while (reader.Read())
                {
                    data.Append("[");+
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        data.Append("{");
                        var ft = reader.GetDataTypeName(i);
                        if(ft.Contains("var"))
                            data.Append($"{reader.GetName(i)}: \"{reader.GetString(i).Replace("\"", "\\\"")}\"" );
                        if(ft.Contains("bool"))
                            data.Append($"{reader.GetName(i)}: {reader.GetByte(i)}" );
                        if(ft.Contains("byte") )
                            data.Append($"{reader.GetName(i)}: {reader.GetByte(i)}" );
                        if(ft.Contains("int") )
                            data.Append($"{reader.GetName(i)}: {reader.GetInt64(i)}" );
                        if(ft.Contains("float") )
                            data.Append($"{reader.GetName(i)}: {reader.GetFloat(i)}" );
                        if(ft.Contains("date") )
                            data.Append($"{reader.GetName(i)}: \"{reader.GetDate(i)}\"" );
                    }
                    data.Append("],");
                    
                    
                }
            }

            data.Length--;
            return data.ToString();
        }
        public void doSomething()
        {
            

            using (var conn = new NpgsqlConnection(_connstr))
            {
                conn.Open();

                // Insert some data
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO data (some_field) VALUES (@p)";
                    cmd.Parameters.AddWithValue("p", "Hello world");
                    cmd.ExecuteNonQuery();
                }

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand("SELECT some_field FROM data", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        Console.WriteLine(reader.GetString(0));
            }
        }
    }
}
