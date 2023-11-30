namespace ProjectManager.Data.Entities;
[Table(nameof(Todo))]

public class Todo : ITrackable
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool Done { get; set; }
    public Instant CreateAt { get; set; }
    public string CreateBy { get; set; }
    public Instant ModifiedAt { get; set; }
    public string ModifiedBy { get; set; }
    public Instant? DeleteAt { get; set; }
    public string? DeleteBy { get; set; }

    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;

}

public static class TodoExtentions
{
    public static IQueryable<Todo> FilterDeleted(this IQueryable<Todo> query)
        => query
        .Where(x => x.DeleteAt == null)
        ;
}


