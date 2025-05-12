using System.Text.Json.Serialization;
using Kashly.Category.Communication.Enums;

namespace Kashly.Category.Communication.Requests;

public class CreateCategoryRequest
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CategoryType Type { get; set; }

    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}
