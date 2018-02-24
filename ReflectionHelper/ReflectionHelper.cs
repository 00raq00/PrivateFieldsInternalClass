using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflactionHelper
{
    public class ReflectionHelper
  {
    public static object CreateInstanceOfInternalClass(string assemblyName, string classNamespace, string className)
    {
      Assembly assembly = Assembly.Load(assemblyName);
      return assembly.CreateInstance($"{classNamespace}.{className}", false, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null, null);

    }

    public static int PrintAllFields(object obj)
    {
      FieldInfo[] fields = GetAllFields(obj);

      foreach (var field in fields)
      {
        Console.WriteLine(field.Name);
        Console.WriteLine($"IsPublic: {field.IsPublic}");
        Console.WriteLine($"IsPrivate: {field.IsPrivate}");
        Console.WriteLine($"IsStatic: {field.IsStatic}\n");
      }

      return fields.Count();
    }

    public static FieldInfo[] GetAllFields(object obj)
    {
      return obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
    }

    public static object GetAndSetValue(object obj, object val, string name)
    {
      object value = GetNonPublicIntFiledValue(obj, name, obj.GetType());
      Console.WriteLine($"{obj.GetType()} {value} {value.GetType().Name}");

      SetNonPublicIntFiledValue(obj, val, name, obj.GetType());

      value = GetNonPublicIntFiledValue(obj, name, obj.GetType());
      Console.WriteLine($"{obj.GetType()} {value} {value.GetType().Name}");
      return value;
    }

    public static object GetNonPublicIntFiledValue(object obj, string fieldName, Type type)
    {
      FieldInfo field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

      return field.GetValue(obj);
    }


    public static void SetNonPublicIntFiledValue(object obj, object val, string fieldName, Type type)
    {
      FieldInfo field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

      field.SetValue(obj, val);
    }
  }
}
