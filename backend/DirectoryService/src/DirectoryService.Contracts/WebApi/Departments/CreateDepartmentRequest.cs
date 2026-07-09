namespace DirectoryService.Contracts.WebApi.Departments;

public record CreateDepartmentRequest(string Name, string Slug, IEnumerable<Guid> LocationIds, Guid? ParentId = null);