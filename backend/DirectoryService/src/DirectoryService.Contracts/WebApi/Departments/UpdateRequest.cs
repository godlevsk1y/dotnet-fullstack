namespace DirectoryService.Contracts.WebApi.Departments;

public record UpdateRequest(string Name, string Slug, Guid? ParentId);