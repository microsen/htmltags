using HtmlTags.Conventions;

namespace HtmlTags.Mvc.Modifiers
{
    public abstract class ElementModifier : ITagModifier<ElementRequest>
    {
        public abstract bool Matches(ElementRequest request);

        public abstract void Modify(ElementRequest request);
    }
}