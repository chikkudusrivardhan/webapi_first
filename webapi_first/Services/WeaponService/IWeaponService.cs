using webapi_first.Dtos.Character;
using webapi_first.Dtos.Weapon;
using webapi_first.Models;

namespace webapi_first.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeaponDto);
    }
}
