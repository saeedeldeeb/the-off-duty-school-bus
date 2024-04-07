using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusManagement.Core.Common.Helpers;
using BusManagement.Core.Data;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;
using BusManagement.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BusManagement.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JWT _jwt;

    public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt)
    {
        _userManager = userManager;
        _jwt = jwt.Value;
    }

    public async Task<AuthorizedVM> RegisterAsync(RegisterDTO model)
    {
        if (await _userManager.FindByEmailAsync(model.Email) is not null)
            return new AuthorizedVM { Message = "Email is already registered!" };

        var user = new ApplicationUser
        {
            Name = model.Name,
            Gender = model.Gender,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            UserName = model.Email.Split('@')[0]
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Aggregate(
                string.Empty,
                (current, error) => current + $"{error.Description},"
            );

            return new AuthorizedVM { Message = errors };
        }

        await _userManager.AddToRoleAsync(user, model.Role);

        var jwtSecurityToken = await CreateJwtToken(user);

        return new AuthorizedVM
        {
            Email = user.Email,
            ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Roles = [model.Role],
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Username = user.UserName
        };
    }

    public async Task<AuthorizedVM> LoginAsync(LoginDTO model)
    {
        var authModel = new AuthorizedVM();

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            authModel.Message = "Email or Password is incorrect!";
            return authModel;
        }

        var jwtSecurityToken = await CreateJwtToken(user);
        var rolesList = await _userManager.GetRolesAsync(user);

        authModel.IsAuthenticated = true;
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        authModel.Email = user.Email ?? string.Empty;
        authModel.Username = user.UserName ?? string.Empty;
        authModel.ExpiresOn = jwtSecurityToken.ValidTo;
        authModel.Roles = rolesList.ToList();

        return authModel;
    }

    private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        foreach (var role in roles)
            roleClaims.Add(new Claim("roles", role));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(
            symmetricSecurityKey,
            SecurityAlgorithms.HmacSha256
        );

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_jwt.DurationInDays),
            signingCredentials: signingCredentials
        );

        return jwtSecurityToken;
    }
}
