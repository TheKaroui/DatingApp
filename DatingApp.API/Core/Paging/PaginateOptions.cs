namespace DatingApp.API.Core.Paging {
    public class PaginateOptions {
        public int CurrentPage { get; }
        public int PageSize { get; }

        public PaginateOptions (int page, int itemNumber) {
            CurrentPage = page;
            PageSize = itemNumber;
        }
    }
}