using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace RestaurantSystem.API.Filters
{
    public class SwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Add custom tags based on controller name
            var controllerName = context.MethodInfo.DeclaringType?.Name.Replace("Controller", "");
            if (!string.IsNullOrEmpty(controllerName))
            {
                operation.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag { Name = controllerName }
                };
            }

            // Add example responses for common error codes
            if (!operation.Responses.ContainsKey("400"))
            {
                operation.Responses.Add("400", new OpenApiResponse
                {
                    Description = "Bad Request - Invalid input data",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Example = new Microsoft.OpenApi.Any.OpenApiObject
                            {
                                ["success"] = new Microsoft.OpenApi.Any.OpenApiBoolean(false),
                                ["message"] = new Microsoft.OpenApi.Any.OpenApiString("Invalid input data"),
                                ["data"] = new Microsoft.OpenApi.Any.OpenApiNull(),
                                ["errors"] = new Microsoft.OpenApi.Any.OpenApiArray
                                {
                                    new Microsoft.OpenApi.Any.OpenApiString("Name is required"),
                                    new Microsoft.OpenApi.Any.OpenApiString("Description must be between 10 and 500 characters")
                                }
                            }
                        }
                    }
                });
            }

            if (!operation.Responses.ContainsKey("404"))
            {
                operation.Responses.Add("404", new OpenApiResponse
                {
                    Description = "Not Found - Resource not found",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Example = new Microsoft.OpenApi.Any.OpenApiObject
                            {
                                ["success"] = new Microsoft.OpenApi.Any.OpenApiBoolean(false),
                                ["message"] = new Microsoft.OpenApi.Any.OpenApiString("Resource not found"),
                                ["data"] = new Microsoft.OpenApi.Any.OpenApiNull(),
                                ["errors"] = new Microsoft.OpenApi.Any.OpenApiArray()
                            }
                        }
                    }
                });
            }

            if (!operation.Responses.ContainsKey("500"))
            {
                operation.Responses.Add("500", new OpenApiResponse
                {
                    Description = "Internal Server Error",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Example = new Microsoft.OpenApi.Any.OpenApiObject
                            {
                                ["success"] = new Microsoft.OpenApi.Any.OpenApiBoolean(false),
                                ["message"] = new Microsoft.OpenApi.Any.OpenApiString("An unexpected error occurred"),
                                ["data"] = new Microsoft.OpenApi.Any.OpenApiNull(),
                                ["errors"] = new Microsoft.OpenApi.Any.OpenApiArray
                                {
                                    new Microsoft.OpenApi.Any.OpenApiString("Please contact support if this issue persists")
                                }
                            }
                        }
                    }
                });
            }
        }
    }
}
