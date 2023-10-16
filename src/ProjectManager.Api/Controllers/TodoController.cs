using Microsoft.AspNetCore.Mvc;
using NodaTime;
using ProjectManager.Api.Controllers.Models.Todos;
using ProjectManager.Data.Entities;
using ProjectManager.Data.Interfaces;
using System.Reflection;

namespace ProjectManager.Api.Controllers
{
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly IClock _clock;
        public TodoController(ILogger<TodoController> logger, IClock clock)
        {
            _logger = logger;
            _clock = clock;
        }
        [HttpPost("api/v1/Todo")]

        public async Task<ActionResult> Create(
            [FromBody] TodoCreateModel model
            )
        {
            var now = _clock.GetCurrentInstant();
            var newTodo = new Todo
            {
                Title = model.Title,
                Description = model.Description
            }.SetCreateBySystem(now);
            return Ok();
        }

    }
}