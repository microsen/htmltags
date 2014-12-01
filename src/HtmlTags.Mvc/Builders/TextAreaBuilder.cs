namespace HtmlTags.Mvc.Builders
{
    public class TextAreaBuilder : ElementBuilder
    {
        public override bool Matches(ElementRequest request)
        {
            return request.IsForEditor()
                   && request.IsType<string>()
                   && (request.InputType == "multiline" || request.GetData<bool>("multiline"));
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var input = new HtmlTag("textarea");
            if (request.HasData("rows")) input.Attr("rows", request.GetData<string>("rows"));
            if (request.HasData("cols")) input.Attr("cols", request.GetData<string>("cols"));
            input.Text(request.GetValue<string>());
            return input;
        }
    }
}