
using System.Collections;
using LiftSystem.DTO;
using MySql.Data.MySqlClient;

namespace LiftSystem.Model
{
    public class LiftModel
    {
        private MySqlCommand _command = DatabaseConnector.GetConnection();

        public void Log(string msg)
        {
            _command.CommandText = @"insert into logs(message) values({msg})";
            _command.ExecuteNonQuery();
        }

        public ArrayList GetAllLogs()
        {
            var list = new ArrayList();
            _command.CommandText = "select * from logs";
            var result = _command.ExecuteReader();

            while (result.Read())
            {
                list.Add(new Log(
                    result.GetInt32("id"),
                    result.GetString("message")
                ));
            }
            result.Close();
            
            return list;
        }
    }
}