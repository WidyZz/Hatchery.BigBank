using AutoMapper;
using Hatchery.BigBank.Data.Entities;
using Hatchery.BigBank.Data.Models;
using Hatchery.BigBank.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hatchery.BigBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public PartnerController(IRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<PartnerModel[]>> Get(bool includeCalculators = false)
        {
            try
            {
                var results = await _repository.GetAllPartnersAsync(includeCalculators);
                return _mapper.Map<PartnerModel[]>(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Partners");
            }
        }

        [HttpGet("id/{partnerId:int}")]
        public async Task<ActionResult<PartnerModel>> GetById(int partnerId, bool includeCalculators = false) {
            try
            {
                var results = await _repository.GetPartnerByIdAsync(partnerId, includeCalculators);
                return _mapper.Map<PartnerModel>(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Partner");
            }
        }

        [HttpGet("calc/{calcId:int}")]
        public async Task<ActionResult<PartnerModel>> GetByCalcId(int calcId) {
            try
            {
                var results = await _repository.GetPartnerByCalculatorIdAsync(calcId);
                return _mapper.Map<PartnerModel>(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Partner");
            }
        }

        [HttpGet("phone/{phoneNumber}")]
        public async Task<ActionResult<PartnerModel[]>> GetByPhoneNumber(string phoneNumber) {
            try
            {
                var results = await _repository.GetPartnersByPhoneNumberAsync(phoneNumber);
                if (results == null) return BadRequest();
                return _mapper.Map<PartnerModel[]>(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Partners");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PartnerModel>> Post([FromBody] PartnerModel model)
        {
            try
            {
                var exists = await _repository.GetPartnerByIBANAsync(model.IBAN);
                if (exists != null) return BadRequest($"Partner with IBAN of {model.IBAN} already exists.");
                var partner = _mapper.Map<Partner>(model);
                _repository.Add(partner);
                if (await _repository.SaveChangesAsync())
                    return Created($"/api/partner/{partner.PartnerId}", _mapper.Map<PartnerModel>(partner));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create Partner");
            }

            return BadRequest();
        }
        [HttpPut]
        public async Task<ActionResult<PartnerModel>> Put([FromBody] PartnerModel model) {
            try
            {
                var oldPartner = await _repository.GetPartnerByIBANAsync(model.IBAN);
                if (oldPartner == null) return NotFound($"Partner with IBAN of {model.IBAN} does not exist.");

                _mapper.Map(model, oldPartner);
                if (await _repository.SaveChangesAsync())
                    return _mapper.Map<PartnerModel>(oldPartner);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create Partner");
            }

            return BadRequest();
        }

        [HttpDelete("{partnerId:int}")]
        public async Task<ActionResult<PartnerModel>> Delete(int partnerId)
        {
            try
            {
                var oldPartner = await _repository.GetPartnerByIdAsync(partnerId);
                if (oldPartner == null) return NotFound();

                var partner = _mapper.Map<Partner>(oldPartner);
                if (partner.ClosedAt != null) return BadRequest("Partnership with this company was already closed.");

                partner.ClosedAt = DateTime.UtcNow;
                _mapper.Map(partner, oldPartner);

                if (await _repository.SaveChangesAsync())
                    return _mapper.Map<PartnerModel>(oldPartner);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to close Partnership");
            }

            return BadRequest();
        }
        [HttpDelete("{iban}")]
        public async Task<ActionResult<PartnerModel>> DeleteByIban(string iban) {
            try
            {
                var oldPartner = await _repository.GetPartnerByIBANAsync(iban);
                if (oldPartner == null) return NotFound();

                var partner = _mapper.Map<Partner>(oldPartner);
                if (partner.ClosedAt != null) return BadRequest("Partnership with this company was already closed.");

                partner.ClosedAt = DateTime.UtcNow;
                _mapper.Map(partner, oldPartner);

                if (await _repository.SaveChangesAsync())
                    return _mapper.Map<PartnerModel>(oldPartner);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to close Partnership");
            }

            return BadRequest();
        }
    }
}
