namespace ProjectManager.Data.Entities;
[Table(nameof(Project))]

public class Project : ITrackable
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool Done { get; set; }
    public Instant CreateAt { get; set; }
    public string CreateBy { get; set; }
    public Instant ModifiedAt { get; set; }
    public string ModifiedBy { get; set; }
    public Instant? DeleteAt { get; set; }
    public string? DeleteBy { get; set; }

    public ICollection<Todo> Todos { get; set; } = new HashSet<Todo>();
}

public static class ProjektExtentions
{
    public static IQueryable<Project> FilterDeleted(this IQueryable<Project> query)
        => query
        .Where(x => x.DeleteAt == null)
        ;
}

