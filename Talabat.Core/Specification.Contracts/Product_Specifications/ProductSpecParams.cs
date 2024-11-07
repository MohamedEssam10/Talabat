using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification.Contracts.Product_Specifications
{
    public class ProductSpecParams
    {
        public string? sort {  get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        private int pageNumber = 1;

        public int PageNumber
        {
            get { return pageNumber; }
            set { pageNumber = value <= 0 ? 1 : value; }
        }

        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > 10 || value <= 0) ? 5 : value; }
        }

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

    }
}
