using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTC.Tools
{
   public class TypeInstanceDictionary : ITypeInstanceDictionary
   {
      Dictionary<int, object> _instanceDictionary = new Dictionary<int, object>();

      public TypeInstanceDictionary()
      {

      }

      public bool Contains<T>()
      {
         return Contains<T, DefaultInstance>(DefaultInstance.Value);
      }

      public bool Contains<T>(int instanceId)
      {
         return Contains<T, int>(instanceId);
      }

      public bool Contains<T>(string instanceId)
      {
         return Contains<T, string>(instanceId);
      }

      public bool Contains<T, I>(I instanceId)
      {
         bool containsInstance = true;

         if (!_instanceDictionary.ContainsKey(ComputeKey<I>(typeof(T), instanceId)))
         {
            containsInstance = false;
         }

         return containsInstance;
      }

      public void Add<T>(T dictionaryEntry)
      {
         (this as ITypeInstanceDictionary).Add<T, DefaultInstance>(dictionaryEntry, DefaultInstance.Value);
      }

      public void Add<T>(T dictionaryEntry, int instanceId)
      {
         (this as ITypeInstanceDictionary).Add<T, int>(dictionaryEntry, instanceId);
      }

      public void Add<T>(T dictionaryEntry, string instanceId)
      {
         (this as ITypeInstanceDictionary).Add<T, string>(dictionaryEntry, instanceId);
      }

      public void Add<T, I>(T dictionaryEntry, I instanceId)
      {
         int key = ComputeKey<I>(typeof(T), instanceId);

         if (_instanceDictionary.ContainsKey(key))
         {
            throw new TypedInstanceDictionaryInstanceExistsException<I>(typeof(T), instanceId);
         }

         _instanceDictionary.Add(key, dictionaryEntry);
      }

      public T Get<T>()
      {
         return (this as ITypeInstanceDictionary).Get<T, DefaultInstance>(DefaultInstance.Value);
      }

      T ITypeInstanceDictionary.Get<T>(int instanceId)
      {
         return (this as ITypeInstanceDictionary).Get<T, int>(instanceId);
      }

      public T Get<T>(string instanceId)
      {
         return (this as ITypeInstanceDictionary).Get<T, string>(instanceId);
      }

      public T Get<T, I>(I instanceId)
      {
         (this as ITypeInstanceDictionary).Verify<T, I>(instanceId);

         return (T)_instanceDictionary[ComputeKey<I>(typeof(T), instanceId)];
      }

      public ITypeInstanceDictionary Verify<T>()
      {
         return (this as ITypeInstanceDictionary).Verify<T, DefaultInstance>(DefaultInstance.Value);
      }

      public ITypeInstanceDictionary Verify<T>(int instanceId)
      {
         return (this as ITypeInstanceDictionary).Verify<T, int>(instanceId);
      }

      public ITypeInstanceDictionary Verify<T>(string instacneId)
      {
         return (this as ITypeInstanceDictionary).Verify<T, string>(instacneId);
      }

      public ITypeInstanceDictionary Verify<T, I>(I instanceId)
      {
         if (!_instanceDictionary.ContainsKey(ComputeKey<I>(typeof(T), instanceId)))
         {
            throw new TypeInstanceDictionaryVerifyNotFoundException<I>(typeof(T), instanceId);
         }

         return this;
      }

      private int ComputeKey<I>(Type instanceType, I instanceId)
      {
         int instanceIdHashCode = instanceId is Enum ? ComputeKeyForEnum<I>(instanceId) : instanceId.GetHashCode();

         return instanceType.GetHashCode() ^ instanceIdHashCode;
      }

      private int ComputeKeyForEnum<I>(I enumInstance)
      {
         return (typeof(I).FullName + Enum.GetName(typeof(I), enumInstance)).GetHashCode();
      }
   }
}
