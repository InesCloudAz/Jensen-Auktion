using JensenAuktion.Repository.DTO;
using JensenAuktion.Repository.Entities;
using JensenAuktion.Repository.Interfaces;
using JensenAuktion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JensenAuktion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly IAdRepository _adRepository;
        private readonly IAdsService _adsService;
        public AdsController(IAdRepository adRepository, IAdsService adsService)
        {
            _adRepository = adRepository;
            _adsService = adsService;
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

        [HttpPost]
        [Authorize]
        public IActionResult CreateAd(AdsCreateDTO ad)
        {
            try
            {
                _adRepository.CreateAd(ad);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(ad);
        }

        [HttpGet("{id}")]
        public IActionResult GetAdById(int id)
        {
            try
            {
                if (_adsService.IsAdClosed(id))
                {

                    return Ok(_adRepository.GetClosedAdById(id));

                }
                else
                {
                    return Ok(_adRepository.GetAdById(id));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateAd(Ad ad)
        {
            try
            {
                if (_adsService.CheckIfAdHasBids(ad.AdID))
                {
                    _adRepository.UpdateAdWithBid(ad);
                    return Ok("Updated without price due to bids " + ad);
                }

                _adRepository.UpdateAd(ad);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(ad);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteAd(int id)
        {
            try
            {
                if (_adsService.CheckIfAdHasBids(id))
                {
                    return BadRequest(new { message = "Cannot delete ad with existing bids" });
                }

                _adRepository.DeleteAd(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok($"Ad {id} deleted");
        }
    }
}
