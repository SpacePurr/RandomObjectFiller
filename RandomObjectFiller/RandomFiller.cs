using System;

namespace RandomObjectFiller
{
    public class RandomFiller
    {
        public static T Generate<T>() where T : class, new()
        {
            return (T)Generate(typeof(T));
        }

        public static object Generate(Type type)
        {
            var t = Activator.CreateInstance(type);

            var properties = type.GetProperties();

            foreach (var propertyInfo in properties)
            {
                var propertyType = propertyInfo.PropertyType;

                propertyInfo.SetValue(t, GetValue(propertyType));
            }

            return t;
        }

        private static object GetValue(Type propertyType)
        {
            if (propertyType.Equals(typeof(string)))
            {
                return "hello";
            }
            else if (propertyType.Equals(typeof(int)))
            {
                return new Random().Next(1, 100);
            }
            else if (propertyType.Equals(typeof(double)))
            {
                return (double)new Random().Next(1, 100);
            }
            else if (propertyType.Equals(typeof(DateTime)))
            {
                return DateTime.Now;
            }
            else if (propertyType.Equals(typeof(decimal)))
            {
                return (decimal)new Random().Next(1, 100);
            }
            else if (propertyType.Equals(typeof(bool)))
            {
                return true;
            }
            else if (propertyType.IsArray)
            {
                var array = Activator.CreateInstance(propertyType, 1);

                var elementType = propertyType.GetElementType();
                var element = GetValue(elementType);
                ((Array)array).SetValue(element, 0);

                return array;
            }
            else if (propertyType.IsClass)
            {
                return Generate(propertyType);
            }
            else
            {
                return default;
            }
        }
    }
}
