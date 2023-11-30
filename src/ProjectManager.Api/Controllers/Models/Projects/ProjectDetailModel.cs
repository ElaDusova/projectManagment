using ProjectManager.Api.Controllers.Models.Todos;
using ProjectManager.Data.Entities;
using System.Runtime.CompilerServices;

namespace ProjectManager.Api.Controllers.Models.Projects
{
    public class ProjectDetailModel
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string CreatedAt { get; set; } = null!;
        public IEnumerable<TodoDetailModel> Todos { get; set; } = Enumerable.Empty<TodoDetailModel>();
    }

    public static class ProjectDetailModelExtentions
    {
        public static ProjectDetailModel ToDetail(this Project source)
        => new()
        {

        };

    }

}
