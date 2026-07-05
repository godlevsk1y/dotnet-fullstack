namespace DirectoryService.Contracts.WebApi.Departments;

public record GetDepartmentResponse(Guid Id, string Name, string Slug, string Path, Guid? ParentId);