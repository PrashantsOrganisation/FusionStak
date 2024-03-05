using FusionStackBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace FusionStackBackEnd.Repository
{
    public class ProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void AddProd(Product prod)
        {
           
            context.products.Add(prod);
            context.SaveChanges();


        }
        public List<Product> GetProd(int page=1,int pageSize=3 )
        {
           


            return context.products.Skip((page)*pageSize).Take(pageSize).ToList();


        }

        public void deleteProduct(int productId)
        {
           
            var productToDelete = context.products.FirstOrDefault(p => p.ProductId == productId);

            if (productToDelete != null)
            {

                context.products.Remove(productToDelete);
                context.SaveChanges();
            }
          
        }

        public void UpdateProduct( Product prod)
        {
            var product = context.products.FirstOrDefault(p => p.ProductId == prod.ProductId);

            if (product != null)
            {
              
                product.ProductName =prod.ProductName;
                product.suplierName = prod.suplierName;
                product.Price = prod.Price;

              
                context.Entry(product).State = EntityState.Modified;

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }

        internal int PageCount()
        {
            return context.products.Count(); ;
        }
    }


    }

