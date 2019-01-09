namespace DatingApp.API.Core.Paging {
    public class PagedResultModel {
        public int CurrentPage { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int TotalNumberOfRecords { get; set; }
        public System.Linq.IQueryable Results { get; set; }
    }
}