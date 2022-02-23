using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private IConfiguration _configuration;
        private SigningConfigurations _signinConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public LoginService(IUserRepository repository,
         IConfiguration configuration,
         SigningConfigurations signingConfigurations,
         TokenConfigurations tokenConfigurations
         )
        {
            _repository = repository;
            _configuration = configuration;
            _signinConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            UserEntity userEntity = new UserEntity();
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                userEntity = await _repository.FindByLogin(user.Email);

                if (userEntity == null)
                    return new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(userEntity.Email),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, userEntity.Email)
                }
            );

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);
            string token = CreateToken(identity, createDate, expirationDate);
            return SuccessObject(createDate, expirationDate, token, userEntity);

        }

        private string CreateToken(ClaimsIdentity identity, DateTime createdDate, DateTime expiration)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signinConfigurations.SigninCredentials,
                Subject = identity,
                NotBefore = createdDate,
                Expires = expiration
            });

            string token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, UserEntity userEntity)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = userEntity.Name,
                message = "Usuário Logado com Sucesso"
            };
        }
    }
}
