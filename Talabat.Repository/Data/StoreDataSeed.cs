using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data
{
    public static class StoreDataSeed
    {
        public static async Task SeedAsync(StoreDbContext dbcontext)
        {
            if (!dbcontext.Brands.Any())
            {
                var brandData = File.ReadAllText("../Talabat.Repository/Data/Data Seed/brands.json");
                var brands = JsonSerializer.Deserialize<List<Brand>>(brandData);


                    foreach (var brand in brands)
                    {
                        dbcontext.Brands.Add(brand);
                    }
                    await dbcontext.SaveChangesAsync();
                
            }

            if (!dbcontext.Categories.Any())
            {
                var CategoryData = File.ReadAllText("../Talabat.Repository/Data/Data Seed/Category.json");
                var Categories = JsonSerializer.Deserialize<List<Category>>(CategoryData);


                    foreach (var Category in Categories)
                    {
                        dbcontext.Categories.Add(Category);
                    }
                    await dbcontext.SaveChangesAsync();
                
            }

            if (dbcontext.Products.Count() == 0)
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/Data Seed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                    foreach (var Product in Products)
                    {
                        dbcontext.Set<Product>().Add(Product);
                    }
                    await dbcontext.SaveChangesAsync();
                
            }

            if(!dbcontext.DeliveryMethod.Any())
            {
                var DeliveryData = File.ReadAllText("../Talabat.Repository/Data/Data Seed/delivery.json");
                var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);

                foreach( var item in delivery)
                    dbcontext.DeliveryMethod.Add(item);
                await dbcontext.SaveChangesAsync();
            }
        }
    }
}
