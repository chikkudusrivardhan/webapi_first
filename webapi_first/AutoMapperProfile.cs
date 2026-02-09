using AutoMapper;
using webapi_first.Dtos.Character;
using webapi_first.Dtos.Skill;
using webapi_first.Dtos.Weapon;
using webapi_first.Models;

namespace webapi_first
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}
