using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using FluentAssertions;
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
    public class CatalogTypeServiceTest
    {
        private readonly ICatalogTypeService _catalogType;

        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;

        private readonly CatalogType _testItem = new CatalogType()
        {
            Type = "Type"
        };
        public CatalogTypeServiceTest()
        {
            _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            
            _logger = new Mock<ILogger<CatalogService>>();

            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogType = new CatalogTypeService(_dbContextWrapper.Object, _logger.Object, _catalogTypeRepository.Object);
        }

        [Fact]
        public async Task Add_Success()
        {
            var testResult = 1;

            _catalogTypeRepository.Setup(s => s.Add(
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogType.Add(_testItem.Type);
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Add_Failed()
        {
            int? testResult = null;

            _catalogTypeRepository.Setup(s => s.Add(
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogType.Add(_testItem.Type);
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Update_Success()
        {
            var testResult = new CatalogType()
            {
                Id = _testItem.Id,
                Type = _testItem.Type,
            };

            _catalogTypeRepository.Setup(s => s.Update(
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogType.Update(_testItem.Id, _testItem.Type);
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Update_Failed()
        {
            CatalogType testResult = null;

            _catalogTypeRepository.Setup(s => s.Update(
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogType.Update(_testItem.Id, _testItem.Type);
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var testResult = 1;

            _catalogTypeRepository.Setup(s => s.Delete(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogType.Delete(_testItem.Id);
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Delete_Failed()
        {
            int? testResult = null;

            _catalogTypeRepository.Setup(s => s.Delete(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogType.Delete(_testItem.Id);
            result.Should().Be(testResult);
        }
    }
}
