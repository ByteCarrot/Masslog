using AutoMapper;
using System;
using System.Linq.Expressions;

namespace ByteCarrot.Masslog.Web.Infrastructure.Extensions
{
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDestination> Map<TSource, TDestination, TMember>(this IMappingExpression<TSource, TDestination> expression,
            Expression<Func<TDestination, object>> destination, Expression<Func<TSource, TMember>> source)
        {
            return expression.ForMember(destination, c => c.MapFrom(source));
        }
    }
}