using GroupProjectBackend.Models.DB;
using GroupProjectBackend.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

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
        public IActionResult SearchPlace(SearchingDto model)
        {
            var places = _dbContext.Places;
            IQueryable<Place> query = null;

            if (model.Label != "")
            {
                query = places.Where(x => x.Name == model.Label);
            }
            if (model.FullAddress != "")
            {
                query = places.Where(x => x.FullAddress == model.FullAddress);
            }
            if (model.CategoryId != 0)
            {
                query = places.Where(x => x.CategoryId == model.CategoryId);
            }
            if (model.AverageRating != 0)
            {
                query = places.Where(x => x.AverageRating >= model.AverageRating && x.AverageRating < model.AverageRating + 1);
            }
            var placeList = query.ToList();

            return Ok(placeList);
        }


        [Produces("application/json")]
        [HttpGet("SearchRoute")]
        public IActionResult SearchRoute()
        {
            return Ok();
        }
    }
}
