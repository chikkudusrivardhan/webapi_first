namespace webapi_first.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
        public Character? character { get; set; }
        public int CharacterId { get; set; }

    }
}
