using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Text;
using ProjectManager.Api.Controllers.Models.Projects;
using ProjectManager.Api.Controllers.Models.Todos;
using ProjectManager.Data;
using ProjectManager.Data.Entities;
using ProjectManager.Data.Interfaces;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace ProjectManager.Api.Controllers
{
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IClock _clock;
        private readonly ApplicationDbContext _dbContext;
        public ProjectController(ILogger<ProjectController> logger, IClock clock, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _clock = clock;
            _dbContext = dbContext;
        }

        [HttpPost("api/v1/Project")]

        public async Task<ActionResult> Create(
            [FromBody] ProjectCreateModel model
            )
        {
            var now = _clock.GetCurrentInstant();


            var newProject = new Project
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description
            }.SetCreateBySystem(now);


            var uniqueCheck = await _dbContext.Set<Project>().AnyAsync(x => x.Name == newProject.Name);
            if (uniqueCheck)
            {
                ModelState.AddModelError<ProjectCreateModel>(x => x.Name, "Name is not unique.");
            }
            if(!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            _dbContext.Add(newProject);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("api/v1/Project")]
        public async Task<ActionResult<IEnumerable<ProjectDetailModel>>> GetList()
        {

            var dbEntities = await _dbContext.Set<Project>().Select(x => new ProjectDetailModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedAt = InstantPattern.ExtendedIso.Format(x.CreateAt),
                Todos = x.Todos.Select(y => new TodoDetailModel
                {
                    Id = y.Id,
                    Title = y.Title,
                    Description = y.Description,
                    CreatedAt = InstantPattern.ExtendedIso.Format(y.CreateAt),
                })
            }).ToListAsync();

            return Ok(dbEntities);
        }

        [HttpGet("api/v1/Project/{id}")]

        public async Task<ActionResult<IEnumerable<ProjectDetailModel>>> Get(
            [FromRoute] Guid id
            )
        {
            var dbEntities = await _dbContext
                 .Set<Project>()
                 .FilterDeleted()
                 .Where(x => x.Id == id)
                 .Select(x => new ProjectDetailModel
                 {
                     Id = x.Id,
                     Description = x.Description,
                     Name = x.Name,
                     CreatedAt = x.CreateAt.ToString(),
                 })
                 .SingleOrDefaultAsync();

            if (dbEntities == null)
            {
                return NotFound();
            }

            return Ok(dbEntities);
        }
        [HttpPatch("api/v1/Project/{id}")]
        public async Task<ActionResult> Update(
         [FromRoute] Guid id,
         [FromBody] ProjectCreateModel patch
        )
        {
            var dbEntities = await _dbContext
                 .Set<Project>()
                 .FilterDeleted()
                 .SingleOrDefaultAsync(x => x.Id == id);

            if (dbEntities == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("api/v1/Project/{id}")]
        public async Task<ActionResult> Delete(
    [FromRoute] Guid id
    )
        {
            var dbEntities = await _dbContext
                 .Set<Project>()
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