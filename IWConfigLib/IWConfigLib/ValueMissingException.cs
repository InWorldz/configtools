using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IWConfigLib
{
    public class ValueMissingException : Exception
    {
        public ValueMissingException(string message)
            : base(message)
        {
        }
    }
}
