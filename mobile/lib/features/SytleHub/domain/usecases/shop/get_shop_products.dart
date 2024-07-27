import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/product_entity.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../repositories/shop.dart';

class GetShopProductUseCase extends UseCase<List<ProductEntity>, Params> {
  final ShopRepository repository;

  GetShopProductUseCase(this.repository);

  @override
  Future<Either<Failure, List<ProductEntity>>> call(Params params) async {
    return await repository.getShopProducts(
      shopId: params.shopId,
      sortBy: params.sortBy,
      sortOrder: params.sortOrder,
      skip: params.skip,
      limit: params.limit,
    );
  }
}

class Params extends Equatable {
  final String shopId;
  final String? sortBy;
  final String? sortOrder;
  final int? skip;
  final int? limit;

  const Params({
    required this.shopId,
    this.sortBy,
    this.sortOrder,
    this.skip = 0,
    this.limit = 15,
  });

  @override
  List<Object> get props => [shopId];
}