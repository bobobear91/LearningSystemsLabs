using System;

namespace Lab_assignment_2.Handlers
{
    [Serializable()]
    public class FuzzyLogicException : Exception
    {
        public FuzzyLogicException() : base() { }
        public FuzzyLogicException(string message) : base(message) { }
        public FuzzyLogicException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected FuzzyLogicException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { } 
    }
}
