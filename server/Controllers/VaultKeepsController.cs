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

}