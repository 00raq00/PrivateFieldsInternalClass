using InternalClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrivateFieldsInternalClass;
using ReflactionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsWithSafeKey;

namespace PrivateFieldsInternalClass.Tests
{
  [TestClass()]
  public class ProgramTests
  {
    [TestMethod()]
    public void PrintAllFieldsTest()
    {
      Assert.AreEqual(ReflectionHelper.PrintAllFields(new PublicClassPrivateFields()), 2);
    }

    [TestMethod()]
    public void GetAllFieldsTest()
    {
      System.Reflection.FieldInfo[] fieldInfo = ReflectionHelper.GetAllFields(new PublicClassPrivateFields());

      Assert.AreEqual(fieldInfo.Count(), 2);
      Assert.IsTrue(fieldInfo.Where(x => x.Name.Equals("privateInt")).Count() == 1);
      Assert.IsTrue(fieldInfo.Where(x => x.Name.Equals("privateString")).Count() == 1);
      Assert.IsTrue(fieldInfo.Where(x => x.Name.Equals("internalInt3")).Count() == 0);

    }

    [TestMethod()]
    public void GetAndSetValueString()
    {
      var tmp = new PublicClassPrivateFields();
      object value = ReflectionHelper.GetNonPublicIntFiledValue(tmp, "privateString", tmp.GetType());
      Assert.IsNotNull(value);
      Assert.IsTrue(value is string);
      Assert.AreEqual(value, "100");

      ReflectionHelper.SetNonPublicIntFiledValue(tmp, "privateString", "privateString", tmp.GetType());

      value = ReflectionHelper.GetNonPublicIntFiledValue(tmp, "privateString", tmp.GetType());
      Assert.IsNotNull(value);
      Assert.IsTrue(value is string);
      Assert.AreEqual(value, "privateString");
    }

    [TestMethod()]
    public void GetAndSetValueInt()
    {
      var tmp = new PublicClassPrivateFields();
      object value = ReflectionHelper.GetNonPublicIntFiledValue(tmp, "privateInt", tmp.GetType());
      Assert.IsNotNull(value);
      Assert.IsTrue(value is Int32);
      Assert.AreEqual(value, 100);

      ReflectionHelper.SetNonPublicIntFiledValue(tmp, 666, "privateInt", tmp.GetType());

      value = ReflectionHelper.GetNonPublicIntFiledValue(tmp, "privateInt", tmp.GetType());
      Assert.IsNotNull(value);
      Assert.IsTrue(value is Int32);
      Assert.AreEqual(value, 666);
    }

    [TestMethod()]
    public void GetAndSetMethodValueInt()
    {
      var tmp = new PublicClassPrivateFields();
      object value = ReflectionHelper.GetAndSetValue(tmp, 1234, "privateInt");
      Assert.IsNotNull(value);
      Assert.IsTrue(value is Int32);
      Assert.AreEqual(value, 1234);
    }

    [TestMethod]
    public void CreateInternalClassInstance()
    {
      object InternalClassPublicConstructorPrivateField = ReflectionHelper.CreateInstanceOfInternalClass("InternalClasses", "InternalClasses", "InternalClassPublicConstructorPrivateField");
      Assert.IsNotNull(InternalClassPublicConstructorPrivateField);

      object InternalClassPrivteConstructorPrivateField = ReflectionHelper.CreateInstanceOfInternalClass("InternalClasses", "InternalClasses", "InternalClassPrivteConstructorPrivateField");
      Assert.IsNotNull(InternalClassPrivteConstructorPrivateField);

      object InternalClassInternalConstructorPrivateField = ReflectionHelper.CreateInstanceOfInternalClass("InternalClasses", "InternalClasses", "InternalClassInternalConstructorPrivateField");
      Assert.IsNotNull(InternalClassInternalConstructorPrivateField);

    }

    [TestMethod()]
    public void CreateInternalClassInstanceAndSetPrivateInt()

    {
      object InternalClassPrivteConstructorPrivateField = ReflectionHelper.CreateInstanceOfInternalClass("InternalClasses", "InternalClasses", "InternalClassPrivteConstructorPrivateField");
      Assert.IsNotNull(InternalClassPrivteConstructorPrivateField);

      object value = ReflectionHelper.GetNonPublicIntFiledValue(InternalClassPrivteConstructorPrivateField, "privateInt", InternalClassPrivteConstructorPrivateField.GetType());
      Assert.IsNotNull(value);
      Assert.IsTrue(value is Int32);
      Assert.AreEqual(value, 667);

      value = ReflectionHelper.GetAndSetValue(InternalClassPrivteConstructorPrivateField, 1234, "privateInt");
      Assert.IsNotNull(value);
      Assert.IsTrue(value is Int32);
      Assert.AreEqual(value, 1234);
    }


    [TestMethod]
    public void RunProgramMainWithDifferentValue()
    {

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Form1 program = new Form1();
     object val= ReflectionHelper.GetNonPublicIntFiledValue(program, "key", program.GetType());

      Assert.AreEqual(val, "PRIVATE KEY");

      Task.Run(async () =>
      {
        await Task.Delay(2000);
        MethodInvoker del = delegate
        {
          ReflectionHelper.InvokeNonPublicMethod(program, "button1_Click", program.GetType(), new object[] { new object(), new EventArgs() });
        };

        program.Invoke(del);
        await Task.Delay(2000);
        program.Close();
      });

      program.ShowDialog();
      program = new Form1();

      ReflectionHelper.SetNonPublicIntFiledValue(program, "666", "key", program.GetType());

       val = ReflectionHelper.GetNonPublicIntFiledValue(program, "key", program.GetType());

      Assert.AreEqual(val, "666");

      Task.Run(async () =>
      {
       await Task.Delay(2000);
        MethodInvoker del = delegate
        {
          ReflectionHelper.InvokeNonPublicMethod(program, "button1_Click", program.GetType(), new object[] { new object(), new EventArgs() });
        };

        program.Invoke(del);
        await Task.Delay(2000);
        program.Close();
      });

      program.ShowDialog();    
    }


    [TestMethod()]
    public void CreateInternalClassInstanceAndSetReadOnlyFields()

    {
      object InternalCallReadonlyVsConst = ReflectionHelper.CreateInstanceOfInternalClass("InternalClasses", "InternalClasses", "InternalCallReadonlyVsConst");
      Assert.IsNotNull(InternalCallReadonlyVsConst);

      object privateReadonlyInt = ReflectionHelper.GetNonPublicIntFiledValue(InternalCallReadonlyVsConst, "privateReadonlyInt", InternalCallReadonlyVsConst.GetType());
      Assert.IsNotNull(privateReadonlyInt);
      Assert.IsTrue(privateReadonlyInt is Int32);
      Assert.AreEqual(privateReadonlyInt, 100);

      privateReadonlyInt = ReflectionHelper.GetAndSetValue(InternalCallReadonlyVsConst, 1234, "privateReadonlyInt");
      Assert.IsNotNull(privateReadonlyInt);
      Assert.IsTrue(privateReadonlyInt is Int32);
      Assert.AreEqual(privateReadonlyInt, 1234);


      object privateReadonlyString = ReflectionHelper.GetNonPublicIntFiledValue(InternalCallReadonlyVsConst, "privateReadonlyString", InternalCallReadonlyVsConst.GetType());
      Assert.IsNotNull(privateReadonlyString);
      Assert.IsTrue(privateReadonlyString is string);
      Assert.AreEqual(privateReadonlyString, "100");

      privateReadonlyString = ReflectionHelper.GetAndSetValue(InternalCallReadonlyVsConst, "1234", "privateReadonlyString");
      Assert.IsNotNull(privateReadonlyString);
      Assert.IsTrue(privateReadonlyString is string);
      Assert.AreEqual(privateReadonlyString, "1234");
    }

    [TestMethod()]
    public void CreateInternalClassInstanceAndSetReadOnlyStaticFields()
    {
      object InternalCallReadonlyVsConst = ReflectionHelper.CreateInstanceOfInternalClass("InternalClasses", "InternalClasses", "InternalCallReadonlyVsConst");
      Assert.IsNotNull(InternalCallReadonlyVsConst);

      object privateReadonlyInt = ReflectionHelper.GetNonPublicIntFiledValue(InternalCallReadonlyVsConst, "privateStaticReadonlyInt", InternalCallReadonlyVsConst.GetType());
      Assert.IsNotNull(privateReadonlyInt);
      Assert.IsTrue(privateReadonlyInt is Int32);
      Assert.AreEqual(privateReadonlyInt, 100);

      privateReadonlyInt = ReflectionHelper.GetAndSetValue(InternalCallReadonlyVsConst, 1234, "privateStaticReadonlyInt");
      Assert.IsNotNull(privateReadonlyInt);
      Assert.IsTrue(privateReadonlyInt is Int32);
      Assert.AreEqual(privateReadonlyInt, 1234);


      object privateReadonlyString = ReflectionHelper.GetNonPublicIntFiledValue(InternalCallReadonlyVsConst, "privateStaticReadonlyString", InternalCallReadonlyVsConst.GetType());
      Assert.IsNotNull(privateReadonlyString);
      Assert.IsTrue(privateReadonlyString is string);
      Assert.AreEqual(privateReadonlyString, "100");

      privateReadonlyString = ReflectionHelper.GetAndSetValue(InternalCallReadonlyVsConst, "1234", "privateStaticReadonlyString");
      Assert.IsNotNull(privateReadonlyString);
      Assert.IsTrue(privateReadonlyString is string);
      Assert.AreEqual(privateReadonlyString, "1234");
    }


    [TestMethod()]
    public void CreateInternalClassInstanceAndSetConstFields()
    {
      object InternalCallReadonlyVsConst = ReflectionHelper.CreateInstanceOfInternalClass("InternalClasses", "InternalClasses", "InternalCallReadonlyVsConst");
      Assert.IsNotNull(InternalCallReadonlyVsConst);

      object privateConstInt = ReflectionHelper.GetNonPublicIntFiledValue(InternalCallReadonlyVsConst, "privateConstInt", InternalCallReadonlyVsConst.GetType());
      Assert.IsNotNull(privateConstInt);
      Assert.IsTrue(privateConstInt is Int32);
      Assert.AreEqual(privateConstInt, 100);

      Assert.ThrowsException< FieldAccessException>(()=> privateConstInt = ReflectionHelper.GetAndSetValue(InternalCallReadonlyVsConst, 1234, "privateConstInt"));
      Assert.IsNotNull(privateConstInt);
      Assert.IsTrue(privateConstInt is Int32);
      Assert.AreEqual(privateConstInt, 100);


      object privateConstString = ReflectionHelper.GetNonPublicIntFiledValue(InternalCallReadonlyVsConst, "privateConstString", InternalCallReadonlyVsConst.GetType());
      Assert.IsNotNull(privateConstString);
      Assert.IsTrue(privateConstString is string);
      Assert.AreEqual(privateConstString, "100");

      Assert.ThrowsException<FieldAccessException>(() => privateConstString = ReflectionHelper.GetAndSetValue(InternalCallReadonlyVsConst, "1234", "privateConstString"));
      Assert.IsNotNull(privateConstString);
      Assert.IsTrue(privateConstString is string);
      Assert.AreEqual(privateConstString, "100");
    }
  }
}
