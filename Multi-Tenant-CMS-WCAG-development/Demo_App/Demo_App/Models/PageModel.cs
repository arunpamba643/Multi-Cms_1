using Demo_App.Services.Pages;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace Demo_App.Models;

public partial class PageModel
{
    public int Id { get; set; }

    public int TenantId { get; set; }
    [Required(ErrorMessage = "Page Title is Mandatory")]
    [MinLength(3)]
    public string PageTitle { get; set; } = null!;
    public bool? IsHomePage { get; set; }
    [Required(ErrorMessage = "Resource Path is Mandatory")]
    public string ResourcePath { get; set; }
    public int MenuId { get; set; }
    public bool? IsLandingPage { get; set; }
    public DateTime? PublishStartDate { get; set; }
    [Required(ErrorMessage = "Scope is Mandatory")]
    public string? PageScopeType { get; set; }
    public DateTime? PublishEndDate { get; set; }
    public string MenuName { get; internal set; }
}

//public class CheckIfResourcePathAvailbleAttribute : ValidationAttribute
//{
//    [Inject]
//    public IPageService pageService { get; set; }

//    private readonly int _tenantId;
//    public CheckIfResourcePathAvailbleAttribute(int tenantId)
//    {
//        _tenantId = tenantId;
//    }

//    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//    {
//        string? resourcePath = value == null ? "" : value.ToString();
//        var isExists = pageService.CheckIfResourcePathAvailable(_tenantId, resourcePath);

//        if (isExists)
//        {
//            return new ValidationResult(ErrorMessage);
//        }

//        return ValidationResult.Success;
//    }
//}