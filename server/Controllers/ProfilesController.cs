namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfilesController : ControllerBase

{
    private readonly AccountService _accountService;

    private readonly Auth0Provider _auth0Provider;
    public ProfilesController(AccountService accountService, Auth0Provider auth0Provider)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
    }

    [HttpGet("{accountId}")]

    public ActionResult<Account> GetProfileById(string accountId)

    // async task
    {
        try
        {
            // Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Account account = _accountService.GetProfileById(accountId);
            // userInfo?.Id
            return Ok(account);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
}


