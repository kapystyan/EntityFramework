using EF.Properties;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var i in GetServerData())
            {
                Console.WriteLine(string.Join(" | ", i.ID, i.nickname, i.password, i.donatename, i.vk, i.telegram) + "\n");
            }
        }
        private static List<ServerData> GetServerData()
        {
            using (IDbConnection connection = new SqlConnection(Settings.Default.DBConnect))
            {
                IDbCommand command = new SqlCommand("select * from player");
                command.Connection = connection;
                connection.Open();
                IDataReader reader = command.ExecuteReader();
                List<ServerData> data = new List<ServerData>();
                while (reader.Read())
                {
                    ServerData serverdata = new ServerData();
                    serverdata.ID = reader.GetInt32(0);
                    serverdata.nickname = reader.GetString(1);
                    serverdata.password = reader.GetString(2);
                    serverdata.donatename = reader.GetString(3);
                    serverdata.vk = reader.GetString(4);
                    serverdata.telegram = reader.GetString(5);
                    data.Add(serverdata);
                }
                return data;
            }
        }
    }
}
