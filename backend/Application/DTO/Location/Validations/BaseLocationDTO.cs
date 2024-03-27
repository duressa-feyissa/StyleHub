using Application.DTO.LocationDTO.DTO;
using FluentValidation;
using Application.Contracts;

namespace Application.DTO.LocationDTO.Validations
{
	public class BaseLocationValidation : AbstractValidator<BaseLocationDTO>
	{
		ILocationRepository  _locationRepository;

		public BaseLocationValidation(ILocationRepository locationRepository)
		{
			_locationRepository = locationRepository;

			RuleFor(x => x.Name)
				.NotNull().WithMessage("Name is required")
				.NotEmpty().WithMessage("Name cannot be empty")
				.Custom((name, context) => context.InstanceToValidate.Name = name.ToLower());

			RuleFor(x => x.Latitude)
				.NotNull().WithMessage("Latitude is required");
				
			RuleFor(x => x.Longitude)
				.NotNull().WithMessage("Longitude is required");

			RuleFor(x => x.Name)
				.MustAsync(async (name, cancellation) =>
				{
					var location = await _locationRepository.GetByName(name);
					return location == null;
				}).WithMessage("Location already exists");
				
			
		}

	}
}
