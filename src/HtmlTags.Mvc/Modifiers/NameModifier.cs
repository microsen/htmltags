using System;
using HtmlTags.Extended.Attributes;

namespace HtmlTags.Mvc.Modifiers
{
    public class NameModifier : ElementModifier
    {
        public override bool Matches(ElementRequest request)
        {
            return request.IsForEditor() && !String.IsNullOrWhiteSpace(request.Name);
        }

        public override void Modify(ElementRequest request)
        {
            request.OriginalTag.Name(request.Name);
        }
    }
}