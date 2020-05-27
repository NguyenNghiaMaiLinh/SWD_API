using AutoMapper;
using DemoApp.Core.Services;
using DemoApp.Core.ViewModel;
using DemoApp.Core.ViewModel.ViewPage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DemoApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        public TaskController(IServiceProvider serviceProvider)
        {
            _taskService = serviceProvider.GetRequiredService<ITaskService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();

        }

        #region get All Task
        [HttpGet]
        public ActionResult<BaseViewModel<PagingResult<TaskViewPage>>> getAllTask([FromQuery] BasePagingRequestViewModel request)
        {
            request.SetDefaultPage();

            var result = _taskService.getAllTask(request);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        #endregion

        #region Get Task by Id

        [HttpGet("getMemberId")]
        public ActionResult<BaseViewModel<TaskViewPage>> getTaskById([FromQuery]string taskId)
        {

            var result = _taskService.getTaskById(taskId);

            return result;
        }

        #endregion

        #region create

        [HttpPost]
        public ActionResult<BaseViewModel<TaskViewPage>> create([FromBody]TaskCreateViewPage request)
        {

            var result = _taskService.create(request);

            return Ok(result);
        }

        #endregion

        #region update
        [HttpPut]
        public ActionResult<BaseViewModel<TaskViewPage>> update([FromBody]TaskUpdateViewPage request)
        {

            var result = _taskService.update(request);

            return result;
        }

        #endregion

        #region delete
        [HttpDelete("{id}")]
        public ActionResult<BaseViewModel<bool>> delete(string id)
        {

            var result = _taskService.delete(id);

            return result;
        }

        #endregion
    }
}