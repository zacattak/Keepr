namespace Keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    // internal KeepClone CreateVaultKeep(VaultKeep vaultKeepData)
    // {
    //     string sql = @"
    //     INSERT INTO 
    //     vaultKeeps(keepId, vaultId, creatorId)
    //     VALUES(@KeepId, @VaultId, @CreatorId);
    //     SELECT
    //     vaultKeep.*,
    //     keep.*,
    //     vault.*,
    //     account.*
    //     FROM vaultKeeps vaultKeep
    //     JOIN keeps keep ON keep.id = vaultKeep.keepId
    //     JOIN vaults vault ON vault.id = vaultKeep.vaultId
    //     JOIN accounts account ON account.id = vaultKeep.creatorId

    //     WHERE vaultKeep.id = LAST_INSERT_ID()
    //     ;";




    //     KeepClone keepClone = _db.Query<VaultKeep, KeepClone, Vault, Account, KeepClone>(sql, (vaultKeep, keep, vault, account) =>

    //     // Vault, Account,                           vault, account
    //     {
    //         keep.VaultKeepId = vaultKeep.Id;
    //         keep.KeepId = vaultKeep.KeepId;
    //         keep.VaultId = vaultKeep.VaultId;
    //         keep.CreatorId = vaultKeep.CreatorId;
    //         keep.Creator = account;

    //         // keep.VaultId = vaultKeep.VaultId;
    //         // keep.VaultKeepId = vaultKeep.VaultId;
    //         // keep.Creator = account;

    //         return keep;

    //     },
    //     vaultKeepData).FirstOrDefault();
    //     return keepClone;
    // }

    internal KeepClone CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO 
        vaultKeeps(keepId, vaultId, creatorId)
        VALUES(@KeepId, @VaultId, @CreatorId);
        SELECT * FROM vaultKeeps WHERE id = LAST_INSERT_ID()
        ;";

        // SELECT * FROM vaultKeeps vaultKeep
        // JOIN keeps keep ON keep.id = vaultKeep.keepId
        // JOIN accounts account ON account.id = keep.creatorId
        // WHERE vaultKeep.id = LAST_INSERT_ID();";



        KeepClone keepClone = _db.Query<KeepClone>(sql, vaultKeepData).FirstOrDefault();
        // KeepClone keepClone = _db.Query<VaultKeep, KeepClone, Account, KeepClone>(sql, (vaultKeep, keep, account) =>
        // {
        //     keep.Id = vaultKeep.Id;
        //     keep.KeepId = vaultKeep.KeepId;
        //     keep.VaultKeepId = vaultKeep.Id;
        //     keep.VaultId = vaultKeep.VaultId;
        //     keep.CreatorId = vaultKeep.CreatorId;
        //     keep.Creator = account;
        //     // keep.
        //     return keep;
        // }, vaultKeepData).FirstOrDefault();
        return keepClone;
    }


    internal List<KeepClone> GetVaultKeepsByVaultId(int vaultId)
    {
        // FIXME you probably dont need to join the vaults table at all, reference postit
        string sql = @"
        SELECT
        vaultKeep.*,
        keep.*,
        account.*
        FROM vaultKeeps vaultKeep
        JOIN keeps keep ON keep.id = vaultKeep.keepId
        JOIN accounts account ON account.id = vaultKeep.creatorId
        WHERE vaultKeep.vaultId = @vaultId
        ;";

        List<KeepClone> keepClones = _db.Query<VaultKeep, KeepClone, Account, KeepClone>(sql, (vaultKeep, keepClone, account) =>
        {
            keepClone.VaultKeepId = vaultKeep.Id;
            keepClone.CreatorId = vaultKeep.CreatorId;
            keepClone.Creator = account;
            return keepClone;
        }, new { vaultId }).ToList();

        return keepClones;

    }

    internal KeepClone GetVaultKeepById(int vaultKeepId)
    {
        // string sql = @"SELECT * FROM vaultKeeps WHERE id = @vaultKeepId;";
        // KeepClone keepClone = _db.Query<KeepClone>(sql, new { vaultKeepId }).FirstOrDefault();
        // return keepClone;


        string sql = @"
        SELECT 
        vaultKeep.*,
        keep.*,
        account.*
        FROM vaultKeeps vaultKeep
        JOIN keeps keep ON keep.id = vaultKeep.keepId
        JOIN accounts account ON account.id = keep.creatorId
        WHERE vaultKeep.id = @vaultKeepId;";
        KeepClone vaultKeep = _db.Query<VaultKeep, KeepClone, Account, KeepClone>(sql, (vaultKeep, keep, account) =>
        {
            keep.KeepId = vaultKeep.KeepId;
            keep.VaultKeepId = vaultKeep.Id;
            keep.VaultId = vaultKeep.VaultId;
            keep.CreatorId = vaultKeep.CreatorId;
            keep.Creator = account;
            return keep;
        }, new { vaultKeepId }).FirstOrDefault();

        return vaultKeep;
    }


    internal void DeleteVaultKeep(int vaultKeepId)
    {
        string sql = "DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1;";
        _db.Execute(sql, new { vaultKeepId });
    }
}