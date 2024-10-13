using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class IssuePatch
{
    [MaxLength(Issue.MaxNameLength)]
    public string Name { get; set; }
    [MaxLength(Issue.MaxDescriptionLength)]
    public string Description { get; set; }
    public IssueType Type { get; set; }
}