namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class KeepTagsController : ControllerBase
{
    private readonly KeepTagsService _keepTagsService;

    private readonly Auth0Provider _auth0Provider;

    public KeepTagsController(KeepTagsService keepTagsService, Auth0Provider auth0Provider)

    {
        _keepTagsService = keepTagsService;
        _auth0Provider = auth0Provider;
    }

    [HttpPost]
    [Authorize]


    public async Task<ActionResult<KeepTag>> CreateKeepTag([FromBody] KeepTag keepTagData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            keepTagData.CreatorId = userInfo.Id;
            KeepTag keepTag = _keepTagsService.CreateKeepTag(keepTagData);
            return Ok(keepTag);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }
    }
}