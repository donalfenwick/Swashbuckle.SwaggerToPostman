using Swashbuckle.SwaggerToPostman.PostmanSchema.Auth;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema
{
    [DataContract]
    public class PostmanFolder : PostmanFolderBase
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    [DataContract]
    public class PostmanFolderBase : CollectionItemBase
    {
        [DataMember(Name = "item")]
        public List<CollectionItemBase> Items { get; set; } = new List<CollectionItemBase>();


        [DataMember(Name = "auth")]
        public PostmanAuth Auth { get; set; }

    }

}
