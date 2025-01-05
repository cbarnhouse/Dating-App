using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class AccountController(DataContext context, ITokenService tokenService, IMapper mapper) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO dto)
        {
            if (await userExists(dto))
            {
                return BadRequest("Username already exists");
            }

            return Ok();

            
            //TODO: Update to use repository
            /*
            try
            if (await userExists(dto))
            {
                if (await isDuplicate(dto))
                {
                    return BadRequest("Username already exists");
                }
                using var hmac = new HMACSHA512();
                var newUser = new AppUser
                {
                    UserName = dto.Username.ToLower(),
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                    PasswordSalt = hmac.Key
                };
                context.Users.Add(newUser);
                await context.SaveChangesAsync();
                var newUserDTO = new UserDTO
                {
                    Username = newUser.UserName,
                    Token = tokenService.CreateToken(newUser)
                };
                return newUserDTO;
                return BadRequest("Username already exists");
            }

            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok();
            */
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO dto)
        {
            //find in db the user that has the same username
            var user = await context.Users.FirstOrDefaultAsync(user => user.UserName.ToLower() == dto.Username.ToLower());

            if (user == null)
            {
                return Unauthorized("User does not exist. Please register.");
            }

            //hash the password from the dto
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var dtoHashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            //compare that to the user's stored hashed password
            for (int i = 0; i < user.PasswordHash.Length; i++)
            {
                if (user.PasswordHash[i] == dtoHashedPassword[i])
                {
                    return new UserDTO
                    { 
                        Username = user.UserName,
                        Token = tokenService.CreateToken(user)
                    };
                }
            }

            return Unauthorized("Incorrect password.");
        }

        private async Task<bool> userExists(RegisterDTO dto)
        {
            return await context.Users.AnyAsync(User => User.UserName.ToLower() == dto.Username.ToLower());
        }
    }
}
