using EcreoLibraryStandard;
using EcreoUserAPI.Repositories.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcreoUserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User>  _userRepo;

        public UserController(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IReadOnlyCollection<User> users = await _userRepo.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            if (id == null)
                return BadRequest();

            User userToFind = await _userRepo.Get(id);

            if (userToFind == null)
                return BadRequest();

            return Ok(userToFind);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepo.Add(user);
                return Ok(user);
            }
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, User user)
        {
            if (id == null)
                return BadRequest();

            if (id != user.BaseID)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _userRepo.Update(user);
            }

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userRepo.Get(id);
            await _userRepo.Delete(user);
            return Ok();
        }
    }
}
