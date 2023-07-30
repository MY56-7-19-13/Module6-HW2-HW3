using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.UnitTests.Services
{
    public class CatalogItemServiceTest
    {
        private readonly ICatalogItemService _catalogService;

        private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;

        private readonly CatalogItem _testItem = new CatalogItem()
        {
            Name = "Name",
            Description = "Description",
            Price = 1000,
            AvailableStock = 100,
            CatalogBrandId = 1,
            CatalogTypeId = 1,
            PictureFileName = "1.png"
        };

        public CatalogItemServiceTest()
        {
            _catalogItemRepository = new Mock<ICatalogItemRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            var testResult = 1;

            _catalogItemRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogService.Add(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            int? testResult = null;

            _catalogItemRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogService.Add(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            var testResult = new CatalogItem()
            {
                Name = _testItem.Name,
                Description = _testItem.Description,
                Price = _testItem.Price,
                AvailableStock = _testItem.AvailableStock,
                CatalogBrandId = _testItem.CatalogBrandId,
                CatalogTypeId = _testItem.CatalogTypeId,
                PictureFileName = _testItem.PictureFileName,
                Id = _testItem.Id,
            };

            _catalogItemRepository.Setup(s => s.Update(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogService.Update(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName, _testItem.Id);

            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Update_Failed()
        {
            CatalogItem testResult = null;

            _catalogItemRepository.Setup(s => s.Update(
             It.IsAny<string>(),
             It.IsAny<string>(),
             It.IsAny<decimal>(),
             It.IsAny<int>(),
             It.IsAny<int>(),
             It.IsAny<int>(),
             It.IsAny<string>(),
             It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogService.Update(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName, _testItem.Id);

            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var testResult = 1;

            _catalogItemRepository.Setup(s => s.Delete(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogService.Delete(_testItem.Id);
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Delete_Failed()
        {
            int? testResult = null;

            _catalogItemRepository.Setup(s => s.Delete(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogService.Delete(_testItem.Id);
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task GetItemById_Success()
        {
            var testResult = new CatalogItem()
            {
                Id = _testItem.Id
            };

            _catalogItemRepository.Setup(s => s.GetItemByIdAsync(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogService.GetItemByIdAsync(_testItem.Id);
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task GetItemById_Failed()
        {
            CatalogItem testResult = null;

            _catalogItemRepository.Setup(s => s.GetItemByIdAsync(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogService.GetItemByIdAsync(_testItem.Id);
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task GetItemsByBrandId_Success()
        {
            var testResult = new List<CatalogItem>()
            {
                new CatalogItem(){ CatalogBrandId = _testItem.CatalogBrandId }
            };

            _catalogItemRepository.Setup(s => s.GetItemsByBrandIdAsync(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogService.GetItemsByBrandIdAsync(_testItem.CatalogBrandId);
            Assert.Equal(testResult, result);
        }

        [Fact]
        public async Task GetItemsByBrandId_Failed()
        {
            List<CatalogItem> testResult = null;

            _catalogItemRepository.Setup(s => s.GetItemsByBrandIdAsync(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogService.GetItemsByBrandIdAsync(_testItem.CatalogBrandId);
            Assert.Equal(testResult, result);
        }

        [Fact]
        public async Task GetItemsByTypeId_Success()
        {
            var testResult = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Id = _testItem.Id,
                    Name = _testItem.Name,
                    Description = _testItem.Description,
                    Price = _testItem.Price,
                    AvailableStock = _testItem.AvailableStock,
                    CatalogBrandId = _testItem.CatalogBrandId,
                    CatalogTypeId = _testItem.CatalogTypeId,
                    PictureFileName = _testItem.PictureFileName,
                }

            };

            _catalogItemRepository.Setup(s => s.GetItemsByTypeIdAsync(
             It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogService.GetItemsByTypeIdAsync(_testItem.CatalogTypeId);
            Assert.Equal(testResult, result);
        }

        [Fact]
        public async Task GetItemsByTypeId_Failed()
        {
            List<CatalogItem> testResult = null;

            _catalogItemRepository.Setup(s => s.GetItemsByTypeIdAsync(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogService.GetItemsByTypeIdAsync(_testItem.CatalogTypeId);
            Assert.Equal(testResult, result);
        }

        [Fact]
        public async Task GetBrandItems_Success()
        {
            var testResult = new List<CatalogBrand>()
            {
                new CatalogBrand(){Id = 1, Brand = "Cup<T> Black Mercury"}
            };

            _catalogItemRepository.Setup(s => s.GetBrandItemsAsync()).ReturnsAsync(testResult);

            var result = await _catalogService.GetBrandItemsAsync();
            Assert.Equal(testResult, result);
        }

        [Fact]
        public async Task GetBrandItems_Failed()
        {
            List<CatalogBrand> testResult = null;

            _catalogItemRepository.Setup(s => s.GetBrandItemsAsync()).ReturnsAsync(testResult);

            var result = await _catalogService.GetBrandItemsAsync();
            Assert.Equal(testResult, result);
        }

        [Fact]
        public async Task GetTypeItems_Success()
        {
            var testResult = new List<CatalogType>()
            {
                new CatalogType(){ Id = 1, Type = "TypeScript"}
            };

            _catalogItemRepository.Setup(s => s.GetTypeItemsAsync()).ReturnsAsync(testResult);

            var result = await _catalogService.GetTypeItemsAsync();
            Assert.Equal(testResult, result);
        }

        [Fact]
        public async Task GetTypeItems_Failed()
        {
            List<CatalogType> testResult = null;

            _catalogItemRepository.Setup(s => s.GetTypeItemsAsync()).ReturnsAsync(testResult);

            var result = await _catalogService.GetTypeItemsAsync();
            Assert.Equal(testResult, result);
        }
    }
}
