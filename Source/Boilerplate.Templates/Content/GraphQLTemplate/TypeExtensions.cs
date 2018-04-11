namespace ApiTemplate
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public static class TypeExtensions
    {
        public static string GetDescription(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type
                .GetCustomAttributes(typeof(DescriptionAttribute), true)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault()
                ?.Description;
        }

        public static string GetDescription(this Type type, string propertyName)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            return type
                .GetProperty(propertyName)
                .GetCustomAttributes(typeof(DescriptionAttribute), true)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault()
                ?.Description;
        }
    }
}
