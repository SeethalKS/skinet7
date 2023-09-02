
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //[ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
{
        
        private readonly IGenericRepository<Product> _productsRepo;

        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        public ProductsController(IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandRepo,IGenericRepository<ProductType> productTypeRepo,IMapper mapper)
        {
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;
           
           
            
        }
       
[HttpGet ]
public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
{
    var spec=new ProductsWithTypesAndBrandsSpecification();
    
    var products=await _productsRepo.ListAsync(spec);

    return Ok(_mapper.Map<IReadOnlyList<Product>,
                         IReadOnlyList<ProductToReturnDto>>(products));

    // return products.Select(product=>new ProductToReturnDto
    // {
    // Id=product.Id,
    // Name=product.Name,
    // Description=product.Description,
    // PictureUrl=product.PictureUrl,
    // Price=product.Price,
    // ProductBrand=product.ProductBrand.Name,
    // ProductType=product.ProductType.Name
    // }).ToList();
}
[HttpGet("{id}")]
public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) 
{
var spec=new ProductsWithTypesAndBrandsSpecification(id);
// return await _productsRepo.GetByIdAsync(id);
// return await _productsRepo.GetEntityWithSpec(spec); for class no 42
var product= await _productsRepo.GetEntityWithSpec(spec);
return _mapper.Map<Product,ProductToReturnDto>(product);
// return new ProductToReturnDto   class 43
// {
//     Id=product.Id,
//     Name=product.Name,
//     Description=product.Description,
//     PictureUrl=product.PictureUrl,
//     Price=product.Price,
//     ProductBrand=product.ProductBrand.Name,
//     ProductType=product.ProductType.Name
// };
}

[HttpGet("brands")]
public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
{
return Ok(await _productBrandRepo.ListAllAsync());
}
[HttpGet("types")]
public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
{
return Ok(await _productTypeRepo.ListAllAsync());
}
}
}