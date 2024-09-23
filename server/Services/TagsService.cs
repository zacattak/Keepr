namespace Keepr.Services;
public class TagsService
{
    private readonly TagsRepository _repository;

    public TagsService(TagsRepository repository)
    {
        _repository = repository;
    }

    internal Tag CreateTag(Tag tagData)
    {
        Tag tag = _repository.CreateTag(tagData);
        return tag;
    }

}