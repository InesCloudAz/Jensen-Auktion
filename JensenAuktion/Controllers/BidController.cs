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
            var userIdFromToken = User.FindFirstValue(ClaimTypes.Sid);
            var userId = _adsService.CheckAdUserId(bid.AdID).ToString();
            try
            {
                if (_adsService.IsAdClosed(bid.AdID))
                {
                    return BadRequest("You can't place a bid on a closed Ad");
                }
                else if (userId == userIdFromToken)
                {
                    return BadRequest("You can't place a bid on your own ad");
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
                if (_adsService.IsAdClosedByBid(id))
                {
                    return BadRequest("You can't remove a bid on a closed Ad");
                }

                bool isDeleted = _bidRepo.DeleteBid(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            return Ok(id + " Is deleted");
        }
    }
}
