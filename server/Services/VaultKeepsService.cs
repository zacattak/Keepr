using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repository;
    private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService)
    {
        _repository = repository;
        _vaultsService = vaultsService;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        // FIXME get vault first to see if I am the creator of it (CRATE REPORT ON RESTAURANT REVIEWS)
        Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);

        if (vault.CreatorId != vaultKeepData.CreatorId)
        {
            throw new Exception("No Permission to delete!");
        }
        VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }

    internal List<KeepClone> GetVaultKeepsByVaultId(int vaultId, string userId)
    {
        _vaultsService.GetVaultById(vaultId, userId);
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