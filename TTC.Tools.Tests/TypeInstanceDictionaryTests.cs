using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTC.Tools.Tests
{
   [TestFixture]
   public class TypeInstanceDictionaryTests
   {
      public class TestClass1
      {
         public string StringProperty1 { get; set; }
         public bool BoolProperty2 { get; set; }
      }

      public class TestClass2
      {
         public string StringProperty1 { get; set; }
         public bool BoolProperty2 { get; set; }
      }

      [Test]
      public void TestContainsFindsInstanceExists()
      {
         ITypeInstanceDictionary dict = new TypeInstanceDictionary();

         dict.Add<string>("Testing");
         Assert.IsTrue(dict.Contains<string>());

         dict.Add<string>("Test with int id 1", 1);
         Assert.IsTrue(dict.Contains<string>(1));

         dict.Add<string>("Test with string id", "String1");
         Assert.IsTrue(dict.Contains<string>("String1"));

         dict.Add<string, InstanceType>("Test with enum instance id", InstanceType.Instance1);
         Assert.IsTrue(dict.Contains<string, InstanceType>(InstanceType.Instance1));
      }

      [Test]
      public void TestContainsFindsInstanceDoesNotExist()
      {
         ITypeInstanceDictionary dict = new TypeInstanceDictionary();

         Assert.IsFalse(dict.Contains<string>());
         Assert.IsFalse(dict.Contains<string>(1));
         Assert.IsFalse(dict.Contains<string>("1"));
         Assert.IsFalse(dict.Contains<string, InstanceType>(InstanceType.Instance1));
      }

      [Test]
      public void CheckDictionaryAddAndGetWithTypeOnly()
      {
         ITypeInstanceDictionary dict = new TypeInstanceDictionary();

         List<int> intList = new List<int> { 0, 1, 2, 3 };
         IDictionary<int, string> dictList = new Dictionary<int, string> { { 0, "0" }, { 1, "1" } };

         dict.Add<List<int>>(intList);
         dict.Add<IDictionary<int, string>>(dictList);

         Assert.DoesNotThrow(() => { dict.Get<List<int>>(); });
         Assert.DoesNotThrow(() => { dict.Get<IDictionary<int, string>>(); });
         Assert.IsNotNull(dict.Get<List<int>>());
         Assert.IsNotNull(dict.Get<IDictionary<int, string>>());
         Assert.AreElementsEqual<int>(new int[4] { 0, 1, 2, 3 }, dict.Get<List<int>>());
         Assert.AreElementsEqual<KeyValuePair<int, string>>(new KeyValuePair<int, string>[2] { new KeyValuePair<int, string>(0, "0"), new KeyValuePair<int, string>(1, "1") }, dict.Get<IDictionary<int, string>>());
      }

      [Test]
      public void CheckDictionaryAddAndGetWithInstanceTemplate()
      {
         ITypeInstanceDictionary dict = new TypeInstanceDictionary();

         TestClass1 class1Instance1 = new TestClass1();

         class1Instance1.StringProperty1 = "TestVal1";
         class1Instance1.BoolProperty2 = true;

         TestClass1 class1Instance2 = new TestClass1();

         class1Instance2.StringProperty1 = "TestVal2";
         class1Instance2.BoolProperty2 = false;

         dict.Add<TestClass1, int>(class1Instance1, 1);
         dict.Add<TestClass1, int>(class1Instance2, 2);

         Assert.IsNotNull(dict.Get<TestClass1, int>(1));
         Assert.IsNotNull(dict.Get<TestClass1, int>(2));
         Assert.IsTrue(Type.ReferenceEquals(class1Instance1, dict.Get<TestClass1, int>(1)));
         Assert.IsTrue(Type.ReferenceEquals(class1Instance2, dict.Get<TestClass1, int>(2)));
      }

      [Test]
      public void TestAddOfTheSameTypeThrowsAnException()
      {
         ITypeInstanceDictionary dict = new TypeInstanceDictionary();

         dict.Add<string>("test");
         Assert.Throws<TypedInstanceDictionaryInstanceExistsException<DefaultInstance>>(() => dict.Add<string>("test2"));

         dict.Add<string, int>("test", 1);
         Assert.Throws<TypedInstanceDictionaryInstanceExistsException<int>>(() => dict.Add<string, int>("test2", 1));
      }

      [Test]
      public void TestGetOfNonExistentTypeThrowsATypedViewDataDictionaryNotFoundException()
      {
         ITypeInstanceDictionary dict = new TypeInstanceDictionary();

         Assert.Throws<TypeInstanceDictionaryVerifyNotFoundException<DefaultInstance>>(() => dict.Get<string>());
         Assert.Throws<TypeInstanceDictionaryVerifyNotFoundException<int>>(() => dict.Get<string, int>(1));
      }

      enum InstanceType { Instance1, Instance2 }

      [Test]
      public void TestTypedViewVerify()
      {
         ITypeInstanceDictionary dict = new TypeInstanceDictionary();

         dict.Add<string>("TestVal");
         dict.Add<TestClass1>(new TestClass1(), "Instance1");
         dict.Add<TestClass1>(new TestClass1(), "Instance2");
         dict.Add<TestClass1>(new TestClass1(), 1);
         dict.Add<TestClass1>(new TestClass1(), 2);
         dict.Add<TestClass1, InstanceType>(new TestClass1(), InstanceType.Instance1);
         dict.Add<TestClass1, InstanceType>(new TestClass1(), InstanceType.Instance2);

         Assert.DoesNotThrow(() => dict.Verify<string>());
         Assert.DoesNotThrow(() => dict.Verify<TestClass1>("Instance1"));
         Assert.DoesNotThrow(() => dict.Verify<TestClass1>("Instance2"));
         Assert.DoesNotThrow(() => dict.Verify<TestClass1>(1));
         Assert.DoesNotThrow(() => dict.Verify<TestClass1>(2));
         Assert.DoesNotThrow(() => dict.Verify<TestClass1, InstanceType>(InstanceType.Instance1));
         Assert.DoesNotThrow(() => dict.Verify<TestClass1, InstanceType>(InstanceType.Instance1));

         // Test chanied verify calls.
         Assert.DoesNotThrow(() => dict.Verify<string>().Verify<TestClass1>("Instance1").Verify<TestClass1>("Instance2").Verify<TestClass1>(1).Verify<TestClass1>(2).Verify<TestClass1, InstanceType>(InstanceType.Instance1).Verify<TestClass1, InstanceType>(InstanceType.Instance1));
      }

      [Test]
      public void TestTypedViewVerifyThrows()
      {
         ITypeInstanceDictionary dict = new TypeInstanceDictionary();

         Assert.Throws<TypeInstanceDictionaryVerifyNotFoundException<DefaultInstance>>(() => dict.Verify<int>());
         Assert.Throws<TypeInstanceDictionaryVerifyNotFoundException<int>>(() => dict.Verify<int>(1));
         Assert.Throws<TypeInstanceDictionaryVerifyNotFoundException<string>>(() => dict.Verify<int>("Instance1"));
         Assert.Throws<TypeInstanceDictionaryVerifyNotFoundException<InstanceType>>(() => dict.Verify<int, InstanceType>(InstanceType.Instance1));
      }

      enum TestInstanceType1 { Instance1 }
      enum TestInstanceType2 { Instance1 }

      /// <summary>
      /// Enumeration values hash to their underlying value. So different enumeration values
      /// will hash to the same value. The TypeInstanceDictionary will hash enumerations based
      /// on the string value of their name, so different values from different enumerated types
      /// will not collide in the dictionary.
      /// </summary>
      [Test]
      public void TestEnumInstanceIdsAreTreatedUniquely()
      {
         ITypeInstanceDictionary dict = new TypeInstanceDictionary();

         dict.Add<string, TestInstanceType1>("val1", TestInstanceType1.Instance1);

         Assert.DoesNotThrow(() => { dict.Add<string, TestInstanceType2>("val2", TestInstanceType2.Instance1); });
         Assert.DoesNotThrow(() => { dict.Add<string, int>("val3", 0); });
         Assert.AreEqual("val1", dict.Get<string, TestInstanceType1>(TestInstanceType1.Instance1));
         Assert.AreEqual("val2", dict.Get<string, TestInstanceType2>(TestInstanceType2.Instance1));
         Assert.AreEqual("val3", dict.Get<string, int>(0));

         // The following shows all the instance ids hash to 0, yet there is not collision in the dictionary.
         Assert.AreEqual(0, (0).GetHashCode());
         Assert.AreEqual(0, TestInstanceType1.Instance1.GetHashCode());
         Assert.AreEqual(0, TestInstanceType2.Instance1.GetHashCode());
      }

      interface IFu
      {
         void Func1();
      }

      public class Fu : IFu
      {
         #region IFu Members

         void IFu.Func1()
         {
            
         }

         #endregion
      }

      interface IFoo
      {
         void FooFunc();
      }

      public class Foo : IFoo
      {

         #region IFoo Members

         public void FooFunc()
         {
            
         }

         #endregion
      }

      [Test]
      public void TestSomeUsageScenario1()
      {
         ITypeInstanceDictionary dict = new TypeInstanceDictionary();

         IFu fu = new Fu();
         dict.Add(fu);
         Assert.IsNotNull(dict.Get<IFu>());

         Fu fu2 = new Fu();
         dict.Add(fu2);
         Assert.IsNotNull(dict.Get<Fu>());
      }

      [Test]
      public void TestUsageScenario2()
      {
         ITypeInstanceDictionary dict = new TypeInstanceDictionary();

         IFu fu = new Fu();
         dict.Add(fu, "Fu1");

         IFu fu2 = new Fu();
         dict.Add(fu2, "Fu2");

         Assert.IsNotNull(dict.Get<IFu>("Fu1"));
         Assert.IsNotNull(dict.Get<IFu>("Fu2"));
      }

      [Test]
      public void TestUsageScenario3()
      {
         ITypeInstanceDictionary dict = new TypeInstanceDictionary();

         IFu fu = new Fu();
         dict.Add(fu, (int)InstanceType.Instance1);

         IFu fu2 = new Fu();
         dict.Add(fu2, (int)InstanceType.Instance2);

         Assert.IsNotNull(dict.Get<IFu>((int)InstanceType.Instance1));
         Assert.IsNotNull(dict.Get<IFu>((int)InstanceType.Instance2));
      }

      [Test]
      public void TestUsageScenario4()
      {
         ITypeInstanceDictionary dict = new TypeInstanceDictionary();

         IFu fu = new Fu();
         dict.Add(fu, InstanceType.Instance1);

         IFu fu2 = new Fu();
         dict.Add(fu2, InstanceType.Instance2);

         Assert.IsNotNull(dict.Get<IFu, InstanceType>(InstanceType.Instance1));
         Assert.IsNotNull(dict.Get<IFu, InstanceType>(InstanceType.Instance2));
      }
   }
}
