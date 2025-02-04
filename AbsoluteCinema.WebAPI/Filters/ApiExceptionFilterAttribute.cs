using AbsoluteCinema.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AbsoluteCinema.WebAPI.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>> 
                {
                    { typeof(ValidationException), HandleValidationException },
                    { typeof(EntityNotFoundException), HandleEntityNotFoundException },
                    { typeof(AlreadyExistEntityException), HandleAlreadyExistEntityException },
                };
        }

        //Викликається автоматично, коли у контролері виникає виняток
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        //Перевіряє, чи є виняток у _exceptionHandlers, якщо так – викликає відповідний обробник.
        //Якщо модель ModelState є недійсною, викликає HandleInvalidModelStateException
        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                //вказує на стандартний HTTP-код 400 Bad Request
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = (ValidationException)context.Exception;

            var details = new ValidationProblemDetails(
                exception.Errors
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray()))
            {

                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleEntityNotFoundException(ExceptionContext context)
        {
            var exception = (EntityNotFoundException)context.Exception;

            var details = new ValidationProblemDetails()
            {
                Title = exception.Message,
                //вказує на стандартний HTTP-код 404 Not Found
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleAlreadyExistEntityException(ExceptionContext context)
        {
            var exception = (AlreadyExistEntityException)context.Exception;

            var details = new ValidationProblemDetails()
            {
                Title = exception.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}