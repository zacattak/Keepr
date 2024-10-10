namespace Keepr.Services;
public class KeepTagsService
{
    private readonly KeepTagsRepository _repository;

    private readonly KeepsService _keepsService;

    public KeepTagsService(KeepTagsRepository repository, KeepsService keepsService)
    {
        _repository = repository;
        _keepsService = keepsService;
    }

    internal KeepTag CreateKeepTag(KeepTag keepTagData)
    {
        KeepTag keepTag = _repository.CreateKeepTag(keepTagData);
        return keepTag;
    }

    internal List<TagClone> GetKeepTagsByKeepId(int keepId)
    {
        _keepsService.GetKeepById(keepId);
        List<TagClone> tagClones = _repository.GetKeepTagsByKeepId(keepId);
        return tagClones;
    }


    internal TagClone GetKeepTagById(int keepTagId)
    {
        TagClone tagClone = _repository.GetKeepTagById(keepTagId);
        if (tagClone == null)
        {
            throw new Exception($"Invalid Kept Keep id: {keepTagId}");

        }
        return tagClone;
    }

    internal string DeleteKeepTag(string userId, int keepTagId)
    {
        TagClone tagClone = GetKeepTagById(keepTagId);
        if (userId != tagClone.CreatorId)
        {
            throw new Exception("No Permission to delete!");
        }
        _repository.DeleteKeepTag(keepTagId);
        return "deleted";
    }

}