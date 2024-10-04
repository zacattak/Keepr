namespace Keepr.Models;

public class Tag

{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string CreatorId { get; set; }

    public string Name { get; set; }

    public Account Creator { get; set; }
}