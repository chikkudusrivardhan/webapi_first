using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using webapi_first.Database;
using webapi_first.Dtos.Character;
using webapi_first.Models;

namespace webapi_first.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContext)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContext;
        }
        
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.user = await _context.Users
                .FirstOrDefaultAsync(c => c.Id == GetUserId());
            _context.Characters.Add(character);
            

            await _context.SaveChangesAsync();
            //characters.Add(_mapper.Map<Character>(newCharactcer));
            serviceResponse.Data = _context.Characters.Where(c => c.user.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => c.user!.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && c.user!.Id == GetUserId());
            //var character = characters.FirstOrDefault(c => c.Id == id);
            
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse; 

        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var dbCharacter = await _context.Characters.Include(c => c.user).FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if(dbCharacter is null || dbCharacter.user!.Id != GetUserId())
                {
                    throw new Exception($"The User does not exist");
                }
                //_mapper.Map<Character>(updatedCharacter);

                dbCharacter.Name = updatedCharacter.Name;
                dbCharacter.HitPoints = updatedCharacter.HitPoints;
                dbCharacter.Strength = updatedCharacter.Strength;
                dbCharacter.Defence = updatedCharacter.Defence;
                dbCharacter.Intelligence = updatedCharacter.Intelligence;
                dbCharacter.Class = updatedCharacter.Class;
                
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            }
            catch (Exception ex) { 
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var dbCharacter = await _context.Characters.FirstAsync(c => c.Id == id && c.user!.Id == GetUserId());
                //var character = characters.First(c => c.Id == id);
                if (dbCharacter is null)
                {
                    throw new Exception($"Character with Id '{id}' not found");
                }

                _context.Characters.Remove(dbCharacter);

                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Characters
                    .Where(c => c.user!.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try 
            {
                var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId &&
                    c.user!.Id == GetUserId());
                if(character is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
                    return serviceResponse;
                }

                var skill = await _context.Skills
                    .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.SkillId);
                if(skill is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Skill not found";
                    return serviceResponse;
                }

                character.Skills!.Add(skill);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
