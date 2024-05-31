using SharedKernel;

namespace Web.Api.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToProblemDetail(this Result result)
        {
            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Bad Request",
                type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                extensions: new Dictionary<string, object?> 
                {
                    { "errors", new[] { result.Error } }
                });
        }
    }
}
