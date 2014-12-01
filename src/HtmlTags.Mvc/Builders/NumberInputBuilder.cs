namespace HtmlTags.Mvc.Builders
{
    public class NumberInputBuilder : ElementBuilder
    {
        public override bool Matches(ElementRequest request)
        {
            return request.IsForEditor() && request.Type.IsNumeric();
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("input").Attr("type", "number");
        }
    }
}