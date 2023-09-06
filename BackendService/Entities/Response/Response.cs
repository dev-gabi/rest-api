namespace Entities.Response
{
    public class Response 
    {
        public int StatusCode { get; set; }
        public string? StatusDescription { get; set; }

        public NamesResponse ConvertToNamesResponse()
        {
            return new NamesResponse()
            {
                StatusCode = StatusCode,
                StatusDescription = StatusDescription
            };
        }
    }
}
