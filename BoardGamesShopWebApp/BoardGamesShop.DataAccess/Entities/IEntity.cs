namespace BoardGamesShop.DataAccess.Entities;

public interface IEntity
{
    public int Id { get; set; }
    
    public Guid ExternalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
}