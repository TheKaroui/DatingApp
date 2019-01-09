using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Core.Extensions;

namespace DatingApp.API.Core.Paging {
    public static class Paging {
        public static PagedResultModel CreatePagedResults<T> (ICollection<T> collection, int page, int pageSize, string orderBy, bool ascending) {
            var queryable = collection.AsQueryable ();
            IQueryable projection;
            int totalNumberOfRecords;
            var totalPageCount = 0;

            if (page > 0 && pageSize > 0) {
                var skipAmount = pageSize * (page - 1);
                projection = !string.IsNullOrEmpty (orderBy) ? queryable.OrderByPropertyOrField (orderBy, ascending).Skip (skipAmount).Take (pageSize) :
                    queryable.Skip (skipAmount).Take (pageSize);
                totalNumberOfRecords = queryable.Count ();
                var mod = totalNumberOfRecords % pageSize;
                totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);
            } else {
                projection = queryable;
                totalNumberOfRecords = queryable.Count ();
            }

            return new PagedResultModel {
                Results = projection,
                    CurrentPage = page,
                    TotalNumberOfPages = totalPageCount,
                    TotalNumberOfRecords = totalNumberOfRecords
            };
        }
    }
}