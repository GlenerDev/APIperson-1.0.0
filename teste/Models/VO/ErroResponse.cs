namespace teste.Models.NovaPasta1.VO
{
    public class ErroResponse
    {
        public ErroResponse(string error, int statusCode)
        {
            this.error = error;
            this.statusCode = statusCode;
        }

        public string error { get; }
        public int statusCode { get; }

    }
}
