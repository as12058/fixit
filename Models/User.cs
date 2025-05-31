using System;
using System.Collections.Generic;

namespace Fixit.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int? CityId { get; set; }

    public string? Address { get; set; }

    public string UserType { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Professional> Professionals { get; set; } = new List<Professional>();

    public string? GoogleId { get; set; } // ADD THIS LINE - make it nullable if not all users use Google
}
