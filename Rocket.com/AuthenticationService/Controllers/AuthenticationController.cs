using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Workers;
using AuthenticationService.Contexts;
using AuthenticationService.Models;
using AuthenticationService.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public AuthenticationController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("user/HashPassword/{password}")]
        public string HashPassword(string password)
        {
            HashWorker worker = new HashWorker();
            string hashedPassword = worker.HashString(password);
            return hashedPassword;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserByUserId(int UserId)
        {
            var requestedUser = await _dbContext.Users.Where(x => x.Id == UserId).FirstOrDefaultAsync();
            if(requestedUser == null)
            {
                return BadRequest(new ApiResponse<string>(false, "The user was not found."));
            }
            return Ok(new ApiResponse<User>(true, requestedUser));
        }

        [HttpPost("user/Register")]
        public async Task<IActionResult> RegisterUser(User user)
        {
            if (user == default)
            {
                return BadRequest(new ApiResponse<string>(false, "Object cannot be null!"));
            }
            if (string.IsNullOrEmpty(user.Username))
            {
                return BadRequest(new ApiResponse<string>(false, "Username cannot be empty!"));
            }

            HashWorker worker = new HashWorker();
            string hashedPassword = worker.HashString(user.Password);
            user.Password = hashedPassword;

            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return Ok(new ApiResponse<User>(true, user));
        }
    }
}
