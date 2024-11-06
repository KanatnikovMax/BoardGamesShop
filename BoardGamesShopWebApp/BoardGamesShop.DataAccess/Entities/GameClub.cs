using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesShop.DataAccess.Entities;

[Table("game_clubs")]
public class GameClub : BaseEntity
{
    public string City { get; set; }
    public string Address { get; set; }
    public DateTime OpeningTime { get; set; }
    public DateTime ClosingTime { get; set; }
    
    public List<GameDay> GameDays { get; set; }
}