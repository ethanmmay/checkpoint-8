
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        private readonly KeepsService _ks;

        public VaultsController(VaultsService vs, KeepsService ks)
        {
            _vs = vs;
            _ks = ks;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vault>> Get()
        {
            try
            {
                return Ok(_vs.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetById(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                Vault vault = _vs.Get(id);
                if (vault.isPrivate && userInfo.id != vault.creatorId)
                {
                    return BadRequest("Vault is private and you aren't the owner.");
                }
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

        [HttpGet("{vaultId}/keeps")]
        public ActionResult<IEnumerable<Keep>> GetKeeps(int vaultId)
        {
            try
            {
                Vault testVault = _vs.Get(vaultId);
                if (testVault.isPrivate)
                {
                    return BadRequest("This is a private vault");
                }
                return Ok(_ks.GetByVaultId(vaultId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> Post([FromBody] Vault newVault)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVault.creatorId = userInfo.id;
                Vault created = _vs.Create(newVault);
                created.creator = userInfo;
                return Ok(created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault editVault)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editVault.id = id;
                editVault.creator = userInfo;
                editVault.creatorId = userInfo.id;
                return Ok(_vs.Edit(editVault, userInfo.id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_vs.Delete(id, userInfo.id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}