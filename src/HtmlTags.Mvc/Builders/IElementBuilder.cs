using HtmlTags.Conventions;

namespace HtmlTags.Mvc.Builders
{
    public interface IElementBuilder : ITagBuilder<ElementRequest>
    {
        ITagBuilderPolicy<ElementRequest> GeTagBuilderPolicy();
        bool Matches(ElementRequest request);
    }
}