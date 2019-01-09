namespace DatingApp.API.Core.Paging {
    public class SortOptions {
        public string OrderByProperty { get; }
        public bool IsAscending { get; }

        public SortOptions (string property, bool isAscending) {
            OrderByProperty = property;
            IsAscending = isAscending;
        }
    }
}