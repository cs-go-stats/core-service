using System;
using System.Linq.Expressions;
using System.Reflection;

namespace core_tests_foundation.ObjectBuilders
{
    public static class ObjectBuilderExtensions
    {
        public static TBuilder ValueFor<TBuilder, TValue>(this TBuilder builder, Expression<Func<TBuilder, TValue>> property, TValue value)
            where TBuilder : ObjectBuilder
        {
            typeof(TBuilder).GetTypeInfo().GetProperty(GetMemberName(property)).SetValue(builder, value);
            return builder;
        }

        private static string GetMemberName<TBuilder, TProperty>(Expression<Func<TBuilder, TProperty>> property)
        {
            var lambda = (LambdaExpression)property;

            var memberExpression = lambda.Body is UnaryExpression unaryExpression
                ? (MemberExpression) unaryExpression.Operand
                : (MemberExpression) lambda.Body;

            return memberExpression.Member.Name;
        }
    }
}