using DataAccessObject.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PRN231PE_SP24_123890_SE160385PE_PRN231_FA23_TrialTest_Se160385_Client.Utils;

public static class GenerateJsonWebTokenString
{
    public static string GenerateJsonWebToken(this UserAccount userAccount, string secretKey, DateTime now)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.SerialNumber ,userAccount.UserAccountId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userAccount.UserFullName),
            new Claim(ClaimTypes.Email, userAccount.UserEmail ?? ""),
            new Claim(ClaimTypes.Role, userAccount.Role.ToString() ?? "")
        };
        var token = new JwtSecurityToken(
            claims: claims,
            expires: now.AddHours(1),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
