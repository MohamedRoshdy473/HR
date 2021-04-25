using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR.Data.DTO;
using HR.Data.Models;
using HR.Domain.Services;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionLevelsController : ControllerBase
    {
        private readonly IPositionsLevelService _positionsLevelService;
        public PositionLevelsController(IPositionsLevelService positionsLevelService)
        {
            _positionsLevelService = positionsLevelService;
        }

        // GET: api/PositionLevels
        [HttpGet]
        public IEnumerable<PositionLevel> GetPositionLevels()
        {
            return _positionsLevelService.GetAllPositionsLevels();
        }

        // GET: api/PositionLevels/5
        [HttpGet("{id}")]
        public ActionResult<PositionLevel> GetPositionLevel(int id)
        {
            return _positionsLevelService.GetPositionsLevel(id);
        }

        // PUT: api/PositionLevels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutPositionLevel(int id, PositionLevel positionLevel)
        {
            if (id != positionLevel.Id)
            {
                return BadRequest();
            }

            try
            {
                _positionsLevelService.UpdatePositionsLevel(id, positionLevel);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }

            return NoContent();
        }

        // POST: api/PositionLevels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<PositionLevel> PostPositionLevel(PositionLevel positionLevel)
        {
            _positionsLevelService.AddPositionsLevel(positionLevel);
            return CreatedAtAction("GetPositionLevel", new { id = positionLevel.Id }, positionLevel);
        }

        // DELETE: api/PositionLevels/5
        [HttpDelete("{id}")]
        public ActionResult<PositionLevel> DeletePositionLevel(int id)
        {
            try
            {
                _positionsLevelService.DeletePositionsLevel(id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }

            return NoContent();
        }
    }
}
