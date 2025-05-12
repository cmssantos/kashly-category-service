using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kashly.Category.Infrastructure.Data.Configurations;

internal class EnumToStringConverter<TEnum> : ValueConverter<TEnum, string>
    where TEnum : struct, Enum
{
    public EnumToStringConverter()
        : base(
            v => v.ToString(),         // enum → string
            v => Enum.Parse<TEnum>(v)) // string → enum
    { }
}
