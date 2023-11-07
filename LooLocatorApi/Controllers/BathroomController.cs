using LooLocatorApi.Models;
using LooLocatorApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LooLocatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BathroomController : ControllerBase
{
    // private readonly DataContext _context;
    private readonly IBathroomService _service;

    public BathroomController(IBathroomService bathroomService)
    {
        // _context = context;
        _service = bathroomService;
    }

    // GET: api/Bathroom
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BathroomDto>>> GetBathrooms()
    {
        var bathrooms = await _service.GetBathroomsAsync();
        if (!bathrooms.Any())
            return Ok();
        return Ok(bathrooms);
    }

    //
    // // GET: api/Bathroom/withinRadius?coordinates=POINT(-122.335167 47.608013)&radiusInMeters=1000
    // [HttpGet("withinRadius")]
    // public async Task<ActionResult<IEnumerable<Bathroom>>>
    //     GetBathroomsWithinRadius(
    //         [FromQuery] string coordinates,
    //         [FromQuery] double radiusInMeters
    //     )
    // {
    //     // if (_context.Bathrooms == null) return NotFound();
    //     // var point = _context.Bathrooms
    //     //     .FromSqlRaw($"SELECT ST_GeomFromText('{coordinates}')")
    //     //     .FirstOrDefault();
    //     // if (point == null) return NotFound();
    //     // return await _context.Bathrooms
    //     //     .FromSqlRaw(
    //     //         $"SELECT * FROM \"Bathrooms\" WHERE ST_DWithin(\"Coordinates\", '{coordinates}', {radiusInMeters})"
    //     //     )
    //     //     .ToListAsync();
    //     throw new NotImplementedException();
    // }
    //
    // // GET: api/Bathroom/5
    // [HttpGet("{id}")]
    // public async Task<ActionResult<Bathroom>> GetBathroom(Guid id)
    // {
    //     // if (_context.Bathrooms == null) return NotFound();
    //     // var bathroom = await _context.Bathrooms.FindAsync(id);
    //     //
    //     // if (bathroom == null) return NotFound();
    //     //
    //     // return bathroom;
    //     throw new NotImplementedException();
    // }
    //
    // // PUT: api/Bathroom/5
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // [HttpPut("{id}")]
    // public async Task<IActionResult> PutBathroom(Guid id, Bathroom bathroom)
    // {
    //     // if (id != bathroom.Id) return BadRequest();
    //     //
    //     // _context.Entry(bathroom).State = EntityState.Modified;
    //     //
    //     // try
    //     // {
    //     //     await _context.SaveChangesAsync();
    //     // }
    //     // catch (DbUpdateConcurrencyException)
    //     // {
    //     //     if (!BathroomExists(id))
    //     //         return NotFound();
    //     //     throw;
    //     // }
    //     //
    //     // return NoContent();
    //     throw new NotImplementedException();
    // }
    //
    // POST: api/Bathroom
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Bathroom>> PostBathroom(Bathroom bathroom)
    {
        // if (_context.Bathrooms == null)
        //     return Problem("Entity set 'DataContext.Bathrooms'  is null.");
        // _context.Bathrooms.Add(bathroom);
        // await _context.SaveChangesAsync();
        //
        // return CreatedAtAction("GetBathroom", new { id = bathroom.Id },
        //     bathroom);
        throw new NotImplementedException();
    }
    //
    // // DELETE: api/Bathroom/5
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteBathroom(Guid id)
    // {
    //     // if (_context.Bathrooms == null) return NotFound();
    //     // var bathroom = await _context.Bathrooms.FindAsync(id);
    //     // if (bathroom == null) return NotFound();
    //     //
    //     // _context.Bathrooms.Remove(bathroom);
    //     // await _context.SaveChangesAsync();
    //     //
    //     // return NoContent();
    //     throw new NotImplementedException();
    // }
    //
    // private bool BathroomExists(Guid id)
    // {
    //     throw new NotImplementedException();
    //
    //     // return (_context.Bathrooms?.Any(e => e.Id == id)).GetValueOrDefault();
    // }
}