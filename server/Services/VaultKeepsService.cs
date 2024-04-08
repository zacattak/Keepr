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
        KeepClone vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }


}