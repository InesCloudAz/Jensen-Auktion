using JensenAuktion.Repository.DTO;
using JensenAuktion.Repository.Entities;
using JensenAuktion.Repository.Interfaces;
using System.Security.Cryptography;

namespace JensenAuktion.Services
{
    public class AdsService : IAdsService
    {
        private readonly IAdRepository _adRepository;
        private readonly IBidRepo _bidRepo;

        public AdsService(IAdRepository adRepository, IBidRepo bidRepo)
        {
            _adRepository = adRepository;
            _bidRepo = bidRepo;
        }

        public bool CheckIfAdHasBids(int adId)
        {
            AdsDTO ad = _adRepository.GetAdById(adId);

            if (ad.Bid == null)
            {
                return true;
            }

            return false;
        }

        public bool IsAdClosed(int adId)
        {
            var ad = _adRepository.GetAdById(adId);
            return ad.EndTime < DateTime.Now;
        }

        public bool IsAdClosedByBid(int id)
        {
            var bid = _bidRepo.GetBidById(id);
            var ad = _adRepository.GetAdById(bid.AdID);

            return ad.EndTime < DateTime.Now;
        }

        public int CheckAdUserId(int id)
        {
              var ad = _adRepository.GetAdById(id);

            return ad.UserId;
        }
    }
}
