using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HtmlTags.Mvc.Bootstrap
{
    public static class HtmlHelpers
    {
        public static HtmlTag Icon<TModel>(this HtmlHelper<TModel> html, string iconName)
        {
            return new Glyphicon(iconName);
        }
        
        public static HtmlTag Button<TModel>(this HtmlHelper<TModel> html, string text,
            ButtonType buttonType = ButtonType.Default,
            ButtonSize buttonSize = ButtonSize.Default,
            params KeyValuePair<string, object>[] data
            )
        {
            var request = html.DisplayRequest()
                .AddData("text", text)
                .AddData("button", true)
                .AddData("buttonType", buttonType)
                .AddData("buttonSize", buttonSize);

            data.ToList().ForEach(x => request.AddData(x.Key, x.Value));
            return request.BuildTag();
        }
        
        public static HtmlTag Button<TModel>(this HtmlHelper<TModel> html,
           string text,
           string action,
           string controller,
           ButtonType buttonType = ButtonType.Default,
           ButtonSize buttonSize = ButtonSize.Default,
           params KeyValuePair<string, object>[] data
           )
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);
            var dlist = data.ToList();
            dlist.Add(new KeyValuePair<string, object>("href", url.Action(action, controller)));
            return html.Button(text, buttonType, buttonSize, dlist.ToArray());
        }
    }
}