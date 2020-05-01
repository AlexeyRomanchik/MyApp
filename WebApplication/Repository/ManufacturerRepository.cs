using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class ManufacturerRepository : RepositoryBase<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }


    }
}
