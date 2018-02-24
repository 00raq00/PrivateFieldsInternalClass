﻿using InternalClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrivateFieldsInternalClass;
using ReflactionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    [TestMethod()]
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
  }
}