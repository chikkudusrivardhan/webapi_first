using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using webapi_first.Database;
using webapi_first.Dtos.Character;
using webapi_first.Dtos.Weapon;
using webapi_first.Models;

namespace webapi_first.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper) {

            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var AddWeaponChar = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId &&
                    c.user!.Id == int.Parse(_httpContextAccessor.HttpContext!
                    .User.FindFirstValue(ClaimTypes.NameIdentifier)!));

                if (AddWeaponChar is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                    return serviceResponse;
                }

                var weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    character = AddWeaponChar

                };

                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(AddWeaponChar);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message =
                    ex.InnerException != null
                        ? ex.InnerException.Message
                        : ex.Message;
            }


            return serviceResponse;
        }
    }
}
