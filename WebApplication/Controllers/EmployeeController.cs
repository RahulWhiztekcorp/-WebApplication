using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeTbl>>> GetEmployeeTbls()
        {
          if (_context.EmployeeTbls == null)
          {
              return NotFound();
          }
            return await _context.EmployeeTbls.ToListAsync();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTbl>> GetEmployeeTbl(int id)
        {
          if (_context.EmployeeTbls == null)
          {
              return NotFound();
          }
            var employeeTbl = await _context.EmployeeTbls.FindAsync(id);

            if (employeeTbl == null)
            {
                return NotFound();
            }

            return employeeTbl;
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeTbl(int id, EmployeeTbl employeeTbl)
        {
            if (id != employeeTbl.Eid)
            {
                return BadRequest();
            }

            _context.Entry(employeeTbl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeTblExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content("Employee is Updated");
        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeTbl>> PostEmployeeTbl(EmployeeTbl employeeTbl)
        {
          if (_context.EmployeeTbls == null)
          {
              return Problem("Entity set 'EmployeeDbContext.EmployeeTbls'  is null.");
          }
            _context.EmployeeTbls.Add(employeeTbl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeTbl", new { id = employeeTbl.Eid }, employeeTbl);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeTbl(int id)
        {
            if (_context.EmployeeTbls == null)
            {
                return NotFound();
            }
            var employeeTbl = await _context.EmployeeTbls.FindAsync(id);
            if (employeeTbl == null)
            {
                return NotFound();
            }

            _context.EmployeeTbls.Remove(employeeTbl);
            await _context.SaveChangesAsync();

            return Content("Employee is Deleted");
        }

        private bool EmployeeTblExists(int id)
        {
            return (_context.EmployeeTbls?.Any(e => e.Eid == id)).GetValueOrDefault();
        }
    }
}
