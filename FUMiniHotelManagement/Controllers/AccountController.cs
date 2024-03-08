using BusinessObject.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Interface;
using Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FUMiniHotelManagement.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IStaffService _staffService;
        private readonly ICustomerServices _customerServices;
        private readonly IConfiguration _configuration;

        public AccountController(IAdminService adminService, IStaffService staffService, IConfiguration configuration, ICustomerServices customerServices)
        {
            _adminService = adminService;
            _staffService = staffService;
            _configuration = configuration;
            _customerServices = customerServices;
        }

        [HttpPost]
        [Route("/api/v1/auth/login")]
        [AllowAnonymous]
        public IActionResult CheckLogin([FromBody] AccountLoginDTO accountLoginDTO)
        {
            if(accountLoginDTO != null)
            {
                if (!String.IsNullOrEmpty(accountLoginDTO.Email) && !String.IsNullOrEmpty(accountLoginDTO.Password))
                {
                    try
                    {
                        var admin = _adminService.CheckLogin(accountLoginDTO.Email, accountLoginDTO.Password);
                        if (!String.IsNullOrEmpty(admin.Email) && !String.IsNullOrEmpty(admin.Password))
                        {
                            Claim[]? claims = null;
                            claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim("Email", admin.Email),
                        new Claim("Role", "Admin")
                            };

                            //create claims details based on the user information

                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                            var token = new JwtSecurityToken(
                                _configuration["Jwt:Issuer"],
                                _configuration["Jwt:Audience"],
                                claims,
                                expires: DateTime.UtcNow.AddMinutes(180),
                                signingCredentials: signIn);

                            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                        }
                        var staff = _staffService.CheckLogin(accountLoginDTO.Email, accountLoginDTO.Password);
                        if (!String.IsNullOrEmpty(staff.Email) && !String.IsNullOrEmpty(staff.Password))
                        {
                            Claim[]? claims = null;
                            claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Role, "Staff"),
                        new Claim("Email", staff.Email),
                        new Claim("Role", "Staff")
                            };

                            //create claims details based on the user information

                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                            var token = new JwtSecurityToken(
                                _configuration["Jwt:Issuer"],
                                _configuration["Jwt:Audience"],
                                claims,
                                expires: DateTime.UtcNow.AddMinutes(180),
                                signingCredentials: signIn);

                            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                        }
                        var customer = _customerServices.CheckLogin(accountLoginDTO.Email, accountLoginDTO.Password);
                        if (customer != null)
                        {
                            if (!String.IsNullOrEmpty(customer.EmailAddress) && !String.IsNullOrEmpty(customer.Password))
                            {
                                Claim[]? claims = null;
                                claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Role, "Customer"),
                        new Claim("Email", customer.EmailAddress),
                        new Claim("Role", "Customer")
                            };

                                //create claims details based on the user information

                                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                                var token = new JwtSecurityToken(
                                    _configuration["Jwt:Issuer"],
                                    _configuration["Jwt:Audience"],
                                    claims,
                                    expires: DateTime.UtcNow.AddMinutes(180),
                                    signingCredentials: signIn);

                                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                            }
                            return BadRequest("Invalid credentials");
                        }
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
            }
            return BadRequest("The value input should not be empty!");
        }
    }
}
