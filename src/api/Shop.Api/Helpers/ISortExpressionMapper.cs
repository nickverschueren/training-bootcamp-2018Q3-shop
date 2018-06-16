using System;
using System.Linq;
using System.Linq.Expressions;

namespace Shop.Api.Helpers
{
    public interface ISortExpressionMapper<TOut>
    {
        bool ValidateSortExpression(string sortExpression);
        IQueryable<TOut> ApplySortExpression(IQueryable<TOut> queryable, string sortExpression);
        IQueryable<TOut> ApplySortExpression<T>(IQueryable<TOut> queryable, string sortExpression, Expression<Func<TOut, T>> thenBy);
    }
}