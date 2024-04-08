namespace Keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal KeepClone CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO 
        vaultKeeps(creatorId, keepId, vaultId)
        VALUES(@CreatorId, @KeepId, @VaultId);
        SELECT
        vaultKeep.*,
        account.*,
        keep.*,
        vault.*
        FROM vaultKeeps vaultKeep
        JOIN accounts account ON account.id = vaultKeep.creatorId
        JOIN keeps keep ON keep.id = vaultKeep.keepId
        JOIN vaults vault ON vault.id = vaultKeep.vaultId
        WHERE vaultKeep.id = LAST_INSERT_ID()
        ;";

        KeepClone vaultKeep = _db.Query<VaultKeep, KeepClone, KeepClone>(sql, (vaultKeep, keepClone) =>
        {
            keepClone.VaultKeepId = vaultKeep.Id;
            return keepClone;
        }, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }

}