namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfilesController : ControllerBase

{
    private readonly AccountService _accountService;

    private readonly Auth0Provider _auth0Provider;

    private readonly KeepsService _keepsService;

    private readonly VaultsService _vaultsService;
    public ProfilesController(AccountService accountService, Auth0Provider auth0Provider, KeepsService keepsService, VaultsService vaultsService)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _keepsService = keepsService;
        _vaultsService = vaultsService;
    }

    // FIXME allow a route parameter
    [HttpGet("{accountId}")]

    // public async Task<ActionResult<Account>> GetProfileById(string accountId)
    public async Task<ActionResult<Account>> GetProfileById(string accountId)

    // async task
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Account account = _accountService.GetProfileById(accountId);
            // userInfo?.Id;
            return Ok(account);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    [HttpGet("{accountId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByAccountId(string accountId)

    {
        try
        {
            List<Keep> keeps = _keepsService.GetKeepsByAccountId(accountId);
            return Ok(keeps);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }
    }


    [HttpGet("{accountId}/vaults")]

    public async Task<ActionResult<List<Vault>>> GetVaultsByAccountId(string accountId)

    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<Vault> vaults = _vaultsService.GetVaultsByAccountId(accountId, userInfo?.Id);
            return Ok(vaults);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }
    }
}


