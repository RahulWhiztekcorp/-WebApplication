using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public DepartmentController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentTbl>>> GetDepartmentTbls()
        {
          if (_context.DepartmentTbls == null)
          {
              return NotFound();
          }
            return await _context.DepartmentTbls.ToListAsync();
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentTbl>> GetDepartmentTbl(int id)
        {
          if (_context.DepartmentTbls == null)
          {
              return NotFound();
          }
            var departmentTbl = await _context.DepartmentTbls.FindAsync(id);

            if (departmentTbl == null)
            {
                return NotFound();
            }

            return departmentTbl;
        }

        // PUT: api/Department/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartmentTbl(int id, DepartmentTbl departmentTbl)
        {
            if (id != departmentTbl.Did)
            {
                return BadRequest();
            }

            _context.Entry(departmentTbl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentTblExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content("Department is Updated");
        }

        // POST: api/Department
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DepartmentTbl>> PostDepartmentTbl(DepartmentTbl departmentTbl)
        {
          if (_context.DepartmentTbls == null)
          {
              return Problem("Entity set 'EmployeeDbContext.DepartmentTbls'  is null.");
          }
            _context.DepartmentTbls.Add(departmentTbl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartmentTbl", new { id = departmentTbl.Did }, departmentTbl);
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentTbl(int id)
        {
            if (_context.DepartmentTbls == null)
            {
                return NotFound();
            }
            var departmentTbl = await _context.DepartmentTbls.FindAsync(id);
            if (departmentTbl == null)
            {
                return NotFound();
            }

            _context.DepartmentTbls.Remove(departmentTbl);
            await _context.SaveChangesAsync();

            return Content("Department is Deleted");
        }

        private bool DepartmentTblExists(int id)
        {
            return (_context.DepartmentTbls?.Any(e => e.Did == id)).GetValueOrDefault();
        }
    }
}
