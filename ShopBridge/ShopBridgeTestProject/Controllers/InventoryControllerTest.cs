using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ShopBridge.Controllers;
using ShopBridge.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ShopBridgeTestProject
{
    public class InventoryControllerTest
    {
        private DbContextOptions<InventoryDBContext> options;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InventoryDBContext>()
           .UseInMemoryDatabase(databaseName: "InventoryDB")
           .Options;
            using (var context = new InventoryDBContext(options))
            {
                if (!context.Inventories.Any(p => p.ProductId == 101))
                {
                    context.Inventories.Add(new Inventory()
                    {
                        ProductId = 101,
                        Name = "PenDrive",
                        Description = "Used for Computers, of 16 GB",
                        Price = 599,
                        ContryOfOrigin = "India"
                    });

                    context.SaveChanges();
                }
            }
        }

        [Test]
        public void GetAllProducts_Success()
        {
                    
            using (var context = new InventoryDBContext(options))
            {
                InventoryController _inventoryController = new InventoryController(context);
               
                var products = _inventoryController.GetAllProducts();                           
    
                Assert.AreEqual(context.Inventories, products.Result);
            }
        }
        [Test]
        public void GetAllProducts_Exception()
        {
            var inventoryDBContextMock = new Mock<InventoryDBContext>();
            var inventoryMock = new Mock<DbSet<Inventory>>();
            inventoryDBContextMock.Setup(x => x.Inventories).Callback(() => throw new Exception());
            InventoryController _inventoryController = new InventoryController(inventoryDBContextMock.Object);

            Assert.ThrowsAsync<Exception>(async () =>
                await _inventoryController.GetAllProducts()
           );
        }

        [Test]
        public void Get_Success()
        {           
            using (var context = new InventoryDBContext(options))
            {
                InventoryController _inventoryController = new InventoryController(context);

                var result = _inventoryController.Get(101);
            
                Assert.NotNull(result);
                Assert.True(result.Result is OkObjectResult);
            }
        }

        [Test]
        public void Get_NotFound()
        {            
            using (var context = new InventoryDBContext(options))
            {
                InventoryController _inventoryController = new InventoryController(context);

                var result = _inventoryController.Get(104);
                                
                Assert.True(result.Result is NotFoundObjectResult);
            }
        }

        [Test]
        public void Get_Exception()
        {
            var inventoryDBContextMock = new Mock<InventoryDBContext>();
            var inventoryMock = new Mock<DbSet<Inventory>>();
            inventoryDBContextMock.Setup(x => x.Inventories).Callback(() => throw new Exception());
            InventoryController _inventoryController = new InventoryController(inventoryDBContextMock.Object);

            Assert.ThrowsAsync<Exception>(async () =>
                await _inventoryController.Get(104)
           );
        }

        [Test]
        public void AddProduct_Success()
        {
            Inventory newproduct =new Inventory()
            {
                ProductId = 102,
                Name = "Disk",
                Description = "Used for Computers, of 500 GB",
                Price = 4999,
                ContryOfOrigin = "India"
            };          
            
            using (var context = new InventoryDBContext(options))
            {
                InventoryController _inventoryController = new InventoryController(context);

                var result = _inventoryController.AddProduct(newproduct);

                Assert.True(result.Result is OkObjectResult);
            }
        }
        [Test]
        public void AddProduct_Unsuccess()
        {
            Inventory newproduct = new Inventory()
            {
                ProductId = 102,
                Name = "Disk",
                Description = "Used for Computers, of 500 GB",
                Price = 4999,
                ContryOfOrigin = "India"
            };
           
            var inventoryDBContextMock = new Mock<InventoryDBContext>();
            inventoryDBContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Callback(() => throw new Exception());
            InventoryController _inventoryController = new InventoryController(inventoryDBContextMock.Object);

             Assert.ThrowsAsync<Exception>(async () =>
                 await _inventoryController.AddProduct(newproduct)
            );                      
        }

        [Test]
        public void ModifyProduct_Success()
        {
            Inventory newproduct = new Inventory()
            {                
                Name = "Ram",
                Description = "Used for Computers, of 4 GB",
                Price = 3000,
                ContryOfOrigin = "India"
            };

            using (var context = new InventoryDBContext(options))
            {
                InventoryController _inventoryController = new InventoryController(context);

                var result = _inventoryController.ModifyProduct(101,newproduct);

                Assert.True(result.Result is OkObjectResult);
            }
        }

        [Test]
        public void ModifyProduct_Unsuccess()
        {
            Inventory newproduct = new Inventory()
            {
                Name = "Ram",
                Description = "Used for Computers, of 4 GB",
                Price = 3000,
                ContryOfOrigin = "India"
            };            
            var inventoryDBContextMock = new Mock<InventoryDBContext>();
            inventoryDBContextMock.Setup(x => x.Inventories).Returns(new InventoryDBContext(options).Inventories);
            inventoryDBContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Callback(() => throw new Exception());
            InventoryController _inventoryController = new InventoryController(inventoryDBContextMock.Object);

            Assert.ThrowsAsync<Exception>(async () =>
                await _inventoryController.ModifyProduct(101, newproduct)
           );
        }
        [Test]
        public void ModifyProduct_Product_NotFound()
        {
            Inventory newproduct = new Inventory()
            {
                Name = "Ram",
                Description = "Used for Computers, of 4 GB",
                Price = 3000,
                ContryOfOrigin = "India"
            };

            using (var context = new InventoryDBContext(options))
            {
                InventoryController _inventoryController = new InventoryController(context);

                var result = _inventoryController.ModifyProduct(104, newproduct);

                Assert.True(result.Result is NotFoundObjectResult);
            }
        }

        [Test]
        public void DeleteProduct_Success()
        {
            using (var context = new InventoryDBContext(options))
            {
                InventoryController _inventoryController = new InventoryController(context);

                var result = _inventoryController.DeleteProduct(101);

                Assert.True(result.Result is OkObjectResult);
            }
        }

        [Test]
        public void DeleteProduct_Unsuccess()
        {
            using (var context = new InventoryDBContext(options))
            {
                InventoryController _inventoryController = new InventoryController(context);

                var result = _inventoryController.DeleteProduct(104);

                Assert.True(result.Result is NotFoundObjectResult);
            }
        }

        [Test]
        public void DeleteProduct_Exception()
        {
            var inventoryDBContextMock = new Mock<InventoryDBContext>();
            inventoryDBContextMock.Setup(x => x.Inventories).Returns(new InventoryDBContext(options).Inventories);
            inventoryDBContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Callback(() => throw new Exception());
            InventoryController _inventoryController = new InventoryController(inventoryDBContextMock.Object);

            Assert.ThrowsAsync<Exception>(async () =>
                await _inventoryController.DeleteProduct(101)
           );
        }
    }
}