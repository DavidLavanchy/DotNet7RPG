using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            characters.Add(newCharacter);
            serviceResponse.Data = characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            serviceResponse.Data = characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<Character>();
            var single = characters.FirstOrDefault(e => e.Id == id);

            serviceResponse.Data = single;
            return serviceResponse;
        }
    }
}