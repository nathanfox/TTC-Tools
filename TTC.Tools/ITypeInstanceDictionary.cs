using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTC.Tools
{
   public interface ITypeInstanceDictionary
   {
      /// <summary>
      /// Add a dictionary entry of type T. The type will be the key for
      /// dictionary retrieval.
      /// </summary>
      /// <typeparam name="T">The type of the dictionary value which is used as the key as well.</typeparam>
      /// <param name="dictionaryEntry">The struct or class to store in the dictionary which will be indexed by type T.</param>
      void Add<T>(T dictionaryEntry);

      /// <summary>
      /// Add a dictionary entry of type T identified by an integer instance id.
      /// </summary>
      /// <typeparam name="T">The type of the dictionary value which is used as part of the key as well.</typeparam>
      /// <param name="dictionaryEntry">The struct or class to store in the dictionary which will be indexed by a combination of type and the instanceId.</param>
      /// <param name="instanceId">An integer instance ID which can be used to store multiple instances of the same type in the dictionary.</param>
      void Add<T>(T dictionaryEntry, int instanceId);

      /// <summary>
      /// Add a dictionary entry of type T identified by a string instance id.
      /// </summary>
      /// <typeparam name="T">The type of the dictionary value which is used a part of the key as well.</typeparam>
      /// <param name="dictionaryEntry">The struct or class to store in the dictionary which will be indexed by a combination of type and the instanceId.</param>
      /// <param name="instanceId">A string instance ID which can be used to store multiple instances of the same type in the dictionary.</param>
      void Add<T>(T dictionaryEntry, string instanceId);

      /// <summary>
      /// Add a dictionary entry of type T identified by an instance id of type I.
      /// </summary>
      /// <typeparam name="T">The type of the dictionary value which is used a part of the key as well.</typeparam>
      /// <typeparam name="I">The type of the instance id parameter.</typeparam>
      /// <param name="dictionaryEntry">The struct or class to store in the dictionary which will be indexed by a combination of type T and the instanceId.</param>
      /// <param name="instanceId">An instance ID of type I which can be used to store multiple instances of the same type T in the dictionary.</param>
      void Add<T, I>(T dictionaryEntry, I instanceId);

      /// <summary>
      /// Gets the dictionary instance of type T.
      /// </summary>
      /// <typeparam name="T">The type of the dictionary entry to retrieve.</typeparam>
      /// <returns>The instance of type T stored in the dictionary.</returns>
      T Get<T>();

      /// <summary>
      /// Gets the dictionary instance of type T identified by an integer instance id.
      /// </summary>
      /// <typeparam name="T">The type of the dictionary entry to retrieve.</typeparam>
      /// <param name="instanceId">The integer instance ID used to store to store the type in the dictionary.</param>
      /// <returns>The instance of type T identified by the integer instanceId.</returns>
      T Get<T>(int instanceId);

      /// <summary>
      /// Gets the dictionary instance of type T identified by a string instance id.
      /// </summary>
      /// <typeparam name="T">The type of the dictionary entry to retrieve.</typeparam>
      /// <param name="instanceId">The instance of type T identified by the string instanceId.</param>
      /// <returns>The instance of type T identified by the string instanceId.</returns>
      T Get<T>(string instanceId);

      /// <summary>
      /// Gets the dictionary instance of type T identified by a an instance id of type I.
      /// </summary>
      /// <typeparam name="T">The type of the dictionary entry to retrieve.</typeparam>
      /// <typeparam name="I">The type of the instance id.</typeparam>
      /// <param name="instanceId">The instance of type T identified by the instanceId of type I.</param>
      /// <returns>The instance of type T identified by the instanceId of type I.</returns>
      T Get<T, I>(I instanceId);

      /// <summary>
      /// Verifies that the dictionary contains an instance of type T. If an instance of type
      /// T is not found then an exception of type TypedViewDataDictionaryNotFoundException will
      /// be thrown.
      /// </summary>
      /// <typeparam name="T">The type of the dictionary entry to verify.</typeparam>
      /// <returns>Returns an instance of the dictionary to allow the chaining of Verify calls.</returns>
      ITypeInstanceDictionary Verify<T>();

      /// <summary>
      /// Verifies that the dictionary contains an instance of type T identified by an integer instance id. 
      /// If an instance of type T is not found then an exception of type 
      /// TypedViewDataDictionaryNotFoundException will be thrown.
      /// </summary>
      /// <typeparam name="T">The type of the dictionary entry to verify.</typeparam>
      /// <param name="instanceId">The integer instance ID used to store to store the type in the dictionary.</param>
      /// <returns>Returns an instance of the dictionary to allow the chaining of Verify calls.</returns>
      ITypeInstanceDictionary Verify<T>(int instanceId);

      /// <summary>
      /// Verifies that the dictionary contains an instance of type T identified by a string instance id. 
      /// If an instance of type T is not found then an exception of type 
      /// TypedViewDataDictionaryNotFoundException will be thrown.
      /// </summary>
      /// <typeparam name="T">The type of the dictionary entry to verify.</typeparam>
      /// <param name="instanceId">The string instance ID used to store to store the type in the dictionary.</param>
      /// <returns>Returns an instance of the dictionary to allow the chaining of Verify calls.</returns>
      ITypeInstanceDictionary Verify<T>(string instacneId);

      /// <summary>
      /// Verifies that the dictionary contains an instance of type T identified by a string instance id. 
      /// If an instance of type T is not found then an exception of type 
      /// TypedViewDataDictionaryNotFoundException will be thrown.
      /// </summary>
      /// <typeparam name="T">The type of the dictionary entry to verify.</typeparam>
      /// <typeparam name="I">The type of the instance id.</typeparam>
      /// <param name="instanceId">The instance ID of type I used to store to store the type in the dictionary.</param>
      /// <returns>Returns an instance of the dictionary to allow the chaining of Verify calls.</returns>
      ITypeInstanceDictionary Verify<T, I>(I instanceId);
   }
}