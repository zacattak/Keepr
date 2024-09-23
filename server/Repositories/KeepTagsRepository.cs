namespace Keepr.Repositories;

public class KeepTagsRepository
{
    private readonly IDbConnection _db;

    public KeepTagsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal KeepTag CreateKeepTag(KeepTag keepTagData)
    {
        string sql = @"
        INSERT INTO 
        keepTags(tagId, keepId, creatorId)
        VALUES(@TagId, @KeepId, @CreatorId);
        SELECT * FROM keepTags WHERE id = LAST_INSERT_ID()
        ;";

        KeepTag keepTag = _db.Query<KeepTag>(sql, keepTagData).FirstOrDefault();

        return keepTag;
    }
}