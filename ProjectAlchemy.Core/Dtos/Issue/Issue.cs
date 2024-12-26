﻿using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Core.Dtos.Issue;

public class Issue
{
    public required int Id { get; set; }
    [Required(AllowEmptyStrings = false)]
    [StringLength(IssueService.MaxNameLength)]
    public required string Name { get; set; }
    [StringLength(IssueService.MaxDescriptionLength)]
    public string Description { get; private set; } = "";
    public required IssueType Type { get; set; }
    public required Lane Lane { get; set; }
}
