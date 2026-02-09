using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi_first.Dtos.Character;
using webapi_first.Models;
using webapi_first.Services.CharacterService;

namespace webapi_first.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {

        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")] 
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return await _characterService.GetAllCharacters();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetbyId(int id)
        {
            return await _characterService.GetCharacterById(id);
        }

        [HttpPost("AddCharacter")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            
            return await _characterService.AddCharacter(newCharacter);

        }

        [HttpPut("UpdateCharacter")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {

            return await _characterService.UpdateCharacter(updatedCharacter);

        }

        [HttpDelete("DeleteCharacter")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateCharacter(int id)
        {

            return await _characterService.DeleteCharacter(id);

        }

        [HttpPost("Add Character Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            return await _characterService.AddCharacterSkill(newCharacterSkill);
        }
    }
}
