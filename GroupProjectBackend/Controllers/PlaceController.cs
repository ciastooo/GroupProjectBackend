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
        public Task<IActionResult> Add(PlaceDto model)
        {
            //TODO: validate model
            var dbModel = new Place
            {
                Name = model.label,
                Latitude = model.position.lat,
                Longitude = model.position.lng,
                Description = model.description,
                IsPublic = model.isPublic,
                FullAddress = model.address,
            };
        }
    }
}
