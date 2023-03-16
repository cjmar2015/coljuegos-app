namespace APP.Models
{
    public class Response<T>
    {
        public bool checkResponse
        {
            get;
            set;
        }
        public string message
        {
            get;
            set;
        }
        public T body
        {
            get;
            set;
        }
    }
}
