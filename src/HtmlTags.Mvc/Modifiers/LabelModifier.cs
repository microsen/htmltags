using System;

namespace HtmlTags.Mvc.Modifiers
{
    public class LabelModifier : ElementModifier
    {
        public override bool Matches(ElementRequest request)
        {
            return request.IsForEditor() && !String.IsNullOrWhiteSpace(request.DisplayName);
        }

        public override void Modify(ElementRequest request)
        {
            var label = new HtmlTag("label");
            request.WrapWith(label);

            label.Text(request.DisplayName);
            if (!String.IsNullOrWhiteSpace(request.Id)) label.Attr("for", request.Id);
        }
    }
}