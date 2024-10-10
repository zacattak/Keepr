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
        vaults(creatorId, name, description, img, isPrivate)
        VALUES(@CreatorId, @Name, @Description, @Img, @IsPrivate);



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

        return vault;

    }

    internal Vault UpdateVault(Vault data)
    {
        string sql = @"
        UPDATE vaults
        SET
        name = @Name,
        description = @Description,
        isPrivate = @IsPrivate
        WHERE id = @Id;

        SELECT 
        vault.*,
        account.*
        FROM vaults vault
        JOIN accounts account ON vault.creatorId = account.id
        WHERE vault.id = @Id
        ;";

        Vault vault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
          {
              vault.Creator = account;
              return vault;
          }, data).FirstOrDefault();
        return vault;
    }

    private Vault _populateCreator(Vault vault, Account account)
    {
        vault.Creator = account;
        return vault;
    }

    internal void DestroyVault(int vaultId)
    {
        string sql = @"
        DELETE FROM vaults
        WHERE id = @vaultId
        ;";
        _db.Execute(sql, new { vaultId });
    }

    internal List<Vault> GetMyVaults(string userId)
    {
        // FIXME get vaults WHERE they are mine
        string sql = @"
        SELECT
        vault.*,
        account.*
        FROM vaults vault
        JOIN accounts account ON account.id = vault.creatorId
        WHERE vault.creatorId = @userId
        ;";

        List<Vault> vaults = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;

            return vault;
        }, new { userId }).ToList();
        return vaults;
    }

    internal List<Vault> GetVaultsByAccountId(string accountId)
    {
        string sql = @"
        SELECT
        vault.*,
        account.*
        FROM vaults vault
        JOIN accounts account ON vault.creatorId = account.id
        WHERE vault.creatorId = @accountId
        ;";

        List<Vault> vaults = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
    {
        vault.Creator = account;

        return vault;
    }, new { accountId }).ToList();
        return vaults;
    }
}