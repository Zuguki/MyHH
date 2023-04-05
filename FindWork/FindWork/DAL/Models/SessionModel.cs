using System;

namespace FindWork.DAL.Models;

public class SessionModel
{
    public Guid DbSessionId { get; set; } = Guid.Empty;
    public string? SessionData { get; set; } = null;
    public DateTime Created { get; set; }
    public DateTime? LastAccessed { get; set; }
    public int? UserId { get; set; } = null;
}