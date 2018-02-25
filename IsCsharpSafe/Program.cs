using InternalClasses;
using ReflactionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrivateFieldsInternalClass
{
public   class Program
  {

 private static  int Value = 1000;
    static void Main(string[] args)
    {
      var PublicClassInternalFields = new PublicClassInternalField();
      var PublicClassPrivateFields = new PublicClassPrivateFields();

      string Name = "internalInt";

      ReflectionHelper.GetAndSetValue(PublicClassInternalFields, Value, Name);

      Value = 2000;
      Name = "privateInt";

      ReflectionHelper.GetAndSetValue(PublicClassPrivateFields, Value, Name);
      
      object InternalClassPublicConstructorPrivateField = ReflectionHelper.CreateInstanceOfInternalClass("InternalClasses", "InternalClasses","InternalClassPublicConstructorPrivateField");
      Console.WriteLine($"Internal class {InternalClassPublicConstructorPrivateField.GetType()} is created fine");
      ReflectionHelper.GetAndSetValue(InternalClassPublicConstructorPrivateField, Value, Name);

      object InternalClassPrivteConstructorPrivateField = ReflectionHelper.CreateInstanceOfInternalClass("InternalClasses", "InternalClasses", "InternalClassPrivteConstructorPrivateField");
                Console.WriteLine($"Internal class {InternalClassPrivteConstructorPrivateField.GetType()} is created fine");
      ReflectionHelper.GetAndSetValue(InternalClassPrivteConstructorPrivateField, Value, Name);

      object InternalClassInternalConstructorPrivateField = ReflectionHelper.CreateInstanceOfInternalClass("InternalClasses", "InternalClasses", "InternalClassInternalConstructorPrivateField");
            Console.WriteLine($"Internal class {InternalClassInternalConstructorPrivateField.GetType()} is created fine");
      ReflectionHelper.GetAndSetValue(InternalClassInternalConstructorPrivateField, Value, Name);

      ReflectionHelper.PrintAllFields(InternalClassInternalConstructorPrivateField);

      Console.ReadLine();
    }

  }
}
