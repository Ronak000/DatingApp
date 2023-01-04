using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//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
namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

   
    public class UsersController : ControllerBase
    {
        
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;   
        }    

        [HttpGet]
        public async  Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users=await _context.Users.ToListAsync();

            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }


    }

}