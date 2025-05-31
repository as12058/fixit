using System;
using System.Collections.Generic;

namespace Fixit.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public int? ProfessionalId { get; set; }

    public int? ServiceId { get; set; }

    public int? ProfessionalRating { get; set; }

    public string? Address { get; set; }

    public int? CityId { get; set; }

    public DateOnly? StartDate { get; set; }

    public byte[] StartTime { get; set; } = null!;

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual City? City { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Professional? Professional { get; set; }

    public virtual Service? Service { get; set; }
}
