﻿using System.ComponentModel.DataAnnotations;

namespace BoardGamesShop.DataAccess.Entities;

public abstract class BaseEntity : IEntity
{
    [Key]
    public int Id { get; set; }
    
    public Guid ExternalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
}