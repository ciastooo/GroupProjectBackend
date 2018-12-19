using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [Produces("application/json")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoute(int id)
        {
            try
            {
                var route = await _dbContext.Routes.Where(r => r.Id == id).Select(r => new RouteDto
                {
                    Id = r.Id,
                    Description = r.Description,
                    Name = r.Name,
                    IsPublic = r.IsPublic,
                    AverageRating = (float)r.UserRatings.Average(ur => ur.UserRating),
                    Places = r.RoutePlaces.OrderBy(rp => rp.Order).Select(rp => new PositionDto
                    {
                        Lat = rp.Latitude,
                        Lng = rp.Longitude
                    }).ToList()
                }).FirstOrDefaultAsync();
                return Ok(route);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json")]
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
                    AverageRating = (float)r.UserRatings.Average(ur => ur.UserRating),
                    Places = r.RoutePlaces.OrderBy(rp => rp.Order).Select(rp => new PositionDto
                    {
                        Lat = rp.Latitude,
                        Lng = rp.Longitude
                    }).ToList()
                }).ToListAsync();
                return Ok(routes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json")]
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
                    AverageRating = (float)r.UserRatings.Average(ur => ur.UserRating),
                    Places = r.RoutePlaces.OrderBy(rp => rp.Order).Select(rp => new PositionDto
                    {
                        Lat = rp.Latitude,
                        Lng = rp.Longitude
                    }).ToList()
                }).ToListAsync();
                return Ok(routes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> AddRoute(string userId, RouteDto model)
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
                    UserId = userId,
                    RoutePlaces = new List<RoutePlace>()
                };

                for (int i = 0; i < model.Places.Count; i++)
                {
                    var dbRoutePolace = new RoutePlace
                    {
                        Order = i,
                        Longitude = model.Places[i].Lng,
                        Latitude = model.Places[i].Lat,
                        Route = dbModel
                    };
                    dbModel.RoutePlaces.Add(dbRoutePolace);
                }

                await _dbContext.Routes.AddAsync(dbModel);
                await _dbContext.SaveChangesAsync();

                var dbRatingModel = new Rating
                {
                    UserId = dbModel.UserId,
                    Route = dbModel,
                    IsAddedByThisUser = true
                };
                await _dbContext.Ratings.AddAsync(dbRatingModel);
                await _dbContext.SaveChangesAsync();

                return Ok();
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
                var dbModel = await _dbContext.Routes.Include(r => r.RoutePlaces).Include(r => r.UserRatings).Where(r => r.Id == id).FirstOrDefaultAsync();
                if (dbModel == null)
                {
                    return NotFound();
                }

                _dbContext.Ratings.RemoveRange(dbModel.UserRatings);
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
                        Longitude = model.Places[i].Lng,
                        Latitude = model.Places[i].Lat,
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

        [HttpPost("AddUserRating/{userId}/{routeId}/{grade}")]
        public async Task<IActionResult> AddUserRating(string userId, int routeId, int grade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rating = await _dbContext.Ratings.Where(r => r.UserId == userId && r.Id == routeId).FirstOrDefaultAsync();

                    if (rating != null)
                    {
                        rating.UserRating = grade;
                    }
                    else
                    {
                        rating = new Rating
                        {
                            UserId = userId,
                            RouteId = routeId,
                            IsAddedByThisUser = false,
                            UserRating = grade
                        };
                        _dbContext.Ratings.Add(rating);
                    }

                    var dbRoute = await _dbContext.Routes.Where(r => r.Id == routeId).Include(p => p.UserRatings).FirstOrDefaultAsync();
                    dbRoute.AverageRating = dbRoute.UserRatings.Where(r => r.UserRating > 0).Average(r => r.UserRating);

                    await _dbContext.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddComment/{userId}/{routeId}/{comment}")]
        public async Task<IActionResult> AddComment(string userId, int routeId, string comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rating = await _dbContext.Ratings.Where(r => r.UserId == userId && r.Id == routeId).FirstOrDefaultAsync();

                    if (rating != null)
                    {
                        rating.Comment = comment;
                    }
                    else
                    {
                        var newRating = new Rating
                        {
                            UserId = userId,
                            RouteId = routeId,
                            IsAddedByThisUser = false,
                            Comment = comment
                        };
                        await _dbContext.Ratings.AddAsync(newRating);
                    }
                    await _dbContext.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Produces("application/json")]
        [HttpGet("GetFavouriteRoutes/{userId}")]
        public async Task<IActionResult> GetFavouriteRoutes(string userId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var routes = _dbContext.Ratings.Where(r => r.UserId == userId && r.RouteId.HasValue && r.IsFavourite)
                        .Select(r => new RouteDto
                        {
                            Id = r.RouteId.Value,
                            Description = r.Route.Description,
                            Name = r.Route.Name,
                            IsPublic = r.Route.IsPublic,
                            AverageRating = (float)r.Route.UserRatings.Average(ur => ur.UserRating),
                            Places = r.Route.RoutePlaces.OrderBy(rp => rp.Order).Select(rp => new PositionDto
                            {
                                Lat = rp.Latitude,
                                Lng = rp.Longitude
                            }).ToList()
                        }).ToListAsync();
                    return Ok(routes);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddRouteToFavourites/{userId}/{routeId}")]
        public async Task<IActionResult> AddToFavourites(string userId, int routeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rating = await _dbContext.Ratings.Where(r => r.UserId == userId && r.RouteId == routeId).FirstOrDefaultAsync();

                    if (rating != null)
                    {
                        rating.IsFavourite = true;
                    }
                    else
                    {
                        rating = new Rating
                        {
                            UserId = userId,
                            RouteId = routeId,
                            IsAddedByThisUser = false,
                            IsFavourite = true
                        };
                        _dbContext.Ratings.Add(rating);
                    }
                    _dbContext.SaveChanges();
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteRouteFromFavourites/{userId}/{routeId}")]
        public async Task<IActionResult> DeleteFromFavourites(string userId, int routeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rating = await _dbContext.Ratings.Where(r => r.UserId == userId && r.RouteId == routeId).FirstOrDefaultAsync();

                    if (rating != null)
                    {
                        rating.IsFavourite = false;
                        _dbContext.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}