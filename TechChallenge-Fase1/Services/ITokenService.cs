using TechChallenge_Application.DTOs;

namespace TechChallenge_Fase1.Services
{
    public interface ITokenService
    {
        string GenerateToken(UsuarioDTO usuario);
    }
}
