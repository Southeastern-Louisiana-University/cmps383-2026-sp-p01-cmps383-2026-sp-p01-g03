using System.ComponentModel.DataAnnotations;

namespace Selu383.SP26.Api.Entities;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(64)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [MaxLength(128)]
    public string DisplayName { get; set; } = string.Empty;
}