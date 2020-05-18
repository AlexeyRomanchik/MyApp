using System;
using System.Linq;
using DataProvider.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Product;
using Models.User;

namespace WebApplication.Controllers
{
    public class ProductGeneralController : Controller
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IReviewRepository _reviewRepository;
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

            if (review == null)
                return RedirectToAction("Index", "Home");

            review.Text = text;
            review.PublicationDate = DateTime.Now;
            _reviewRepository.Update(review);
            _repositoryWrapper.Save();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Rate(int rating, Guid id)
        {
            var productId = id;
            var productRating = new Rating
            {
                Id = Guid.NewGuid(),
                ProductId = id,
                Value = rating,
                UserId = _userManager.GetUserId(User)
            };

            _ratingRepository.Create(productRating);
            _repositoryWrapper.Save();

            return RedirectToAction("Info", "ProductGeneral", new {id = productId});
        }

        public IActionResult ChangeRating(int rating, Guid id)
        {
            var productRating = _ratingRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (productRating == null)
                return RedirectToAction("Index", "Home");

            productRating.Value = rating;
            _ratingRepository.Update(productRating);
            _repositoryWrapper.Save();

            return RedirectToAction("Info", "ProductGeneral", id);
        }

        public IActionResult Info(Guid id)
        {
            var product = _repositoryWrapper.ProductRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product == null)
                return RedirectToAction("Index", "Home");

            return product.Category.Name switch
                {
                "Видеокарты" => RedirectToAction("Info", "GraphicsCard", new {id}),
                "Оперативная память" => RedirectToAction("Info", "Ram", new {id}),
                "Процессоры" => RedirectToAction("Info", "Cpu", new {id}),
                "Жесткие диски" => RedirectToAction("Info", "Hdd", new {id}),
                "Материнские платы" => RedirectToAction("Info", "Motherboard", new {id}),
                "Блоки питания" => RedirectToAction("Info", "PowerSupply", new {id}),
                _ => RedirectToAction("Index", "Home"),
                };
        }
    }
}