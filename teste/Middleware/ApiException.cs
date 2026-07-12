namespace APIperson.Middleware
{
    public class ApiException
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public ApiException(string statuscode, string message,string details)
        {
            StatusCode = statuscode;
            Message = message;
            Details = details;
        }
    }
}
