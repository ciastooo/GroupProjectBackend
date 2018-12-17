using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProjectBackend.Models.DB;
using GroupProjectBackend.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroupProjectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly GroupProjectDbContext _dbContext;

        public RouteController(GroupProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPublicRoutes()
        {
            try
            {
                var routes = await _dbContext.Routes.Where(r => r.IsPublic).Select(r => new RouteDto
                {
                    Id = r.Id,
                    Description = r.Description,
                    Name = r.Name,
                    IsPublic = r.IsPublic,
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
                return Ok(routes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllUserRoutes(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest();
                }
                var routes = await _dbContext.Routes.Where(r => r.UserId == userId).Select(r => new RouteDto
                {
                    Id = r.Id,
                    Description = r.Description,
                    Name = r.Name,
                    IsPublic = r.IsPublic,
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
                return Ok(routes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> AddRoute(RouteDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var dbModel = new Route
                {
                    Description = model.Description,
                    Name = model.Name,
                    IsPublic = model.IsPublic,
                    RoutePlaces = new List<RoutePlace>()
                };

                for(int i = 0; i < model.Places.Count; i++)
                {
                    var dbRoutePolace = new RoutePlace
                    {
                        Order = i,
                        PlaceId = model.Places[i].Id,
                        Route = dbModel
                    };
                    dbModel.RoutePlaces.Add(dbRoutePolace);
                }

                await _dbContext.Routes.AddAsync(dbModel);
                await _dbContext.SaveChangesAsync();

                return Ok(dbModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dbModel = await _dbContext.Routes.Include(r => r.RoutePlaces).Where(r => r.Id == id).FirstOrDefaultAsync();
                if (dbModel == null)
                {
                    return NotFound();
                }

                _dbContext.RoutePlaces.RemoveRange(dbModel.RoutePlaces);
                _dbContext.Routes.Remove(dbModel);

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoute(RouteDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var dbModel = await _dbContext.Routes.Include(r => r.RoutePlaces).Where(r => r.Id == model.Id).FirstOrDefaultAsync();

                if (dbModel == null)
                {
                    return NotFound();
                }

                dbModel.Name = model.Name;
                dbModel.Description = model.Description;
                dbModel.IsPublic = model.IsPublic;

                _dbContext.RoutePlaces.RemoveRange(dbModel.RoutePlaces);

                for (int i = 0; i < model.Places.Count; i++)
                {
                    var dbRoutePolace = new RoutePlace
                    {
                        Order = i,
                        PlaceId = model.Places[i].Id,
                        Route = dbModel
                    };
                    dbModel.RoutePlaces.Add(dbRoutePolace);
                }

                await _dbContext.SaveChangesAsync();

                return Ok(dbModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}