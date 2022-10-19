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
    public class NodeIOsController : ControllerBase
    {
        private readonly DGSContextDb _context;

        public NodeIOsController(DGSContextDb context)
        {
            _context = context;
        }

        // GET: api/NodeIOs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NodeIO>>> GetNodeIOs()
        {
            return await _context.NodeIOs.ToListAsync();
        }

        // GET: api/NodeIOs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NodeIO>> GetNodeIO(int id)
        {
            var nodeIO = await _context.NodeIOs.FindAsync(id);

            if (nodeIO == null)
            {
                return NotFound();
            }

            return nodeIO;
        }

        // PUT: api/NodeIOs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNodeIO(int id, NodeIO nodeIO)
        {
            if (id != nodeIO.Id)
            {
                return BadRequest();
            }

            _context.Entry(nodeIO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NodeIOExists(id))
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

        // POST: api/NodeIOs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NodeIO>> PostNodeIO(NodeIO nodeIO)
        {
            _context.NodeIOs.Add(nodeIO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNodeIO", new { id = nodeIO.Id }, nodeIO);
        }

        // DELETE: api/NodeIOs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNodeIO(int id)
        {
            var nodeIO = await _context.NodeIOs.FindAsync(id);
            if (nodeIO == null)
            {
                return NotFound();
            }

            _context.NodeIOs.Remove(nodeIO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NodeIOExists(int id)
        {
            return _context.NodeIOs.Any(e => e.Id == id);
        }
    }
}
