using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema
{
    //common base class for folders and items as they can exist in the same collection of objects
    [DataContract]
    public abstract class CollectionItemBase
    {

        [DataMember(Name = "description")]
        public PostmanDescription Description { get; set; }

        [DataMember(Name = "event")]
        public List<PostmanEvent> Events { get; set; } = new List<PostmanEvent>();

        [DataMember(Name = "variable")]
        public List<PostmanVariable> Variables { get; set; } = new List<PostmanVariable>();
    }
}
