namespace Keepr.Services;

public class VaultsService
{
    private readonly VaultsRepository _repository;

    public VaultsService(VaultsRepository repository)
    {
        _repository = repository;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = _repository.CreateVault(vaultData);

        return vault;
    }
    internal Vault GetVaultById(int vaultId, string userId)

    {
        Vault vault = _repository.GetVaultById(vaultId);

        if (vault == null) throw new Exception($"Invalid id: {vaultId}");

        if (vault.IsPrivate == true && userId != vault.CreatorId)
        {
            throw new Exception($"Invalid id: {vaultId} ⚠️");
        }

        return vault;
    }

    internal Vault UpdateVault(int vaultId, Vault vaultData, string userId)
    {
        Vault vaultToUpdate = GetVaultById(vaultId, userId);

        if (vaultToUpdate.CreatorId != userId) throw new Exception("This is not your vault! Begone!!!");

        vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;
        vaultToUpdate.Description = vaultData.Description ?? vaultToUpdate.Description;
        // vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;

        Vault vault = _repository.UpdateVault(vaultToUpdate);
        return vault;
    }
    internal string DestroyVault(int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId, userId);
        if (vault.CreatorId == userId)
        {
            _repository.DestroyVault(vaultId);
            return $"{vault.Name} removed";
        }
        else
        {
            throw new Exception("You don't have permission to destroy this! Begone!!!");
        }
    }
}