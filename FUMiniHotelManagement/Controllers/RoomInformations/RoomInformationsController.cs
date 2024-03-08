using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Query;

namespace Ass1.Controllers.RoomInformations
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoomInformationsController : ControllerBase
    {
        private IRoomInformationService _service;

        public RoomInformationsController(IRoomInformationService services)
        {
            _service = services;
        }

        // GET: api/RoomInformations
        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpGet]
        public ActionResult<IEnumerable<RoomInformation>> GetRoomInformations()
        {
            if (_service.GetRooms() == null)
            {
                return NotFound();
            }
            return Ok(_service.GetRooms().ToList());
        }

        // GET: api/RoomInformations/5
        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpGet("{id}")]
        public ActionResult<RoomInformation> GetRoomInformation(int id)
        {
            if (_service.GetRooms() == null)
            {
                return NotFound();
            }
            var roomInformation = _service.GetRoomById(id);

            if (roomInformation == null)
            {
                return NotFound();
            }

            return Ok(roomInformation);
        }

        // PUT: api/RoomInformations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Staff")]
        [HttpPut("{id}")]
        public IActionResult PutRoomInformation(int id, RoomInformation roomInformation)
        {
            if (id != roomInformation.RoomId)
            {
                return BadRequest();
            }


            try
            {
                _service.UpdateRoom(roomInformation);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (_service.GetRoomById(roomInformation.RoomId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/RoomInformations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Staff")]
        [HttpPost]
        public ActionResult<RoomInformation> PostRoomInformation(RoomInformation roomInformation)
        {
            if (_service.GetRooms() == null)
            {
                return Problem("Entity set 'FUMiniHotelManagementContext.RoomInformations'  is null.");
            }
            _service.AddNew(roomInformation);


            return Ok("Room " + roomInformation.RoomId + " was created!");
        }

        // DELETE: api/RoomInformations/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteRoomInformation(int id)
        {
            if (_service.GetRooms() == null)
            {
                return NotFound();
            }
            var roomInformation = _service.GetRoomById(id);
            if (roomInformation == null)
            {
                return NotFound();
            }

            _service.Delete(roomInformation);

            return NoContent();
        }


    }
}
