using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        public OwnerController(ILoggerManager logger, IRepositoryWrapper repoWrapper, IMapper mapper)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            try
            {
                var owners = await _repoWrapper.Owner.GetAllOwners();
                _logger.LogInfo($"Returned all owners from database");
                var ownerResults = _mapper.Map<IEnumerable<OwnerDTO>>(owners);
                return Ok(ownerResults);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOwners action : {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnerByID(int id)
        {
            try
            {
                var owner = await _repoWrapper.Owner.GetOwnerById(id);
                if (owner == null)
                {
                    _logger.LogError($"Owner with id : {id} was not found in db.");
                    return NotFound();
                }
                _logger.LogInfo($"Returned owner with the id : {id} ");
                var ownerResult = _mapper.Map<OwnerDTO>(owner);
                return Ok(ownerResult);

            } catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside getOwnerByID action : {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("{id}/accounts", Name = "OwnerById")]
        public async Task<IActionResult> GetOwnerWithDetails(int id)
        {
            try
            {
                var owner = await _repoWrapper.Owner.GetOwnerWithDetails(id);
                if (owner == null)
                {
                    _logger.LogError($"Owner with id : {id} was not found in db.");
                    return NotFound();
                }
                var ownerResult = _mapper.Map<OwnerDTO>(owner);
                return Ok(ownerResult);
            } catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerWithAccounts action : {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromBody]OwnerForCreateDTO owner)
        {
            try
            {
                if (owner == null)
                {
                    _logger.LogError("Owner object sent from client is null");
                    return BadRequest("Owner object is empty");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object  sent from client");
                    return BadRequest("Invalid owner object ");
                }

                var ownerEntity =  _mapper.Map<Owner>(owner);

                _repoWrapper.Owner.CreateOwner(ownerEntity);
               await _repoWrapper.SaveAsync();

                var createdOwner = _mapper.Map<OwnerDTO>(ownerEntity);
                return CreatedAtRoute("OwnerById", new { id = createdOwner.id }, createdOwner);

            } catch (Exception ex)
            {
                _logger.LogError($"Something wnet wrong inisde CreateOwner action : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwner(int id, [FromBody] OwnerForUpdateDTO owner)
        {
            try
            {
                if (owner == null)
                {
                    _logger.LogError("Owner object sent from client is null");
                    return BadRequest("Owner object is empty");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object  sent from client");
                    return BadRequest("Invalid owner object ");
                }
                var ownerEntity = await _repoWrapper.Owner.GetOwnerById(id);
                if (ownerEntity == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(owner, ownerEntity);

                _repoWrapper.Owner.UpdateOwner(ownerEntity);
                await _repoWrapper.SaveAsync ();
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wnet wrong inisde UpdateOwner action : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{ownerId}")]
        public async Task<IActionResult> DeleteOwner(int ownerId)
        {
            try
            {
                var ownerEntity = await _repoWrapper.Owner.GetOwnerById(ownerId);
                if(ownerEntity == null)
                {
                    _logger.LogError($"Owner with id: {ownerId}, hasn't been found in db.");
                    return NotFound();
                }
                var clientAccounts = await _repoWrapper.Account.AccountsByOwner(ownerId);
                if (clientAccounts.Any())
                {
                    _logger.LogError($"Cannot delete owner with id: {ownerId}. It has related accounts. Delete those accounts first");
                    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");

                }
                _repoWrapper.Owner.DeleteOwner(ownerEntity);
               await _repoWrapper.SaveAsync();
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wnet wrong inisde DeleteOwner action : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
  
    }
}
