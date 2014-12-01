using HtmlTags.Mvc.Builders;
using HtmlTags.Mvc.Modifiers;

namespace HtmlTags.Mvc
{
    public class DefaultHtmlConventions : ElementConventionLibrary
    {
        public DefaultHtmlConventions()
        {
            Add(new BoolBuilder());
            Add(new ButtonBuilder());
            Add(new SubmitModifier());
            Add(new EnumBuilder());
            Add(new MultilineDisplayBuilder());
            Add(new NumberInputBuilder());
            Add(new SelectBuilder());
            Add(new TextAreaBuilder());
            Add(new TextBoxBuilder());

            Add(new IdModifier());
            Add(new NameModifier());
            Add(new LabelModifier());
        }

    }
}