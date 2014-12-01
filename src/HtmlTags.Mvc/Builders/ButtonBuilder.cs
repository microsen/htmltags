namespace HtmlTags.Mvc.Builders
{
    public class ButtonBuilder : ElementBuilder
    {
        public override bool Matches(ElementRequest request)
        {
            return request.GetData<bool>("button");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("button")
                .Attr("type", "button")
                .AppendHtml(request.GetData<string>("text"));
        }
    }
}