namespace Keepr.Repositories;

public class TagsRepository
{
    private readonly IDbConnection _db;

    public TagsRepository(IDbConnection db)
    {
        _db = db;
    }



    // internal Tag CreateTag(Tag tagData, int keepId)
    // {
    //     tagData.Name = tagData.Name.ToLower();

    //     string existingTagCheckSql = @"
    //     SELECT kt.*
    //     FROM keepTags kt
    //     JOIN tags t ON kt.tagId = t.id
    //     WHERE t.name = @Name AND kt.keepId = @KeepId;";

    //     var existingKeepTag = _db.QueryFirstOrDefault(existingTagCheckSql, new { Name = tagData.Name, KeepId = keepId });

    //     if (existingKeepTag != null)
    //     {
    //         // Return null or throw an exception if the tag already exists for this keep
    //         return null; // or throw new Exception("This tag already exists for the given keep.");
    //     }

    //     string checkSql = "SELECT COUNT(1) FROM tags WHERE name = @Name;";
    //     int existingCount = _db.ExecuteScalar<int>(checkSql, new { Name = tagData.Name });

    //     if (existingCount > 0)
    //     {
    //         // Instead of throwing an exception, return null or a specific value
    //         return null;
    //         // throw new Exception($"A tag with the name '{tagData.Name}' already exists.");
    //     }


    //     string sql = @"
    //     INSERT INTO 
    //     tags(creatorId, name)
    //     VALUES(@CreatorId, @Name);

    //     SELECT
    //     tag.*,
    //     account.*
    //     FROM tags tag
    //     JOIN accounts account on tag.creatorId = account.id
    //     WHERE tag.id = LAST_INSERT_ID();";

    //     Tag tag = _db.Query<Tag, Account, Tag>(sql, (tag, account) =>
    //    {
    //        tag.Creator = account;
    //        return tag;
    //    }, tagData).FirstOrDefault();
    //     return tag;

    // }


    internal Tag CreateTag(Tag tagData, int keepId)
    {
        // Convert tag name to lowercase to avoid case-sensitive duplicates
        tagData.Name = tagData.Name.ToLower();

        string checkExistingTagSql = @"
        SELECT tag.* FROM tags tag
        JOIN keepTags kt ON kt.tagId = tag.id
        WHERE LOWER(tag.name) = @Name AND kt.keepId = @KeepId
        LIMIT 1;
    ";
        var existingTag = _db.Query<Tag>(checkExistingTagSql, new { Name = tagData.Name, KeepId = keepId }).FirstOrDefault();

        if (existingTag != null)
        {
            // Return the existing tag without attempting to create a new keepTag
            return existingTag;
        }

        string insertTagSql = @"
        INSERT INTO tags (creatorId, name)
        VALUES (@CreatorId, @Name);
        SELECT LAST_INSERT_ID();
    ";

        int tagId = _db.ExecuteScalar<int>(insertTagSql, tagData);
        tagData.Id = tagId;

        return tagData;
    }




    // internal Tag CreateTag(Tag tagData, int keepId, string creatorId)
    // {
    //     // Normalize tag name to lowercase
    //     tagData.Name = tagData.Name.ToLower();

    //     // First, check if a tag with this name already exists for the specified keep
    //     string checkKeepTagSql = @"
    //     SELECT COUNT(1) FROM keepTags kt
    //     JOIN tags t ON kt.tagId = t.id
    //     WHERE kt.keepId = @KeepId AND LOWER(t.name) = @Name;
    // ";

    //     int existingKeepTagCount = _db.ExecuteScalar<int>(checkKeepTagSql, new { KeepId = keepId, Name = tagData.Name });

    //     // If a tag with this name is already associated with the specified keep, return null or handle accordingly
    //     if (existingKeepTagCount > 0)
    //     {
    //         return null; // Prevent creation
    //     }

    //     // Check if a tag with this name already exists in the tags table, independent of keeps
    //     string checkTagSql = "SELECT * FROM tags WHERE LOWER(name) = @Name LIMIT 1;";
    //     Tag existingTag = _db.Query<Tag>(checkTagSql, new { Name = tagData.Name }).FirstOrDefault();

    //     // If a tag with this name exists, reuse it. Otherwise, create a new tag.
    //     Tag tag = existingTag ?? CreateNewTag(tagData);

    //     // Associate the tag with the keep in keepTags
    //     string associateTagSql = @"
    //         INSERT INTO keepTags (keepId, tagId, creatorId)
    //         VALUES (@KeepId, @TagId, @CreatorId);
    //     ";
    //     _db.Execute(associateTagSql, new { KeepId = keepId, TagId = tag.Id, CreatorId = creatorId });

    //     return tag;
    // }

    // // Helper method to create a new tag if it doesn't already exist
    // private Tag CreateNewTag(Tag tagData)
    // {
    //     string sql = @"
    //     INSERT INTO tags (creatorId, name)
    //     VALUES (@CreatorId, @Name);

    //     SELECT 
    //         tag.*, 
    //         account.* 
    //     FROM tags tag
    //     JOIN accounts account ON tag.creatorId = account.id
    //     WHERE tag.id = LAST_INSERT_ID();
    // ";

    //     return _db.Query<Tag, Account, Tag>(sql, (tag, account) =>
    //     {
    //         tag.Creator = account;
    //         return tag;
    //     }, tagData).FirstOrDefault();
    // }


}