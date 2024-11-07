using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;


namespace Talabat.Core.Specification.Contracts.Product_Specifications
{
    public class ProductWithBrandAndCategorySpecification : BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecification(ProductSpecParams productSpec) : base(P =>
                (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search) )
                &&
                (!productSpec.BrandId.HasValue || P.BrandId == productSpec.BrandId) 
                &&
                (!productSpec.CategoryId.HasValue || P.CategoryId == productSpec.CategoryId)
            )
        {
            Include.Add(P => P.Brand);
            Include.Add(P => P.Category);

            if(!string.IsNullOrEmpty(productSpec.sort))
            {
                switch(productSpec.sort)
                {
                    case "priceAsec":
                        OrderBy = (P => P.Price);
                        break;
                    case "priceDesc":
                        OrderByDescending = (P => P.Price);
                        break;
                    case "Name":
                        OrderBy = (P => P.Name);
                        break;
                }
            }

            ApplyPagination((productSpec.PageNumber - 1) * productSpec.PageSize, productSpec.PageSize);
        }

        public ProductWithBrandAndCategorySpecification(int Id) : base(P => P.Id == Id)
        {
            Include.Add(P => P.Brand);
            Include.Add(P => P.Category);
        }
    }
}
