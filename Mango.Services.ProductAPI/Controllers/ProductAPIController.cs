using AutoMapper;

using Mango.services.ProductAPI.Data;
using Mango.Services.ProductAPI.Model;
using Mango.Services.ProductAPI.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    //[Authorize]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private  ResponseDto _response;
        private IMapper _mapper;

        public ProductAPIController(AppDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseDto();
            _mapper = mapper;
        }


        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> products = _dbContext.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
                
            }
            return _response;    
        }

        //[HttpGet]
        //[Route("id:int")]
        //[Route("{id:int}")]
        [HttpGet("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product product = _dbContext.Products.First(x => x.ProductId == id);
                _response.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }



        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponseDto Post([FromBody] ProductDto productDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(productDto);
                _dbContext.Products.Add(obj);
                _dbContext.SaveChanges();

                _response.Result=_mapper.Map<ProductDto>(obj);
                _response.isSuccess=true;
                
               
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] ProductDto productDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(productDto);
                _dbContext.Products.Update(obj);
                _dbContext.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(obj);
                _response.isSuccess = true;


            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var product = _dbContext.Products.First(x => x.ProductId == id);
                if(product != null)
                {
                    _dbContext.Remove(product);
                    _dbContext.SaveChanges();
                    _response.isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}
