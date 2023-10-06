namespace LiftSystem.DTO
{
    public class Log
    {
        private int id;
        private string message;

        public Log(int id, string message)
        {
            this.id = id;
            this.message = message;
        }

        public Log(string message)
        {
            this.message = message;
        }

        public int Id => id;

        public string Message => message;
    }
}