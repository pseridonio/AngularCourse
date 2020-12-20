using DatingAppAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingAppAPI.Controllers
{
    [ApiController, Authorize, Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public UsersController(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entities.User>>> GetUsers()
        {
            IEnumerable<Entities.User> users = await _dbContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entities.User>> GetUser(int id)
        {
            Entities.User user = await _dbContext.Users.FindAsync(id);
            return Ok(user);
        }
    }
}
