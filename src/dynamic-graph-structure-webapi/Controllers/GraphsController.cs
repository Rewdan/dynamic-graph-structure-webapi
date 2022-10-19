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
    public class GraphsController : ControllerBase
    {
        private readonly DGSContextDb _context;

        public GraphsController(DGSContextDb context)
        {
            _context = context;
        }

        // GET: api/Graphs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Graph>>> GetGraphs()
        {
            return await _context.Graphs.ToListAsync();
        }

        // GET: api/Graphs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Graph>> GetGraph(int id)
        {
            var graph = await _context.Graphs.FindAsync(id);

            if (graph == null)
            {
                return NotFound();
            }

            return graph;
        }

        // PUT: api/Graphs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGraph(int id, Graph graph)
        {
            if (id != graph.Id)
            {
                return BadRequest();
            }

            _context.Entry(graph).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GraphExists(id))
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

        // POST: api/Graphs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Graph>> PostGraph(Graph graph)
        {
            _context.Graphs.Add(graph);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGraph", new { id = graph.Id }, graph);
        }

        // DELETE: api/Graphs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGraph(int id)
        {
            var graph = await _context.Graphs.FindAsync(id);
            if (graph == null)
            {
                return NotFound();
            }

            _context.Graphs.Remove(graph);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GraphExists(int id)
        {
            return _context.Graphs.Any(e => e.Id == id);
        }
    }
}
