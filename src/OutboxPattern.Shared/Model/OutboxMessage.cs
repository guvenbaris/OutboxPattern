using System.ComponentModel.DataAnnotations.Schema;

namespace OutboxPattern.Shared.Model;

[Table("outbox_message")]
public class OutboxMessage
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("type")]
    public string Type { get; set; } = string.Empty;

    [Column("content")]
    public string Content { get; set; } = string.Empty;

    [Column("processed_date")]
    public DateTime? ProcessedDate { get; set; }

    [Column("created_date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [Column("created_event_id")]
    public Guid CreatedEventId { get; set; }    

    [Column("error")]
    public string Error { get; set; } = string.Empty;

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
}
