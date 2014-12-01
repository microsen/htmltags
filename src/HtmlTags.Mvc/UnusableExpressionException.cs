using System;
using System.Linq.Expressions;

namespace HtmlTags.Mvc
{
    public class UnusableExpressionException<TModel, TProperty> : Exception
    {
        public UnusableExpressionException(Expression<Func<TModel, TProperty>> expression)
            : base(String.Format("'{0}' cannot be cast as a MemberExpression", expression))
        {
        }
    }
}