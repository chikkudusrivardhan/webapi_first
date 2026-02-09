using webapi_first.Dtos.Fight;
using webapi_first.Models;

namespace webapi_first.Services.Fights
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto weaponAttackDto);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto skillAttackDto);
        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto fightResultDto);
        Task<ServiceResponse<List<HighscoreDto>>> GetHighscore();
    }
}
