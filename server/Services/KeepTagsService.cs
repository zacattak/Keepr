namespace Keepr.Services;
public class KeepTagsService
{
    private readonly KeepTagsRepository _repository;

    public KeepTagsService(KeepTagsRepository repository)
    {
        _repository = repository;
    }

    internal KeepTag CreateKeepTag(KeepTag keepTagData)
    {
        KeepTag keepTag = _repository.CreateKeepTag(keepTagData);
        return keepTag;
    }

}