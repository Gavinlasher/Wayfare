using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wayfare.Models;
using Wayfare.Services;

namespace Wayfare.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TripsController : ControllerBase
  {
    private readonly TripsService _ts;

    public TripsController(TripsService ts)
    {
      _ts = ts;
    }

    [HttpGet]
    public ActionResult<List<Trip>> GetAll()
    {
      try
      {
        List<Trip> trips = _ts.GetAll();
        return Ok(trips);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Trip>> Create([FromBody] Trip tripData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        tripData.CreatorId = userInfo.Id;
        Trip trip = _ts.Create(tripData);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Trip>> Edit([FromBody] Trip updates, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updates.Id = id;
        Trip updated = _ts.Edit(updates, userInfo);
        return Ok(updated);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Trip> GetById(int id)
    {
      try
      {
        Trip trip = _ts.GetById(id);
        return Ok(trip);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _ts.Remove(id, userInfo);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}