namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repository;

    public VaultKeepsService(VaultKeepsRepository repository)
    {
        _repository = repository;
    }

    internal KeepClone CreateVaultKeep(VaultKeep vaultKeepData)
    {
        KeepClone keepClone = _repository.CreateVaultKeep(vaultKeepData);
        return keepClone;
    }

    internal List<KeepClone> GetVaultKeepsByVaultId(int vaultId)
    {
        List<KeepClone> keepClones = _repository.GetVaultKeepsByVaultId(vaultId);
        return keepClones;
    }

    internal VaultKeep FindVaultKeepById(int vaultKeepId)
    {
        VaultKeep vaultKeep = _repository.FindVaultKeepById(vaultKeepId);
        if (vaultKeep == null)
        {
            throw new Exception($"Invalid ID: {vaultKeepId}");
        }
        return vaultKeep;
    }

    internal string DeleteVaultKeep(string userId, int vaultKeepId)
    {
        VaultKeep vaultKeep = FindVaultKeepById(vaultKeepId);
        if (userId != vaultKeep.CreatorId)
        {
            throw new Exception("No Permission to delete!");
        }
        _repository.DeleteVaultKeep(vaultKeepId);
        return "deleted";
    }
}