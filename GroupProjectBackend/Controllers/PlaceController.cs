using GroupProjectBackend.Models.DB;
using GroupProjectBackend.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;

namespace GroupProjectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly GroupProjectDbContext _dbContext;

        public PlaceController(GroupProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // TO DO: get my visited places -->potrzebne nam to wgl, czy można pominąć?
        [HttpPut("AddPlace")]
        public async Task<IActionResult> Add(PlaceDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dbPlaceModel = Mapper.Map<Place>(model);
                    await _dbContext.Places.AddAsync(dbPlaceModel);

                    var dbRatingModel = new Rating
                    {
                        UserId = model.UserId,
                        PlaceId = dbPlaceModel.Id,
                        IsAddedByThisUser = true
                    };
                    await _dbContext.Ratings.AddAsync(dbRatingModel);
                    await _dbContext.SaveChangesAsync();

                    model.Id = dbPlaceModel.Id;
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("EditPlace")]
        public async Task<IActionResult> Edit(PlaceDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dbPlaceModel = await _dbContext.Places.Where(p => p.Id == model.Id).FirstOrDefaultAsync();
                    if (dbPlaceModel == null)
                        return NotFound();

                    dbPlaceModel.Description = model.Description;
                    dbPlaceModel.Name = model.Label;
                    dbPlaceModel.Latitude = model.Position.Lat;
                    dbPlaceModel.Longitude = model.Position.Lng;
                    dbPlaceModel.IsPublic = model.IsPublic;
                    dbPlaceModel.FullAddress = model.FullAddress;

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

        [HttpDelete("DeletePlace/{placeId}")]
        public async Task<IActionResult> Delete(int placeId)
        {
            try
            {
                var dbPlaceModel = _dbContext.Places.Where(p => p.Id == placeId).FirstOrDefault();
                if (dbPlaceModel == null)
                    return NotFound();

                var dbPlaceRatings = _dbContext.Ratings.Where(r => r.PlaceId == placeId).ToList();
                dbPlaceRatings.ForEach(rating => _dbContext.Ratings.Remove(rating));

                _dbContext.Places.Remove(dbPlaceModel);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddPlaceToFavourites/{userId}/{placeId}")]
        public async Task<IActionResult> AddPlaceToFavourites(string userId, int placeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var placeRating = await _dbContext.Ratings.Where(r => r.UserId == userId && r.PlaceId == placeId).FirstOrDefaultAsync();

                    //means that user has earlier either added this place by himself or just added comment to this place
                    if (placeRating != null)
                    {
                        placeRating.IsFavourite = true;
                    }
                    else
                    {
                        var newPlaceRating = new Rating
                        {
                            UserId = userId,
                            PlaceId = placeId,
                            IsAddedByThisUser = false,
                            IsFavourite = true
                        };
                        _dbContext.Ratings.Add(newPlaceRating);
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

        [HttpPost("DeletePlaceFromFavourites/{userId}/{placeId}")]
        public async Task<IActionResult> DeletePlaceFromFavourites(string userId, int placeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var placeRating = _dbContext.Ratings.Where(r => r.UserId == userId && r.PlaceId == placeId).FirstOrDefault();

                    //means that user has earlier either added this place by himself or just added comment to this place
                    if (placeRating != null)
                    {
                        placeRating.IsFavourite = false;
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


        [HttpPost("AddUserRating/{userId}/{placeId}/{grade}")]
        public async Task<IActionResult> AddUserRating(string userId, int placeId, int grade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var placeRating = _dbContext.Ratings.Where(r => r.UserId == userId && r.PlaceId == placeId).FirstOrDefault();

                    //means that user has earlier either added this place by himself or just added comment to this place
                    if (placeRating != null)
                    {
                        placeRating.UserRating = grade;
                    }
                    else
                    {
                        var newPlaceRating = new Rating
                        {
                            UserId = userId,
                            PlaceId = placeId,
                            IsAddedByThisUser = false,
                            UserRating = grade
                        };
                        _dbContext.Ratings.Add(newPlaceRating);
                    }

                    var place = _dbContext.Places.Where(p => p.Id == placeId).SingleOrDefault();
                    place.AverageRating = (float)_dbContext.Ratings.Where(r => r.PlaceId == placeId).Average(ur => ur.UserRating);

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

        //jak robię jakikolwiek update na np.Ratings to chyba warto sprawdzić czy istnieje wgl miejsce lub trasa o takim Id?
        //i chyba dodawanie komentarzy do tras powinno być w routeController, mimo, że to będzie takie trochę powielenie kodu?
        [HttpPost("AddComment/{userId}/{placeId}/{comment}")]
        public async Task<IActionResult> AddComment(string userId, int placeId, string comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rating = new Rating();
                    rating = _dbContext.Ratings.Where(r => r.UserId == userId && r.PlaceId == placeId).FirstOrDefault();

                    //means that user has earlier either added this place by himself or added comment to this place or added it to fav
                    if (rating != null)
                    {
                        rating.Comment = comment;
                    }
                    else
                    {
                        var newRating = new Rating
                        {
                            UserId = userId,
                            PlaceId = placeId,
                            IsAddedByThisUser = false,
                            Comment = comment
                        };
                        _dbContext.Ratings.Add(newRating);
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


        [Produces("application/json")]
        [HttpGet("GetFavouritePlaces/{userId}")]
        public async Task<IActionResult> GetFavouritePlaces(string userId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var favPlaces = _dbContext.Ratings.Where(r => r.UserId == userId && r.IsFavourite == true)
                        .Select(r => new PlaceDto
                        {
                            Id = r.Place.Id,
                            UserId = r.UserId,
                            Label = r.Place.Name,
                        }).ToList();
                    return Ok(favPlaces);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Only to show this section "Places added by me". To get place data to place editing popup use function GetPlace.
        [Produces("application/json")]
        [HttpGet("GetMyPlaces/{userId}")]
        public async Task<IActionResult> GetMyPlaces(string userId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var myPlaces = _dbContext.Ratings.Where(r => r.UserId == userId && r.IsAddedByThisUser == true)
                        .Select(r => new PlaceDto
                        {
                            Id = r.Place.Id,
                            UserId = r.UserId,
                            Label = r.Place.Name
                        }).ToList();
                    return Ok(myPlaces);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpGet("GetPlace/{id}")]
        public async Task<IActionResult> GetPlace(int id) // get place model to edit and to show on PlaceCard
        {
            try
            {
                var place = _dbContext.Places.Where(p => p.Id == id).Select(p => new PlaceDto
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
                    FullAddress = p.FullAddress,
                    Category = new CategoryDto
                    {
                        Id = p.Category.Id,
                        Name = p.Category.Name,
                        Description = p.Category.Description
                    }
                }).FirstOrDefault();
                return Ok(place);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpGet("GetRating/{userId}/{placeId}")]
        public async Task<IActionResult> GetRating(string userId, int placeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var placeRating = _dbContext.Ratings.Where(r => r.PlaceId == placeId && r.UserId == userId)
                        .Select(r => new RatingDto
                        {
                            IsFavourite = r.IsFavourite,
                            UserRating = r.UserRating
                        }).FirstOrDefault();

                    //this user has neither added this place, nor commented it, nor given the rating to it.
                    if (placeRating == null)
                        placeRating = new RatingDto();
                    placeRating.Comments = _dbContext.Ratings.Where(r => r.PlaceId == placeId)
                        .Select(r => new CommentDto
                        {
                            UserName = r.User.Name,
                            Comment = r.Comment
                        }).ToList();
                    return Ok(placeRating);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = _dbContext.Categories.ToList();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
