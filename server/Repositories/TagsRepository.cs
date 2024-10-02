namespace Keepr.Repositories;

public class TagsRepository
{
    private readonly IDbConnection _db;

    public TagsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Tag CreateTag(Tag tagData)
    {
        string sql = @"
        INSERT INTO 
        tags(creatorId, name)
        VALUES(@CreatorId, @Name);

        SELECT
        tag.*,
        account.*
        FROM tags tag
        JOIN accounts account on tag.creatorId = account.id
        WHERE tag.id = LAST_INSERT_ID();";

        Tag tag = _db.Query<Tag, Account, Tag>(sql, (tag, account) =>
       {
           tag.Creator = account;
           return tag;
       }, tagData).FirstOrDefault();
        return tag;

    }
}