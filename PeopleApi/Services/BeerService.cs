using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeopleApi.DTOS;
using PeopleApi.Models;
using PeopleApi.Repository;

namespace PeopleApi.Services
{
    public class BeerService : ICommonServices<BeerDTO, BeerIdDTO, BeerUpdateDTO>
    {
        
        private IRepository<Beer> _beerRepository;
        private IMapper _mapper;
        public BeerService( IRepository<Beer> beerRepository, IMapper mapper)
        {
            
            _beerRepository = beerRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public List<string> Errors { get; }

        public async Task<IEnumerable<BeerDTO>> Get()
        {
            var beers = await _beerRepository.Get();

            return beers.Select(b => _mapper.Map<BeerDTO>(b)) ;
        }
        

        public async Task<BeerDTO> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);  
            if(beer != null)
            {
                var beerDTO = _mapper.Map<BeerDTO>(beer);

                return beerDTO;
            }
            return null;
        }
        public async Task<BeerDTO> Add(BeerIdDTO beerIdDTO)
        {
            var beer = _mapper.Map<Beer>(beerIdDTO);

            await _beerRepository.Add(beer);
            await _beerRepository.Saver();

            var beerDTO = _mapper.Map<BeerDTO>(beer);

            return beerDTO;
        }

        public async Task<BeerDTO> Update(int id, BeerUpdateDTO beerUpdateDTO)
        {
            var beer = await _beerRepository.GetById(id);

            if(beer != null)
            {
                beer = _mapper.Map<BeerUpdateDTO, Beer>(beerUpdateDTO, beer);

                _beerRepository.Update(beer);
                await _beerRepository.Saver();

                var beerDTO = _mapper.Map<BeerDTO>(beer);

                return beerDTO;

            }

            return null;
        }

        public async Task<BeerDTO> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDTO = _mapper.Map<BeerDTO>(beer);

                _beerRepository.Delete(beer);
                await _beerRepository.Saver() ;

               

                return beerDTO;

            }

            return null;
        }

        public bool Validate(BeerIdDTO beerIdDTO)
        {
            if(_beerRepository.Search(b => b.Name == beerIdDTO.Name).Count() > 0)
            {
                Errors.Add("No puede existir una cerveza con un nombre ya existente");
                return false;
            }

            return true;
        }

        public bool Validate(BeerUpdateDTO beerUpdateDTO)
        {
            if(_beerRepository.Search(b => b.Name == beerUpdateDTO.Name && beerUpdateDTO.Id != b.BeerId).Count() > 0)
            {
                Errors.Add("No puede existir una cerveza con un nombre ya existente");
                return false;
            }

            return true;
        }




    }
}
