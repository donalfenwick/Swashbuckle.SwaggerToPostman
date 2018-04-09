using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Compare
{
    public class PostmanQueryParamComparer : IEqualityComparer<PostmanQueryParam>
    {
        public bool Equals(PostmanQueryParam x, PostmanQueryParam y)
        {
            if (x.Disabled != y.Disabled) return false;
            if (x.Key != y.Key) return false;
            if (x.Value != y.Value) return false;
            if (x.Description != null && x.Description != null)
            {
                if (x.Description.Content != y.Description.Content) return false;
                if (x.Description.Type != y.Description.Type) return false;
                if (x.Description.Version != y.Description.Version) return false;
            }
            return true;
        }

        public int GetHashCode(PostmanQueryParam obj)
        {
            return obj.GetHashCode();
        }
    }
}
