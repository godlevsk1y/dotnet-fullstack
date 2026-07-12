using CSharpFunctionalExtensions;
using DirectoryService.Contracts.WebApi.Departments;
using DirectoryService.Core.Departments.Failures.Exceptions;
using DirectoryService.Core.Extensions;
using DirectoryService.Core.Locations;
using DirectoryService.Core.Locations.Failures.Exceptions;
using DirectoryService.Domain.Models;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Shared.Errors;
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
    
    public async Task<Result<DepartmentDto, Error>> CreateAsync(CreateDepartmentRequest dto, CancellationToken cancellationToken)
    {
        var validationResult = await _createDepartmentRequestValidator.ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToError();
        }

        List<Location> locations = [];
        foreach (var locationId in dto.LocationIds)
        {
            var location = await _locationsRepository.GetByIdAsync(locationId, cancellationToken);
            if (location is null)
            {
                return LocationErrors.NotFound(locationId);
            }
            
            locations.Add(location);
        }
        
        Department? parentDepartment = null;
        if (dto.ParentId is not null)
        {
            parentDepartment = await _departmentsRepository.GetByIdAsync(dto.ParentId.Value, cancellationToken);
            if (parentDepartment is null)
            {
                return DepartmentErrors.NotFound(dto.ParentId.Value);
            }
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

    public async Task<Result<Guid, Error>> UpdateAsync(Guid id, UpdateDepartmentRequest dto, CancellationToken cancellationToken)
    {
        var validationResult = await _updateDepartmentRequestValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToError();
        }

        var department = await _departmentsRepository.GetByIdWithParentAsync(id, cancellationToken);
        if (department is null)
        {
            return DepartmentErrors.NotFound(id);
        }

        if (dto.Name is not null)
        {
            department.Rename(dto.Name);
        }

        if (dto.Slug is not null)
        {
            department.ChangeSlug(new Slug(dto.Slug));
        }

        if (dto.ParentId == Guid.Empty)
        {
            department.SetParent(parent: null);
        }
        else if (dto.ParentId is not null)
        {
            var parentDepartment = await _departmentsRepository.GetByIdAsync(dto.ParentId.Value, cancellationToken);
            if (parentDepartment is null)
            {
                return DepartmentErrors.NotFound(dto.ParentId.Value);
            }
        
            department.SetParent(parentDepartment);
        }
        
        await _departmentsRepository.SaveAsync(cancellationToken);
        
        return department.Id.Value;
    }

    public async Task<UnitResult<Error>> AddLocationAsync(Guid departmentId, Guid locationId, CancellationToken cancellationToken)
    {
        var department = await _departmentsRepository.GetByIdAsync(departmentId, cancellationToken);

        if (department is null)
        {
            return DepartmentErrors.NotFound(departmentId);
        }
        
        var location = await _locationsRepository.GetByIdAsync(locationId, cancellationToken);
        if (location is null)
        {
            return LocationErrors.NotFound(locationId);
        }

        var departmentLocation = new DepartmentLocation(department.Id, location.Id);

        if (await _departmentsRepository.HasDepartmentLocationAsync(departmentLocation, cancellationToken))
        {
            return DepartmentErrors.LocationAlreadyAdded(departmentId, locationId);
        }
        
        await _departmentsRepository.AddLocationAsync(departmentLocation, cancellationToken);
    }

    public async Task RemoveLocationAsync(Guid departmentId, Guid locationId, CancellationToken cancellationToken)
    {
        var departmentLocation = await _departmentsRepository
            .GetDepartmentLocation(departmentId, locationId, cancellationToken) ?? 
            throw new DepartmentLocationNotFoundException(departmentId, locationId);
        
        await _departmentsRepository.RemoveLocationAsync(departmentLocation, cancellationToken);
    }

    [LoggerMessage(
        Level = LogLevel.Information, 
        Message = "Department created with id {departmentId}")]
    private partial void LogDepartmentCreated(Guid departmentId);
}