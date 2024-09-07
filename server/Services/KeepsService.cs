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

    internal Keep GetKeepByIdAndIncrementViews(int keepId)
    {
        Keep keep = GetKeepById(keepId);

        keep.Views++;

        _repository.EditKeep(keep);

        return keep;
    }

    internal Keep KeptKeep(int keepId)
    {
        Keep keep = GetKeepById(keepId);
        keep.Kept++;
        Keep keptKeep = _repository.EditKeep(keep);
        return keptKeep;

    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _repository.GetKeepById(keepId);
        return keep;
    }

    internal Keep EditKeep(Keep updateData, int keepId, string userId)
    {
        // FIXME I shouldnt be able to edit someone elses keep



        Keep originalKeep = GetKeepById(keepId);

        if (originalKeep.CreatorId != userId)
        {

            throw new Exception("You don't have permission to edit this!");

        }
        originalKeep.Name = updateData.Name?.Length > 0 ? updateData.Name : originalKeep.Name;
        originalKeep.Description = updateData.Description?.Length > 0 ? updateData.Description : originalKeep.Description;
        originalKeep.Img = updateData.Img?.Length > 0 ? updateData.Img : originalKeep.Img;
        Keep newKeep = _repository.EditKeep(originalKeep);
        return newKeep;
    }

    internal string DestroyKeep(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId);
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