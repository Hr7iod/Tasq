using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasq.ActionFilters
{
    public class ValidateChildTasqExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateChildTasqExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;

            var tasqId = (Guid)context.ActionArguments["tasqId"];
            var tasq = await _repository.Tasq.GetTasqAsync(tasqId, false);

            if (tasq == null)
            {
                _logger.LogInfo($"Tasq with id: {tasqId} doesn't exist in the database.");
                context.Result = new NotFoundResult();
                return;
            }

            var childId = (Guid)context.ActionArguments["childId"];
            var child = await _repository.Tasq.GetChildAsync(tasqId, childId, trackChanges);

            if (child == null)
            {
                _logger.LogInfo($"Child with id: {childId} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("child", child);
                await next();
            }
        }
    }
}
