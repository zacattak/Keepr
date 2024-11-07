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
    //     // Normalize the tag name to lowercase
    //     tagData.Name = tagData.Name.ToLower();

    //     // Step 1: Retrieve all tagIds associated with this keepId from the keepTags table
    //     string getTagIdsSql = @"
    //         SELECT kt.tagId
    //         FROM keepTags kt
    //         WHERE kt.keepId = @KeepId;
    //     ";

    //     // Execute the query to get all tagIds for the given keepId
    //     var tagIds = _db.Query<int>(getTagIdsSql, new { KeepId = keepId }).ToList();

    //     // Step 2: If no tags are associated with the keep, proceed to create the new tag
    //     if (!tagIds.Any())
    //     {
    //         return CreateNewTag(tagData);  // No tags, safe to create the new tag
    //     }

    //     // Step 3: Retrieve the tag names associated with the tagIds from the tags table
    //     string getTagNamesSql = @"
    //         SELECT t.name
    //         FROM tags t
    //         WHERE t.id IN @TagIds;
    //     ";

    //     // Execute the query to retrieve all tag names for the given tagIds
    //     var existingTagNames = _db.Query<string>(getTagNamesSql, new { TagIds = tagIds }).ToList();

    //     // Step 4: Check if the tag name already exists in the existingTagNames list
    //     if (existingTagNames.Contains(tagData.Name))
    //     {
    //         // If a tag with the same name already exists for the given keepId, return null (or throw an error)
    //         return null;  // Return null or throw new Exception("Tag already exists for this keep.");
    //     }

    //     // Step 5: Proceed to create the new tag if no duplicates are found
    //     return CreateNewTag(tagData);
    // }

    // // Helper method to handle the tag creation if no duplicate is found
    // private Tag CreateNewTag(Tag tagData)
    // {
    //     // SQL to insert the new tag
    //     string insertTagSql = @"
    //         INSERT INTO tags (creatorId, name)
    //         VALUES (@CreatorId, @Name);

    //         SELECT
    //             tag.*,
    //             account.*
    //         FROM tags tag
    //         JOIN accounts account ON tag.creatorId = account.id
    //         WHERE tag.id = LAST_INSERT_ID();
    //     ";

    //     // Insert the new tag and retrieve the tag along with its creator account
    //     Tag tag = _db.Query<Tag, Account, Tag>(insertTagSql, (tag, account) =>
    //     {
    //         tag.Creator = account;
    //         return tag;
    //     }, tagData).FirstOrDefault();

    //     return tag;
    // }


    internal Tag CreateTag(Tag tagData, int keepId)
    {
        // Convert tag name to lowercase to avoid case-sensitive duplicates
        tagData.Name = tagData.Name.ToLower();

        string checkExistingTagSql = @"
        SELECT tag.* 
        FROM tags tag
        JOIN keepTags kt ON kt.tagId = tag.id
        WHERE tag.name = @Name AND kt.keepId = @KeepId
        LIMIT 1;
        ";
        var existingTag = _db.Query<Tag>(checkExistingTagSql, new { Name = tagData.Name, KeepId = keepId }).FirstOrDefault();

        Console.WriteLine(existingTag != null
    ? $"Found existing tag with ID: {existingTag.Id}, Name: {existingTag.Name}"
    : "No existing tag found");

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




    // string checkExistingTagSql = @"
    // SELECT tag.* FROM tags tag
    // JOIN keepTags kt ON kt.tagId = tag.id
    // WHERE LOWER(tag.name) = @Name AND kt.keepId = @KeepId
    // LIMIT 1;





}