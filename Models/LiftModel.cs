
using System;
using System.Collections;
using LiftSystem.DTO;
using MySql.Data.MySqlClient;

namespace LiftSystem.Model
{
    public class LiftModel
    {
        private MySqlCommand _command;

        public void Log(string msg)
        { 
            _command = DatabaseConnector.GetConnection();
            
            _command.CommandText = $"insert into logs(message) values('{msg}');";
            _command.ExecuteNonQuery();
            
            DatabaseConnector.CloseConnection();
            LogsEventEmitter.EmitLog((int)_command.LastInsertedId, msg, new DateTime().ToString());
        }

        public ArrayList GetAllLogs()
        {
            _command = DatabaseConnector.GetConnection();
            var list = new ArrayList();
            _command.CommandText = "select * from logs order by id desc";
            var result = _command.ExecuteReader();

            while (result.Read())
            {
                list.Add(new Log(
                    result.GetInt32("id"),
                    result.GetString("message"),
                    result.GetDateTime("created").ToString()
                ));
            }
            result.Close();
            DatabaseConnector.CloseConnection();
            
            return list;
        }
    }
}