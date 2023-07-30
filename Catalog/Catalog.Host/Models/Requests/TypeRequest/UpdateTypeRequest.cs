namespace Catalog.Host.Models.Requests.TypeRequest
{
    public class UpdateTypeRequest
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
    }
}
