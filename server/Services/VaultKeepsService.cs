using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repository;
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;

    public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService, KeepsService keepsService)
    {
        _repository = repository;
        _vaultsService = vaultsService;
        _keepsService = keepsService;
    }

    internal KeepClone CreateVaultKeep(VaultKeep vaultKeepData)
    {
        // FIXME get vault first to see if I am the creator of it (CRATE REPORT ON RESTAURANT REVIEWS)
        Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);

        if (vault.CreatorId != vaultKeepData.CreatorId)
        {
            throw new Exception("You don't have permission!");
        }

        int keepId = vaultKeepData.KeepId;
        _keepsService.KeptKeep(keepId);
        KeepClone vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
        //ANCHOR KeepClone vaultKeep 
        // VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }

    internal List<KeepClone> GetVaultKeepsByVaultId(int vaultId, string userId)
    {
        _vaultsService.GetVaultById(vaultId, userId);
        List<KeepClone> keepClones = _repository.GetVaultKeepsByVaultId(vaultId);
        return keepClones;
    }

    internal KeepClone GetVaultKeepById(int vaultKeepId)
    {
        KeepClone keepClone = _repository.GetVaultKeepById(vaultKeepId);
        if (keepClone == null)
        {
            throw new Exception($"Invalid Kept Keep id: {vaultKeepId}");

        }
        return keepClone;
    }

    internal string DeleteVaultKeep(string userId, int vaultKeepId)
    {
        KeepClone keepClone = GetVaultKeepById(vaultKeepId);
        if (userId != keepClone.CreatorId)
        {
            throw new Exception("No Permission to delete!");
        }
        _repository.DeleteVaultKeep(vaultKeepId);
        return "deleted";
    }
}