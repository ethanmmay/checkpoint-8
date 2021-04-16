
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
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;

        public VaultKeepsController(VaultKeepsService vks)
        {
            _vks = vks;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VaultKeep>> Get()
        {
            try
            {
                return Ok(_vks.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<VaultKeep>> Get(int id)
        {
            try
            {
                return Ok(_vks.Get(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Post([FromBody] VaultKeep newVaultKeep)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVaultKeep.creatorId = userInfo.id;
                VaultKeep created = _vks.Create(newVaultKeep);
                return Ok(created);
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
                return Ok(_vks.Delete(id, userInfo.id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}