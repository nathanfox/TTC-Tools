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

        void ITypeInstanceDictionary.Add<T>(T dictionaryEntry)
        {
            (this as ITypeInstanceDictionary).Add<T, DefaultInstance>(dictionaryEntry, DefaultInstance.Value);
        }

        void ITypeInstanceDictionary.Add<T>(T dictionaryEntry, int instanceId)
        {
            (this as ITypeInstanceDictionary).Add<T, int>(dictionaryEntry, instanceId);
        }

        void ITypeInstanceDictionary.Add<T>(T dictionaryEntry, string instanceId)
        {
            (this as ITypeInstanceDictionary).Add<T, string>(dictionaryEntry, instanceId);
        }

        void ITypeInstanceDictionary.Add<T, I>(T dictionaryEntry, I instanceId)
        {
            int key = ComputeKey<I>(typeof(T), instanceId);

            if ( _instanceDictionary.ContainsKey(key) )
            {
                throw new TypedInstanceDictionaryInstanceExistsException<I>(typeof(T), instanceId);
            }

            _instanceDictionary.Add(key, dictionaryEntry);
        }

        T ITypeInstanceDictionary.Get<T>()
        {
            return (this as ITypeInstanceDictionary).Get<T, DefaultInstance>(DefaultInstance.Value);
        }

        T ITypeInstanceDictionary.Get<T>(int instanceId)
        {
            return (this as ITypeInstanceDictionary).Get<T, int>(instanceId);
        }

        T ITypeInstanceDictionary.Get<T>(string instanceId)
        {
            return (this as ITypeInstanceDictionary).Get<T, string>(instanceId);
        }

        T ITypeInstanceDictionary.Get<T, I>(I instanceId)
        {
            (this as ITypeInstanceDictionary).Verify<T, I>(instanceId);

            return (T)_instanceDictionary[ComputeKey<I>(typeof(T), instanceId)];
        }

        ITypeInstanceDictionary ITypeInstanceDictionary.Verify<T>()
        {
            return (this as ITypeInstanceDictionary).Verify<T, DefaultInstance>(DefaultInstance.Value);
        }

        ITypeInstanceDictionary ITypeInstanceDictionary.Verify<T>(int instanceId)
        {
            return (this as ITypeInstanceDictionary).Verify<T, int>(instanceId);
        }

        ITypeInstanceDictionary ITypeInstanceDictionary.Verify<T>(string instacneId)
        {
            return (this as ITypeInstanceDictionary).Verify<T, string>(instacneId);
        }

        ITypeInstanceDictionary ITypeInstanceDictionary.Verify<T, I>(I instanceId)
        {
            if (!_instanceDictionary.ContainsKey(ComputeKey<I>(typeof(T), instanceId)))
            {
                throw new TypeInstanceDictionaryVerifyNotFoundException<I>(typeof(T), instanceId);
            }

            return this;
        }

        private int ComputeKey<I>(Type instanceType, I instanceId)
        {
            return instanceType.GetHashCode() ^ instanceId.GetHashCode();
        }
    }
}
