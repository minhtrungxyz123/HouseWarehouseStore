namespace Master.Service
{
    public class AdminSearchContext
    {
        public string? Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}