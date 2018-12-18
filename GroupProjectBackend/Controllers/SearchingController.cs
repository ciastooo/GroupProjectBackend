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
        public async Task<IActionResult> SearchPlace(PlaceSearchingDto model)
        {
            try
            {
                var query = _dbContext.Places.Where(p => p.IsPublic);

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
                    var lngKm = 111.32;
                    var minLat = model.Lat - model.Distance / latKm;
                    var maxLat = model.Lat + model.Distance / latKm;
                    var minLng = model.Lng - model.Distance / lngKm;
                    var maxLng = model.Lng + model.Distance / lngKm;
                    query = query.Where(p => (minLat <= p.Latitude && p.Latitude <= maxLat) && (minLng <= p.Longitude && p.Longitude <= maxLng));
                }
                var placeList = await query.Select(p => new PlaceDto
                {
                    Id = p.Id,
                    UserId = p.UserRatings.Where(r => r.IsAddedByThisUser).FirstOrDefault().UserId,
                    Label = p.Name,
                    Description = p.Description,
                    IsPublic = p.IsPublic,
                    AverageRating = p.AverageRating,
                    Category = new CategoryDto
                    {
                        Description = p.Category.Description,
                        Id = p.CategoryId,
                        Name = p.Category.Name
                    },
                    Position = new PositionDto
                    {
                        Lng = p.Longitude,
                        Lat = p.Latitude
                    },
                    FullAddress = p.FullAddress
                }).ToListAsync();

                return Ok(placeList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Produces("application/json")]
        [HttpGet("SearchRoute")]
        public async Task<IActionResult> SearchRoute(SearchingDto model)
        {
            try
            {
                var query = _dbContext.Routes.AsQueryable();

                if (!string.IsNullOrEmpty(model.Label))
                {
                    query = query.Where(r => r.Name.ToUpperInvariant().Contains(model.Label.ToUpperInvariant()));
                }
                if (model.AverageRating != 0)
                {
                    query = query.Where(r => r.RoutePlaces.Any(rp => rp.Place.AverageRating >= model.AverageRating && rp.Place.AverageRating < model.AverageRating + 1));
                }

                var placeList = await query.Select(r => new RouteDto
                {
                    Id = r.Id,
                    Description = r.Description,
                    Name = r.Name,
                    IsPublic = r.IsPublic,
                    AverageRating = (float)r.UserRatings.Average(ur => ur.UserRating),
                    Places = r.RoutePlaces.OrderBy(rp => rp.Order).Select(rp => new PlaceDto
                    {
                        Id = rp.PlaceId,
                        Label = rp.Place.Name,
                        Position = new PositionDto
                        {
                            Lat = rp.Place.Latitude,
                            Lng = rp.Place.Longitude
                        },
                        Description = rp.Place.Description,
                        IsPublic = rp.Place.IsPublic,
                        FullAddress = rp.Place.FullAddress,
                        Category = new CategoryDto
                        {
                            Id = rp.Place.Category.Id,
                            Name = rp.Place.Category.Name,
                            Description = rp.Place.Category.Description
                        }
                    }).ToList()
                }).ToListAsync();

                return Ok(placeList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
