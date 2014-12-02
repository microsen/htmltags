using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using HtmlTags.Conventions;
using HtmlTags.Mvc.Builders;

namespace HtmlTags.Mvc
{
    public class ElementConventionLibrary : HtmlConventionLibrary
    {
        public TagLibrary<ElementRequest> Library { get; private set; }

        public ElementConventionLibrary()
        {
            Library = For<ElementRequest>();

        }

        public CategoryExpression<ElementRequest> EditorsIf(Func<ElementRequest, bool> matches)
        {
            return Library.If(x => x.Intent == TagRequestIntent.Edit && matches.Invoke(x));
        }

        public CategoryExpression<ElementRequest> EditorsAlways()
        {
            return Library.If(x => x.Intent == TagRequestIntent.Edit);
        }

        public CategoryExpression<ElementRequest> DisplaysIf(Func<ElementRequest, bool> matches)
        {
            return Library.If(x => x.Intent == TagRequestIntent.Display && matches.Invoke(x));
        }

        public CategoryExpression<ElementRequest> DisplaysAlways()
        {
            return Library.If(x => x.Intent == TagRequestIntent.Display);
        }

        public CategoryExpression<ElementRequest> If(Func<ElementRequest, bool> matches)
        {
            return Library.If(matches);
        }

        public void Add(IElementBuilder builder)
        {
            Library.Add(builder.GeTagBuilderPolicy());
        }

        public void Add(ITagModifier<ElementRequest> modifier)
        {
            Library.Add(modifier);
        }

    }
}