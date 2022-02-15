using Microsoft.AspNetCore.Mvc;

using Azure.Core.Extensions;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weather.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportErrorController : ControllerBase
    {
        QueueServiceClient _queueServiceClient;

        public ReportErrorController(QueueServiceClient queueServiceClient)
        {
            _queueServiceClient = queueServiceClient;
        }

        // POST api/<ReportErrorController>
        [HttpPost]
        public IActionResult Post([FromQuery] string value)
        {
            var queue = _queueServiceClient.CreateQueue("badforecast");

            queue.Value.SendMessage(value);

            return Ok();
        }
    }
}
