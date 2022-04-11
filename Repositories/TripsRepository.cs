using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Wayfare.Interfaces;
using Wayfare.Models;

namespace Wayfare.Repositories
{
  public class TripsRepository : IRepository<Trip, >
  {
    private readonly IDbConnection _db;

    public TripsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Trip Create(Trip data)
    {
      string sql = @"
    INSERT INTO trips
    (name,creatorId)
    VALUE
    (@Name,@CreatorId);
    SELECT LAST_INSERT_ID();
    ";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public string Delete(int id)
    {
      string sql = @"
        DELETE FROM trips WHERE id = @id LIMIT 1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "delorted";
      }

      void Edit(Trip og)
      {
        string sql = @"
      UPDATE trips
      SET
       name = @Name
      WHERE id = @Id;";
        _db.Execute(sql, og);
      }

      internal List<Trip> GetAll()
      {
        string sql = @"
      SELECT 
        t.*,
        a.* 
      FROM trpis t
      JOIN accounts a ON t.creatorId = a.id
      ";
        return _db.Query<Trip, Account, Trip>(sql, (t, a) =>
        {

          t.Creator = a;
          return t;
        }).ToList();
      }

      internal Trip GetById(int id)
      {
        string sql = "SELECT * FROM trips WHERE id = @id;";
        return _db.QueryFirstOrDefault<Trip>(sql, new { id });
      }
    }
  }