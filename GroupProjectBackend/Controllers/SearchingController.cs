using GroupProjectBackend.Models.DB;
using GroupProjectBackend.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GroupProjectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchingController : ControllerBase
    {
        private readonly GroupProjectDbContext _dbContext;
        public SearchingController(GroupProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Produces("application/json")]
        [HttpPost("SearchPlace")]
        public async Task<IActionResult> SearchPlace(SearchingDto model)
        {
            try
            {
                var query = _dbContext.Places.AsQueryable();

                if (!string.IsNullOrEmpty(model.Label))
                {
                    query = query.Where(x => x.Name.ToUpperInvariant().Contains(model.Label.ToUpperInvariant()));
                }
                if (!string.IsNullOrEmpty(model.FullAddress))
                {
                    query = query.Where(x => x.FullAddress.ToUpperInvariant().Contains(model.FullAddress.ToUpperInvariant()));
                }
                if (model.CategoryId != 0)
                {
                    query = query.Where(x => x.CategoryId == model.CategoryId);
                }
                if (model.AverageRating != 0)
                {
                    query = query.Where(x => x.AverageRating >= model.AverageRating && x.AverageRating < model.AverageRating + 1);
                }
                if (model.Distance > 0 && model.Lat != 0 && model.Lng != 0)
                {
                    var latKm = 110.57;
                    var lngKm = Math.Cos(model.Lat) * 111.32;
                    var minLat = model.Lat - model.Distance / latKm;
                    var maxLat = model.Lat + model.Distance / latKm;
                    var minLng = model.Lat - model.Distance / lngKm;
                    var maxLng = model.Lat + model.Distance / lngKm;
                    query = query.Where(p => (minLat <= p.Latitude && p.Latitude <= maxLat) && (minLng <= p.Longitude && p.Longitude <= maxLng));
                }
                var placeList = await query.ToListAsync();

                return Ok(placeList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Produces("application/json")]
        [HttpGet("SearchRoute")]
        public IActionResult SearchRoute()
        {
            return Ok();
        }
    }
}
