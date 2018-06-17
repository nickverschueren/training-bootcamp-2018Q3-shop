using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Shop.Api.Helpers
{
    public abstract class SortExpressionMapper
    {
        protected static readonly MethodInfo OrderByMethod;
        protected static readonly MethodInfo OrderByDescendingMethod;

        static SortExpressionMapper()
        {
            var methods = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public);
            OrderByMethod = methods.Single(m => m.Name == nameof(Queryable.OrderBy) &&
                                                 m.GetParameters().Length == 2);
            OrderByDescendingMethod = methods.Single(m => m.Name == nameof(Queryable.OrderByDescending) &&
                                                           m.GetParameters().Length == 2);
        }
    }

    public abstract class SortExpressionMapper<TIn, TOut> : SortExpressionMapper, ISortExpressionMapper<TOut>
    {
        private readonly Dictionary<string, LambdaExpression> _mappings;

        protected SortExpressionMapper()
        {
            _mappings = GetMappings();

            OrderBy = (q, l) => (IQueryable<TOut>)OrderByMethod
                .MakeGenericMethod(typeof(TOut), l.Body.Type).Invoke(null, new object[] { q, l });

            OrderByDescending = (q, l) => (IQueryable<TOut>)OrderByDescendingMethod
                .MakeGenericMethod(typeof(TOut), l.Body.Type).Invoke(null, new object[] { q, l });
        }

        protected void SetMapping<TProperty>(string sourcePropertyName, Expression<Func<TOut, TProperty>> targetPropExpression)
        {
            _mappings[sourcePropertyName] = targetPropExpression;
        }

        protected void RemoveMapping(string sourcePropertyName)
        {
            _mappings.Remove(sourcePropertyName);
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

            queryable = sort.order != SortOrder.Descending ?
                OrderBy(queryable, lambda) :
                OrderByDescending(queryable, lambda);

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

        private static Func<IQueryable<TOut>, LambdaExpression, IQueryable<TOut>> OrderBy { get; set; }

        private static Func<IQueryable<TOut>, LambdaExpression, IQueryable<TOut>> OrderByDescending { get; set; }

        private static Dictionary<string, LambdaExpression> GetMappings()
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
    }
}
