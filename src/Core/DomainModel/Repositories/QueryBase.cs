namespace ByteCarrot.Masslog.Core.DomainModel.Repositories
{
    public abstract class QueryBase
    {
        public string SortColumn { get; set; }

        public SortDirection SortDirection { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}