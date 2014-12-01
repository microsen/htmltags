using System;
using HtmlTags.Extended.Attributes;

namespace HtmlTags.Mvc.Builders
{
    public class EnumBuilder : ElementBuilder
    {
        public override bool Matches(ElementRequest request)
        {
            return request.IsForEditor()
                   && request.Type.IsEnum;
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var select = new HtmlTag("select");

            foreach (var name in Enum.GetNames(request.Type))
            {
                var option = new HtmlTag("option");
                option.Text(name);
                if (name == request.GetData("value").ToString()) option.Attr("selected");
                option.Value((int)Enum.Parse(request.Type, name));
                select.Children.Add(option);
            }

            return select;
        }
    }
}