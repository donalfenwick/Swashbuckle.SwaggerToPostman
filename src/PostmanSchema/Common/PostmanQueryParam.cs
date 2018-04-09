using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Common
{
    [DataContract]
    public class PostmanQueryParam
    {
        public PostmanQueryParam() { }
        public PostmanQueryParam(string key, string value, string description = "")
        {
            this.Key = key;
            this.Value = value;
            this.Description = new PostmanDescription(description);
        }
        [DataMember(Name = "key")]
        public string Key { get; set; }
        [DataMember(Name = "value")]
        public string Value { get; set; }
        [DataMember(Name = "disabled")]
        public bool Disabled { get; set; }
        [DataMember(Name = "description")]
        public PostmanDescription Description { get; set; }
    }
}
