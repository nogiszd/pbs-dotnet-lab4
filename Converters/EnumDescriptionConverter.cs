using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace WinLab4.Converters;

public class EnumDescriptionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return "Nieznane";

        Type enumType = value.GetType();
        FieldInfo? field = enumType.GetField(value.ToString()!);

        if (field == null) return value.ToString()!;

        DescriptionAttribute? attribute = field.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString()!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || targetType == null) return Binding.DoNothing;

        foreach (var field in targetType.GetFields())
        {
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            if (attribute != null && attribute.Description == value.ToString())
            {
                return Enum.Parse(targetType, field.Name);
            }
        }

        return Binding.DoNothing;
    }
}
