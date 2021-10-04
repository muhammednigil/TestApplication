using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain;

namespace TestApplication.Controllers
{
    public class TestApplicationControllerBase : ControllerBase
    {
        public TestApplicationControllerBase()
        {
        }


        protected IActionResult HandleValidationException(ValidationException ex)
        {
            var response = ServiceResponse.ErrorResponse(ex);
            response.msg = string.IsNullOrWhiteSpace(ex.Message) ? "validation failed" : ex.Message;
            response.errorlst = ex.Errors.Select(e => new ErrorMessage { error = e.ErrorMessage, value = e.AttemptedValue?.ToString() }).ToList();
            return BadRequest(response);
        }

        protected IActionResult HandleUserException(Exception ex)
        {
            return BadRequest(ServiceResponse.ErrorResponse(ex));
        }

        protected IActionResult HandleOtherException(Exception ex)
        {
            var response = ServiceResponse.ErrorResponse(ex);
            response.msg = "processing request failed";
            response.errorlst = new List<ErrorMessage>() { new ErrorMessage() { error = ex.Message } };
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        protected IActionResult OkServiceResponse(object payload, string message = null)
        {
            return Ok(ServiceResponse.SuccessResponse(message, payload));
        }

        protected IActionResult NotFoundServiceResponse(string message)
        {
            return NotFound(ServiceResponse.ErrorResponse(message));
        }

        protected IActionResult CreatedServiceResponse(object payload, string message = null)
        {
            return Created("", ServiceResponse.SuccessResponse(message, payload));
        }

        protected IActionResult HandleValidationException(ValidationException ex, bool activate)
        {
            var response = ServiceResponse.ErrorResponse(ex);
            response.msg = activate == true ? ex.Message : "Validation failed";
            response.errorlst = ex.Errors.Select(e => new ErrorMessage { error = e.ErrorMessage, value = e.AttemptedValue?.ToString() }).ToList();
            return BadRequest(response);
        }

        protected IActionResult HandleValidationExceptionErrorCode(ValidationException ex)
        {
            var response = ServiceResponse.ErrorResponse(ex);
            response.msg = string.IsNullOrWhiteSpace(ex.Message) ? "Validation failed" : ex.Message;
            response.errorlst = ex.Errors.Select(e => new ErrorMessage { error = e.PropertyName, value = e.ErrorMessage.ToString() }).ToList();
            return BadRequest(response);
        }

        protected IActionResult UserDefinedErrorWithMessage(string message, int statusCode = StatusCodes.Status400BadRequest)
        {
            return StatusCode(statusCode, ServiceResponse.ErrorResponse(message));

        }

    }
}
