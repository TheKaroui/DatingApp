using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Core.Extensions;

namespace DatingApp.API.Core.Paging {
    public class PagedModel<T> {
        public int CurrentPage { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int TotalNumberOfRecords { get; set; }
        public IList<T> Results { get; set; }

        public static IList<T> SortBusinessModel (IEnumerable<T> collection, SortOptions so) {
            return so != null && !string.IsNullOrEmpty (so.OrderByProperty) ?
                collection.AsQueryable ().OrderByPropertyOrField (so.OrderByProperty, so.IsAscending).ToList () :
                collection.ToList ();
        }
    }
}