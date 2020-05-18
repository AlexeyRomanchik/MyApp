using System.Collections.Generic;
using System.Linq;
using DataProvider.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Product;
using Models.User;
using Moq;
using WebApplication.Controllers;
using WebApplication.Services;
using WebApplication.Services.Filters;
using WebApplication.Services.SortServices;
using Xunit;

namespace WebApplication.Tests
{
    public class RamControllerTests
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfUsers()
        {
            
            var mock = new Mock<IRepositoryWrapper>();

            mock.Setup(repo => repo.RamRepository.FindAll()).Returns(GetTestRamProducts() as IQueryable<Ram>);
            var controller = new RamController(mock.Object, new SortServiceWrapper(), new RamFilter(), new FileService(), null);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Model);
            Assert.Equal(GetTestRamProducts().Count, model.Count());
        }

        private static List<Ram> GetTestRamProducts()
        {
            var products = new List<Ram>
            {
                new Ram()
                {
                    Product = new Product()
                    {
                        Manufacturer = new Manufacturer()
                        {
                            Name = "Intel"
                        },
                        Category = new Category(),
                        Ratings = new List<Rating>()
                    }
                }
            };
            return products;
        }
    }
}
