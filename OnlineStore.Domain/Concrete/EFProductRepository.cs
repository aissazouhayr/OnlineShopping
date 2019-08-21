using OnlineStore.Domain.Abstract;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext Context = new EFDbContext();
        public IEnumerable<Product> Products {
            get { return Context.Products; }
        }

        public Product DeleteProduct(int ProductId)
        {
            Product dbentry = Context.Products.Find(ProductId);
            if (dbentry !=null)
            {
                Context.Products.Remove(dbentry);
                Context.SaveChanges();
            }
            return dbentry;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId==0)
            {
                Context.Products.Add(product);
            }
            else
            {
                Product dbEntry = Context.Products.Find(product.ProductId);
                if (dbEntry !=null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            Context.SaveChanges();
        }
    }
}
