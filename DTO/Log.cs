namespace LiftSystem.DTO
{
    public class Log
    {
        private int id;
        private string message;
        private string created;

        public Log(int id, string message, string created)
        {
            this.id = id;
            this.message = message;
            this.created = created;
        }

        public Log(string message)
        {
            this.message = message;
        }

        public int Id => id;

        public string Message => message;

        public string Created => created;
    }
}