namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase
{
    private readonly VaultsService _vaultsService;

    private readonly VaultKeepsService _vaultKeepsService;

    private readonly Auth0Provider _auth0Provider;

    public VaultsController(VaultsService vaultsService, Auth0Provider auth0Provider, VaultKeepsService vaultKeepsService)
    {
        _vaultsService = vaultsService;
        _auth0Provider = auth0Provider;
        _vaultKeepsService = vaultKeepsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault vault = _vaultsService.CreateVault(vaultData);
            return Ok(vault);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }
    }
    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.GetVaultById(vaultId, userInfo?.Id);
            return Ok(vault);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    [HttpPut("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<Vault>> UpdateVault(int vaultId, [FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.UpdateVault(vaultId, vaultData, userInfo.Id);
            return Ok(vault);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{vaultId}")]
    [Authorize]

    public async Task<ActionResult<Vault>> DestroyVault(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _vaultsService.DestroyVault(vaultId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }
    }

    // [HttpGet("{vaultId}/keeps")] // 'https://localhost:7045/api/albums/4/pictures'
    // public ActionResult<List<KeepClone>> GetVaultKeepsByVaultId(int vaultId)
    // {
    //     try
    //     {
    //         List<KeepClone> keeps = _vaultKeepsService.GetVaultKeepsByVaultId(vaultId);
    //         return Ok(keeps);
    //     }
    //     catch (Exception exception)
    //     {
    //         return BadRequest(exception.Message);
    //     }
    // }

}