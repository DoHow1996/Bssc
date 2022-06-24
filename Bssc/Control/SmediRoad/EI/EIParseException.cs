using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EI
{
    /// <summary>
    /// EI数据解析异常
    /// </summary>
    class EIParseException : Exception
    {
        public EIParseException()
        {
        }

        public EIParseException(string message) : base(message)
        {
        }

        public EIParseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EIParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
