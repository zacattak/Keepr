namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultKeepsController : ControllerBase

{
    private readonly VaultKeepsService _vaultKeepsService;

    private readonly Auth0Provider _auth0Provider;

    public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider)
    {
        _vaultKeepsService = vaultKeepsService;
        _auth0Provider = auth0Provider;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<KeepClone>> CreateVaultKeep(VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

            vaultKeepData.CreatorId = userInfo.Id;

            KeepClone keepClone = _vaultKeepsService.CreateVaultKeep(vaultKeepData);

            return Ok(keepClone);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }

    }

    [HttpDelete("{vaultKeepId}")]
    [Authorize]

    public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _vaultKeepsService.DeleteVaultKeep(userInfo.Id, vaultKeepId);
            return Ok(message);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }

    }


}