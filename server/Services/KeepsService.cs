using System;
using System.Collections.Generic;
using System.Linq;
using server.Models;
using server.Repositories;

namespace server.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        private readonly VaultKeepsRepository _vksr;
        public KeepsService(KeepsRepository repo, VaultKeepsRepository vk_repo)
        {
            _repo = repo;
            _vksr = vk_repo;
        }

        public IEnumerable<Keep> GetAll()
        {
            IEnumerable<Keep> keeps = _repo.GetAll();
            return keeps;
        }

        internal Keep Get(int id)
        {
            var data = _repo.Get(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }


        public Keep Create(Keep newKeep)
        {
            newKeep.id = _repo.Create(newKeep);
            return newKeep;
        }

        internal Keep Edit(Keep editKeep, string userId)
        {
            Keep original = Get(editKeep.id);
            if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Edit a Keep You did not Create"); }
            editKeep.name = editKeep.name == null ? original.name : editKeep.name;
            return _repo.Edit(editKeep);

        }

        internal string Delete(int id, string userId)
        {
            Keep original = Get(id);
            if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Delete a Keep You did not Create"); }
            _repo.Remove(id);
            return "successfully deleted";
        }

        internal IEnumerable<Keep> GetByProfileId(string profileId)
        {
            IEnumerable<Keep> keeps = _repo.GetByProfileId(profileId);
            return keeps;
        }

        internal IEnumerable<KeepViewModel> GetByVaultId(int vaultId)
        {
            return _vksr.GetByVaultId(vaultId);
        }
    }
}