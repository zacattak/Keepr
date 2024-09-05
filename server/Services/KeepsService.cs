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

    internal Keep GetKeepByIdAndIncrementViews(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId, userId);

        keep.Views++;

        _repository.EditKeep(keep);

        return keep;
    }

    internal Keep GetKeepById(int keepId, string userId)
    {
        Keep keep = _repository.GetKeepById(keepId);
        return keep;
    }

    internal Keep EditKeep(Keep updateData, int keepId, string userId)
    {
        // FIXME I shouldnt be able to edit someone elses keep



        Keep originalKeep = GetKeepById(keepId, userId);

        if (originalKeep.CreatorId != userId)
        {

            throw new Exception("You don't have permission to delete this!");

        }
        originalKeep.Name = updateData.Name?.Length > 0 ? updateData.Name : originalKeep.Name;
        originalKeep.Description = updateData.Description?.Length > 0 ? updateData.Description : originalKeep.Description;
        originalKeep.Img = updateData.Img?.Length > 0 ? updateData.Img : originalKeep.Img;
        Keep newKeep = _repository.EditKeep(originalKeep);
        return newKeep;
    }

    internal string DestroyKeep(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId, userId);
        if (keep.CreatorId != userId)
        {

            throw new Exception("You don't have permission to delete this!");

        }



        _repository.DestroyKeep(keepId);
        return $"{keep.Name} destroyed";


    }

    internal List<Keep> GetKeepsByAccountId(string profileId)
    {
        List<Keep> keeps = _repository.GetKeepsByAccountId(profileId);
        return keeps;
    }
}