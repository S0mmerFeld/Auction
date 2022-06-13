using Auction.DAL;
using Auction.Models;
using Auction.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.BLL
{
    public class ProductService : IProductService
    {
        private readonly AuctionDbContext _context;
        public ProductService(AuctionDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddAsync(Product model)
        {
            var rec = new Product()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                //StepPrice = model.StepPrice,
                //StartPrice = model.StartPrice,
                //StartTime = model.StartTime,
                //FinishTime = model.FinishTime
            };

            _context.Products.Add(rec);


            await _context.SaveChangesAsync();
            return rec;
        }

        public async Task DeleteAsync(int recid)
        {
            var rec = await GetByIdAsync(recid);
            //if (rec == null)
                //throw new ItemNotFoundException($"{typeof(AutomatMachine).Name} with id {discountCardId} not found");

            _context.Products.Remove(rec);

            await _context.SaveChangesAsync();

        }

        public async Task<List<Product>> GetAllAsync()
        {
            var rec = await _context.Products.Select(x => new Product()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                /*StepPrice = x.StepPrice,
                StartPrice = x.StartPrice,
                StartTime = x.StartTime,
                FinishTime = x.FinishTime*/
            }).ToListAsync();

            return rec;

        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var prod = await _context.Products.Where(x => x.Id == id).Select(x => new Product()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                //StepPrice = x.StepPrice,
                //StartPrice = x.StartPrice,
                //StartTime =x.StartTime,
                //FinishTime = x.FinishTime
            }).FirstOrDefaultAsync();
           
           // if (prod == null)
                //throw new ItemNotFoundException($"{typeof(DiscountCard).Name} item with id {discountCardId} not found.");
                //return prod;

            return prod;
        }

        public Task UpdateAsync(int id, Product model)
        {
            throw new NotImplementedException();
        }
    }
}
