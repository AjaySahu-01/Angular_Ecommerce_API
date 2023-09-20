using Angular_Ecommerce.Context;
using Angular_Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angular_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
      
            private readonly AppDbContext _context;
            public UsersController(AppDbContext userDbContext)
            {
                _context = userDbContext;
            }
            [HttpPost("authenticate")]
            public async Task<IActionResult> Authenticate([FromBody] User user)

            {
                if (user == null)
                    return BadRequest();

                var user_data = await _context.users.FirstOrDefaultAsync(x => x.UserName == user.UserName && x.Password == user.Password);
                if (user_data == null)
                {
                    return NotFound(new { Message = "User Not found!" });
                }
                return Ok(new { Message = "Login Success!" });


            }
            [HttpPost("register")]
            public async Task<IActionResult> RegisterUser([FromBody] User user)
            {
                if (user == null)
                    return BadRequest();
                await _context.users.AddAsync(user);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "User Register!" });
            }

        }
    }
