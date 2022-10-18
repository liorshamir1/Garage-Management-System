using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) : base(string.Format(@"Out of range exception, the value should be between {0} and {1}", i_MinValue, i_MaxValue))
        {
        }
    }
}
