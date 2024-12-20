namespace Keepr.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO
        keeps(creatorId, name, description, img)
        VALUES(@CreatorId, @Name, @Description, @Img);

        SELECT
        keep.*,
        account.*
        FROM keeps keep
        JOIN accounts account ON keep.creatorId = account.id
        WHERE keep.id = LAST_INSERT_ID();";

        Keep keep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
        keep.Creator = account;
        return keep;
    }, keepData).FirstOrDefault();
        return keep;

    }

    internal List<Keep> GetKeeps()
    {
        string sql = @"
        SELECT  
        keep.*,
        account.*
        FROM keeps keep
        JOIN accounts account on keep.creatorId = account.id
        ;";

        List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }).ToList();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
            SELECT
            keep.*,
            account.*
            FROM keeps keep
            JOIN accounts account ON keep.creatorId = account.id
            WHERE keep.id = @keepId
            ;";

        Keep keep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }, new { keepId }).FirstOrDefault();
        return keep;
    }

    internal Keep EditKeep(Keep updateData)
    {
        string sql = @"
        UPDATE keeps SET
        name = @name,
        description = @description,
        img = @img,
        views = @views,
        kept = @kept
        WHERE id = @id;

        SELECT 
        keep.*,
        account.*
        FROM keeps keep
        JOIN accounts account ON keep.creatorId = account.id
        WHERE keep.id = @id
        ;";
        Keep keep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }, updateData).FirstOrDefault();
        return keep;
    }

    internal void DestroyKeep(int keepId)
    {
        string sql = @"
        DELETE FROM keeps
        WHERE id = @keepId;";
        _db.Execute(sql, new { keepId });
    }

    internal List<Keep> GetKeepsByAccountId(string accountId)
    {
        string sql = @"
        SELECT
        keep.*,
        account.*
        FROM keeps keep 
        JOIN accounts account ON keep.creatorId = account.id
        WHERE keep.creatorId = @accountId
        
        ;";

        List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            // vault.CreatorId = account.Id;
            return keep;
        }, new { accountId }).ToList();
        return keeps;
    }

    public IEnumerable<Keep> GetKeepsByTagName(string tagName)
    {
        string sql = @"
        SELECT k.*
        FROM keeps k
        JOIN keepTags kt ON k.id = kt.keepId
        JOIN tags t ON t.id = kt.tagId
        WHERE LOWER(t.name) = @TagName";

        // Use lowercase for case-insensitive comparison
        return _db.Query<Keep>(sql, new { TagName = tagName.ToLower() });
    }

}


