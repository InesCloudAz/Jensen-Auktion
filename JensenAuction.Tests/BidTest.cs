using JensenAuktion.Controllers;
using JensenAuktion.Repository.Entities;
using JensenAuktion.Repository.Interfaces;
using JensenAuktion.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace JensenAuctionBid.Tests
{
    public class BidTest
    {
        [Fact]
        public void CreateBid_Success_ReturnsBidId()
        {
            // Arrange
            var mockBidRepo = new Mock<IBidRepo>();
            var mockAdsService = new Mock<IAdsService>();
            var bid = new Bid { Price = 100.5f, AdID = 1, UserID = 2 };
            mockBidRepo.Setup(repo => repo.CreateBid(bid)).Returns(123);
            mockAdsService.Setup(service => service.IsAdClosed(It.IsAny<int>())).Returns(false);

            var controller = new BidController(mockBidRepo.Object, mockAdsService.Object);

            // Act
            var result = controller.CreateBid(bid);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;

            Assert.NotNull(returnValue);

            // Dynamically validate properties
            var bidID = returnValue.GetType().GetProperty("BidID")?.GetValue(returnValue);
            var message = returnValue.GetType().GetProperty("Message")?.GetValue(returnValue);

            Assert.NotNull(bidID);
            Assert.NotNull(message);

            Assert.Equal(123, (int)bidID);
            Assert.Equal("Bid created successfully.", (string)message);
        }

        [Fact]
        public void CreateBid_NullBid_ReturnsBadRequest()
        {
            // Arrange
            var mockBidRepo = new Mock<IBidRepo>();
            var mockAdsService = new Mock<IAdsService>();
            mockAdsService.Setup(service => service.IsAdClosed(It.IsAny<int>())).Returns(false);
            var controller = new BidController(mockBidRepo.Object, mockAdsService.Object);

            // Act
            var result = controller.CreateBid(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeleteBid_Success_ReturnsOk()
        {
            // Arrange
            var mockBidRepo = new Mock<IBidRepo>();
            var mockAdsService = new Mock<IAdsService>();
            mockBidRepo.Setup(repo => repo.DeleteBid(123)).Returns(true);
            mockAdsService.Setup(service => service.IsAdClosed(It.IsAny<int>())).Returns(false);

            var controller = new BidController(mockBidRepo.Object, mockAdsService.Object);

            // Act
            var result = controller.DeleteBid(123);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;

            Assert.NotNull(returnValue);

            // Dynamically validate properties
            var message = returnValue.GetType().GetProperty("Message")?.GetValue(returnValue);

            Assert.NotNull(message);
            Assert.Equal("Bid deleted successfully.", (string)message);
        }

        [Fact]
        public void DeleteBid_BidNotFound_ReturnsNotFound()
        {
            // Arrange
            var mockBidRepo = new Mock<IBidRepo>();
            var mockAdsService = new Mock<IAdsService>();
            mockBidRepo.Setup(repo => repo.DeleteBid(123)).Returns(false);
            mockAdsService.Setup(service => service.IsAdClosed(It.IsAny<int>())).Returns(false);

            var controller = new BidController(mockBidRepo.Object, mockAdsService.Object);

            // Act
            var result = controller.DeleteBid(123);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void CreateBid_RepoThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var mockBidRepo = new Mock<IBidRepo>();
            var mockAdsService = new Mock<IAdsService>();
            var bid = new Bid { Price = 100.5f, AdID = 1, UserID = 2 };
            mockBidRepo.Setup(repo => repo.CreateBid(bid)).Throws(new System.Exception("Database error"));
            mockAdsService.Setup(service => service.IsAdClosed(It.IsAny<int>())).Returns(false);

            var controller = new BidController(mockBidRepo.Object, mockAdsService.Object);

            // Act
            var result = controller.CreateBid(bid);

            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, errorResult.StatusCode);
        }
    }
}
