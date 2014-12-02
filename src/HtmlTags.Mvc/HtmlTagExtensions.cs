namespace HtmlTags.Mvc
{
    public static class HtmlTagExtensions
    {
        public static HtmlTag Prepend(this HtmlTag parent, HtmlTag child)
        {
            parent.Append(child); // to get the parent of the child set properly
            parent.Children.Remove(child);
            parent.Children.Insert(0, child);
            return parent;
        }
    }
}