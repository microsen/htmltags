using System;

namespace HtmlTags.Mvc.Modifiers
{
    public class IdModifier : ElementModifier
    {
        public override bool Matches(ElementRequest request)
        {
            return request.IsForEditor() && !String.IsNullOrWhiteSpace(request.Id);
        }

        public override void Modify(ElementRequest request)
        {
            request.OriginalTag.Id(request.Id);
        }
    }
}