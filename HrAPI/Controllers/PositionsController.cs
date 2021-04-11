using HR.Models;
using HR.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionsService _positionsService;

        public PositionsController(IPositionsService positionsService)
        {
            _positionsService = positionsService;
        }

        // GET: api/Positions
        [HttpGet]
        public IEnumerable<Positions> GetPositions()
        {
            return  _positionsService.GetAllPositions();
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        public ActionResult<Positions> GetPositions(int id)
        {
            return _positionsService.GetPosition(id);
        }

        // PUT: api/Positions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutPositions(int id, Positions positions)
        {
            try
            {
                _positionsService.UpdatePosition(id, positions);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }

            return NoContent();
        }

        // POST: api/Positions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Positions> PostPositions(Positions positions)
        {
           _positionsService.AddPosition(positions);

            return CreatedAtAction("GetPositions", new { id = positions.Id }, positions);
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        public ActionResult<Positions> DeletePositions(int id)
        {
            try
            {
                _positionsService.DeletePosition(id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }

            return NoContent();
        }
    }
}
