using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FurnitureStore.db;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace FurnitureStore.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FurnitureStoreContext _context;

        public UsersController(FurnitureStoreContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
       [Authorize(Roles ="Администратор")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
             return await _context.Users.Include(r=>r.IdRoleNavigation).Include(d=>d.IdDepartmentNavigation).Include(p=>p.IdPositionNavigation).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
     
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }



        //search with email 
        [HttpGet("email")]
      [Authorize(Roles =   "Администратор")]

        public async Task<ActionResult<IEnumerable<User>>> GetUsers(string email)
        {
            return await _context.Users.Where(u=>u.Login.ToLower().Contains(email.ToLower())).Include(r => r.IdRoleNavigation).Include(d => d.IdDepartmentNavigation).Include(p => p.IdPositionNavigation).ToListAsync();
        }


        //authorization 
        [HttpGet("auth")]
        public async Task<ActionResult<User>> GetUser(string email, string password)
        {
              var user = await _context.Users.Include(p=>p.IdRoleNavigation).FirstOrDefaultAsync(p => p.Login==email && p.Password==password);
          //  var user = await _context.Users.FindAsync(email);
            if (user == null)
            {
                return Unauthorized();
            }

            return user;
        } 
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Администратор, Продавец") ]

        public async Task<IActionResult> PutUser(int id, User user)
        {user.IdUser = id;
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
       [Authorize(Roles = "Администратор")]

        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.IdUser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        } 


        [HttpPost("/token")]
        public IActionResult Token(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return new UnauthorizedResult();
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var userId = identity.FindFirst("IdUser")?.Value; // Получаем идентификатор пользователя из Claims
            var response = new
            {
                access_token = encodedJwt,
            
                username = identity.Name,
                 role = identity.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value,
                IdUser = userId // Добавляем идентификатор пользователя в ответ
            };

            return new JsonResult(response);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User user = _context.Users.Include(r => r.IdRoleNavigation).FirstOrDefault(x => x.Login == username && x.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                     
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.IdRoleNavigation.RoleName),
                    new Claim("IdUser", user.IdUser.ToString()) // Добавляем Claim с идентификатором пользователя
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.IdUser == id);
        }
    }
}
