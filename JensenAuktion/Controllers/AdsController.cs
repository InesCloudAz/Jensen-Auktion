using JensenAuktion.Repository.Entities;
using JensenAuktion.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JensenAuktion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly IAdRepository _adRepository;
        public AdsController(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        [HttpPost]
        //[Authorize]
        public IActionResult CreateAd(Ad ad)
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

        [HttpPut]
        //[Authorize]
        public IActionResult UpdateAd(Ad ad)
        {
            try
            {
                _adRepository.UpdateAd(ad);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(ad);
        }


        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult DeleteAd(int id)
        {
            try
            {
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
