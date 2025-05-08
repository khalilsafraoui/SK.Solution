using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Moq;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Exceptions;
using SK.Inventory.Application.Features.Products.Commands;
using SK.Inventory.Application.Features.Products.Queries;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Domain.Entities.Product;
using SK.Inventory.Infrastructure.SqlServer.Persistence;
using SK.Inventory.Infrastructure.SqlServer.Repositories;
using System.ComponentModel.DataAnnotations;

namespace SK.Inventory.IntegrationTests
{
    public class ProductsIntegrationTests
    {
        private readonly IMapper _mapper;
        private readonly InventoryDbContext Context;
        public IProductRepository ProductRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        public ProductsIntegrationTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>().ReverseMap();
                cfg.CreateMap<Category, CategoryDto>().ReverseMap();
            });
            _mapper = config.CreateMapper();


            // Use InMemoryDatabase for EF Core
            var _dbOptions = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;

            Context = new InventoryDbContext(_dbOptions);
            // Mock the IWebHostEnvironment
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockWebHostEnvironment.Setup(env => env.WebRootPath).Returns(Path.GetTempPath());
            ProductRepository = new ProductRepository(Context, _mockWebHostEnvironment.Object);
            CategoryRepository = new CategoryRepository(Context);
        }

        #region create a product
        [Fact]
        public async Task Handle_ShouldCreateProduct_AndReturnProductDto()
        {
           

            var category = new Category { Id = 1, Name = "Electronics" };
            await Context.Categories.AddAsync(category);
            await Context.SaveChangesAsync();

            var handler = new CreateProductCommandHandler(
                ProductRepository,
                CategoryRepository,
                _mapper
            );

            var productDto = new ProductDto { Name = "Laptop", Price = 1000, CategoryId = 1 };
            var command = new CreateProductCommand(productDto);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productDto.Name, result.Name);
            Assert.True(result.Id > 0);

            var saved = await Context.Products.FindAsync(result.Id);
            Assert.NotNull(saved);
            Assert.Equal("Laptop", saved.Name);
        }


        [Theory]
        [InlineData("", 1000, 1)]    // Invalid name
        [InlineData("Laptop", 0, 1)] // Invalid price
        [InlineData("Laptop", 1000, 999)] // Non-existing category
        public async Task Handle_ShouldThrowValidationException_WhenInvalidInput(string name, decimal price, int categoryId)
        {
            
            Context.Categories.Add(new Category { Id = 1, Name = "Electronics" });
            await Context.SaveChangesAsync();
           
            var handler = new CreateProductCommandHandler(ProductRepository, CategoryRepository, _mapper);

            var dto = new ProductDto { Name = name, Price = price, CategoryId = categoryId };
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateProductCommand(dto), CancellationToken.None));
        }
        #endregion

        #region update a product
        [Fact]
        public async Task Handle_ShouldUpdateProduct_AndReturnProductDto()
        {
            
            Context.Categories.Add(new Category { Id = 1, Name = "Electronics" });
            Context.Products.Add(new Product { Id = 1, Name = "Old Laptop", Price = 500, CategoryId = 1 });
            await Context.SaveChangesAsync();

            var handler = new UpdateProductCommandHandler(ProductRepository, CategoryRepository, _mapper);

            var updatedDto = new ProductDto { Id = 1, Name = "New Laptop", Price = 1000, CategoryId = 1 };
            var result = await handler.Handle(new UpdateProductCommand(updatedDto), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("New Laptop", result.Name);
            Assert.Equal(1000, result.Price);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenProductDoesNotExist()
        {
            var handler = new UpdateProductCommandHandler(ProductRepository, CategoryRepository, _mapper);

            var dto = new ProductDto { Id = 999, Name = "Laptop", Price = 1000, CategoryId = 1 };
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new UpdateProductCommand(dto), CancellationToken.None));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(1500)]
        public async Task Handle_ShouldThrowValidationException_WhenPriceIsInvalid(decimal price)
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Laptop", Price = 500, CategoryId = 1 };
            var category = new Category { Id = 1, Name = "Electronics" };
            await Context.Categories.AddAsync(category);
            await Context.Products.AddAsync(product);
            await Context.SaveChangesAsync();

            var productDto = new ProductDto { Id = 1, Name = "Laptop", Price = price, CategoryId = 1 };

            var handler = new UpdateProductCommandHandler(ProductRepository, CategoryRepository, _mapper);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                handler.Handle(new UpdateProductCommand(productDto), CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenProductNameIsEmpty()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Electronics" };
            var product = new Product { Id = 1, Name = "Laptop", Price = 500, CategoryId = 1 };
            await Context.Categories.AddAsync(category);
            await Context.Products.AddAsync(product);
            await Context.SaveChangesAsync();

            var productDto = new ProductDto { Id = 1, Name = "", Price = 800, CategoryId = 1 };

            var handler = new UpdateProductCommandHandler(ProductRepository, CategoryRepository, _mapper);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                handler.Handle(new UpdateProductCommand(productDto), CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCategoryIdIsInvalid()
        {
            // Arrange
            var existingCategory = new Category { Id = 1, Name = "Electronics" };
            var product = new Product { Id = 1, Name = "Laptop", Price = 500, CategoryId = 1 };
            await Context.Categories.AddAsync(existingCategory);
            await Context.Products.AddAsync(product);
            await Context.SaveChangesAsync();

            var productDto = new ProductDto { Id = 1, Name = "Laptop", Price = 800, CategoryId = 0 };

            var handler = new UpdateProductCommandHandler(ProductRepository, CategoryRepository, _mapper);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                handler.Handle(new UpdateProductCommand(productDto), CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCategoryNotFound()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Electronics" };
            var product = new Product { Id = 1, Name = "Laptop", Price = 500, CategoryId = 1 };
            await Context.Categories.AddAsync(category);
            await Context.Products.AddAsync(product);
            await Context.SaveChangesAsync();

            var productDto = new ProductDto { Id = 1, Name = "Laptop", Price = 800, CategoryId = 2 };

            var handler = new UpdateProductCommandHandler(ProductRepository, CategoryRepository, _mapper);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                handler.Handle(new UpdateProductCommand(productDto), CancellationToken.None));
        }
        #endregion

        #region delete a product
        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenProductExists()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Laptop", Price = 500, CategoryId = 1 };
            var category = new Category { Id = 1, Name = "Electronics" };
            await Context.Categories.AddAsync(category);
            await Context.Products.AddAsync(product);
            await Context.SaveChangesAsync();

            var handler = new DeleteProductCommandHandler(ProductRepository);

            // Act
            var result = await handler.Handle(new DeleteProductCommand(product.Id), CancellationToken.None);

            // Assert
            Assert.True(result);
            var deletedProduct = await Context.Products.FindAsync(product.Id);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            // Arrange
            var handler = new DeleteProductCommandHandler(ProductRepository);

            // Act
            var result = await handler.Handle(new DeleteProductCommand(999), CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region get a product by id
        [Fact]
        public async Task Handle_ShouldReturnProductDto_WhenProductExists()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Electronics" };
            var product = new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 1000,
                CategoryId = category.Id,
                Category = category
            };

            await Context.Categories.AddAsync(category);
            await Context.Products.AddAsync(product);
            await Context.SaveChangesAsync();

            var handler = new GetProductByIdQueryHandler(ProductRepository, _mapper);
            var query = new GetProductByIdQuery(product.Id);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.CategoryId, result.CategoryId);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            var handler = new GetProductByIdQueryHandler(ProductRepository, _mapper);
            var query = new GetProductByIdQuery(999); // non-existent ID

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region get all products
        [Fact]
        public async Task Handle_ShouldReturnListOfProductDtos_WhenProductsExist()
        {
            // Arrange
            var category1 = new Category { Id = 1, Name = "Electronics" };
            var category2 = new Category { Id = 2, Name = "Mobile Devices" };

            var product1 = new Product { Id = 1, Name = "Laptop", Price = 1000, CategoryId = category1.Id, Category = category1 };
            var product2 = new Product { Id = 2, Name = "Phone", Price = 500, CategoryId = category2.Id, Category = category2 };

            await Context.Categories.AddRangeAsync(category1, category2);
            await Context.Products.AddRangeAsync(product1, product2);
            await Context.SaveChangesAsync();

            var handler = new GetAllProductsQueryHandler(ProductRepository, _mapper);
            var query = new GetAllProductsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Name == "Laptop");
            Assert.Contains(result, p => p.Name == "Phone");
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            var handler = new GetAllProductsQueryHandler(ProductRepository, _mapper);
            var query = new GetAllProductsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        #endregion
    }
}