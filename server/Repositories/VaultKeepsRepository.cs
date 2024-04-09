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
        vaultKeeps(keepId, vaultId, creatorId)
        VALUES(@KeepId, @VaultId, @CreatorId);
        SELECT
        vaultKeep.*,
        keep.*,
        vault.*,
        account.*
        FROM vaultKeeps vaultKeep
        JOIN keeps keep ON keep.id = vaultKeep.keepId
        JOIN vaults vault ON vault.id = vaultKeep.vaultId
        JOIN accounts account ON account.id = vaultKeep.creatorId

        WHERE vaultKeep.id = LAST_INSERT_ID()
        ;";




        KeepClone keepClone = _db.Query<VaultKeep, KeepClone, Vault, Account, KeepClone>(sql, (vaultKeep, keep, vault, account) =>
        {
            keep.VaultKeepId = vaultKeep.Id;
            keep.KeepId = vaultKeep.KeepId;
            keep.VaultKeepId = vaultKeep.VaultId;
            keep.Creator = account;

            return keep;

        },
        vaultKeepData).FirstOrDefault();
        return keepClone;
    }

    internal List<KeepClone> GetVaultKeepsByVaultId(int vaultId)
    {
        string sql = @"
        SELECT
        vaultKeep.*,
        vault.*
        FROM vaultKeeps vaultKeep
        JOIN vaults vault ON vault.id = vaultKeep.vaultId
        WHERE vaultKeep.vaultId = @vaultId
        ;";
        List<KeepClone> keepClones = _db.Query<VaultKeep, KeepClone, KeepClone>(sql, (vaultKeep, keepClone) =>
        {
            keepClone.VaultKeepId = vaultKeep.Id;
            return keepClone;
        }, new { vaultId }).ToList();

        return keepClones;

    }

    internal VaultKeep FindVaultKeepById(int vaultKeepId)
    {
        string sql = @"SELECT * FROM vaultKeeps WHERE id = @vaultKeepId;";
        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
        return vaultKeep;
    }

    internal void DeleteVaultKeep(int vaultKeepId)
    {
        string sql = @"
        DELETE FROM vaultKeeps
        WHERE id = @vaultKeepId;";
        _db.Execute(sql, new { vaultKeepId });
    }
}