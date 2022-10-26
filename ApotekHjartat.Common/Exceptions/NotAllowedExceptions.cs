using System;
using System.Collections.Generic;
using System.Text;

namespace ApotekHjartat.Common.Exceptions
{
    public class NotAllowedException : Exception
    {
        public readonly string InnerMessage;
        public NotAllowedException(string message) : base(message) { }
        public NotAllowedException(string message, string innerMessage) : base(message)
        {
            InnerMessage = innerMessage;
        }
    }
}
