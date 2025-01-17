using JensenAuktion.Repository.Entities;
using JensenAuktion.Repository.Interfaces;
using JensenAuktion.Repository.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JensenAuktion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidRepo _bidRepo;

        public BidController(IBidRepo bidRepo)
        {
            _bidRepo = bidRepo;
        }

        // POST: api/Bid
        [HttpPost]
        public IActionResult CreateBid([FromBody] Bid bid)
        {
            if (bid == null)
            {
                return BadRequest("Bid data is invalid.");
            }

            try
            {
                int newBidID = _bidRepo.CreateBid(bid);
                return Ok(new { BidID = newBidID, Message = "Bid created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteBid(int id)
        {
            try
            {
                bool isDeleted = _bidRepo.DeleteBid(id);
                if (isDeleted)
                {
                    return Ok(new { Message = "Bid deleted successfully." });
                }
                else
                {
                    return NotFound($"Bid with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
