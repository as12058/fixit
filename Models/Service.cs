using System;
using System.Collections.Generic;

namespace Fixit.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? CategoryId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Professional> Professionals { get; set; } = new List<Professional>();
}
