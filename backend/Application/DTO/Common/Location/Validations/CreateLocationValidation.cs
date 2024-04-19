using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Application.DTO.Common.Location.DTO;
using FluentValidation;

namespace backend.Application.DTO.Common.Location.Validations
{
	public class CreateLocationValidation : AbstractValidator<CreateLocationDTO>
	{
		ILocationRepository  _locationRepository;

		public CreateLocationValidation(ILocationRepository locationRepository)
		{
			_locationRepository = locationRepository;

			RuleFor(x => x.Name)
				.NotNull().WithMessage("Name is required")
				.NotEmpty().WithMessage("Name cannot be empty")
				.Custom((name, context) => context.InstanceToValidate.Name = name.ToLower());

			RuleFor(x => x.Latitude)
				.NotNull().WithMessage("Latitude is required")
				.GreaterThanOrEqualTo(-90).WithMessage("Latitude must be greater than or equal to -90")
				.LessThanOrEqualTo(90).WithMessage("Latitude must be less than or equal to 90");
				
			RuleFor(x => x.Longitude)
				.NotNull().WithMessage("Longitude is required")
				.GreaterThanOrEqualTo(-180).WithMessage("Longitude must be greater than or equal to -180")
				.LessThanOrEqualTo(180).WithMessage("Longitude must be less than or equal to 180");

			RuleFor(x => x.Name)
				.MustAsync(async (name, cancellation) =>
				{
					var location = await _locationRepository.GetByName(name);
					return location == null;
				}).WithMessage("Location already exists");
				
			
		}

	}
}
