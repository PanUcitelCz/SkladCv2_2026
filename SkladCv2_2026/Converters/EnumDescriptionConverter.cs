using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SkladCv2_2026.Converters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // 1. Pokud hodnota není Enum (např. null), vrátíme ji beze změny
            if (value is not Enum myEnum) return value;

            // 2. Získáme popis
            return GetEnumDescription(myEnum);
        }

        // ConvertBack nepotřebujeme, protože data jen zobrazujeme
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        // Pomocná metoda, která pomocí Reflection přečte atribut [Description]
        private static string GetEnumDescription(Enum enumObj)
        {
            FieldInfo? fieldInfo = enumObj.GetType().GetField(enumObj.ToString());
            if (fieldInfo == null) return enumObj.ToString(); // Fallback

            var attrib = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

            // Fallback, pokud není atribut
            return attrib?.Description ?? enumObj.ToString();
        }
    }
}
