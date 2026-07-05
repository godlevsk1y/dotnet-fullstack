namespace DirectoryService.Contracts.WebApi.Departments;

public record CreateRequest(string Name, string Slug, Guid? ParentId = null);