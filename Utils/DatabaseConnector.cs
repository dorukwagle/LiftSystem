using MySql.Data.MySqlClient;

namespace LiftSystem
{
    public static class DatabaseConnector
    {
        private static MySqlConnection _connection;
        private static MySqlCommand _command;
        
        public static MySqlCommand GetConnection()
        {
            if (_connection == null)
                _connection = new MySqlConnection(Constants.SqlConnectionString);
            if (_command == null)
            {
                _command = new MySqlCommand();
                _command.Connection = _connection;
            }

            _connection.Open();
            return _command;
        }

        public static void CloseConnection()
        {
            _connection.Close();
        }
    }
}