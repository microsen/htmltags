namespace HtmlTags.Mvc.Bootstrap
{
    public class ValidationMessage : HtmlTag
    {
        public ValidationMessage(string message)
            : base("span")
        {
            Text(message);
            AddClass("text-danger");
        }
    }
}