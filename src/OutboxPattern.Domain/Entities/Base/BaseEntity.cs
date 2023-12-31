﻿using System.ComponentModel.DataAnnotations.Schema;

namespace OutboxPattern.Domain.Entities.Base;
public class BaseEntity
{
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}
