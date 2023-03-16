using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PhoneBook.Lib.Domain.Exceptions;

#pragma warning disable CS1591

namespace PhoneBook.Api;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CustomException exception)
        {
            var problemDetails = CustomProblemDetailsFactory.CreateProblemDetails(
                statusCode: (int) HttpStatusCode.BadRequest,
                problemCode: exception.ProblemCode,
                title: "A handled exception occured",
                detail: context.Exception.Message);

            context.Result = new Problem(problemDetails);
        }
        else
        {
            var problemDetails = CustomProblemDetailsFactory.CreateProblemDetails(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An unhandled exception occured.");

            context.Result = new Problem(problemDetails);
        }
    }
}

public class Problem : ObjectResult
{
    public Problem(ProblemDetails details) : base(details)
    {
        if (details.Status != null) StatusCode = (int) details.Status;
    }
}

public static class CustomProblemDetailsFactory
{
    public static CustomProblemDetails CreateProblemDetails(int? statusCode = null, string? title = null, string? type = null, string? detail = null,
        string? instance = null, int? problemCode = null)
    {
        var problemDetails = new CustomProblemDetails
        {
            Status      = statusCode,
            Title       = title,
            Type        = type,
            Detail      = detail,
            Instance    = instance,
            ProblemCode = problemCode
        };

        return problemDetails;
    }
}

public class CustomProblemDetails : ProblemDetails
{
    public int? ProblemCode { get; set; }
}