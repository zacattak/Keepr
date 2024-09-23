namespace Keepr.Models;

public class KeepTag
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string CreatorId { get; set; }

    public Account Creator { get; set; }

    public int TagId { get; set; }

    public int KeepId { get; set; }
}