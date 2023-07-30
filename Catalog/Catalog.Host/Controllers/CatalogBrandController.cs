using System.Net;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests.BrandRequest;
using Catalog.Host.Models.Response.BrandResponse;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBrandController> _logger;
    private readonly ICatalogBrandService _catalogBrandService;

    public CatalogBrandController(
        ILogger<CatalogBrandController> logger,
        ICatalogBrandService catalogBrandService)
    {
        _logger = logger;
        _catalogBrandService = catalogBrandService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateBrandRequest request)
    {
        var result = await _catalogBrandService.Add(request.Brand);
        return Ok(new AddBrandResponse<int?>() { Id = result });
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateBrandResponse<CatalogBrand>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateBrandRequest request)
    {
        var result = await _catalogBrandService.Update(request.Id, request.Brand);
        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(DeleteBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(DeleteBrandRequest request)
    {
        var result = await _catalogBrandService.Delete(request.Id);
        return Ok(result);
    }
}