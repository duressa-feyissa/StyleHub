import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../entities/product/image_entity.dart';
import '../../repositories/shop.dart';

class GetShopProductsImageUseCase extends UseCase<List<ImageEntity>, Params> {
  final ShopRepository repository;

  GetShopProductsImageUseCase(this.repository);

  @override
  Future<Either<Failure, List<ImageEntity>>> call(Params params) async {
    return await repository.getShopProductsImages(
      shopId: params.shopId,
      skip: params.skip,
      limit: params.limit,
    );
  }
}

class Params extends Equatable {
  final String shopId;
  final int? skip;
  final int? limit;

  const Params({
    required this.shopId,
    this.skip = 0,
    this.limit = 15,
  });

  @override
  List<Object> get props => [shopId];
}
