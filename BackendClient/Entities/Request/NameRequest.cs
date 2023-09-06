namespace Entities.Request
{
    public abstract class NameRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NickName { get; set; }
    }
}
