using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using server.Models;

namespace server.Repositories
{
    public class VaultsRepository
    {

        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Vault> GetAll()
        {
            string sql = @"
            SELECT 
            vault.*,
            profile.* 
            FROM vaults vault 
            JOIN profiles profile ON vault.creatorId = profile.id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.creator = profile;
                return vault;
            }, splitOn: "id");
        }

        internal Vault Get(int id)
        {
            string sql = @"
            SELECT 
            vault.*,
            profile.* 
            FROM vaults vault 
            JOIN profiles profile ON vault.creatorId = profile.id
            WHERE vault.id = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.creator = profile;
                return vault;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal IEnumerable<Vault> GetByProfileId(string id)
        {
            string sql = @"
            SELECT
            vault.*,
            p.*
            FROM vaults vault
            JOIN profiles p ON p.id = vault.creatorId
            WHERE vault.creatorId = @id AND vault.isPrivate = false;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.creator = profile;
                return vault;
            }, new { id }, splitOn: "id");
        }

        internal IEnumerable<Vault> GetPrivate(string id)
        {
            string sql = @"
            SELECT
            vault.*,
            p.*
            FROM vaults vault
            JOIN profiles p ON p.id = vault.creatorId
            WHERE vault.creatorId = @id";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.creator = profile;
                return vault;
            }, new { id }, splitOn: "id");
        }

        internal int Create(Vault newVault)
        {
            string sql = @"
            INSERT INTO vaults
            (name, creatorId, description, isPrivate)
            VALUES
            (@name, @creatorId, @description, @isPrivate);
            SELECT LAST_INSERT_ID()";
            return _db.ExecuteScalar<int>(sql, newVault);
        }

        internal Vault Edit(Vault editData)
        {
            string sql = @"
            UPDATE vaults
            SET 
            name = @name,
            description = @description
            WHERE id = @id;";
            _db.Execute(sql, editData);
            return editData;
        }

        internal void Remove(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

    }
}