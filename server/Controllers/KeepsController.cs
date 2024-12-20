namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]


public class KeepsController : ControllerBase
{
    private readonly KeepsService _keepsService;
    private readonly Auth0Provider _auth0Provider;

    private readonly KeepTagsService _keepTagsService;

    public KeepsController(KeepsService keepsService, Auth0Provider auth0Provider, KeepTagsService keepTagsService)
    {
        _keepsService = keepsService;
        _auth0Provider = auth0Provider;
        _keepTagsService = keepTagsService;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep keep = _keepsService.CreateKeep(keepData);
            return Ok(keep);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }
    }
    [HttpGet]
    public ActionResult<List<Keep>> GetKeeps()
    {
        try
        {
            List<Keep> keeps = _keepsService.GetKeeps();
            return Ok(keeps);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{keepId}")]
    public async Task<ActionResult<Keep>> GetKeepById(int keepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.GetKeepByIdAndIncrementViews(keepId);
            return Ok(keep);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }
    }
    [HttpPut("{keepId}")]
    [Authorize]

    public async Task<ActionResult<Keep>> EditKeep([FromBody] Keep updateData, int keepId)
    {
        try
        {
            // updateData.Id = keepId;
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Keep newKeep = _keepsService.EditKeep(updateData, keepId, userInfo.Id);
            return Ok(newKeep);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    [HttpDelete("{keepId}")]
    [Authorize]

    public async Task<ActionResult<string>> DestroyKeep(int keepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _keepsService.DestroyKeep(keepId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }
    }

    // [HttpGet("{keepId}/keepTags")]
    // public async Task<ActionResult<List<TagClone>>> GetKeepTagsByKeepId(int keepId)
    // {
    //     try
    //     {
    //         Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
    //         List<TagClone> keepTags = _keepTagsService.GetKeepTagsByKeepId(keepId);
    //         return Ok(keepTags);
    //     }
    //     catch (Exception exception)
    //     {
    //         return BadRequest(exception.Message);
    //     }
    // }

    [HttpGet("search")]
    public ActionResult<IEnumerable<Keep>> SearchKeepsByTag(string tagName)
    {
        try
        {
            var keeps = _keepsService.GetKeepsByTagName(tagName);
            return Ok(keeps);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}