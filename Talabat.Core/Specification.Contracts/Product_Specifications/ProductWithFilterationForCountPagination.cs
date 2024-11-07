using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification.Contracts.Product_Specifications
{
    public class ProductWithFilterationForCountPagination : BaseSpecifications<Product> 
    {
        public ProductWithFilterationForCountPagination(ProductSpecParams productSpecParams) : base(P => 
            (string.IsNullOrEmpty(productSpecParams.Search) || P.Name.Contains(productSpecParams.Search) )
            &&
            (!productSpecParams.BrandId.HasValue || P.BrandId == productSpecParams.BrandId)
            &&
            (!productSpecParams.CategoryId.HasValue || P.CategoryId == productSpecParams.CategoryId)
        )
        {
            
        }

    }
}
