using Intoxalock.Regular.POC.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Intoxalock.Regular.POC.Web.Apis
{
    public static class DLValidationApi
    {
        public static RouteGroupBuilder MapDLValidationApiEndpoints(this RouteGroupBuilder routes)
        {
            routes.MapPost("validate", PostDLtoValidateAsync);
            routes.WithTags("DLValidation");
            return routes;
        }

        private static async Task<IResult> PostDLtoValidateAsync(HttpRequest request, [FromServices] RegulaOcrService regulaOcrService)
        {
            var file = request.Form.Files.FirstOrDefault();
            if (file == null || file.Length == 0)
            {
                return Results.BadRequest("No file uploaded.");
            }

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var result = await regulaOcrService.PerformOcrAsync(filePath);
            if (result == null)
            {
                return Results.StatusCode(500);
            }

            return Results.Ok(result);
        }
    }
}
