import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../entities/product/product_entity.dart';
import '../../repositories/product.dart';

class GetProductsUseCase extends UseCase<List<ProductEntity>, Params> {
  final ProductRepository repository;

  GetProductsUseCase(this.repository);

  @override
  Future<Either<Failure, List<ProductEntity>>> call(Params params) async {
    return await repository.getProducts(
      token: params.token,
      search: params.search,
      colorIds: params.colorIds,
      sizeIds: params.sizeIds,
      categoryIds: params.categoryIds,
      designIds: params.designIds,
      brandIds: params.brandIds,
      materialIds: params.materialIds,
      isNegotiable: params.isNegotiable,
      minPrice: params.minPrice,
      maxPrice: params.maxPrice,
      minQuantity: params.minQuantity,
      maxQuantity: params.maxQuantity,
      latitudes: params.latitudes,
      longitudes: params.longitudes,
      radiusInKilometers: params.radiusInKilometers,
      condition: params.condition,
      sortBy: params.sortBy,
      sortOrder: params.sortOrder,
      skip: params.skip,
      limit: params.limit,
    );
  }
}

class Params extends Equatable {
  final String token;
  final String? search;
  final List<String>? colorIds;
  final List<String>? sizeIds;
  final List<String>? categoryIds;
  final List<String>? brandIds;
  final List<String>? materialIds;
  final List<String>? designIds;
  final bool? isNegotiable;
  final double? minPrice;
  final double? maxPrice;
  final int? minQuantity;
  final int? maxQuantity;
  final double? latitudes;
  final double? longitudes;
  final double? radiusInKilometers;
  final String? condition;
  final String? sortBy;
  final String? sortOrder;
  final int? skip;
  final int? limit;

  const Params({
    required this.token,
    this.search,
    this.colorIds,
    this.sizeIds,
    this.categoryIds,
    this.brandIds,
    this.designIds,
    this.materialIds,
    this.isNegotiable,
    this.minPrice,
    this.maxPrice,
    this.minQuantity,
    this.maxQuantity,
    this.latitudes,
    this.longitudes,
    this.radiusInKilometers,
    this.condition,
    this.sortBy,
    this.sortOrder,
    this.skip,
    this.limit,
  });

  @override
  List<Object?> get props => [
        search,
        colorIds,
        sizeIds,
        categoryIds,
        brandIds,
        materialIds,
        isNegotiable,
        minPrice,
        maxPrice,
        designIds,
        minQuantity,
        maxQuantity,
        latitudes,
        longitudes,
        radiusInKilometers,
        condition,
        sortBy,
        sortOrder,
        token,
      ];
}
