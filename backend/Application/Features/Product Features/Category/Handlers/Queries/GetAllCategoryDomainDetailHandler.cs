using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.CategoryDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Category.Requests.Queries;
using MediatR;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Application.Features.Product_Features.Category.Handlers.Queries
{
    public class GetAllCategoryDomainDetailHandler : IRequestHandler<GetAllCategoryDomainDetail,
        Dictionary<string, Dictionary<string, List<CategoryShareResponseDTO>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCategoryDomainDetailHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Dictionary<string, Dictionary<string, List<CategoryShareResponseDTO>>>> Handle(GetAllCategoryDomainDetail request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll();
            if (categories == null)
            {
                throw new NotFoundException("No categories found");
            }

            var categoryResponse = new Dictionary<string, Dictionary<string, List<CategoryShareResponseDTO>>>();

            foreach (var category in categories)
            {
                var domains = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(category.Domain);
                if (domains != null)
                {
                    foreach (var domain in domains.Keys)
                    {
                        if (!categoryResponse.ContainsKey(domain))
                        {
                            categoryResponse[domain] = new Dictionary<string, List<CategoryShareResponseDTO>>();
                        }

                        foreach (var subLevel in domains[domain])
                        {
                            if (!categoryResponse[domain].ContainsKey(subLevel))
                            {
                                categoryResponse[domain][subLevel] = new List<CategoryShareResponseDTO>();
                            }

                            categoryResponse[domain][subLevel].Add(
                                new CategoryShareResponseDTO
                                {
                                    Id = category.Id,
                                    Name = category.Name,
                                    Image = category.Image
                                }
                            );
                        }
                    }
                }
            }

            return categoryResponse;
        }
    }
}
