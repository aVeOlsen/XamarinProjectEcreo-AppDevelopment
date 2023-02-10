using EcreoLibraryStandard;
using EcreoUserAPI.Repositories.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcreoUserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AbsenceController : ControllerBase
    {
        private readonly IRepository<User> _uRepo;
        private readonly IRepository<AbsenceStatus> _absenceRepo;

        public AbsenceController(IRepository<User> urepo, IRepository<AbsenceStatus> arepo)
        {
            _uRepo = urepo;
            _absenceRepo = arepo;
        }
        [HttpGet("GetAll/{id}")]
        public async Task<IActionResult> GetAll(string id)
        {
            IReadOnlyCollection<AbsenceStatus> userAbsence;
            userAbsence = await _absenceRepo.Where(a => a.User.BaseID == id);
            return Ok(userAbsence);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var user = await _uRepo.Get(id);
            if (user == null)
            {
                return BadRequest();
            }
            //user.CurrentAbsenceStatus = _absenceRepo.Where(x => )
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AbsenceStatus status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(status);
            }
            if (ModelState.IsValid)
            {
                await _absenceRepo.Add(status);
            }
            return Ok(status);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, AbsenceStatus status)
        {
            if(id == null)
                return BadRequest();

            if(id != status.BaseID)
                return BadRequest(status);

            if (ModelState.IsValid)
            {
                await _absenceRepo.Update(status);
            }

            return Ok(status);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            AbsenceStatus status = await _absenceRepo.Get(id);
            await _absenceRepo.Delete(status);
            return Ok();
        }
    }
}
