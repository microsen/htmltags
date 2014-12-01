namespace HtmlTags.Mvc.Bootstrap
{
    public class Glyphicon : HtmlTag
    {
        public Glyphicon(string iconName)
            : base("span")
        {
            AddClasses("glyphicon", "glyphicon-" + iconName);
        }
    }
}