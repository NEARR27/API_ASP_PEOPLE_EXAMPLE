using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleApi.DTOS;
using PeopleApi.Models;
using PeopleApi.Services;
using PeopleApi.Validators;

namespace PeopleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        
        private IValidator<BeerIdDTO> _beerInsertValidator;
        private IValidator<BeerUpdateDTO> _beerUpdateValidator;
        private ICommonServices<BeerDTO, BeerIdDTO, BeerUpdateDTO> _beerServices;

        public BeerController(
            IValidator<BeerIdDTO> beerInsertValidator,
            IValidator<BeerUpdateDTO> beerUpdateValidator,
            [FromKeyedServices("beerService")]ICommonServices<BeerDTO, BeerIdDTO, BeerUpdateDTO> beerServices)
        {
           
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerServices = beerServices;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDTO>> Get() =>
            await _beerServices.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDTO>> GetById(int id)
        {
            var beerDTO = await _beerServices.GetById(id);

            return beerDTO == null ? NotFound(): Ok(beerDTO);
            
        }

        [HttpPost]
        public async Task<ActionResult<BeerDTO>> Add(BeerIdDTO beerIdDTO)
        {
            var validatorResult = await _beerInsertValidator.ValidateAsync(beerIdDTO);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            if (!_beerServices.Validate(beerIdDTO))
            {
                return BadRequest(_beerServices.Errors);
            }

            var beerDTO = await _beerServices.Add(beerIdDTO); 

            

            return CreatedAtAction(nameof(GetById), new {id = beerDTO.Id}, beerDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDTO>> Update(int id, BeerUpdateDTO beerUpdateDTO)
        {
            var vallidationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDTO);
            if(!vallidationResult.IsValid)
            {
                return BadRequest(vallidationResult.Errors);
            }

            if (!_beerServices.Validate(beerUpdateDTO))
            {
                return BadRequest(_beerServices.Errors);
            }

            var beerDTO = await _beerServices.Update(id, beerUpdateDTO);

          
            return beerDTO == null ? NotFound() : Ok(beerDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDTO>> Delete(int id)
        {
            var beerDTO = await _beerServices.Delete(id);

            return beerDTO == null ? NotFound() : Ok(beerDTO);

        }
    }
}
