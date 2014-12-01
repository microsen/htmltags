namespace HtmlTags.Mvc.Modifiers
{
    public class SubmitModifier : ElementModifier
    {
        public override bool Matches(ElementRequest request)
        {
            return request.GetData<bool>("button") && request.GetData<bool>("submit");
        }

        public override void Modify(ElementRequest request)
        {
            request.OriginalTag.Attr("type", "submit");
        }
    }
}