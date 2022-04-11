using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wayfare.Models;
using Wayfare.Repositories;

namespace Wayfare.Services
{
  public class TripsService
  {
    private readonly TripsRepository ts_repo;

    public TripsService(TripsRepository ts_repo)
    {
      this.ts_repo = ts_repo;
    }

    internal List<Trip> GetAll()
    {
      return ts_repo.GetAll();
    }

    internal Trip Create(Trip tripData)
    {
      return ts_repo.Create(tripData);
    }

    internal Trip Edit(Trip updates, Account userInfo)
    {
      Trip og = ts_repo.GetById(updates.Id);
      if (og.CreatorId != userInfo.Id)
      {
        throw new Exception("not yours to edit");
      }
      og.Name = updates.Name ?? og.Name;
      ts_repo.Edit(og);
      return og;
    }

    internal Trip GetById(int id)
    {
      Trip found = ts_repo.GetById(id);
      if (found == null)
      {
        throw new Exception("invaild id");
      }
      return found;
    }
    internal string Remove(int id, Account userInfo)
    {

    }
  }
}