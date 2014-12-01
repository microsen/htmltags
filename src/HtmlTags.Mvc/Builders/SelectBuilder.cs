using System.Collections.Generic;
using HtmlTags.Extended.Attributes;

namespace HtmlTags.Mvc.Builders
{
    public class SelectBuilder : ElementBuilder
    {
        public override bool Matches(ElementRequest request)
        {
            return request.IsForEditor() && request.HasData("keyvalues");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var select = new HtmlTag("select");
            if (request.Required)
            {
                select.Children.Add(new HtmlTag("option"));
            }

            var kvps = request.GetData<IEnumerable<KeyValuePair<string, string>>>("keyvalues");

            foreach (var kvp in kvps)
            {
                var option = new HtmlTag("option");
                option.Text(kvp.Value);
                if (kvp.Key == request.GetData<string>("selectedValue")) option.Attr("selected");
                option.Value(kvp.Key);
                select.Children.Add(option);
            }

            return select;
        }
    }
}