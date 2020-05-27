using DemoApp.Core.ViewModel;
using DemoApp.Core.ViewModel.ViewPage;
using System.Collections.Generic;

namespace DemoApp.Core.Services
{
    public interface ITaskService
    {
        BaseViewModel<TaskViewPage> getTaskById(string id);
        BaseViewModel<PagingResult<TaskViewPage>> getAllTask(BasePagingRequestViewModel request);
        BaseViewModel<TaskViewPage> create(TaskCreateViewPage request);
        BaseViewModel<TaskViewPage> update(TaskUpdateViewPage request);
        BaseViewModel<bool> delete(string id);
    }
}
