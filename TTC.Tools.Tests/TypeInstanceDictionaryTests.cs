using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

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
        public void CheckDictionaryAddAndGetWithTypeOnly()
        {
            ITypeInstanceDictionary viewData = new TypeInstanceDictionary();

            List<int> intList = new List<int> { 0, 1, 2, 3 };
            IDictionary<int, string> dictList = new Dictionary<int, string> { {0, "0"}, { 1, "1" } };

            viewData.Add<List<int>>(intList);
            viewData.Add<IDictionary<int, string>>(dictList);

            Assert.DoesNotThrow(() => { viewData.Get<List<int>>(); });
            Assert.DoesNotThrow(() => { viewData.Get<IDictionary<int, string>>(); });
            Assert.IsNotNull(viewData.Get<List<int>>());
            Assert.IsNotNull(viewData.Get<IDictionary<int, string>>());
            Assert.AreElementsEqual<int>(new int[4] { 0, 1, 2, 3 }, viewData.Get<List<int>>());
            Assert.AreElementsEqual<KeyValuePair<int, string>>(new KeyValuePair<int, string>[2] { new KeyValuePair<int, string>(0, "0"), new KeyValuePair<int, string>( 1, "1" ) }, viewData.Get<IDictionary<int, string>>());
        }

        [Test]
        public void CheckDictionaryAddAndGetWithInstanceTemplate()
        {
           ITypeInstanceDictionary viewData = new TypeInstanceDictionary();

            TestClass1 class1Instance1 = new TestClass1();

            class1Instance1.StringProperty1 = "TestVal1";
            class1Instance1.BoolProperty2 = true;

            TestClass1 class1Instance2 = new TestClass1();

            class1Instance2.StringProperty1 = "TestVal2";
            class1Instance2.BoolProperty2 = false;

            viewData.Add<TestClass1, int>(class1Instance1, 1);
            viewData.Add<TestClass1, int>(class1Instance2, 2);

            Assert.IsNotNull(viewData.Get<TestClass1, int>(1));
            Assert.IsNotNull(viewData.Get<TestClass1, int>(2));
            Assert.IsTrue(Type.ReferenceEquals(class1Instance1, viewData.Get<TestClass1, int>(1)));
            Assert.IsTrue(Type.ReferenceEquals(class1Instance2, viewData.Get<TestClass1, int>(2)));
        }

        [Test]
        public void TestAddOfTheSameTypeThrowsAnException()
        {
           ITypeInstanceDictionary viewData = new TypeInstanceDictionary();

           viewData.Add<string>("test");
           Assert.Throws<TypedInstanceDictionaryInstanceExistsException<DefaultInstance>>(() => viewData.Add<string>("test2"));

           viewData.Add<string, int>("test", 1);
           Assert.Throws<TypedInstanceDictionaryInstanceExistsException<int>>(() => viewData.Add<string, int>("test2", 1));
        }

        [Test]
        public void TestGetOfNonExistentTypeThrowsATypedViewDataDictionaryNotFoundException()
        {
           ITypeInstanceDictionary viewData = new TypeInstanceDictionary();

           Assert.Throws<TypeInstanceDictionaryVerifyNotFoundException<DefaultInstance>>(() => viewData.Get<string>());
           Assert.Throws<TypeInstanceDictionaryVerifyNotFoundException<int>>(() => viewData.Get<string, int>(1));
        }

        enum InstanceType { Instance1 = 3, Instance2 = 4 }

        [Test]
        public void TestTypedViewVerify()
        {
           ITypeInstanceDictionary viewData = new TypeInstanceDictionary();

           viewData.Add<string>("TestVal");
           viewData.Add<TestClass1>(new TestClass1(), "Instance1");
           viewData.Add<TestClass1>(new TestClass1(), "Instance2");
           viewData.Add<TestClass1>(new TestClass1(), 1);
           viewData.Add<TestClass1>(new TestClass1(), 2);
           viewData.Add<TestClass1, InstanceType>(new TestClass1(), InstanceType.Instance1);
           viewData.Add<TestClass1, InstanceType>(new TestClass1(), InstanceType.Instance2);

           Assert.DoesNotThrow(() => viewData.Verify<string>());
           Assert.DoesNotThrow(() => viewData.Verify<TestClass1>("Instance1"));
           Assert.DoesNotThrow(() => viewData.Verify<TestClass1>("Instance2"));
           Assert.DoesNotThrow(() => viewData.Verify<TestClass1>(1));
           Assert.DoesNotThrow(() => viewData.Verify<TestClass1>(2));
           Assert.DoesNotThrow(() => viewData.Verify<TestClass1, InstanceType>(InstanceType.Instance1));
           Assert.DoesNotThrow(() => viewData.Verify<TestClass1, InstanceType>(InstanceType.Instance1));

           // Test chanied verify calls.
           Assert.DoesNotThrow(() => viewData.Verify<string>().Verify<TestClass1>("Instance1").Verify<TestClass1>("Instance2").Verify<TestClass1>(1).Verify<TestClass1>(2).Verify<TestClass1, InstanceType>(InstanceType.Instance1).Verify<TestClass1, InstanceType>(InstanceType.Instance1));
        }

        [Test]
        public void TestTypedViewVerifyThrows()
        {
           ITypeInstanceDictionary viewData = new TypeInstanceDictionary();

           Assert.Throws<TypeInstanceDictionaryVerifyNotFoundException<DefaultInstance>>(() => viewData.Verify<int>());
           Assert.Throws<TypeInstanceDictionaryVerifyNotFoundException<int>>(() => viewData.Verify<int>(1));
           Assert.Throws<TypeInstanceDictionaryVerifyNotFoundException<string>>(() => viewData.Verify<int>("Instance1"));
           Assert.Throws<TypeInstanceDictionaryVerifyNotFoundException<InstanceType>>(() => viewData.Verify<int, InstanceType>(InstanceType.Instance1));
        }
    }
}
