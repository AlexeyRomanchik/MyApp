using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ProductGeneralController : Controller
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly UserManager<User> _userManager;

        public ProductGeneralController(IRepositoryWrapper repositoryWrapper, UserManager<User> userManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _ratingRepository = repositoryWrapper.RatingRepository;
            _userManager = userManager;
            _reviewRepository = repositoryWrapper.ReviewRepository;
        }

        public IActionResult AddReview(string text, Guid id)
        {
            var review = new Review
            {
                Id = Guid.NewGuid(),
                ProductId = id,
                Text = text,
                PublicationDate = DateTime.Now,
                UserId = _userManager.GetUserId(User),
                ReviewVerified = true
            };

            _reviewRepository.Create(review);
            _repositoryWrapper.Save();

            return RedirectToAction("Index", "Home");
        }

        public void DeleteReview(Guid id)
        {
            var review = _reviewRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            _reviewRepository.Delete(review);
            _repositoryWrapper.Save();
        }

        public void DeleteRating(Guid id)
        {
            var rating = _ratingRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            _ratingRepository.Delete(rating);
            _repositoryWrapper.Save();
        }



        public IActionResult ChangeReview(string text, Guid id)
        {
            var review = _reviewRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if(review == null)
                return RedirectToAction("Index", "Home");

            review.Text = text;
            review.PublicationDate = DateTime.Now;
            _reviewRepository.Update(review);
            _repositoryWrapper.Save();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Rate(int rating, Guid id)
        {
            var productRating = new Rating
            {
                Id = Guid.NewGuid(),
                ProductId = id,
                Value = rating,
                UserId = _userManager.GetUserId(User)
            };

            _ratingRepository.Create(productRating);
            _repositoryWrapper.Save();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangeRating(int rating, Guid id)
        {
            var productRating = _ratingRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (productRating == null)
                return RedirectToAction("Index", "Home");
            
            productRating.Value = rating;
            _ratingRepository.Update(productRating);
            _repositoryWrapper.Save();

            return RedirectToAction("Index", "Home");
        }

    }
}