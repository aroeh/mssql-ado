using System.ComponentModel.DataAnnotations;

namespace Mssql.Ado.Shared.Models;

public record Restuarant
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string CuisineType { get; set; } = string.Empty;

    public Uri? Website { get; set; }

    [Phone]
    public string Phone { get; set; } = string.Empty;

    public Location Address { get; set; } = new Location();
}
