using System;
using System.Collections.Generic;

namespace Fixit.Models;

public partial class Customer
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? Rating { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User? User { get; set; }
}
