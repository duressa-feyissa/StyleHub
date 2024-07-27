import 'package:either_dart/either.dart';
import 'package:style_hub/core/errors/failure.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/image_entity.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/product_entity.dart';
import 'package:style_hub/features/SytleHub/domain/entities/shop/review_entity.dart';
import 'package:style_hub/features/SytleHub/domain/entities/shop/shop_entity.dart';
import 'package:style_hub/features/SytleHub/domain/entities/shop/working_hour_entity.dart';

import '../../../../core/errors/exception.dart';
import '../../../../core/network/internet.dart';
import '../../domain/repositories/shop.dart';
import '../datasources/remote/shop.dart';

class ShopRepositoryImpl extends ShopRepository {
  final ShopRemoteDataSource remoteDataSource;
  final NetworkInfo networkInfo;

  ShopRepositoryImpl({
    required this.remoteDataSource,
    required this.networkInfo,
  });

  @override
  Future<Either<Failure, List<ShopEntity>>> getShop(
      {required String token,
      String? search,
      List<String>? category,
      int? rating,
      bool? verified,
      bool? active,
      String? ownerId,
      double? latitudes,
      double? longitudes,
      double? radiusInKilometers,
      String? condition,
      String? sortBy,
      String? sortOrder,
      int? skip = 0,
      int? limit = 15}) async {
    if (await networkInfo.isConnected) {
      try {
        final shops = await remoteDataSource.getShop(
          token: token,
          search: search,
          category: category,
          rating: rating,
          verified: verified,
          active: active,
          ownerId: ownerId,
          latitudes: latitudes,
          longitudes: longitudes,
          radiusInKilometers: radiusInKilometers,
          condition: condition,
          sortBy: sortBy,
          sortOrder: sortOrder,
          skip: skip,
          limit: limit,
        );
        return Right(shops);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, ShopEntity>> getShopById({required String id}) async {
    if (await networkInfo.isConnected) {
      try {
        final shop = await remoteDataSource.getShopById(id: id);
        return Right(shop);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, List<ProductEntity>>> getShopProducts(
      {required String shopId,
      String? sortBy,
      String? sortOrder,
      int? skip = 0,
      int? limit = 15}) async {
    if (await networkInfo.isConnected) {
      try {
        final products = await remoteDataSource.getShopProducts(
          shopId: shopId,
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

  @override
  Future<Either<Failure, List<ImageEntity>>> getShopProductsImages(
      {required String shopId, int? skip = 0, int? limit = 15}) async {
    if (await networkInfo.isConnected) {
      try {
        final images = await remoteDataSource.getShopProductsImages(
          shopId: shopId,
          skip: skip,
          limit: limit,
        );
        return Right(images);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, List<String>>> getShopProductsVideos(
      {required String shopId, int? skip = 0, int? limit = 15}) async {
    if (await networkInfo.isConnected) {
      try {
        final videos = await remoteDataSource.getShopProductsVideos(
          shopId: shopId,
          skip: skip,
          limit: limit,
        );
        return Right(videos);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, List<ReviewEntity>>> getShopReviews(
      {required String shopId,
      String? userId,
      String? sortBy,
      String? sortOrder,
      int? rating,
      int? skip = 0,
      int? limit = 15}) async {
    if (await networkInfo.isConnected) {
      try {
        final reviews = await remoteDataSource.getShopReviews(
          shopId: shopId,
          userId: userId,
          sortBy: sortBy,
          sortOrder: sortOrder,
          rating: rating,
          skip: skip,
          limit: limit,
        );
        return Right(reviews);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, List<WorkingHourEntity>>> getShopWorkingHours(
      {required String shopId}) async {
    if (await networkInfo.isConnected) {
      try {
        final workingHours = await remoteDataSource.getShopWorkingHours(
          shopId: shopId,
        );
        return Right(workingHours);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, ProductEntity>> addProduct(
      {required String token,
      required String title,
      required String description,
      required int price,
      required bool isFixedPrice,
      required String condition,
      required bool inStock,
      required String shopId,
      required String status,
      required List<String> images,
      required List<String> colorIds,
      required List<String> sizeIds,
      required List<String> categoryIds,
      required List<String> brandIds,
      required List<String> materialIds,
      required List<String> designIds,
      String? videoUrl}) async {
    if (await networkInfo.isConnected) {
      try {
        final product = await remoteDataSource.addProduct(
          token: token,
          title: title,
          description: description,
          price: price,
          isFixedPrice: isFixedPrice,
          condition: condition,
          inStock: inStock,
          shopId: shopId,
          status: status,
          images: images,
          colorIds: colorIds,
          sizeIds: sizeIds,
          categoryIds: categoryIds,
          brandIds: brandIds,
          materialIds: materialIds,
          designIds: designIds,
          videoUrl: videoUrl,
        );
        return Right(product);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, ImageEntity>> addProductImage(
      {required String token, required String base64Image}) async {
    if (await networkInfo.isConnected) {
      try {
        final image = await remoteDataSource.addProductImage(
          token: token,
          base64Image: base64Image,
        );
        return Right(image);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }
  
  @override
  Future<Either<Failure, ProductEntity>> deleteProductById({required String token, required String productId}) async{
    if (await networkInfo.isConnected) {
      try {
        final product = await remoteDataSource.deleteProductById(
          token: token,
          productId: productId,
        );
        return Right(product);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }
}
