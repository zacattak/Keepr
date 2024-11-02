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
        tagData.Name = tagData.Name.ToLower();

        // string checkSql = @"
        //         SELECT * FROM tags
        //         WHERE name = @Name;
        //     ";

        // Tag existingTag = _db.Query<Tag>(checkSql, new { tagData.Name }).FirstOrDefault();
        // if (existingTag != null)
        // {
        //     throw new Exception("A tag with this name already exists.");
        // }

        string checkSql = "SELECT COUNT(1) FROM tags WHERE name = @Name;";
        int existingCount = _db.ExecuteScalar<int>(checkSql, new { Name = tagData.Name });

        if (existingCount > 0)
        {
            // Instead of throwing an exception, return null or a specific value
            return null; // Indicate that the tag already exists
        }


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

    //     internal Tag FindTagByNameAndKeepId(string name, int keepId)
    // {
    //     string sql = @"
    //     SELECT tag.* FROM tags tag
    //     JOIN keepTags kt ON kt.tagId = tag.id
    //     WHERE LOWER(tag.name) = @Name AND kt.keepId = @KeepId
    //     LIMIT 1;";

    //     return _db.Query<Tag>(sql, new { Name = name.ToLower(), KeepId = keepId }).FirstOrDefault();
    // }
}