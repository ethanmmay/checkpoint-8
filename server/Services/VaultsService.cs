using System;
using System.Collections.Generic;
using System.Linq;
using server.Models;
using server.Repositories;

namespace server.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;
        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Vault> GetAll()
        {
            IEnumerable<Vault> vaults = _repo.GetAll();
            return vaults;
        }

        internal Vault Get(int id)
        {
            var data = _repo.Get(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }

        internal IEnumerable<Vault> GetByProfileId(string profileId)
        {
            IEnumerable<Vault> vaults = _repo.GetByProfileId(profileId);
            return vaults;
        }

        internal IEnumerable<Vault> GetPrivate(string accountId)
        {
            IEnumerable<Vault> vaults = _repo.GetPrivate(accountId);
            return vaults;
        }

        public Vault Create(Vault newVault)
        {
            newVault.id = _repo.Create(newVault);
            return newVault;
        }

        internal Vault Edit(Vault editVault, string userId)
        {
            Vault original = Get(editVault.id);
            if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Edit a Vault You did not Create"); }
            editVault.name = editVault.name == null ? original.name : editVault.name;
            return _repo.Edit(editVault);

        }


        internal string Delete(int id, string userId)
        {
            Vault original = Get(id);
            if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Delete a Vault You did not Create"); }
            _repo.Remove(id);
            return "successfully deleted";
        }

    }
}