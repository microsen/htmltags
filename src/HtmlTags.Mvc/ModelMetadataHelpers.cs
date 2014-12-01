using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace HtmlTags.Mvc
{
    public static class ModelMetadataHelpers
    {
        public static MemberExpression GetMemberExpression<TModel, TProperty>(
            Expression<Func<TModel, TProperty>> expression)
        {
            MemberExpression memberExpression;
            try
            {
                memberExpression = expression.Body as MemberExpression ??
                                   ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }
            catch
            {
                throw new UnusableExpressionException<TModel, TProperty>(expression);
            }
            if (memberExpression == null) throw new UnusableExpressionException<TModel, TProperty>(expression);
            return memberExpression;
        }

        public static ModelMetadata GetMetaDataFor<T>(this HtmlHelper<T> html, MemberExpression expression)
        {
            var propertyName = expression.Member.Name;
            return html.ViewData.ModelMetadata.Properties.FirstOrDefault(x => x.PropertyName == propertyName);
        }

        public static ModelMetadata GetMetaDataFor<TModel, TResult>(this HtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata;
        }

        public static ModelState GetModelStateFor<T>(this HtmlHelper<T> html, string propertyName)
        {
            ModelState msValue;
            html.ViewData.ModelState.TryGetValue(propertyName, out msValue);
            return msValue ?? new ModelState();
        }

        public static ElementRequest AddModelMetadata(this ElementRequest request,
            ModelMetadata metadata,
            ModelState modelstate)
        {
            request.Value = metadata.Model;
            request.Name = metadata.PropertyName;
            request.DisplayName = metadata.GetDisplayName();
            request.Id = HtmlHelper.GenerateIdFromName(metadata.PropertyName);
            request.Required = IsRequired(metadata);
            request.ReadOnly = metadata.IsReadOnly;
            request.Order = metadata.Order;
            request.Hidden = metadata.ShowForEdit;
            request.Description = metadata.Description;
            request.ModelState = modelstate;

            if (!String.IsNullOrEmpty(metadata.DataTypeName))
            {
                var inputType = metadata.DataTypeName.ToLower()
                    .Replace("address", "")
                    .Replace("text", "");
                request.InputType = inputType;
            }

            metadata.AdditionalValues.ToList().ForEach(kvp => request.AddData(kvp.Key, kvp.Value));
            return request;
        }

        private static bool IsRequired(ModelMetadata metadata)
        {
            if (!metadata.ModelType.IsValueType) return metadata.IsRequired;
            if (metadata.IsNullableValueType) return metadata.IsRequired;
            if (metadata.Container == null) return metadata.IsRequired;
            return metadata.ContainerType.GetProperty(metadata.PropertyName)
                .GetCustomAttributes(typeof(RequiredAttribute), false).Any();
        }
    }
}