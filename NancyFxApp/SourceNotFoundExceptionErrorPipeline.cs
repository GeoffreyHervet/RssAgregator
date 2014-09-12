using System.Text;
using Nancy;
using Nancy.Bootstrapper;

namespace NancyFxApp
{
    public class SourceNotFoundExceptionErrorPipeline : IApplicationStartup
    {
        public void Initialize(IPipelines pipelines)
        {
            pipelines.OnError += (context, exception) =>
            {
                if (exception is SourceNotFoundException)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        ContentType = "text/html",
                        Contents = (stream) =>
                        {
                            var errorMessage =
                                Encoding.UTF8.GetBytes(
                                    exception.Message);
                            stream.Write(errorMessage, 0,
                                         errorMessage.Length);
                        }
                    };
                }
                return HttpStatusCode.InternalServerError;
            };
        }
    }
}