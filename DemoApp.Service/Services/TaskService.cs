using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using DemoApp.Core.Constants;
using DemoApp.Core.Data.Enity;
using DemoApp.Core.Infrastructure;
using DemoApp.Core.Repositories;
using DemoApp.Core.Services;
using DemoApp.Core.ViewModel;
using DemoApp.Core.ViewModel.ViewPage;

namespace DemoApp.Service.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; public TaskService(IUnitOfWork unitOfWork, IMapper mapper, ITaskRepository taskRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = taskRepository;
            _mapper = mapper;
        }

        public BaseViewModel<TaskViewPage> create(TaskCreateViewPage request)
        {

            var entity = new Task();
            entity.SetDefaultInsertValue(_repository.GetUsername());
            entity.IsDelete = false;
            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Status = 0;

            _repository.Add(entity);
            Save();
            return new BaseViewModel<TaskViewPage>
            {
                Data = _mapper.Map<TaskViewPage>(entity),
                StatusCode = HttpStatusCode.Created,
                Code = MessageConstants.SUCCESS,
            };
        }

        public BaseViewModel<bool> delete(string id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                return new BaseViewModel<bool>
                {
                    Code = MessageConstants.NOTFOUND,
                    Description = ErrMessageConstants.NOTFOUND,
                    Data = false,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
            else if (entity.CreateBy != _repository.GetUsername())
            {
                return new BaseViewModel<bool>
                {
                    Code = MessageConstants.FAILURE,
                    Description = ErrMessageConstants.INVALID_PERMISSION,
                    Data = false,
                    StatusCode = HttpStatusCode.PreconditionFailed
                };
            }
            entity.SetDefaultUpdateValue(_repository.GetUsername());
            entity.IsDelete = true;
            _repository.Update(entity);
            Save();
            return new BaseViewModel<bool>
            {
                Data = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public BaseViewModel<PagingResult<TaskViewPage>> getAllTask(BasePagingRequestViewModel request)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var result = new BaseViewModel<PagingResult<TaskViewPage>>();

            var data = _repository.getAllTask(pageIndex, pageSize, request.Search).ToList();
            if (data == null || data.Count == 0)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Description = MessageConstants.NO_RECORD;
                result.Code = MessageConstants.NO_RECORD;
            }
            else
            {
                result.StatusCode = HttpStatusCode.OK;
                result.Description = null;
                result.Code = MessageConstants.SUCCESS;
                var pageSizeReturn = pageSize;
                if (data.Count < pageSize)
                {
                    pageSizeReturn = data.Count;
                }

                result.Data = new PagingResult<TaskViewPage>
                {
                    Results = _mapper.Map<IEnumerable<TaskViewPage>>(data),
                    PageIndex = pageIndex,
                    PageSize = pageSizeReturn,
                    TotalRecords = data.Count()
                }; 
            }

            return result;
        }

        public BaseViewModel<TaskViewPage> getTaskById(string id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                return new BaseViewModel<TaskViewPage>
                {
                    Code = MessageConstants.NOTFOUND,
                    Description = ErrMessageConstants.NOTFOUND,
                    Data = null,
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new BaseViewModel<TaskViewPage>
            {
                Code = MessageConstants.SUCCESS,
                Description = null,
                Data = _mapper.Map<TaskViewPage>(entity),
                StatusCode = HttpStatusCode.OK
            };
        }

        public BaseViewModel<TaskViewPage> update(TaskUpdateViewPage request)
        {
            var entity = _repository.GetById(request.Id);
            if (entity == null)
            {
                return new BaseViewModel<TaskViewPage>
                {
                    Code = MessageConstants.NOTFOUND,
                    Description = ErrMessageConstants.NOTFOUND,
                    Data = null,
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Status = request.Status;
            _repository.Update(entity);
            Save();
            return new BaseViewModel<TaskViewPage>
            {
                Code = MessageConstants.SUCCESS,
                Description = null,
                StatusCode = HttpStatusCode.OK,
                Data = _mapper.Map<TaskViewPage>(entity)
            };
        }
        private void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
