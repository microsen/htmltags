using HtmlTags.Extended.Attributes;

namespace HtmlTags.Mvc.Builders
{
    public class BoolBuilder : ElementBuilder
    {
        public override bool Matches(ElementRequest request)
        {
            return request.Type == typeof(bool)
                   && request.Intent == TagRequestIntent.Edit;
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var tag = new HtmlTag("input").Attr("type", "checkbox").Value("True");
            if (request.GetValue<bool>()) tag.Attr("checked");
            return tag;
        }
    }
}