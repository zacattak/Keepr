namespace Keepr.Repositories;
public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }
    internal Vault CreateVault(VaultsRepository vaultData)
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

}