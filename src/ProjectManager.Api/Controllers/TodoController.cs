using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Text;
using ProjectManager.Api.Controllers.Models.Todos;
using ProjectManager.Data;
using ProjectManager.Data.Entities;
using ProjectManager.Data.Interfaces;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace ProjectManager.Api.Controllers
{
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly IClock _clock;
        private readonly ApplicationDbContext _dbContext;
        public TodoController(ILogger<TodoController> logger, IClock clock, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _clock = clock;
            _dbContext = dbContext;
        }

        [HttpPost("api/v1/Todo")]

        public async Task<ActionResult> Create(
            [FromBody] TodoCreateModel model
            )
        {
            var now = _clock.GetCurrentInstant();
            var newTodo = new Todo
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Description = model.Description,
                ProjectId = model.ProjectId,
            }.SetCreateBySystem(now);

            _dbContext.Add(newTodo);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("api/v1/Todo")]

        public async Task<ActionResult<TodoDetailModel>> GetList()
        {
            var dbEntities = await _dbContext
                 .Set<Todo>()
                 .FilterDeleted()
                 .Select(x => new TodoDetailModel
                 {
                     Id = x.Id,
                     Description = x.Description,
                     Title = x.Title,
                     //ToDo.CreateAt , chyb9 d
                     CreatedAt = InstantPattern.ExtendedIso.Format(x.CreateAt),
                 })
                 .ToListAsync();

            return Ok(dbEntities);
        }

        [HttpGet("api/v1/Todo/{id}")]

        public async Task<ActionResult<IEnumerable<TodoDetailModel>>> Get(
            [FromRoute] Guid id
            )
        {
            var dbEntities = await _dbContext
                 .Set<Todo>()
                 .FilterDeleted()
                 .Where(x => x.Id == id)
                 .Select(x => new TodoDetailModel
                 {
                     Id = x.Id,
                     Description = x.Description,
                     Title = x.Title,
                     CreatedAt = InstantPattern.ExtendedIso.Format(x.CreateAt),
                 })
                 .SingleOrDefaultAsync();

            if (dbEntities == null)
            {
                return NotFound();
            }

            return Ok(dbEntities);
        }
        [HttpPatch("api/v1/Todo/{id}")]
        public async Task<ActionResult> Update(
         [FromRoute] Guid id,
         [FromBody] JsonPatchDocument<TodoCreateModel> patch
        )
        {
            var dbEntity = await _dbContext
                 .Set<Todo>()
                 .FilterDeleted()
                 .SingleOrDefaultAsync(x => x.Id == id);

            if (dbEntity == null)
            {
                return NotFound();
            }

            var now = _clock.GetCurrentInstant();

            var toUpdate = new TodoCreateModel
            {
                Description = dbEntity.Description,
                Title = dbEntity.Title,
            };

            patch.ApplyTo(toUpdate);

            if (!(ModelState.IsValid && TryValidateModel(toUpdate)))
            {
                return ValidationProblem(ModelState);
            }
            dbEntity.Title = toUpdate.Title;
            dbEntity.Description = toUpdate.Description;
            dbEntity.SetModifyBySystem(now);

            await _dbContext.SaveChangesAsync();

            dbEntity = await _dbContext.Set<Todo>().FirstAsync(x => x.Id == id);

            return Ok(new TodoDetailModel
            {
                Description = dbEntity.Description,
                Title = dbEntity.Title,
                CreatedAt = InstantPattern.ExtendedIso.Format(dbEntity.CreateAt)
            });
        }

            [HttpDelete("api/v1/Todo/{id}")]
            public async Task<ActionResult> Delete(
        [FromRoute] Guid id
        )
            {
                var dbEntities = await _dbContext
                     .Set<Todo>()
                     .FilterDeleted()
                     .SingleOrDefaultAsync(x => x.Id == id);

                if (dbEntities == null)
                {
                    return NotFound();
                }

                dbEntities.SetDeleteBySystem(_clock.GetCurrentInstant());
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
        }


    }