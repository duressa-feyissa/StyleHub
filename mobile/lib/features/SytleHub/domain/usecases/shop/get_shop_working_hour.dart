import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../entities/shop/working_hour_entity.dart';
import '../../repositories/shop.dart';

class GetShopWorkingHourUseCase
    extends UseCase<List<WorkingHourEntity>, Params> {
  final ShopRepository repository;

  GetShopWorkingHourUseCase(this.repository);

  @override
  Future<Either<Failure, List<WorkingHourEntity>>> call(Params params) async {
    return await repository.getShopWorkingHours(
      shopId: params.shopId,
    );
  }
}

class Params extends Equatable {
  final String shopId;

  const Params({
    required this.shopId,
  });

  @override
  List<Object> get props => [shopId];
}
