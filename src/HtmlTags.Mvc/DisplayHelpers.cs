using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace HtmlTags.Mvc
{
    public static class DisplayHelpers
    {
        public static HtmlTag ShowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = html.GetMetaDataFor(expression);
            var modelstate = html.GetModelStateFor(metadata.PropertyName);
            var request = BuildTagRequest<TProperty>(metadata, modelstate);
            return request.BuildTag();
        }

        private static ElementRequest BuildTagRequest<TProperty>(ModelMetadata metadata, ModelState modelstate)
        {
            return new ElementRequest
                   {
                       Intent = TagRequestIntent.Display,
                   }.SetType<TProperty>()
                .AddModelMetadata(metadata, modelstate);
        }

        public static HtmlTag Show<TModel, TValue>(this HtmlHelper<TModel> html, TValue value)
        {
            var request = new ElementRequest
                          {
                              Intent = TagRequestIntent.Display
                          }.SetType<TValue>();

            request.Value = value;
            return request.BuildTag();
        }

        public static ElementRequest DisplayRequest<TModel>(this HtmlHelper<TModel> html)
        {
            var request = new ElementRequest
                          {
                              Intent = TagRequestIntent.Display
                          };
            return request;
        }
    }
}