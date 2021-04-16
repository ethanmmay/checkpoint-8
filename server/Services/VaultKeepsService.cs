using System;
using System.Collections.Generic;
using System.Linq;
using server.Models;
using server.Repositories;

namespace server.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        public VaultKeepsService(VaultKeepsRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<VaultKeep> GetAll()
        {
            IEnumerable<VaultKeep> vaults = _repo.GetAll();
            return vaults;
        }

        internal VaultKeep Get(int id)
        {
            var data = _repo.Get(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }


        public VaultKeep Create(VaultKeep newVaultKeep)
        {
            newVaultKeep.id = _repo.Create(newVaultKeep);
            return newVaultKeep;
        }

        internal string Delete(int id, string userId)
        {
            VaultKeep original = Get(id);
            if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Delete a VaultKeep You did not Create"); }
            _repo.Remove(id);
            return "successfully deleted";
        }

    }
}