using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DatingApp.API.Core.Extensions {
    public static class Extensions {

        /// <summary>
        /// Order List by properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="ascending"></param>
        /// <returns>Returns ordered list by field</returns>
        public static System.Linq.IQueryable<T> OrderByPropertyOrField<T>(this IQueryable<T> queryable, string propertyOrFieldName, bool ascending = true) {
            var elementType = typeof (T);
            var orderByMethodName = ascending ? "OrderBy" : "OrderByDescending";
            var parameterExpression = Expression.Parameter (elementType);
            var body = propertyOrFieldName.Split ('.').Aggregate<string, Expression> (parameterExpression, Expression.PropertyOrField);
            var selector = Expression.Lambda (body, parameterExpression);
            var orderByExpression = Expression.Call (typeof (Queryable), orderByMethodName, new [] { elementType, body.Type }, queryable.Expression, selector);
            return queryable.Provider.CreateQuery<T> (orderByExpression);
        }

    }
}