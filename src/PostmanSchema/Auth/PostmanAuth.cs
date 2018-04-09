using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Auth
{
    [DataContract]
    public class PostmanAuth
    {
        // todo: implement a specific type for the settings for each auth type 

        [DataMember(Name = "type")]
        public PostmanAuthType Type { get; set; } = PostmanAuthType.noauth;
        [DataMember(Name = "awsv4")]
        public object Awsv4 { get; set; }
        [DataMember(Name = "basic")]
        public object Basic { get; set; }
        [DataMember(Name = "bearer")]
        public object Bearer { get; set; }
        [DataMember(Name = "digest")]
        public object Digest { get; set; }
        [DataMember(Name = "hawk")]
        public object Hawk { get; set; }
        [DataMember(Name = "ntlm")]
        public object Ntlm { get; set; }
        [DataMember(Name = "noauth")]
        public object Noauth { get; set; }
        [DataMember(Name = "oauth1")]
        public object Oauth1 { get; set; }
        [DataMember(Name = "oauth2")]
        public object Oauth2 { get; set; }
    }

    [DataContract]
    public enum PostmanAuthType
    {
        [EnumMember]awsv4,
        [EnumMember]basic,
        [EnumMember]bearer,
        [EnumMember]digest,
        [EnumMember]hawk,
        [EnumMember]noauth,
        [EnumMember]oauth1,
        [EnumMember]oauth2,
        [EnumMember]ntlm
    }
}