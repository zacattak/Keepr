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

    internal List<TagClone> GetKeepTagsByKeepId(int keepId)
    {
        string sql = @"
        SELECT
        keepTag.*,
        keep.*
        FROM keepTags keepTag
        JOIN keeps keep ON keep.id = keepTag.keepId
        WHERE keepTag.keepId = @keepId
        ;";

        List<TagClone> tagClones = _db.Query<VaultKeep, TagClone, TagClone>(sql, (keepTag, tagClone) =>
       {
           tagClone.KeepTagId = keepTag.Id;
           return tagClone;
       }, new { keepId }).ToList();

        return tagClones;
    }

    internal TagClone GetKeepTagById(int keepTagId)
    {


        string sql = @"
        SELECT 
        keepTag.*,
        tag.*,
        account.*
        FROM keepTags keepTag
        JOIN tags tag ON tag.id = keepTag.tagId
        JOIN accounts account ON account.id = tag.creatorId
        WHERE keepTag.id = @keepTagId;";
        TagClone tagClone = _db.Query<KeepTag, TagClone, Account, TagClone>(sql, (keepTag, tag, account) =>
        {
            tag.KeepId = keepTag.KeepId;
            tag.KeepTagId = keepTag.Id;
            tag.KeepId = keepTag.KeepId;
            tag.CreatorId = keepTag.CreatorId;
            tag.Creator = account;
            return tag;
        }, new { keepTagId }).FirstOrDefault();

        return tagClone;
    }

    internal void DeleteKeepTag(int keepTagId)
    {
        string sql = "DELETE FROM keepTags WHERE id = @keepTagId LIMIT 1;";
        _db.Execute(sql, new { keepTagId });
    }
}