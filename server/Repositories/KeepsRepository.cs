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


}