using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopBridge.Models;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryDBContext _inventoryDBContext;
        public InventoryController(InventoryDBContext inventoryDBContext)
        {
            _inventoryDBContext = inventoryDBContext;
        }

        // GET: api/Inventory
        [HttpGet]
        [Consumes("application/json")]
        [Produces("text/plain", "application/json")]
        public async Task<IEnumerable<Inventory>>  GetAllProducts()
        {
            try
            {
                var products = _inventoryDBContext.Inventories.Select(x => x).ToList();

                return products;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //GET: api/Inventory/5
        [HttpGet("{id}", Name = "Get")]
        [Consumes("application/json")]
        [Produces("text/plain", "application/json")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (_inventoryDBContext.Inventories.Any(p => p.ProductId == id))
                {
                    var products = _inventoryDBContext.Inventories.Where(x => x.ProductId == id).First();

                    return Ok(products);
                }
                else
                {
                    return NotFound("Requested product does not exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: api/Inventory
        [HttpPost]
        [Consumes("application/json")]
        [Produces("text/plain","application/json")]
        public async Task<IActionResult> AddProduct([FromBody] Inventory inventoryDetails)
        {
            try
            {
                _inventoryDBContext.Add(inventoryDetails);
                await _inventoryDBContext.SaveChangesAsync();
                return Ok("New product is added");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // PUT: api/Inventory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProduct(int id, [FromBody] Inventory inventoryDetails)
        {
            try
            {
                if (_inventoryDBContext.Inventories.Any(p => p.ProductId == id))
                {

                    var product = _inventoryDBContext.Inventories.Where(p => p.ProductId == id).First();
                    
                    product.Name = inventoryDetails.Name;
                    product.Price = inventoryDetails.Price;
                    product.Description = inventoryDetails.Description;
                    product.ContryOfOrigin = inventoryDetails.ContryOfOrigin;
                     await _inventoryDBContext.SaveChangesAsync();
                    return Ok("The Product is updated : " + id + "");
                }
                else
                {
                    return NotFound("Requested product does not exist");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (_inventoryDBContext.Inventories.Any(p => p.ProductId == id))
                {
                    var product = _inventoryDBContext.Inventories.Where(p => p.ProductId == id).First();
                    _inventoryDBContext.Remove(product);
                    await _inventoryDBContext.SaveChangesAsync();
                    return Ok("The Product is deleted : "+ id +"");
                }
                else
                {
                    return NotFound("Requested product does not exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
