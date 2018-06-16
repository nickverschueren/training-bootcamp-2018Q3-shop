using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Shop.Api.Helpers
{
    public abstract class SortExpressionMapper<TIn, TOut> : ISortExpressionMapper<TOut>
    {
        private readonly Dictionary<string, LambdaExpression> _mappings;
        private readonly MethodInfo _orderByMethod;
        private readonly MethodInfo _orderByDescendingMethod;

        protected SortExpressionMapper()
        {
            _mappings = GetMappings();

            var methods = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public);
            _orderByMethod = methods.Single(m => m.Name == nameof(Queryable.OrderBy) &&
                                                   m.GetParameters().Length == 2);
            _orderByDescendingMethod = methods.Single(m => m.Name == nameof(Queryable.OrderByDescending) &&
                                                             m.GetParameters().Length == 2);
        }

        protected void SetMapping<TProperty>(string sourcePropertyName, Expression<Func<TOut, TProperty>> targetPropExpression)
        {
            _mappings[sourcePropertyName] = targetPropExpression;
        }

        protected void RemoveMapping(string sourcePropertyName)
        {
            _mappings.Remove(sourcePropertyName);
        }

        private Dictionary<string, LambdaExpression> GetMappings()
        {
            var mappings = new Dictionary<string, LambdaExpression>(
                StringComparer.InvariantCultureIgnoreCase);
            foreach (var inProperty in typeof(TIn).GetProperties())
            {
                var mapping = typeof(TOut).GetProperty(inProperty.Name);
                if (mapping != null)
                {
                    var param = Expression.Parameter(typeof(TOut));
                    var lambda = Expression.Lambda(Expression.Property(param, mapping), param);

                    mappings.Add(inProperty.Name, lambda);
                }
            }

            return mappings;
        }

        public bool ValidateSortExpression(string sortExpression)
        {
            if (sortExpression == null)
                return true;
            if (!TryParseSortExpression(sortExpression, out var sort))
                return false;

            return _mappings.ContainsKey(sort.field);
        }

        public IQueryable<TOut> ApplySortExpression(IQueryable<TOut> queryable, string sortExpression)
        {
            if (sortExpression == null ||
                !TryParseSortExpression(sortExpression, out var sort))
                return queryable;

            var lambda = _mappings[sort.field];
            var parameters = new object[] { queryable, lambda };

            if (sort.order != SortOrder.Descending)
                queryable = (IQueryable<TOut>)_orderByMethod.MakeGenericMethod(typeof(TOut), lambda.Body.Type).Invoke(null, parameters);
            else
                queryable = (IQueryable<TOut>)_orderByDescendingMethod.MakeGenericMethod(typeof(TOut), lambda.Body.Type).Invoke(null, parameters);

            return queryable;
        }

        public IQueryable<TOut> ApplySortExpression<T>(IQueryable<TOut> queryable, string sortExpression, Expression<Func<TOut, T>> thenBy)
        {
            var queriable = ApplySortExpression(queryable, sortExpression);
            if (queriable is IOrderedQueryable<TOut> orderedQueriable)
                return orderedQueriable.ThenBy(thenBy);
            return queryable.OrderBy(thenBy);
        }

        private static bool TryParseSortExpression(string sortExpression, out (string field, SortOrder order) sort)
        {
            sort = (null, SortOrder.Unspecified);
            var matches = Regex.Match(sortExpression, Constants.SortExpressionFormat);
            if (!matches.Success) return false;

            sort = (matches.Groups.Single(g => g.Name == "field").Value,
                matches.Groups.Single(g => g.Name == "order").Value == "+" ?
                SortOrder.Ascending : SortOrder.Descending);

            return true;
        }
    }
}
