using GroupProjectBackend.Models.DB;
using GroupProjectBackend.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProjectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class PlaceController : ControllerBase
    {
        private readonly GroupProjectDbContext _dbContext;

        public PlaceController(GroupProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPut]
        public async Task<IActionResult> Add(PlaceDto model)
        {
            try
            {
                //TODO: validate model and handle categories

                var dbModel = new Place
                {
                    Name = model.Label,
                    Latitude = model.Position.Lat,
                    Longitude = model.Position.Lng,
                    Description = model.Description,
                    IsPublic = model.IsPublic,
                    FullAddress = model.Address,
                    CategoryId = model.Category.Id
                };

                await _dbContext.Places.AddAsync(dbModel);
                await _dbContext.SaveChangesAsync();

                model.Id = dbModel.Id;
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpGet("GetAllPlaces")]
        public async Task<IActionResult> GetAllPlaces()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpGet("GetOnePlace/{id}")]
        public async Task<IActionResult> GetOnePlace(int id)
        {
            try
            {
                var place = _dbContext.Places.Join(_dbContext.Categories,
                      p => p.CategoryId,
                      c => c.Id, (p, c) => new PlaceDto
                      {
                          Id = p.Id,
                          Label = p.Name,
                          Position = new PositionDto
                          {
                              Lat = p.Latitude,
                              Lng = p.Longitude
                          },
                          Description = p.Description,
                          IsPublic = p.IsPublic,
                          Address = p.FullAddress,
                          Category = new CategoryDto
                          {
                              Id = c.Id,
                              Name = c.Name,
                              Description = c.Description
                          }
                      }).Where(x => x.Id == id).Single();

                return Ok(place);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
