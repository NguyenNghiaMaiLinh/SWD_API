namespace DemoApp.Core.ViewModel.ViewPage
{
    public partial class TaskViewPage
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
    }

    public partial class TaskCreateViewPage
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public partial class TaskUpdateViewPage
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
    }
}
