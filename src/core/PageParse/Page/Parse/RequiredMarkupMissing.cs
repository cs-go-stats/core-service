using System;
using System.Runtime.Serialization;
using HtmlAgilityPack;

namespace CSGOStats.Infrastructure.Core.PageParse.Page.Parse
{
    [Serializable]
    public class RequiredMarkupMissing : XPathException
    {
        public RequiredMarkupMissing(HtmlNode root, string path)
            : base(root, path, $"Required path '{path}' in current context didn't found.")
        {
        }

        protected RequiredMarkupMissing(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}