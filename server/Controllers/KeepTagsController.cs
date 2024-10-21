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


    [HttpGet("{keepTagId}")]
    public async Task<ActionResult<KeepTag>> GetKeepTagById(int keepTagId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            TagClone tagClone = _keepTagsService.GetKeepTagById(keepTagId);
            return Ok(tagClone);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{keepTagId}")]
    [Authorize]

    // [HttpGet("{keepId}/tags")]
    // // public async Task<ActionResult<List<TagClone>>> GetKeepTagsByKeepId(int keepId)
    // public ActionResult<List<TagClone>> GetKeepTagsByKeepId(int keepId)
    // {
    //     try
    //     {
    //         // Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
    //         List<TagClone> keepTags = _keepTagsService.GetKeepTagsByKeepId(keepId);
    //         return Ok(keepTags);
    //     }
    //     catch (Exception exception)
    //     {
    //         return BadRequest(exception.Message);
    //     }
    // }

    public async Task<ActionResult<string>> DeleteKeepTag(int keepTagId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _keepTagsService.DeleteKeepTag(userInfo.Id, keepTagId);
            return Ok(message);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }
    }

    // [HttpGet("{keepId}/tags")]
    // // public async Task<ActionResult<List<TagClone>>> GetKeepTagsByKeepId(int keepId)
    // public ActionResult<List<TagClone>> GetKeepTagsByKeepId(int keepId)
    // {
    //     try
    //     {
    //         // Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
    //         List<TagClone> keepTags = _keepTagsService.GetKeepTagsByKeepId(keepId);
    //         return Ok(keepTags);
    //     }
    //     catch (Exception exception)
    //     {
    //         return BadRequest(exception.Message);
    //     }
    // }

    [HttpGet("{keepId}/tags")]
    public async Task<ActionResult<List<TagClone>>> GetKeepTagsByKeepId(int keepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<TagClone> keepTags = _keepTagsService.GetKeepTagsByKeepId(keepId);
            return Ok(keepTags);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

}