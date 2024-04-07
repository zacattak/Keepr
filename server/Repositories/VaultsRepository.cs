namespace Keepr.Repositories;
public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }
    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
        INSERT INTO 
        vaults(creatorId, name, description, img)
        VALUES(@CreatorId, @Name, @Description, @Img);

        SELECT
        vault.*,
        account.*
        FROM vaults vault
        JOIN accounts account ON vault.creatorId = account.id
        WHERE vault.id = LAST_INSERT_ID()
        ;";
        Vault vault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, vaultData).FirstOrDefault();

        return vault;
    }

    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT 
        vault.*,
        account.*
        FROM vaults vault
        JOIN accounts account ON vault.creatorId = account.id
        WHERE vault.id = @vaultId
        ;";
        Vault vault = _db.Query<Vault, Account, Vault>(sql, _populateCreator, new { vaultId }).FirstOrDefault();
        // {
        //     vault.Creator = account;
        //     return vault;
        // }, new { vaultId }).FirstOrDefault();
        return vault;

    }

    internal Vault UpdateVault(Vault data)
    {
        string sql = @"
        UPDATE vaults
        SET
        name = @Name,
        description = @Description,
        isPrivate = @IsPrivate,
        WHERE id = @Id;

        SELECT 
        vault.*,
        account.*
        FROM vaults vault
        JOIN accounts account ON account.id = vault.creatorId
        WHERE vault.id = @Id
        ;";

        Vault vault = _db.Query<Vault, Account, Vault>(sql, _populateCreator, data).FirstOrDefault();

        return vault;
    }

    private Vault _populateCreator(Vault vault, Account account)
    {
        vault.Creator = account;
        return vault;
    }
}