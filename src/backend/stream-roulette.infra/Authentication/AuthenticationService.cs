using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using stream_roulette.core.Interfaces.Repositories;
using stream_roulette.core.Models.Users;

namespace stream_roulette.infra.Authentication;

internal sealed class AuthenticationService(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator) : IAuthenticationService
{
    public async Task<TokenResponse?> LoginAsync(LoginRequestData loginData)
    {
        var user = await userRepository.GetByUsernameAsync(loginData.Username);
        if (user is null || new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, loginData.Password) ==
            PasswordVerificationResult.Failed)
        {
            return null;
        }

        return await CreateTokenResposeAsync(user);
    }

    public async Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequestData refreshData)
    {
        var user = await userRepository.GetByIdAsync(refreshData.UserId);
        if (user is null || user.RefreshToken != refreshData.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        return await CreateTokenResposeAsync(user);
    }

    private async Task<TokenResponse> CreateTokenResposeAsync(User user)
    {
        return new()
        {
            AccessToken = jwtTokenGenerator.Generate(user),
            RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
        };
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
    {
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
        await userRepository.UpdateAsync(user);

        return refreshToken;
    }
}