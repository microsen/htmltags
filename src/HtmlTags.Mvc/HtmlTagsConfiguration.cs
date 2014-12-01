using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HtmlTags.Conventions;

namespace HtmlTags.Mvc
{
    public static class HtmlTagsConfiguration
    {
        public static TagGeneratorFactory TagGeneratorFactory;

        public static void Configure(IEnumerable<HtmlConventionLibrary> htmlConventionLibraries)
        {
            var requestBuilder = new TagRequestBuilder(new ITagRequestActivator[0]);
            var conventionLibrary = new DefaultHtmlConventions();

            foreach (var htmlConventionLibrary in htmlConventionLibraries)
            {
                conventionLibrary.Import(htmlConventionLibrary);
            }

            TagGeneratorFactory = new TagGeneratorFactory(new ActiveProfile(), conventionLibrary, requestBuilder);

        }
    }

   


}