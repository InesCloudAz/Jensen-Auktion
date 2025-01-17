using JensenAuktion.Interfaces;
using JensenAuktion.Repository.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace JensenAuktion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly IJensenAuctionContext _adRepository;
        public AdsController(IJensenAuctionContext adRepository)
        {
            _adRepository = adRepository;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateAd( Ad newAd)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);
                newAd.OwnerID = userId;

                _adRepository.AddAd(newAd);
                _adRepository.SaveChanges();

                return CreatedAtAction(nameof(CreateAd), new { id = newAd.AdID }, newAd);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllAds()
        {
            try
            {
                var ads = _adRepository.GetAllAds();
                return Ok(ads);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAdByID(int id)
        {
            var ad = _adRepository.GetAdById(id);

            if (ad == null)
            {
                return NotFound(new { message = "Ad not found" });
            }

            return Ok(ad);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult PutAd(int id,Ad updatedAd)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);
                var ad = _adRepository.GetAdById(id) as Ad;

                if (ad == null)
                    return NotFound(new { message = "Ad not found" });

                if (ad.OwnerID != userId)
                {
                    return Forbid("You can only update your own ads");
                }

                // Update allowed fields
                ad.Title = updatedAd.Title;
                ad.Description = updatedAd.Description;
                ad.Price = updatedAd.Price;
                ad.StartDate = updatedAd.StartDate;
                ad.EndDate = updatedAd.EndDate;

                _adRepository.UpdateAd(ad);

                return Ok(new { message = "Ad updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteAd(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);
                var ad = _adRepository.GetAdById(id) as Ad;

                if (ad == null)
                    return NotFound(new { message = "Ad not found" });

                if (ad.OwnerID != userId)
                {
                    return Forbid("You can only delete your own ads");
                }

                _adRepository.DeleteAd(ad);

                return Ok(new { message = "Ad deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
