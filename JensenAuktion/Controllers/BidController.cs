using JensenAuktion.Repository.Entities;
using JensenAuktion.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JensenAuktion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidRepo _bidRepo;
        private readonly IAdsService _adsService;

        public BidController(IBidRepo bidRepo, IAdsService adsService)
        {
            _bidRepo = bidRepo;
            _adsService = adsService;
        }

        // POST: api/Bid
        [HttpPost]
        public IActionResult CreateBid([FromBody] Bid bid)
        {
            if (bid == null) return BadRequest("Bid data is invalid.");
            try
            {
                if (_adsService.IsAdClosed(bid.AdID))
                {
                    return BadRequest("You can't place a bid on a closed Ad");
                }
                if (bid == null)
                {
                    return BadRequest("Bid data is invalid.");
                }
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
