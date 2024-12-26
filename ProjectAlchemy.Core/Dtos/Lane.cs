using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Core.Dtos;

public class Lane
{
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false)]
    [StringLength(LaneService.MaxNameLength)]
    public string Name { get; set; }
}