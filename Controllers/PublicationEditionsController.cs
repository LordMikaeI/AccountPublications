using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kursach_Backend.DbContexts;
using Kursach_Backend.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Kursach_Backend.Models.Employees;

namespace Kursach_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PublicationEditionsController : ControllerBase
    {
        private ApplicationContext _context;

        public PublicationEditionsController()
        {
            _context = new ApplicationContext();
        }

        [HttpGet]
        [Authorize(Roles = "admin, manager")]
        public IActionResult GetPublicationEditions()
        {
            var editions = _context.PublicationEditions.Include(pe => pe.Publications)
                .Select(pe => new
                {
                    pe.Id,pe.EditionName,pe.Description,Publications = pe.Publications
                    .Select(p => new
                    {
                        p.Id,p.Title,p.Description,p.PublicationDate
                    })
                })
                .ToList();

            return Ok(editions);
        }

        [HttpPost("edition")]
        [Authorize(Roles = "admin")]
        public IActionResult AddPublicationEdition([FromBody] AddPublicationEditionRequestDto addEditionDto)
        {
            var edition = _context.PublicationEditions.Add(new PublicationEdition
            {
                EditionName = addEditionDto.EditionName,
                Description = addEditionDto.Description
            });
            _context.SaveChanges();
            return Ok(edition.Entity?.Id ?? 0);
        }

        [HttpPut("edition")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdatePublicationEdition([FromBody] UpdatePublicationEditionRequestDto updateEditionDto)
        {
            var edition = _context.PublicationEditions.FirstOrDefault(pe => pe.Id == updateEditionDto.Id);
            if (edition != null)
            {
                edition.EditionName = updateEditionDto.EditionName;
                edition.Description = updateEditionDto.Description;
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("Edition not found");
        }

        [HttpDelete("edition")]
        [Authorize(Roles = "admin")]
        public IActionResult DeletePublicationEdition(int id)
        {
            var edition = _context.PublicationEditions.FirstOrDefault(pe => pe.Id == id);
            if (edition != null)
            {
                _context.PublicationEditions.Remove(edition);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("Edition not found");
        }
    }
}
