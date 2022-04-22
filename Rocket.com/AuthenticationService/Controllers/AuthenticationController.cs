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
        [HttpPost]
        public string HashPassword(string password)
        {
            HashWorker worker = new HashWorker();
            string hashedPassword = worker.HashString(password);
            return hashedPassword;
        }
        [HttpGet("user/{userId")]
        public async Task<IActionResult> GetUserByUserId(int UserId)
        {
            var requestedUser = await _dbContext.Users.Where(x => x.Id == UserId).FirstOrDefaultAsync();
            User user = new User(requestedUser.Username, requestedUser.Email, requestedUser.Password, requestedUser.CreationDate, requestedUser.LastLogin);
            return Ok(new ApiResponse<User>(true, user));
        }
    }
}
