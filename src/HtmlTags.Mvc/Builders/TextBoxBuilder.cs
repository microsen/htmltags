using HtmlTags.Extended.Attributes;

namespace HtmlTags.Mvc.Builders
{
    public class TextBoxBuilder : ElementBuilder
    {
        public override bool Matches(ElementRequest request)
        {
            return request.IsForEditor()
                   && request.IsType<string>()
                   && request.InputType != "multiline"
                   && !request.GetData<bool>("multiline");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var input = new HtmlTag("input");
            input.Attr("type", "text");

            input.Value(request.GetValue<string>());

            var name = request.Name.ToLower();
            var inputType = request.InputType;
            if (name.Contains("email") || inputType == "email") input.Attr("type", "email");
            if (name.Contains("password") || inputType == "password") input.Attr("type", "password");

            return input;
        }
    }
}