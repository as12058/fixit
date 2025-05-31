using System;
using System.Collections.Generic;

namespace Fixit.Models;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Professional> Professionals { get; set; } = new List<Professional>();
}
