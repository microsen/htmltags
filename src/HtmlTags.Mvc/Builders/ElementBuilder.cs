using System;
using HtmlTags.Conventions;

namespace HtmlTags.Mvc.Builders
{
    public abstract class ElementBuilder : IElementBuilder
    {
        public ITagBuilderPolicy<ElementRequest> GeTagBuilderPolicy()
        {
            return new ConditionalTagBuilderPolicy<ElementRequest>(Matches, Build);
        }

        public abstract bool Matches(ElementRequest request);

        public abstract HtmlTag Build(ElementRequest request);
    }
}