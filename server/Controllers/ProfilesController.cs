using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using server.Models;
using server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _service;
        private readonly VaultsService _vserv;
        private readonly KeepsService _kserv;

        public ProfilesController(ProfilesService service, VaultsService vserv, KeepsService kserv)
        {
            _service = service;
            _vserv = vserv;
            _kserv = kserv;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> Get(string id)
        {
            try
            {
                Profile profile = _service.GetProfileById(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        public ActionResult<IEnumerable<Vault>> GetVaultsByProfileId(string id)
        {
            try
            {
                IEnumerable<Vault> vaults = _vserv.GetByProfileId(id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<IEnumerable<Keep>> GetKeepsByProfileId(string id)
        {
            try
            {
                IEnumerable<Keep> keeps = _kserv.GetByProfileId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/privatevaults")]
        public ActionResult<IEnumerable<Vault>> GetPrivateVaults(string id)
        {
            try
            {
                IEnumerable<Vault> vaults = _vserv.GetPrivate(id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}