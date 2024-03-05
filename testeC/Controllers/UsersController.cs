using Microsoft.AspNetCore.Mvc;
using TestC.Models; // Corrigido para corresponder ao namespace do modelo User
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestC.Controllers // Corrigido para corresponder ao namespace correto
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Nome = "Joao", Email = "joao@mail.com", DataCadastro = new DateTime(2020,1,1)}
        }; // Corrigido, adicionado ponto e vírgula

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return users;
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            if (users.Any(u => u.Email == user.Email)) // Corrigido, 'u' em vez de 'uint'
            {
                return BadRequest("Usuario com este e-mail ja existe."); // Corrigido, adicionado ponto e vírgula
            }

            user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            users.Remove(user);
            return NoContent();
        }
    }
}
