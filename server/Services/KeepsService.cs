namespace Keepr.Services;
public class KeepsService
{
    private readonly KeepsRepository _repository;

    public KeepsService(KeepsRepository repository)
    {
        _repository = repository;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = _repository.CreateKeep(keepData);
        return keep;
    }
    internal List<Keep> GetKeeps()
    {
        List<Keep> keeps = _repository.GetKeeps();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _repository.GetKeepById(keepId);
        return keep;
    }
}