using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetRPG.DTOs.Character;
using DotNetRPG.Models;

namespace DotNetRPG.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character {Id = 1, Name = "Sam", Class = RpgClass.Mage},
            new Character {Id = 2, Name = "Frodo", Class = RpgClass.Knight}
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(e => e.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(e => _mapper.Map<GetCharacterDto>(e)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = characters.Select(e => _mapper.Map<GetCharacterDto>(e)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var single = characters.FirstOrDefault(e => e.Id == id);

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(single);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {

                var character = characters.FirstOrDefault(e => e.Id == newCharacter.Id);

                if (character is null)
                {
                    throw new Exception($"Character with Id '{newCharacter.Id}' not found");
                }

                character.Name = newCharacter.Name;
                character.HitPoints = newCharacter.HitPoints;
                character.Strength = newCharacter.Strength;
                character.Defense = newCharacter.Defense;
                character.Intelligence = newCharacter.Intelligence;
                character.Class = newCharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {

                var character = characters.First(e => e.Id == id);

                if (character is null)
                {
                    throw new Exception($"Character with Id '{id}' not found");
                }
                characters.Remove(character);

                serviceResponse.Data = characters.Select(e => _mapper.Map<GetCharacterDto>(e)).ToList();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}