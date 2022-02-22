using AutoMapper;
using Hatchery.BigBank.Data.Entities;
using Hatchery.BigBank.Data.Models;
using Hatchery.BigBank.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hatchery.BigBank.Controllers
{
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public CalculatorController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/[controller]")]
        public async Task<ActionResult<LoanRequest[]>> GetAll(bool includePartners = false)
        {
            try
            {
                var calculators = await _repository.GetAllCalculatorsAsync(includePartners);
                return _mapper.Map<LoanRequest[]>(calculators);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't get calculators");
            }
        }
        [HttpGet("api/[controller]/id/{id:int}")]
        public async Task<ActionResult<LoanRequest>> GetById(int id, bool includePartners = false) {
            try
            {
                var calculators = await _repository.GetCalculatorByIdAsync(id, includePartners);
                return _mapper.Map<LoanRequest>(calculators);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't get calculators");
            }
        }
        [HttpGet("api/[controller]/phone/{phoneNumber}")]
        public async Task<ActionResult<CalculatorModel[]>> GetByPhone(string phoneNumber, bool includePartners = false) {
            try
            {
                var calculators = await _repository.GetCalculatorByPhoneNumberAsync(phoneNumber, includePartners);
                return _mapper.Map<CalculatorModel[]>(calculators);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't get calculators");
            }
        }

        [HttpPost("api/[controller]/partner/{partnerId:int}")]
        public async Task<ActionResult<CalculatorModel>> Post(int partnerId, [FromBody] CalculatorModel model)
        {
            try
            {
                var existing = await _repository.GetCalculatorByIdAsync(model.CalculatorId);
                if (existing != null) return BadRequest($"Calculator with ID of ({model.CalculatorId})");
                var partner = await _repository.GetPartnerByIdAsync(partnerId);
                var calc = _mapper.Map<LoanRequest>(model);
                calc.Partner = partner;

                _repository.Add(calc);
                if (await _repository.SaveChangesAsync())
                    return Created($"/api/calculator/{calc.LoanRequestId}", _mapper.Map<CalculatorModel>(calc));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't create calculator");
            }
            return BadRequest();
        }
        [HttpPut("api/[controller]/id/{id:int}")]
        public async Task<ActionResult<CalculatorModel>> Put(int id, [FromBody] CalculatorModel model) {
            try
            {
                var oldCalc = await _repository.GetCalculatorByIdAsync(model.CalculatorId);
                if (oldCalc == null) return NotFound($"No calculator with ID of {model.CalculatorId}");
                _mapper.Map(model, oldCalc);

                if (await _repository.SaveChangesAsync())
                    return _mapper.Map<CalculatorModel>(oldCalc);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't create calculator");
            }
            return BadRequest();
        }
    }
}
