using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Api.Controllers.Models.Projekts
{
    public class ProjectCreateModel
    {
        [Required(ErrorMessage = "{0} field is required", AllowEmptyStrings = false)]
        public string Name { get; set; } = null;
        public string? Description { get; set; }
    }
}
