import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';
import 'package:style_hub/features/SytleHub/domain/entities/shop/review_entity.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../repositories/shop.dart';

class GetShopReviewUseCase extends UseCase<List<ReviewEntity>, Params> {
  final ShopRepository repository;

  GetShopReviewUseCase(this.repository);

  @override
  Future<Either<Failure, List<ReviewEntity>>> call(Params params) async {
    return await repository.getShopReviews(
      shopId: params.shopId,
      userId: params.userId,
      sortBy: params.sortBy,
      sortOrder: params.sortOrder,
      rating: params.rating,
      skip: params.skip,
      limit: params.limit,
    );
  }
}

class Params extends Equatable {
  final String shopId;
  final String? userId;
  final String? sortBy;
  final String? sortOrder;
  final int? rating;
  final int? skip;
  final int? limit;

  const Params({
    required this.shopId,
    this.userId,
    this.sortBy,
    this.sortOrder,
    this.rating,
    this.skip = 0,
    this.limit = 15,
  });

  @override
  List<Object> get props => [shopId];
}
