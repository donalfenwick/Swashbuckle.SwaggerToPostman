using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman.Converters;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Swashbuckle.SwaggerToPostman.Converters
{
    public class UrlObjectConverter : IUrlObjectConverter
    {   
        private readonly DefaultValueFactory valueCreator;

        public UrlObjectConverter(DefaultValueFactory valueCreator)
        {
            this.valueCreator = valueCreator;
        }

        public PostmanUrl Convert(string path, List<IParameter> swaggerOperationParameters, string host, string basePath)
        {
            // replace aspnet route segments (/put/{id}) with postman style ones (/put/:id) so they can be populated with postman variables
            Regex ItemRegex = new Regex("{.*?}", RegexOptions.Compiled);
            foreach (Match match in ItemRegex.Matches(path))
            {
                string replacement = match.Value.Replace('{', ':').TrimEnd('}');
                path = path.Replace(match.Value, replacement);
            }
            
            var urlObject = new PostmanUrl
            {
                Raw = "",
                QueryParams = new List<PostmanQueryParam>()
            };

            string rawUrl = "";
            if (!string.IsNullOrEmpty(host))
            {

                Uri uri = null;
                if (Uri.IsWellFormedUriString(host, UriKind.Absolute))
                {
                    uri = new Uri(host, UriKind.Absolute);
                }

                if (uri == null || (!uri.Scheme.StartsWith("http")))
                {
                    string constructedBaseUrl = $"http://{host.Trim('/')}/";
                    if (Uri.IsWellFormedUriString(constructedBaseUrl, UriKind.Absolute))
                    {
                        uri = new Uri(constructedBaseUrl);
                    }
                }

                if (uri != null)
                {
                    if (!string.IsNullOrEmpty(basePath))
                    {
                        uri = new Uri(uri, basePath);
                    }

                    urlObject.Host = uri.Host;
                    urlObject.Port = uri.Port.ToString();
                    urlObject.Protocol = uri.Scheme;

                    rawUrl = uri.AbsoluteUri.TrimEnd('/');
                }

                string fullPath = $"{(basePath ?? "").TrimEnd('/')}/{path.TrimStart('/')}";

                urlObject.Path = fullPath.Trim('/').Split('/');
            }
            
            rawUrl = $"{(rawUrl ?? "").TrimEnd('/')}/{path.TrimStart('/')}".TrimEnd('/');

            var pathParams = swaggerOperationParameters.OfType<NonBodyParameter>().Where(p => p.In == SwashbuckleParameterTypeConstants.Path).ToList();

            var queryParams = swaggerOperationParameters.OfType<NonBodyParameter>().Where(p => p.In == SwashbuckleParameterTypeConstants.Query).ToList();
            if (queryParams.Count > 0)
            {
                rawUrl += "?" + string.Join("&", (queryParams.Select(x => x.Name + "={{" + x.Name + "}}")));
            }
            urlObject.Raw = rawUrl;

            var allUrlParams = swaggerOperationParameters
                .OfType<NonBodyParameter>()
                .Where(p => p.In == SwashbuckleParameterTypeConstants.Query || p.In == SwashbuckleParameterTypeConstants.Path)
                .ToList();

            foreach(NonBodyParameter p in allUrlParams)
            {
               var defaultValue = valueCreator.GetDefaultValueFromFormat(p.Format, p.Type);
                if (p.In == SwashbuckleParameterTypeConstants.Query)
                {
                    urlObject.QueryParams.Add(new PostmanQueryParam
                    {
                        Key = p.Name,
                        Value = (defaultValue != null ? defaultValue.ToString() : ""),
                        Disabled = false,
                        Description = new PostmanDescription { Content = p.Description }
                    });
                }
                urlObject.Variables.Add(new PostmanVariable
                {
                    Description = new PostmanDescription { Content = p.Description },
                    Disabled = false,
                    Id = p.Name,
                    Key = p.Name,
                    Name = p.Name,
                    System = false,
                    Type = p.Type,
                    Value = defaultValue != null ? defaultValue.ToString() : ""
                });
            }
            return urlObject;
        }
    }
}
