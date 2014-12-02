using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HtmlTags.Conventions;

namespace HtmlTags.Mvc
{
    public class ElementRequest : TagRequest
    {
        private object _value;
        public Type Type { get; set; }
        public TagRequestIntent Intent { get; set; }

        public Object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (Type == typeof(NullType))
                    Type = _value.GetType();
            }
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public bool Required { get; set; }
        public bool ReadOnly { get; set; }
        public int Order { get; set; }
        public bool Hidden { get; set; }
        public string Description { get; set; }
        public ModelState ModelState { get; set; }
        public string InputType { get; set; }
        public IDictionary<string, object> AdditionalData { get; set; }

        private abstract class NullType
        {
            private NullType() { }
        }
        public ElementRequest()
        {
            AdditionalData = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            Type = typeof(NullType);
        }

        public override object ToToken()
        {
            return this;
        }

        
    }

    public static class ElementRequestHelpers
    {
        public static HtmlTag BuildTag(this ElementRequest request)
        {
            return HtmlTagsConfiguration.TagGeneratorFactory.GeneratorFor<ElementRequest>().Build(request);
        }
    }
}