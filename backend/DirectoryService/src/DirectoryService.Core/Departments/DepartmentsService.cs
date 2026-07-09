using DirectoryService.Contracts.WebApi.Departments;
using DirectoryService.Core.Locations;
using DirectoryService.Domain.Models;
using DirectoryService.Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Core.Departments;

public partial class DepartmentsService : IDepartmentsService
{
    private readonly IDepartmentsRepository _departmentsRepository;
    private readonly ILocationsRepository _locationsRepository;
    
    private readonly IValidator<CreateDepartmentRequest> _createDepartmentRequestValidator;
    private readonly IValidator<UpdateDepartmentRequest> _updateDepartmentRequestValidator;

    private readonly ILogger<DepartmentsService> _logger;

    public DepartmentsService(
        IDepartmentsRepository departmentsRepository,
        ILocationsRepository locationsRepository,
        IValidator<CreateDepartmentRequest> createDepartmentRequestValidator,
        IValidator<UpdateDepartmentRequest> updateDepartmentRequestValidator,
        ILogger<DepartmentsService> logger
    )
    {
        _createDepartmentRequestValidator = createDepartmentRequestValidator;
        _updateDepartmentRequestValidator = updateDepartmentRequestValidator;
        _logger = logger;
        _departmentsRepository = departmentsRepository;
        _locationsRepository = locationsRepository;
    }
    
    public async Task<DepartmentDto> CreateAsync(CreateDepartmentRequest dto, CancellationToken cancellationToken)
    {
        var validationResult = await _createDepartmentRequestValidator.ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        List<Location> locations = [];
        foreach (var id in dto.LocationIds)
        {
            var location = await _locationsRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException($"Location with id {id} not found");
            
            locations.Add(location);
        }
        
        Department? parentDepartment = null;
        if (dto.ParentId is not null)
        {
            parentDepartment = await _departmentsRepository.GetByIdAsync(dto.ParentId.Value, cancellationToken) 
                               ?? throw new KeyNotFoundException($"Department with id {dto.ParentId} not found");
        }

        var slug = new Slug(dto.Slug);
        
        var department = new Department(dto.Name, slug, parentDepartment);

        var departmentLocations = locations.Select(l => new DepartmentLocation(department.Id, l.Id));
        
        var createdDepartmentId = await _departmentsRepository.AddAsync(
            department, 
            departmentLocations, 
            cancellationToken
        );
        
        LogDepartmentCreated(createdDepartmentId);

        return new DepartmentDto(
            createdDepartmentId,
            department.Name,
            department.Slug,
            department.Path.Value,
            department.ParentId?.Value
        );
    }

    public async Task<Guid> UpdateAsync(Guid id, UpdateDepartmentRequest dto, CancellationToken cancellationToken)
    {
        var validationResult = await _updateDepartmentRequestValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var department = await _departmentsRepository.GetByIdWithParentAsync(id, cancellationToken) 
                         ?? throw new KeyNotFoundException($"Department with id {id} not found");

        if (dto.Name is not null)
        {
            department.Rename(dto.Name);
        }

        if (dto.Slug is not null)
        {
            department.ChangeSlug(new Slug(dto.Slug));
        }

        if (dto.ParentId is not null)
        {
            if (string.IsNullOrEmpty(department.ParentId?.Value.ToString()))
            {
                department.SetParent(parent: null);
            }
            else
            {
                var parentDepartment = await _departmentsRepository.GetByIdAsync(dto.ParentId.Value, cancellationToken)
                                       ?? throw new KeyNotFoundException(
                                           $"Parent department with id {dto.ParentId} not found"
                                       );
                
                department.SetParent(parentDepartment);
            }
        }
        
        await _departmentsRepository.SaveAsync(cancellationToken);
        
        return department.Id;
    }

    [LoggerMessage(
        Level = LogLevel.Information, 
        Message = "Department created with id {departmentId}")]
    private partial void LogDepartmentCreated(Guid departmentId);
}