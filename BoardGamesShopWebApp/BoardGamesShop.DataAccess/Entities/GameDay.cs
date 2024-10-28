using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesShop.DataAccess.Entities;

[Table("game_days")]
public class GameDay : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public int Duration { get; set; }
    public int MinAge { get; set; }
    public int PeopleMax { get; set; }
    public int Price { get; set; }
}