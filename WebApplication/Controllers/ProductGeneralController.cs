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
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly UserManager<User> _userManager;

        public ProductGeneralController(IRepositoryWrapper repositoryWrapper, UserManager<User> userManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _ratingRepository = repositoryWrapper.RatingRepository;
            _userManager = userManager;
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
            var productRating = _ratingRepository.FindByCondition(
                x => x.User.Id == _userManager.GetUserId(User) && x.ProductId == id
                     ).FirstOrDefault();

            if (productRating != null)
                productRating.Value = rating;

            _ratingRepository.Update(productRating);
            _repositoryWrapper.Save();

            return RedirectToAction("Index", "Home");
        }

    }
}