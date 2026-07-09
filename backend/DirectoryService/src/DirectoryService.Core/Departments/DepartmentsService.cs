using DirectoryService.Contracts.WebApi.Departments;
using DirectoryService.Domain.Models;
using DirectoryService.Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Core.Departments;

public partial class DepartmentsService : IDepartmentsService
{
    private readonly IValidator<CreateDepartmentRequest> _createDepartmentRequestValidator;
    private readonly ILogger<DepartmentsService> _logger;
    private readonly IDepartmentsRepository _departmentsRepository;

    public DepartmentsService(
        IDepartmentsRepository departmentsRepository,
        IValidator<CreateDepartmentRequest> createDepartmentRequestValidator,
        ILogger<DepartmentsService> logger
    )
    {
        _createDepartmentRequestValidator = createDepartmentRequestValidator;
        _logger = logger;
        _departmentsRepository = departmentsRepository;
    }
    
    public async Task<DepartmentDto> CreateAsync(CreateDepartmentRequest dto, CancellationToken cancellationToken)
    {
        var validationResult = await _createDepartmentRequestValidator.ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        Department? parentDepartment = null;
        if (dto.ParentId is not null)
        {
            parentDepartment = await _departmentsRepository.GetByIdAsync(dto.ParentId.Value, cancellationToken) 
                               ?? throw new KeyNotFoundException($"Department with id {dto.ParentId} not found");
        }

        var slug = new Slug(dto.Slug);
        
        var department = new Department(dto.Name, slug, parentDepartment);

        var createdDepartmentId = await _departmentsRepository.AddAsync(department, cancellationToken);
        
        LogDepartmentCreated(createdDepartmentId);

        return new DepartmentDto(
            createdDepartmentId,
            department.Name,
            department.Slug,
            department.Path.Value,
            department.ParentId?.Value)
        ;
    }
    
    [LoggerMessage(
        Level = LogLevel.Information, 
        Message = "Department created with id {departmentId}")]
    private partial void LogDepartmentCreated(Guid departmentId);
}