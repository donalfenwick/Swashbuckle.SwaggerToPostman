using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.Contracts
{
    [DataContract]
    public class ErrorResponse
    {
        [DataMember]
        public string Message { get; set; }
    }
}
