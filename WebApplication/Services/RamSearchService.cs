using System;
using System.Linq;
using Models.Product;

namespace Logic.Services
{
    public class RamSearchService
    {
        private const string MemoryType = "тип";
        private const string TotalMemory = "объем";
        public IQueryable<Ram> GetProductsByCommands(string searchLine, IQueryable<Ram> products)
        {
            searchLine = searchLine.ToLower().Replace(" ", "");

            var commands = searchLine.Split('@', StringSplitOptions.RemoveEmptyEntries);

            var productsResult = products;

            foreach (var command in commands)
            {
                var parameters = command.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if (parameters.Length == 2)
                {
                    var param = parameters[1].Split(',').ToList();
                    if (parameters[0] == MemoryType)
                    {
                        productsResult = productsResult.Where(x => param.Contains(x.MemoryType.Type));
                    }
                    if (parameters[0] == TotalMemory)
                    {
                        productsResult = productsResult.Where(x => param.Contains(x.TotalMemory.ToString()));
                    }
                }
            }

            return productsResult;
        }
    }
}
