namespace Catalog.Host.Models.Response.ItemResponse;

public class AddItemResponse<T>
{
    public T Id { get; set; } = default!;
}