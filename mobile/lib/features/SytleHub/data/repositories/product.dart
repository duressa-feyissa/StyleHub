import 'package:either_dart/either.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/product_entity.dart';

import '../../../../core/errors/exception.dart';
import '../../../../core/errors/failure.dart';
import '../../../../core/network/internet.dart';
import '../../domain/entities/product/brand_entity.dart';
import '../../domain/entities/product/category_entity.dart';
import '../../domain/entities/product/color_entity.dart';
import '../../domain/entities/product/design_entity.dart';
import '../../domain/entities/product/domain_entity.dart';
import '../../domain/entities/product/location_entity.dart';
import '../../domain/entities/product/material_entity.dart';
import '../../domain/entities/product/size_entity.dart';
import '../../domain/repositories/product.dart';
import '../datasources/remote/product.dart';

class ProductRepositoryImpl implements ProductRepository {
  final ProductRemoteDataSource remoteDataSource;
  final NetworkInfo networkInfo;

  ProductRepositoryImpl({
    required this.remoteDataSource,
    required this.networkInfo,
  });

  @override
  Future<Either<Failure, List<ColorEntity>>> getColors() async {
    if (await networkInfo.isConnected) {
      try {
        final colors = await remoteDataSource.getColors();
        return Right(colors);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, List<BrandEntity>>> getBrands() async {
    if (await networkInfo.isConnected) {
      try {
        final brands = await remoteDataSource.getBrands();
        return Right(brands);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, List<CategoryEntity>>> getCategories() async {
    if (await networkInfo.isConnected) {
      try {
        final categories = await remoteDataSource.getCategories();
        return Right(categories);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, List<MaterialEntity>>> getMaterials() async {
    if (await networkInfo.isConnected) {
      try {
        final materials = await remoteDataSource.getMaterials();
        return Right(materials);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, List<SizeEntity>>> getSizes() async {
    if (await networkInfo.isConnected) {
      try {
        final sizes = await remoteDataSource.getSizes();
        return Right(sizes);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, List<LocationEntity>>> getLocations() async {
    if (await networkInfo.isConnected) {
      try {
        final locations = await remoteDataSource.getLocations();
        return Right(locations);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, List<DesignEntity>>> getDesigns() async {
    if (await networkInfo.isConnected) {
      try {
        final designs = await remoteDataSource.getDesigns();
        return Right(designs);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, List<DomainEntity>>> getDomains() async {
    if (await networkInfo.isConnected) {
      try {
        final domains = await remoteDataSource.getDomains();
        return Right(domains);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, List<ProductEntity>>> getProducts({
    String? search,
    List<String>? colorIds,
    List<String>? sizeIds,
    List<String>? categoryIds,
    List<String>? brandIds,
    List<String>? materialIds,
    List<String>? designIds,
    bool? isNegotiable,
    double? minPrice,
    double? maxPrice,
    int? minQuantity,
    int? maxQuantity,
    double? latitudes,
    double? longitudes,
    double? radiusInKilometers,
    String? condition,
    String? sortBy,
    String? sortOrder,
    int? skip = 0,
    int? limit = 15,
  }) async {
    if (await networkInfo.isConnected) {
      try {
        final products = await remoteDataSource.getProducts(
          search: search,
          colorIds: colorIds,
          sizeIds: sizeIds,
          categoryIds: categoryIds,
          brandIds: brandIds,
          materialIds: materialIds,
          designIds: designIds,
          isNegotiable: isNegotiable,
          minPrice: minPrice,
          maxPrice: maxPrice,
          minQuantity: minQuantity,
          maxQuantity: maxQuantity,
          latitudes: latitudes,
          longitudes: longitudes,
          radiusInKilometers: radiusInKilometers,
          condition: condition,
          sortBy: sortBy,
          sortOrder: sortOrder,
          skip: skip,
          limit: limit,
        );
        return Right(products);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }
}
