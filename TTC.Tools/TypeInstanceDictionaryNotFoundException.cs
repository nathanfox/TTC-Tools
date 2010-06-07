using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTC.Tools
{
    public class TypeInstanceDictionaryVerifyNotFoundException<I> : Exception
    {
        /// <summary>
        /// Initializes a new instance of the TypedViewDataDictionaryNotFoundException class.
        /// </summary>
        /// <param name="instanceNotFound"></param>
        public TypeInstanceDictionaryVerifyNotFoundException(Type typeNotFound, I instanceNotFound)
        {
            TypeNotFound = typeNotFound;
            InstanceNotFound = instanceNotFound;
        }

        public Type TypeNotFound { get; set; }
        public I InstanceNotFound { get; set; }
    }
}
