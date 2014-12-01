namespace HtmlTags.Mvc.Builders
{
    public class MultilineDisplayBuilder : ElementBuilder
    {
        public override bool Matches(ElementRequest request)
        {
            return request.IsForDisplay()
                   && TextAreaBuilder.IsTextArea(request);
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var tag = new HtmlTag("span");
            var text = request.GetValue<string>() ?? "";
            text = text.Replace("\n", "</br>");
            tag.AppendHtml(text);
            return tag;
        }
    }
}