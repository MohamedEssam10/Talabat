using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
using Talabat.API.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;
using Talabat.Core.Specification.Contracts.Product_Specifications;

namespace Talabat.API.Controllers
{
    public class ProductController : BaseAPIController
    {
        private readonly IGenericRepository<Product> ProductRepo;
        private readonly IGenericRepository<Brand> BrandRepo;
        private readonly IGenericRepository<Category> CategoryRepo;
        private readonly IMapper Mapper;

        public ProductController(IGenericRepository<Product> ProductRepo,
            IGenericRepository<Brand> BrandRepo,
            IGenericRepository<Category> CategoryRepo,
            IMapper Mapper)
        {
            this.ProductRepo = ProductRepo;
            this.BrandRepo = BrandRepo;
            this.CategoryRepo = CategoryRepo;
            this.Mapper = Mapper;
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            //var products = await productRepo.GetAllAsync();
            //return Ok(products);
            var specification = new ProductWithBrandAndCategorySpecification(productSpecParams);
            var products = await ProductRepo.GetAllWithSpecAsync(specification);

            var Data = Mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var Count = await ProductRepo.GetCountAsync(new ProductWithFilterationForCountPagination(productSpecParams));
            return Ok(new Pagination<ProductToReturnDto>(productSpecParams.PageNumber, productSpecParams.PageSize, Count, Data) { });
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int Id)
        {
            //var product = await productRepo.GetAsync(Id);
            //if(product == null)
            //    return NotFound();
            //return Ok(product);

            var specification = new ProductWithBrandAndCategorySpecification(Id);
            var product = await ProductRepo.GetWithSpecAsync(specification);
            if (product is null)
                return NotFound();
            var ProductDto = Mapper.Map<Product,ProductToReturnDto>(product);
            return Ok(ProductDto);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Brand>>> GetBrands()
        {
            var brands = await BrandRepo.GetAllAsync();
            return Ok(brands);
        }




        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            var categories = await CategoryRepo.GetAllAsync();
            return Ok(categories);
        }

    }
}
