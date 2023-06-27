using Microsoft.Extensions.Configuration;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Logic.Implementations
{
    public class AuthenticationManagement: IAuthenticationManagement
    {
        private IDataManagement _datamanagement;
        private ICommonLogic _commonLogic;
        private readonly IConfiguration _config;
        public AuthenticationManagement(IDataManagement dataBaseContext, ICommonLogic commonLogic, IConfiguration config)
        {
            _datamanagement = dataBaseContext;
            _commonLogic = commonLogic;
            _config = config;
        }
        public async Task<AuthenticationResponseDto> AuthenticateReuest(AuthenticationRequestDto request)
        {
            try
            {
                var response = new AuthenticationResponseDto();
                var credentails = _datamanagement.Channel.Where(c => c.ChannelKey == request.apikey).FirstOrDefault();
                if (credentails != null)
                {
                    var inputstring = credentails.ChannelKey + credentails.ChannelSecretKey + request.requestdatetime;
                    if (_commonLogic.IsValidCredentails(request.signature, inputstring))
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
                return new AuthenticationResponse()
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
