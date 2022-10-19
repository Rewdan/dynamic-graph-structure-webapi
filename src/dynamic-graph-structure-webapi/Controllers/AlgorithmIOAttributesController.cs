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
    public class AlgorithmIOAttributesController : ControllerBase
    {
        private readonly DGSContextDb _context;

        public AlgorithmIOAttributesController(DGSContextDb context)
        {
            _context = context;
        }

        // GET: api/AlgorithmIOAttributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlgorithmIOAttribute>>> GetAlgorithmIOAttributes()
        {
            return await _context.AlgorithmIOAttributes.ToListAsync();
        }

        // GET: api/AlgorithmIOAttributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlgorithmIOAttribute>> GetAlgorithmIOAttribute(int id)
        {
            var algorithmIOAttribute = await _context.AlgorithmIOAttributes.FindAsync(id);

            if (algorithmIOAttribute == null)
            {
                return NotFound();
            }

            return algorithmIOAttribute;
        }

        // PUT: api/AlgorithmIOAttributes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlgorithmIOAttribute(int id, AlgorithmIOAttribute algorithmIOAttribute)
        {
            if (id != algorithmIOAttribute.Id)
            {
                return BadRequest();
            }

            _context.Entry(algorithmIOAttribute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlgorithmIOAttributeExists(id))
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

        // POST: api/AlgorithmIOAttributes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlgorithmIOAttribute>> PostAlgorithmIOAttribute(AlgorithmIOAttribute algorithmIOAttribute)
        {
            _context.AlgorithmIOAttributes.Add(algorithmIOAttribute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlgorithmIOAttribute", new { id = algorithmIOAttribute.Id }, algorithmIOAttribute);
        }

        // DELETE: api/AlgorithmIOAttributes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlgorithmIOAttribute(int id)
        {
            var algorithmIOAttribute = await _context.AlgorithmIOAttributes.FindAsync(id);
            if (algorithmIOAttribute == null)
            {
                return NotFound();
            }

            _context.AlgorithmIOAttributes.Remove(algorithmIOAttribute);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlgorithmIOAttributeExists(int id)
        {
            return _context.AlgorithmIOAttributes.Any(e => e.Id == id);
        }
    }
}
