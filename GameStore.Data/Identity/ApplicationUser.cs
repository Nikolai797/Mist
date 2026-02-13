using GameStore.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Data.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    
    public virtual ICollection<Order>? Orders { get; set; }
}