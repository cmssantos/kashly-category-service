using Kashly.Category.Domain.Enums;

namespace Kashly.Category.Domain.Entities;

public class Category
{
    public const int MaxIconLength = 50;
    public const int MaxColorLength = 7;
    public const int MaxUserIdLength = 450; // Default max length for ASP.NET Identity user IDs
    public const int MaxDescriptionLength = 100;

    public int Id { get; }
    public CategoryType Type { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string Icon { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string UserId { get; private set; } = string.Empty;

    public bool IsDeleted => this.DeletedAt.HasValue;

    public bool IsActive => this.DeletedAt == null;

    protected Category() { }

    public Category(CategoryType type, string description, string icon, string color, string userId)
    {
        this.Type = type;
        this.Icon = icon;
        this.Color = color;
        this.UserId = userId;
        this.Description = description;
        this.CreatedAt = DateTime.UtcNow;
    }

    public void Update(string description, string icon, string color)
    {
        this.Icon = icon;
        this.Color = color;
        this.Description = description;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        if (this.IsDeleted)
        {
            throw new InvalidOperationException("Category is already deleted.");
        }
        this.DeletedAt = DateTime.UtcNow;
    }

    public void Restore()
    {
        if (!this.IsDeleted)
        {
            throw new InvalidOperationException("Category is not deleted.");
        }
        this.DeletedAt = null;
    }
}
