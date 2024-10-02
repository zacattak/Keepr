namespace Keepr.Models;

public class TagClone : Tag
{
    public int TagId { get; set; }
    public int KeepTagId { get; set; }

    public int KeepId { get; set; }
}