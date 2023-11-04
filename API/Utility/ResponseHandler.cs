using API.Controllers;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace API.Utility
{

    public class ResponseHandler : ControllerBase
    {
        private readonly ILogger<ResponseHandler> _logger;
        public ResponseHandler (
            ILogger<ResponseHandler> logger
        )
        {
            this._logger = logger;
        }

        public static IActionResult success<T>(
            string indetifier,
            T data
        )
        {
            var response = new
            {
                code = "SUCCESSFUL_OPERATION",
                message = "Successful Operation",
                indetifier = indetifier,
                date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                data = data
            };

            return new ObjectResult( response );
            
        }

        public static  IActionResult created<T>(
            string indetifier,
            T data
        )
        {
            var response = new
            {
                code = "CREATED",
                message = "Created",
                indetifier = indetifier,
                date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                data = data
            };

            return new ObjectResult(response);

        }

        public static IActionResult badRequestResponse()
        {
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }

        public static IActionResult NotFoundResponse()
        {
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        public static IActionResult internalServerError()
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

    }
}
