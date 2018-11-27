using GroupProjectBackend.Models.DB;
using GroupProjectBackend.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GroupProjectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                    Name = model.label,
                    Latitude = model.position.lat,
                    Longitude = model.position.lng,
                    Description = model.description,
                    IsPublic = model.isPublic,
                    FullAddress = model.address,
                };

                await _dbContext.Places.AddAsync(dbModel);
                await _dbContext.SaveChangesAsync();

                model.id = dbModel.Id;
                return Ok(model);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
