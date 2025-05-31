using System;
using System.Collections.Generic;

namespace Fixit.Models;

public partial class Professional
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? HourlyRate { get; set; }

    public string? Experience { get; set; }

    public int? Availability { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User? User { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
