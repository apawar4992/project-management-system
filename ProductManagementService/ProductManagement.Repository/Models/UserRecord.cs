using System;
using System.Collections.Generic;

namespace ProductManagement.Repository.Models;

public partial class UserRecord
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PhoneNumber { get; set; }
}
