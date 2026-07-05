namespace DirectoryService.Contracts.WebApi.Departments;

public record CreateDepartmentResponse(Guid Id, string Name, string Slug, string Path, Guid? ParentId);