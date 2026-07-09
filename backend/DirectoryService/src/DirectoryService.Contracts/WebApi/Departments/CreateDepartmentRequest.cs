namespace DirectoryService.Contracts.WebApi.Departments;

public record CreateDepartmentRequest(
    string Name,
    string Slug,
    IEnumerable<Guid>? LocationIds,
    Guid? ParentId = null)
{
    public CreateDepartmentRequest(string Name, string Slug, Guid? ParentId = null) 
        : this(Name, Slug, [], ParentId) { }
}