using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.Features.Product_Features.Product.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Handlers.Queries;

public class GetTotalAnalyticsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetTotalAnalytics, ProductAnalyticResponse>
{
    public async Task<ProductAnalyticResponse> Handle(GetTotalAnalytics request, CancellationToken cancellationToken)
    {
        var totalFavorite = await unitOfWork.FavouriteProductRepository.Count(request.ProductId);
        var totalView = await unitOfWork.ProductViewRepository.GetTotalViewCountAsync(request.ProductId);
        var todayView = await unitOfWork.ProductViewRepository.GetTodayViewCountAsync(request.ProductId);
        var thisWeekView  =  await unitOfWork.ProductViewRepository.GetViewCountByWeekAsync(request.ProductId);
       var thisMothView  =  await unitOfWork.ProductViewRepository.GetViewCountByMonthAsync(request.ProductId);
       var thisYearView  =  await unitOfWork.ProductViewRepository.GetViewCountByYearAsync(request.ProductId);
       var totalContacted = await unitOfWork.ContactedProductRepository.GetProductContactCountAsync(request.ProductId);

       return new ProductAnalyticResponse
       {
            TotalContacted = totalContacted,
            TotalFavorites = totalFavorite,
            TotalViews = totalView,
            TodayViews = todayView,
            ThisMonthViews = thisMothView,
            ThisWeekViews = thisWeekView,
            ThisYearViews = thisYearView
       };
    }
}