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

    // internal List<KeepClone> GetVaultKeepsByVaultId(int vaultId)
    // {
    //     List<KeepClone> keepClones = _repository.GetVaultKeepsByVaultId(vaultId);
    //     return keepClones;
    // }


}