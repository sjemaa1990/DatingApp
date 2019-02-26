using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SGS.eCalc.DTO;
using SGS.eCalc.Models;
using SGS.eCalc.Repository;

namespace SGS.eCalc.Controllers
{
[Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userForRegister){
            

            userForRegister.UserName = userForRegister.UserName.ToLower();
            if(await _authRepository.UserExists(userForRegister.UserName))
            return BadRequest("User already exist");
            
            var userToCreate = new User(){
                UserName = userForRegister.UserName
            };
            var createdUser = await _authRepository.Register(userToCreate, userForRegister.Password );

            return StatusCode(201);
        }
         [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO){
            try{
                  var userFromRepository = await _authRepository.Login(userLoginDTO.UserName.ToLower(),userLoginDTO.Password);
                if(userFromRepository == null)
                    return Unauthorized();
                
                var claims = new []{
                    new Claim(ClaimTypes.NameIdentifier, userFromRepository.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepository.UserName)
                    
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                
                var tokenDescriptor = new SecurityTokenDescriptor{

                    Subject             = new ClaimsIdentity(claims),
                    Expires             = DateTime.Now.AddDays(1),
                    SigningCredentials  = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok (new {
                    Token = tokenHandler.WriteToken(token)
                });
            }
            catch{
                throw new Exception("Not possibale to login");
            }
          
        }
    }
}