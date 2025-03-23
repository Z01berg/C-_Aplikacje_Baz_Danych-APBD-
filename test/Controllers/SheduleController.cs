using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ScheduleController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AddSchedule([FromBody] Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSchedule), new { id = schedule.IdSchedule }, schedule);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(schedule);
        }
    }
}