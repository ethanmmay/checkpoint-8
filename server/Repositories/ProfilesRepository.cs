using System;
using System.Collections.Generic;
using System.Data;
using server.Models;
using Dapper;

namespace server.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetById(string id)
    {
      string sql = "SELECT * FROM profiles WHERE id = @id";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }

    internal Profile Create(Profile newProfile)
    {
      string sql = @"
            INSERT INTO profiles
              (id, name, email, picture)
            VALUES
              (@id, @name, @email, @picture)";
      _db.Execute(sql, newProfile);
      return newProfile;
    }

    internal Profile Edit(Profile update)
    {
      string sql = @"
            UPDATE profiles
            SET 
              name = @name,
              picture = @picture
            WHERE id = @id;";
      _db.Execute(sql, update);
      return update;
    }
  }
}