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

    public bool IsDeleted => DeletedAt.HasValue;

    public bool IsActive => DeletedAt == null;

    protected Category() { }

    public Category(CategoryType type, string description, string icon, string color, string userId)
    {
        Type = type;
        Icon = icon;
        Color = color;
        UserId = userId;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string description, string icon, string color)
    {
        Icon = icon;
        Color = color;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        if (IsDeleted)
        {
            throw new InvalidOperationException("Category is already deleted.");
        }
        DeletedAt = DateTime.UtcNow;
    }

    public void Restore()
    {
        if (!IsDeleted)
        {
            throw new InvalidOperationException("Category is not deleted.");
        }
        DeletedAt = null;
    }

    public void AssignUser(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
        }

        if (userId.Length > MaxUserIdLength)
        {
            throw new ArgumentException($"User ID cannot exceed {MaxUserIdLength} characters.", nameof(userId));
        }

        UserId = userId;
    }
}
