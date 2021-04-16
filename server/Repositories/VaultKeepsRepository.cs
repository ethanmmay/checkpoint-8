using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using server.Models;

namespace server.Repositories
{
    public class VaultKeepsRepository
    {

        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<VaultKeep> GetAll()
        {
            string sql = @"
       SELECT 
       vaultkeep.*,
       profile.* 
       FROM vaultkeeps vaultkeep 
       JOIN profiles profile ON vaultkeep.creatorId = profile.id;";
            return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultkeep, profile) =>
            {
                return vaultkeep;
            }, splitOn: "id");
        }

        internal VaultKeep Get(int id)
        {
            string sql = @"
       SELECT 
       vaultkeep.*,
       profile.* 
       FROM vaultkeeps vaultkeep 
       JOIN profiles profile ON vaultkeep.creatorId = profile.id
       WHERE vaultkeep.id = @id;";
            return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultkeep, profile) =>
            {
                return vaultkeep;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }


        internal int Create(VaultKeep newVaultKeep)
        {
            string sql = @"
            INSERT INTO vaultkeeps
            (creatorId, vaultId, keepId)
            VALUES
            (@creatorId, @vaultId, @keepId);
            SELECT LAST_INSERT_ID()";
            return _db.ExecuteScalar<int>(sql, newVaultKeep);
        }

        internal void Remove(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

        internal IEnumerable<KeepViewModel> GetByVaultId(int vaultId)
        {
            string sql = @"SELECT 
            k.*,
            vk.id as VaultKeepId
            FROM vaultkeeps vk
            JOIN keeps k ON k.id = vk.keepId
            WHERE vaultId = @vaultId;";
            return _db.Query<KeepViewModel>(sql, new { vaultId });
        }
    }
}