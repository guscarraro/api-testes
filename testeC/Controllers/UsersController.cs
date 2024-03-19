using Microsoft.AspNetCore.Mvc;
using TestC.Models;
using TestC.Data; // Adicione este using para acessar ApplicationDbContext
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TestC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return _context.Users.ToList();
        }

        // POST: api/Users/register
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return BadRequest("Usuário com este e-mail já existe.");
            }

            user.Password = _passwordHasher.HashPassword(user, user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // POST: api/Users/login
        [HttpPost("login")]
        public ActionResult Login([FromBody] User loginRequest)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginRequest.Email);

            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.Password, loginRequest.Password) != PasswordVerificationResult.Success)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            // Aqui, insira a lógica para gerar e retornar um token JWT

            return Ok(new { Message = "Login bem-sucedido.", Token = "token_aqui" });
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
    }
}
