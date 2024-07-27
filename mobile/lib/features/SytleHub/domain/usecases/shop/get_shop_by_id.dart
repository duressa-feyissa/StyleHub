import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';
import 'package:style_hub/features/SytleHub/domain/entities/shop/shop_entity.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../repositories/shop.dart';

class GetShopByIdUseCase extends UseCase<ShopEntity, Params> {
  final ShopRepository repository;

  GetShopByIdUseCase(this.repository);

  @override
  Future<Either<Failure, ShopEntity>> call(Params params) async {
    return await repository.getShopById(
      id: params.id,
    );
  }
}

class Params extends Equatable {
  final String id;

  const Params({
    required this.id,
  });

  @override
  List<Object> get props => [id];
}
