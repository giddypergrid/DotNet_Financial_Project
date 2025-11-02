using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Helpers;
using System.Linq.Expressions;

namespace backend.Helpers
{
    public static class QueryableFunctions
    {
        public static IQueryable<T> QuerySortingByProperty<T>(IQueryable<T> queryable, Expression<Func<T, object>> keySelector, bool isDescending, bool doSort=false)
        {
            if (!doSort || keySelector == null)
            {
                EasyLogger.Instance.Log(nameof(QueryableFunctions), nameof(QuerySortingByProperty), $"doSort is false or keySelector is null");
                return queryable;
            }
            return isDescending
            ? queryable.OrderByDescending(keySelector)
            : queryable.OrderBy(keySelector);
        }
        public enum CompareType{
            LessThan,
            LessThanOrEqual,
            Equal,
            GreaterThan,
            GreaterThanOrEqual,
        }
        public static IQueryable<T> QueryFilterByProperty<T>(IQueryable<T> queryable, string propertyName, string targetValue, CompareType compareType, bool doFilter=false)
        {
            var property = typeof(T).GetProperty(propertyName);
            if (property == null || !doFilter){
                EasyLogger.Instance.Log(nameof(QueryableFunctions), nameof(QueryFilterByProperty), $"Property {propertyName} not found or doFilter is false");
                return queryable;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var member = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(targetValue);

            Expression comparison;
            switch (compareType)
            {
                case CompareType.LessThan:
                    comparison = Expression.LessThan(member, constant);
                    break;
                case CompareType.LessThanOrEqual:
                    comparison = Expression.LessThanOrEqual(member, constant);
                    break;
                case CompareType.Equal:
                    comparison = Expression.Equal(member, constant);
                    break;
                case CompareType.GreaterThan:
                    comparison = Expression.GreaterThan(member, constant);
                    break;
                case CompareType.GreaterThanOrEqual:
                    comparison = Expression.GreaterThanOrEqual(member, constant);
                    break;
                default:
                    throw new NotSupportedException($"Unsupported comparison: {compareType}");
            }

            var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
            return queryable.Where(lambda);
        }
    }
}