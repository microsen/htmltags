using System;
using System.Collections.Generic;
using System.Linq;
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
            return request.Build();
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
            return request.Build();
        }

        public static ElementRequest Show<TModel>(this HtmlHelper<TModel> html)
        {
            var request = new ElementRequest
                          {
                              Intent = TagRequestIntent.Display
                          };
            return request;
        }
    }

    public static class EditorHelpers
    {
        public static HtmlTag InputFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = html.GetMetaDataFor(expression);
            var modelstate = html.GetModelStateFor(metadata.PropertyName);
            var request = BuildTagRequest<TProperty>(metadata, modelstate);
            return request.Build();
        }

        private static ElementRequest BuildTagRequest<TProperty>(ModelMetadata metadata, ModelState modelstate)
        {
            return new ElementRequest
            {
                Intent = TagRequestIntent.Edit,
            }.SetType<TProperty>()
                          .AddModelMetadata(metadata, modelstate);
        }

        public static HtmlTag Input<TModel, TValue>(this HtmlHelper<TModel> html, TValue value)
        {
            var request = new ElementRequest
            {
                Intent = TagRequestIntent.Edit
            }.SetType<TValue>();
            return request.Build();
        }

        public static ElementRequest Input<TModel>(this HtmlHelper<TModel> html)
        {
            return new ElementRequest
                   {
                       Intent = TagRequestIntent.Edit
                   };
        }


        public static HtmlTag InputFor<TModel, TKey, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TKey>> selectedExpression,
            Expression<Func<TModel, IEnumerable<TProperty>>> availableValuesExpression,
            Func<TProperty, string> keyGetter,
            Func<TProperty, string> nameGetter)
        {
            var selectedMetadata = html.GetMetaDataFor(selectedExpression);
            var valuesMetadata = html.GetMetaDataFor(availableValuesExpression);

            var availableValues = (IEnumerable<TProperty>)valuesMetadata.Model;

            var modelstate = html.GetModelStateFor(selectedMetadata.PropertyName);
            var request = BuildTagRequest<TProperty>(selectedMetadata, modelstate);
            request.AddData("keyvalues", availableValues.Select(x => new KeyValuePair<string, string>(keyGetter.Invoke(x), nameGetter.Invoke(x))));
            if (selectedMetadata.Model != null) request.AddData("selectedKey", keyGetter.Invoke((TProperty)selectedMetadata.Model));
            return request.Build();
        }

    }
}