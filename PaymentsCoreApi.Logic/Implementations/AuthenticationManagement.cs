using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PaymentsCoreApi.Data.Contexts;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Logic.Implementations
{
    public class AuthenticationManagement: IAuthenticationManagement
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IDataManagement _datamanagement;
        private readonly ICommonLogic _commonLogic;
        private readonly IConfiguration _config;
        public AuthenticationManagement(DataBaseContext dataBaseContext,IDataManagement datamanagement, ICommonLogic commonLogic, IConfiguration config)
        {
            _datamanagement = datamanagement;
            _commonLogic = commonLogic;
            _config = config;
            _dataBaseContext = dataBaseContext;
        }
        public async Task<AuthenticationResponseDto> AuthenticateReuest(AuthenticationRequestDto request)
        {
            try
            {
                var response = new AuthenticationResponseDto();
                var credentails = _dataBaseContext.Channel.Where(c => c.ChannelKey == request.Apikey).FirstOrDefault();
                if (credentails != null)
                {
                    var inputstring = credentails.ChannelKey + credentails.ChannelSecretKey + request.RequestTimestamp;
                    if (_commonLogic.IsValidCredentails(request.Signature, inputstring))
                    {
                        response = GenerateToken();
                    }
                    else
                    {
                        response.token_type = "";
                        response.access_token = "";
                        response.expires_in = "";

                    }
                }
                else
                {
                    response.token_type = "";
                    response.access_token = "";
                    response.expires_in = "";
                }
                return response;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private AuthenticationResponseDto GenerateToken()
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var Expiry = DateTime.UtcNow.AddMinutes(5);
                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, "user"),
            new Claim("fullName", "PaymentsAPI Service"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Aud, _config["JWTSettings:Audience"]),
            new Claim(JwtRegisteredClaimNames.Iss, _config["JWTSettings:Issuer"])
                };

                var token = new JwtSecurityToken(
                    issuer: _config["Issuer"],
                    audience: _config["Audience"],
                    claims: claims,
                    expires: Expiry,
                    signingCredentials: credentials
                    );
                return new AuthenticationResponseDto()
                {
                    access_token = new JwtSecurityTokenHandler().WriteToken(token),
                    expires_in = Expiry.ToString(),
                    token_type = ""
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
