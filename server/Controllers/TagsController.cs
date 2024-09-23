namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TagsController : ControllerBase
{
    private readonly TagsService _tagsService;

    private readonly Auth0Provider _auth0Provider;

    public TagsController(TagsService tagsService, Auth0Provider auth0Provider)

    {
        _tagsService = tagsService;
        _auth0Provider = auth0Provider;
    }

    [HttpPost]
    [Authorize]


    public async Task<ActionResult<Tag>> CreateTag([FromBody] Tag tagData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            tagData.CreatorId = userInfo.Id;
            Tag tag = _tagsService.CreateTag(tagData);
            return Ok(tag);
        }
        catch (Exception exception)
        {

            return BadRequest(exception.Message);
        }
    }
}