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
    public class AttributeTypesController : ControllerBase
    {
        private readonly DGSContextDb _context;

        public AttributeTypesController(DGSContextDb context)
        {
            _context = context;
        }

        // GET: api/AttributeTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeType>>> GetAttributeTypes()
        {
            return await _context.AttributeTypes.ToListAsync();
        }

        // GET: api/AttributeTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeType>> GetAttributeType(int id)
        {
            var attributeType = await _context.AttributeTypes.FindAsync(id);

            if (attributeType == null)
            {
                return NotFound();
            }

            return attributeType;
        }

        // PUT: api/AttributeTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttributeType(int id, AttributeType attributeType)
        {
            if (id != attributeType.Id)
            {
                return BadRequest();
            }

            _context.Entry(attributeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttributeTypeExists(id))
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

        // POST: api/AttributeTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AttributeType>> PostAttributeType(AttributeType attributeType)
        {
            _context.AttributeTypes.Add(attributeType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttributeType", new { id = attributeType.Id }, attributeType);
        }

        // DELETE: api/AttributeTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttributeType(int id)
        {
            var attributeType = await _context.AttributeTypes.FindAsync(id);
            if (attributeType == null)
            {
                return NotFound();
            }

            _context.AttributeTypes.Remove(attributeType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttributeTypeExists(int id)
        {
            return _context.AttributeTypes.Any(e => e.Id == id);
        }
    }
}
