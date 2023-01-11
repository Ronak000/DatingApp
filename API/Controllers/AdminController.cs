using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        
        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        
        [HttpGet("users-with-roles")]
        [Authorize(Policy ="RequireAdminRole")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
                .OrderBy(o => o.UserName)
                .Select(u => new
                {
                    u.Id, // return user-id
                    Username = u.UserName,  // return user's name
                    Roles = u.UserRoles.Select( r => r.Role.Name).ToList() // return rollname
                })
                .ToListAsync();

            return Ok(users);
        }

        [Authorize(Policy ="RequireAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username,[FromQuery]string roles)
        {
            if(string.IsNullOrEmpty(roles)) return BadRequest("you must select at least one role");

            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);

            if(user==null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if(!result.Succeeded) return BadRequest("Failed to add roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if(!result.Succeeded) return BadRequest("Failed to remove from role");

            return Ok(await _userManager.GetRolesAsync(user));

        }

        
        [HttpGet("photos-to-moderate")]
        [Authorize(Policy ="ModeratePhotoRole")]
        public ActionResult GetPhotosForModeration()
        {
            return Ok("admin and moderator can see this");
        }
    }
}