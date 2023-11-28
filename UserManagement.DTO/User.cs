using System.ComponentModel.DataAnnotations;

namespace UserManagement.DTO;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [MaxLength(64)]
    public string Password { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public UserProfile? UserProfile { get; set; }
}