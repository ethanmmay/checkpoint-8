using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using server.Models;

namespace server.Repositories
{
    public class KeepsRepository
    {

        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Keep> GetAll()
        {
            string sql = @"
            SELECT 
            keep.*,
            profile.* 
            FROM keeps keep 
            JOIN profiles profile ON keep.creatorId = profile.Id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.creator = profile;
                return keep;
            }, splitOn: "id");
        }

        internal Keep Get(int id)
        {
            string sql = @"
            SELECT 
            keep.*,
            profile.* 
            FROM keeps keep 
            JOIN profiles profile ON keep.creatorId = profile.Id
            WHERE keep.id = @id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.creator = profile;
                return keep;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }


        internal int Create(Keep newKeep)
        {
            string sql = @"
            INSERT INTO keeps
            (name, creatorId, description, img, keeps, views, shares)
            VALUES
            (@name, @creatorId, @description, @img, @keeps, @views, 0);
            SELECT LAST_INSERT_ID()";
            return _db.ExecuteScalar<int>(sql, newKeep);
        }

        internal Keep Edit(Keep editData)
        {
            string sql = @"
            UPDATE keeps
            SET 
            name = @name,
            description = @description
            WHERE id = @id;";
            _db.Execute(sql, editData);
            return editData;
        }

        internal IEnumerable<Keep> GetByProfileId(string id)
        {
            string sql = @"
            SELECT
            keep.*,
            p.*
            FROM keeps keep
            JOIN profiles p ON p.id = keep.creatorId
            WHERE keep.creatorId = @id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.creator = profile;
                return keep;
            }, new { id }, splitOn: "id");
        }

        internal void Remove(int Id)
        {
            string sql = "DELETE FROM keeps WHERE Id = @Id LIMIT 1";
            _db.Execute(sql, new { Id });
        }

    }
}