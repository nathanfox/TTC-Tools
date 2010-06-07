using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTC.Tools
{
    public class TypedInstanceDictionaryInstanceExistsException<I> : Exception
    {
        /// <summary>
        /// Initializes a new instance of the TypedViewDataDictionaryInstanceExistsException class.
        /// </summary>
        public TypedInstanceDictionaryInstanceExistsException(Type typeThatAlreadyExists, I instanceThatAlreadyExists)
        {
            TypeNotFound = typeThatAlreadyExists;
            InstanceNotFound = instanceThatAlreadyExists;
        }

        public Type TypeNotFound { get; set; }
        public I InstanceNotFound { get; set; }
    }
}
