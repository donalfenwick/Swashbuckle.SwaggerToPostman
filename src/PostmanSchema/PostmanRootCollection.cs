using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema
{
    /// <summary>
    /// Root object to build a postman collection from
    /// </summary>
    [DataContract]
    public class PostmanRootCollection : PostmanFolderBase
    {
        [DataMember(Name = "info")]
        public PostmanCollectionInfo Info { get; set; }
    }
}
