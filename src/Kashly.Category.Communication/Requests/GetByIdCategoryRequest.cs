namespace Kashly.Category.Communication.Requests;

public class GetByIdCategoryRequest(int id)
{
    public int Id { get; set; } = id;
}
