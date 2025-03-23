using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly TokenService _tokenService;

    public AuthController(ApplicationDbContext context, IPasswordHasher<User> passwordHasher, TokenService tokenService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Login == request.Login))
            return BadRequest("User already exists");

        var user = new User
        {
            Login = request.Login,
            PasswordHash = _passwordHasher.HashPassword(null, request.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Login == request.Login);
        if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            return Unauthorized("Invalid login or password");

        var accessToken = _tokenService.GenerateAccessToken(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Login),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        });

        var refreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await _context.SaveChangesAsync();

        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);
        if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
            return Unauthorized("Invalid or expired refresh token");

        var accessToken = _tokenService.GenerateAccessToken(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Login),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        });

        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await _context.SaveChangesAsync();

        return Ok(new { AccessToken = accessToken, RefreshToken = newRefreshToken });
    }
}
