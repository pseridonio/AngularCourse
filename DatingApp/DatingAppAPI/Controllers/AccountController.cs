using DatingAppAPI.Data;
using DatingAppAPI.DTOs;
using DatingAppAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ITokenService _tokenService;

        public AccountController(DatabaseContext databaseContext, ITokenService tokenService)
        {
            this._databaseContext = databaseContext;
            this._tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<DTOs.UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserAlreadExists(registerDTO.Name))
                return BadRequest("User alread exists");

            using HMACSHA512 hmac = new HMACSHA512();

            Entities.User user = new Entities.User()
            {
                Name = registerDTO.Name,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };

            _databaseContext.Users.Add(user);
            await _databaseContext.SaveChangesAsync();

            UserDTO resultUser = new UserDTO() { Name = user.Name, Token = _tokenService.CreateToken(user) };
            return Ok(resultUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<DTOs.UserDTO>> Login (LoginDTO loginDTO)
        {
            Entities.User user = await _databaseContext.Users.Where(user => user.Name.ToLower() == loginDTO.Name.ToLower()).SingleOrDefaultAsync();

            if (user == null)
                return Unauthorized("User not found");

            using HMACSHA512 hmac = new HMACSHA512(user.PasswordSalt);

            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            if (!passwordHash.SequenceEqual(user.PasswordHash))
                return Unauthorized("Invalid password");

            UserDTO resultUser = new UserDTO() { Name = user.Name, Token = _tokenService.CreateToken(user) };
            return Ok(resultUser);
        }

        private async Task<bool> UserAlreadExists(string name)
        {
            return await _databaseContext.Users.AnyAsync(user => user.Name.ToLower() == name.ToLower());
        }



    }
}
