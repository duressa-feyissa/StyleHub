import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/product_entity.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../repositories/shop.dart';

class DeleteProductByIdUseCase extends UseCase<ProductEntity, Params> {
  final ShopRepository repository;

  DeleteProductByIdUseCase(this.repository);

  @override
  Future<Either<Failure, ProductEntity>> call(Params params) async {
    return await repository.deleteProductById(
      productId: params.id,
      token: params.token,
    );
  }
}

class Params extends Equatable {
  final String id;
  final String token;

  const Params({
    required this.id,
    required this.token,
  });

  @override
  List<Object> get props => [id, token];
}
