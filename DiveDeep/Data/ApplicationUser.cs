using DiveDeep.Models;
using Microsoft.AspNetCore.Identity;
namespace DiveDeep.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public List<Booking>? Bookings { get; set; } = new();
}
