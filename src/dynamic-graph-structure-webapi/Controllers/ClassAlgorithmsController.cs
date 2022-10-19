using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DynamicGraphStructure.WebApi.Database;
using DynamicGraphStructure.WebApi.Database.Models;

namespace DynamicGraphStructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassAlgorithmsController : ControllerBase
    {
        private readonly DGSContextDb _context;

        public ClassAlgorithmsController(DGSContextDb context)
        {
            _context = context;
        }

        // GET: api/ClassAlgorithms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassAlgorithm>>> GetClassAlgorithm()
        {
            return await _context.ClassAlgorithm.ToListAsync();
        }

        // GET: api/ClassAlgorithms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassAlgorithm>> GetClassAlgorithm(int id)
        {
            var classAlgorithm = await _context.ClassAlgorithm.FindAsync(id);

            if (classAlgorithm == null)
            {
                return NotFound();
            }

            return classAlgorithm;
        }

        // PUT: api/ClassAlgorithms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassAlgorithm(int id, ClassAlgorithm classAlgorithm)
        {
            if (id != classAlgorithm.Id)
            {
                return BadRequest();
            }

            _context.Entry(classAlgorithm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassAlgorithmExists(id))
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

        // POST: api/ClassAlgorithms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassAlgorithm>> PostClassAlgorithm(ClassAlgorithm classAlgorithm)
        {
            _context.ClassAlgorithm.Add(classAlgorithm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassAlgorithm", new { id = classAlgorithm.Id }, classAlgorithm);
        }

        // DELETE: api/ClassAlgorithms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassAlgorithm(int id)
        {
            var classAlgorithm = await _context.ClassAlgorithm.FindAsync(id);
            if (classAlgorithm == null)
            {
                return NotFound();
            }

            _context.ClassAlgorithm.Remove(classAlgorithm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassAlgorithmExists(int id)
        {
            return _context.ClassAlgorithm.Any(e => e.Id == id);
        }
    }
}
