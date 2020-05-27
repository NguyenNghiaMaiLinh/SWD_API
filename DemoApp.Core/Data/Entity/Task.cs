namespace DemoApp.Core.Data.Enity
{
    public partial class Task : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsDelete { get; set; }
        public int? Status { get; set; }
    }
}
