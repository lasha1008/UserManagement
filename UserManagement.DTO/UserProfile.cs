using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.DTO;

public class UserProfile
{
    [Key]
    public int UserProfileId {  get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(11)]
    public string PersonalNumber { get; set; } = null!;


    [Required]
    public User User { get; set; } = null!;
}
