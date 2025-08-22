using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Exceptions;
using SK.Inventory.Application.Features.Products.Commands;
using SK.Inventory.Application.Features.Products.Dtos;
using SK.Inventory.Application.Features.Products.Queries;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Domain.Entities.Product;
using System.ComponentModel.DataAnnotations;

namespace SK.Inventory.Tests
{
    public class ProductsTests
    {
        #region create
        [Fact]
        public async Task Handle_ShouldCreateProductAndReturnProductDto()
        {
            // Arrange
            var mockRepo = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            var productDto = new Product_GeneralInformations { Name = "Laptop", Price = 1000, CategoryId = 1 };
            var product = new Product { Name = "Laptop", Price = 1000, CategoryId = 1 };
            var savedProduct = new Product { Id = 1, Name = "Laptop", Price = 1000, CategoryId = 1 };
            var returnedDto = new Product_GeneralInformations { Id = 1, Name = "Laptop", Price = 1000, CategoryId = 1 };

            var category = new Category { Id = 1, Name = "Electronics" };

            // Mock category check
            mockRepo.Setup(repo => repo.Categories.GetByIdAsync(1)).ReturnsAsync(category);

            // Mock mapping and repository calls
            mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(product);
            mockRepo.Setup(r => r.Products.CreateAsync(product)).ReturnsAsync(savedProduct);
            mockMapper.Setup(m => m.Map<Product_GeneralInformations>(savedProduct)).Returns(returnedDto);

            var handler = new CreateProductCommandHandler(mockRepo.Object, mockMapper.Object, NullLogger<CreateProductCommandHandler>.Instance);
            var command = new CreateProductCommand(productDto);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(returnedDto.Id, result.GeneralInformationsDto?.Id);
            Assert.Equal(returnedDto.Name, result.GeneralInformationsDto?.Name);
            Assert.Equal(returnedDto.Price, result.GeneralInformationsDto?.Price);

            mockMapper.Verify(m => m.Map<Product>(productDto), Times.Once);
            mockRepo.Verify(r => r.Products.CreateAsync(product), Times.Once);
            mockMapper.Verify(m => m.Map<Product_GeneralInformations>(savedProduct), Times.Once);
            mockRepo.Verify(r => r.Categories.GetByIdAsync(1), Times.Once); // ✅ validation check
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenFieldsAreInvalid()
        {
            // Arrange
            var mockRepo = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            var invalidProductDtos = new List<Product_GeneralInformations>
        {
            new Product_GeneralInformations { Name = "", Price = 1000, CategoryId = 1 },  // Invalid name
            new Product_GeneralInformations { Name = "Laptop", Price = 0, CategoryId = 1 }, // Invalid price
            new Product_GeneralInformations { Name = "Laptop", Price = 1000, CategoryId = 999 } // Invalid category WhenCategoryDoesNotExist
        };

            foreach (var productDto in invalidProductDtos)
            {
                var product = new Product { Name = productDto.Name, Price = productDto.Price, CategoryId = productDto.CategoryId };

                mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(product);
                mockRepo.Setup(repo => repo.Categories.GetByIdAsync(productDto.CategoryId)).ReturnsAsync((Category)null);

                var handler = new CreateProductCommandHandler(mockRepo.Object, mockMapper.Object,NullLogger<CreateProductCommandHandler>.Instance);
                var command = new CreateProductCommand(productDto);

                var result = await handler.Handle(command, CancellationToken.None);
                Assert.NotNull(result);
                Assert.False(result.IsSuccess);

                Assert.NotEmpty(result.ErrorMessage);
                if (string.IsNullOrEmpty(productDto.Name))
                {
                    Assert.Equal("Product name is required.", result.ErrorMessage);
                }
                else if (productDto.Price <= 0)
                {
                    Assert.Equal("Product price must be between 0.01 and 1000.", result.ErrorMessage);
                }
                else if (productDto.CategoryId <= 0 || productDto.CategoryId == 999)
                {
                    Assert.Equal("Category with ID 999 does not exist.", result.ErrorMessage);
                }
                
            }
        }
        #endregion

        #region update
        [Fact]
        public async Task Handle_ShouldUpdateProductAndReturnProductDto()
        {
            // Arrange
            var mockRepo = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product_GeneralInformations, Product>();
                cfg.CreateMap<Product, Product_GeneralInformations>();
            });
            var mapper = config.CreateMapper();

            var productDto = new Product_GeneralInformations { Id = 1, Name = "Laptop", Price = 1000, CategoryId = 1 };
            var existingProduct = new Product { Id = 1, Name = "Old Laptop", Price = 500, CategoryId = 1 };
            var updatedProduct = new Product { Id = 1, Name = "Laptop", Price = 1000, CategoryId = 1 };
            var category = new Category { Id = 1, Name = "Electronics" };

            mockRepo.Setup(repo => repo.Products.GetByIdAsync(1)).ReturnsAsync(existingProduct);
            mockRepo.Setup(repo => repo.Products.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(updatedProduct);
            mockRepo.Setup(repo => repo.Categories.GetByIdAsync(1)).ReturnsAsync(category);

            var handler = new UpdateProductCommandHandler(mockRepo.Object, mapper, NullLogger<UpdateProductCommandHandler>.Instance);
            var command = new UpdateProductCommand(productDto);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(productDto.Id, result.GeneralInformationsDto?.Id);
            Assert.Equal(productDto.Name, result.GeneralInformationsDto?.Name);
            Assert.Equal(productDto.Price, result.GeneralInformationsDto?.Price);
            Assert.Equal(productDto.CategoryId, result.GeneralInformationsDto?.CategoryId);

            mockRepo.Verify(r => r.Products.GetByIdAsync(1), Times.Once);
            mockRepo.Verify(r => r.Products.UpdateAsync(It.Is<Product>(p => p.Price == 1000 && p.Name == "Laptop")), Times.Once);
            mockRepo.Verify(r => r.Categories.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenProductDoesNotExist()
        {
            var mockRepo = new Mock<IUnitOfWork>();
            
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<ProductDto, Product>()));

            mockRepo.Setup(r => r.Products.GetByIdAsync(1)).ReturnsAsync((Product)null);

            var handler = new UpdateProductCommandHandler(mockRepo.Object, mapper, NullLogger<UpdateProductCommandHandler>.Instance);
            var command = new UpdateProductCommand(new Product_GeneralInformations { Id = 1 });

            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsSuccess);
            Assert.Equal("Product with key 1 was not found.", result.ErrorMessage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(1500)]
        public async Task Handle_ShouldThrowValidationException_WhenPriceIsInvalid(decimal price)
        {
            var productDto = new Product_GeneralInformations { Id = 1, Name = "Laptop", Price = price, CategoryId = 1 };
            var product = new Product { Id = 1, Name = "Laptop", Price = 500, CategoryId = 1 };
            var category = new Category { Id = 1, Name = "Electronics" };

            var mockRepo = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            mockRepo.Setup(r => r.Products.GetByIdAsync(1)).ReturnsAsync(product);
            mockRepo.Setup(r => r.Categories.GetByIdAsync(1)).ReturnsAsync(category);
            mockMapper.Setup(m => m.Map(productDto, product)).Callback(() => product.Price = price);

            var handler = new UpdateProductCommandHandler(mockRepo.Object, mockMapper.Object, NullLogger<UpdateProductCommandHandler>.Instance);
            var result = await handler.Handle(new UpdateProductCommand(productDto), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal("Product price must be between 0.01 and 1000.", result.ErrorMessage);

        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenProductNameIsEmpty()
        {
            var productDto = new Product_GeneralInformations { Id = 1, Name = "", Price = 800, CategoryId = 1 };
            var product = new Product { Id = 1, Name = "Laptop", Price = 500, CategoryId = 1 };
            var category = new Category { Id = 1, Name = "Electronics" };

            var mockRepo = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            mockRepo.Setup(r => r.Products.GetByIdAsync(1)).ReturnsAsync(product);
            mockRepo.Setup(r => r.Categories.GetByIdAsync(1)).ReturnsAsync(category);
            mockMapper.Setup(m => m.Map(productDto, product)).Callback(() => product.Name = productDto.Name);

            var handler = new UpdateProductCommandHandler(mockRepo.Object, mockMapper.Object, NullLogger<UpdateProductCommandHandler>.Instance);

            var result = await handler.Handle(new UpdateProductCommand(productDto), CancellationToken.None);
            Assert.False(result.IsSuccess);
            Assert.Equal("Product name is required.", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCategoryIdIsInvalid()
        {
            var productDto = new Product_GeneralInformations { Id = 1, Name = "Laptop", Price = 800, CategoryId = 0 };
            var product = new Product { Id = 1, Name = "Laptop", Price = 500, CategoryId = 1 };

            var mockRepo = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            mockRepo.Setup(r => r.Products.GetByIdAsync(1)).ReturnsAsync(product);
            mockMapper.Setup(m => m.Map(productDto, product)).Callback(() => product.CategoryId = 0);

            var handler = new UpdateProductCommandHandler(mockRepo.Object, mockMapper.Object, NullLogger<UpdateProductCommandHandler>.Instance);

            var result = await handler.Handle(new UpdateProductCommand(productDto), CancellationToken.None);
            Assert.False(result.IsSuccess);
            Assert.Equal("Category ID must be greater than 0.", result.ErrorMessage);
            
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCategoryNotFound()
        {
            var productDto = new Product_GeneralInformations { Id = 1, Name = "Laptop", Price = 800, CategoryId = 2 };
            var product = new Product { Id = 1, Name = "Laptop", Price = 500, CategoryId = 1 };

            var mockRepo = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            mockRepo.Setup(r => r.Products.GetByIdAsync(1)).ReturnsAsync(product);
            mockRepo.Setup(r => r.Categories.GetByIdAsync(2)).ReturnsAsync((Category)null);
            mockMapper.Setup(m => m.Map(productDto, product)).Callback(() => product.CategoryId = 2);

            var handler = new UpdateProductCommandHandler(mockRepo.Object, mockMapper.Object, NullLogger<UpdateProductCommandHandler>.Instance);

            var result = await handler.Handle(new UpdateProductCommand(productDto), CancellationToken.None);
            Assert.False(result.IsSuccess);
            Assert.Equal("Category with ID 2 does not exist.", result.ErrorMessage);
        }
        #endregion

        #region delete
        
        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenProductExists()
        {
            // Arrange
            var productId = 1;
            var product = new Product { Id = productId };

            var mockRepo = new Mock<IUnitOfWork>();
            var mockWebHostRepo = new Mock<IWebHostEnvironment>();
            mockRepo.Setup(r => r.Products.GetByIdAsync(productId)).ReturnsAsync(product);
            mockRepo.Setup(r => r.Products.DeleteAsync(product)).ReturnsAsync(product);

            var handler = new DeleteProductCommandHandler(mockRepo.Object, mockWebHostRepo.Object, NullLogger<DeleteProductCommandHandler>.Instance);

            // Act
            var result = await handler.Handle(new DeleteProductCommand(productId), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            mockRepo.Verify(r => r.Products.DeleteAsync(product), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = 99;

            var mockRepo = new Mock<IUnitOfWork>();
            var mockWebHostRepo = new Mock<IWebHostEnvironment>();
            mockRepo.Setup(r => r.Products.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            var handler = new DeleteProductCommandHandler(mockRepo.Object, mockWebHostRepo.Object, NullLogger<DeleteProductCommandHandler>.Instance);

            // Act
            var result = await handler.Handle(new DeleteProductCommand(productId), CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRepositoryThrows()
        {
            // Arrange
            var productId = 1;

            var mockRepo = new Mock<IUnitOfWork>();
            var mockWebHostRepo = new Mock<IWebHostEnvironment>();
            mockRepo.Setup(r => r.Products.GetByIdAsync(productId)).ThrowsAsync(new Exception("DB error"));

            var handler = new DeleteProductCommandHandler(mockRepo.Object, mockWebHostRepo.Object, NullLogger<DeleteProductCommandHandler>.Instance);
            // Act
            var result = await handler.Handle(new DeleteProductCommand(productId), CancellationToken.None);
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("An_unexpected_error_occurred_while_deleting_the_product", result.ErrorMessage);

        }
        #endregion

        #region get by id
        [Fact]
        public async Task Handle_ShouldReturnProductDto_WhenProductExists()
        {
            // Arrange
            var productId = 1;
            var product = new Product { Id = productId, Name = "Laptop", Price = 1000, CategoryId = 1 };
            var productDto = new ProductDto { Id = productId, Name = "Laptop", Price = 1000, CategoryId = 1 };

            var mockRepo = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            mockRepo.Setup(r => r.Products.GetByIdAsync(productId)).ReturnsAsync(product);
            mockMapper.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);

            var handler = new GetProductByIdQueryHandler(mockRepo.Object, mockMapper.Object, NullLogger<GetProductByIdQueryHandler>.Instance);
            var query = new GetProductByIdQuery(productId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(productDto.Id, result.ProductDto.Id);
            Assert.Equal(productDto.Name, result.ProductDto.Name);
            Assert.Equal(productDto.Price, result.ProductDto.Price);

            mockRepo.Verify(r => r.Products.GetByIdAsync(productId), Times.Once);
            mockMapper.Verify(m => m.Map<ProductDto>(product), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = 99;

            var mockRepo = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            mockRepo.Setup(r => r.Products.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            var handler = new GetProductByIdQueryHandler(mockRepo.Object, mockMapper.Object, NullLogger<GetProductByIdQueryHandler>.Instance);
            var query = new GetProductByIdQuery(productId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.ProductDto);
            mockRepo.Verify(r => r.Products.GetByIdAsync(productId), Times.Once);
            mockMapper.Verify(m => m.Map<Product_GeneralInformations>(It.IsAny<Product>()), Times.Never);
        }
        #endregion

        #region getAll
        [Fact]
        public async Task Handle_ShouldReturnListOfProductDtos_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 1000, CategoryId = 1 },
        new Product { Id = 2, Name = "Phone", Price = 500, CategoryId = 2 }
    };

            var productDtos = new List<ProductDto>
    {
        new ProductDto { Id = 1, Name = "Laptop", Price = 1000, CategoryId = 1 },
        new ProductDto { Id = 2, Name = "Phone", Price = 500, CategoryId = 2 }
    };

            var mockRepo = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            mockRepo.Setup(r => r.Products.GetAllAsync(0,0)).ReturnsAsync(() => (products, products.Count));
            mockMapper.Setup(m => m.Map<List<ProductDto>>(products)).Returns(productDtos);

            var handler = new GetAllProductsQueryHandler(mockRepo.Object, mockMapper.Object, NullLogger<GetAllProductsQueryHandler>.Instance);
            var query = new GetAllProductsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(productDtos.Count, result.ProductsDto?.Count);
            Assert.Equal(productDtos[0].Name, result.ProductsDto?[0].Name);

            mockRepo.Verify(r => r.Products.GetAllAsync(0,0), Times.Once);
            mockMapper.Verify(m => m.Map<List<ProductDto>>(products), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            var mockRepo = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            mockRepo.Setup(r => r.Products.GetAllAsync(0,0)).ReturnsAsync(() => (new List<Product>(), 0));
            mockMapper.Setup(m => m.Map<List<ProductDto>>(It.IsAny<List<Product>>())).Returns(new List<ProductDto>());

            var handler = new GetAllProductsQueryHandler(mockRepo.Object, mockMapper.Object, NullLogger<GetAllProductsQueryHandler>.Instance);
            var query = new GetAllProductsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Empty(result.ProductsDto);

            mockRepo.Verify(r => r.Products.GetAllAsync(0,0), Times.Once);
            mockMapper.Verify(m => m.Map<List<ProductDto>>(It.IsAny<List<Product>>()), Times.AtMost(1));
        }
        #endregion
    }
}