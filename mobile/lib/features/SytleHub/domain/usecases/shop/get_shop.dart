import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';
import 'package:style_hub/core/errors/failure.dart';
import 'package:style_hub/features/SytleHub/domain/entities/shop/shop_entity.dart';
import 'package:style_hub/features/SytleHub/domain/repositories/shop.dart';

import '../../../../../core/use_cases/usecase.dart';

class GetShopUseCase extends UseCase<List<ShopEntity>, Params> {
  final ShopRepository repository;

  GetShopUseCase(this.repository);

  @override
  Future<Either<Failure, List<ShopEntity>>> call(Params params) async {
    return await repository.getShop(
      token: params.token,
      search: params.search,
      category: params.category,
      rating: params.rating,
      verified: params.verified,
      active: params.active,
      ownerId: params.ownerId,
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
  final List<String>? category;
  final int? rating;
  final bool? verified;
  final bool? active;
  final String? ownerId;
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
    this.category,
    this.rating,
    this.verified,
    this.active,
    this.ownerId,
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
        token,
        search,
        category,
        rating,
        verified,
        active,
        ownerId,
        latitudes,
        longitudes,
        radiusInKilometers,
        condition,
        sortBy,
        sortOrder,
        skip,
        limit,
      ];
}
