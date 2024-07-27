import 'dart:convert';

import 'package:http/http.dart' show Client;
import 'package:style_hub/features/SytleHub/data/models/product/image_model.dart';
import 'package:style_hub/features/SytleHub/data/models/product/product_model.dart';
import 'package:style_hub/features/SytleHub/data/models/shop/shop_model.dart';
import 'package:style_hub/setUp/url/urls.dart';

import '../../../../../core/errors/exception.dart';
import '../../models/shop/review_model.dart';
import '../../models/shop/working_hour_model.dart';

abstract class ShopRemoteDataSource {
  Future<List<ShopModel>> getShop({
    required String token,
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
    int? skip,
    int? limit,
  });

  Future<ShopModel> getShopById({
    required String id,
  });

  Future<List<ImageModel>> getShopProductsImages({
    required String shopId,
    int? skip,
    int? limit,
  });

  Future<List<String>> getShopProductsVideos({
    required String shopId,
    int? skip,
    int? limit,
  });

  Future<List<ReviewModel>> getShopReviews({
    required String shopId,
    String? userId,
    String? sortBy,
    String? sortOrder,
    int? rating,
    int? skip,
    int? limit,
  });

  Future<List<ProductModel>> getShopProducts({
    required String shopId,
    String? sortBy,
    String? sortOrder,
    int? skip,
    int? limit,
  });

  Future<List<WorkingHourModel>> getShopWorkingHours({
    required String shopId,
  });

  Future<ProductModel> addProduct({
    required String token,
    required String title,
    required String description,
    required int price,
    required bool isFixedPrice,
    required String condition,
    required bool inStock,
    required String status,
    required String shopId,
    required List<String> images,
    required List<String> colorIds,
    required List<String> sizeIds,
    required List<String> categoryIds,
    required List<String> brandIds,
    required List<String> materialIds,
    required List<String> designIds,
    String? videoUrl,
  });

  Future<ImageModel> addProductImage({
    required String token,
    required String base64Image,
  });

  Future<ProductModel> deleteProductById({
    required String token,
    required String productId,
  });
}

class ShopRemoteDataSourceImpl implements ShopRemoteDataSource {
  final Client client;

  ShopRemoteDataSourceImpl({required this.client});

  @override
  Future<List<ShopModel>> getShop({
    required String token,
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
    int? skip,
    int? limit,
  }) async {
    String query = '?';

    if (search != null) query += '$search&';
    if (category != null && category.isNotEmpty) {
      for (var categoryId in category) {
        query += 'category=$categoryId&';
      }
    }
    if (rating != null) query += 'rating=$rating&';
    if (verified != null) query += 'verified=$verified&';
    if (active != null) query += 'active=$active&';
    if (ownerId != null) query += 'ownerId=$ownerId&';
    if (latitudes != null) query += 'latitudes=$latitudes&';
    if (longitudes != null) query += 'longitudes=$longitudes&';
    if (radiusInKilometers != null) {
      query += 'radiusInKilometers=$radiusInKilometers&';
    }
    if (condition != null) query += 'condition=$condition&';
    if (sortBy != null) query += 'sortBy=$sortBy&';
    if (sortOrder != null) query += 'sortOrder=$sortOrder&';
    if (skip != null) query += 'skip=$skip&';
    if (limit != null) query += 'limit=$limit&';

    final response = await client.get(
      Uri.parse(Urls.shop + query),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
    );

    if (response.statusCode == 200) {
      final shops = jsonDecode(response.body) as List;
      return shops.map((shop) => ShopModel.fromJson(shop)).toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<List<ProductModel>> getShopProducts(
      {required String shopId,
      String? sortBy,
      String? sortOrder,
      int? skip,
      int? limit}) async {
    String query = '?';
    query += 'shopId=$shopId&';
    if (sortBy != null) query += 'sortBy=$sortBy&';
    if (sortOrder != null) query += 'sortOrder=$sortOrder&';
    if (skip != null) query += 'skip=$skip&';
    if (limit != null) query += 'limit=$limit&';
    final url = Uri.parse(Urls.product + query);
    final response = await client.get(
      url,
      headers: {
        'Content-Type': 'application/json',
      },
    );
    if (response.statusCode == 200) {
      final products = jsonDecode(response.body) as List;
      return products.map((product) => ProductModel.fromJson(product)).toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<List<ImageModel>> getShopProductsImages(
      {required String shopId, int? skip, int? limit}) async {
    String query = '?';
    if (skip != null) query += 'skip=$skip&';
    if (limit != null) query += 'limit=$limit&';

    final response = await client.get(
      Uri.parse('${Urls.shop}/$shopId/products/images$query'),
      headers: {
        'Content-Type': 'application/json',
      },
    );

    if (response.statusCode == 200) {
      final images = jsonDecode(response.body) as List;
      return images.map((image) => ImageModel.fromJson(image)).toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<List<String>> getShopProductsVideos(
      {required String shopId, int? skip, int? limit}) async {
    String query = '?';
    if (skip != null) query += 'skip=$skip&';
    if (limit != null) query += 'limit=$limit&';

    final response = await client.get(
      Uri.parse('${Urls.shop}/$shopId/products/videos"$query'),
      headers: {
        'Content-Type': 'application/json',
      },
    );

    if (response.statusCode == 200) {
      final videos = jsonDecode(response.body) as List;
      return videos.map((video) => video.toString()).toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<List<ReviewModel>> getShopReviews(
      {required String shopId,
      String? userId,
      String? sortBy,
      String? sortOrder,
      int? rating,
      int? skip,
      int? limit}) async {
    String query = '?';
    if (userId != null) query += 'userId=$userId&';
    if (sortBy != null) query += 'sortBy=$sortBy&';
    if (sortOrder != null) query += 'sortOrder=$sortOrder&';
    if (rating != null) query += 'rating=$rating&';
    if (skip != null) query += 'skip=$skip&';
    if (limit != null) query += 'limit=$limit&';

    final response = await client.get(
      Uri.parse('${Urls.review}/all/$shopId$query'),
      headers: {
        'Content-Type': 'application/json',
      },
    );

    if (response.statusCode == 200) {
      final reviews = jsonDecode(response.body) as List;
      return reviews.map((review) => ReviewModel.fromJson(review)).toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<List<WorkingHourModel>> getShopWorkingHours(
      {required String shopId}) async {
    final response = await client.get(
      Uri.parse('${Urls.workingHour}/$shopId/shop'),
      headers: {
        'Content-Type': 'application/json',
      },
    );
    if (response.statusCode == 200) {
      final workingHours = jsonDecode(response.body) as List;
      return workingHours
          .map((workingHour) => WorkingHourModel.fromJson(workingHour))
          .toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<ShopModel> getShopById({required String id}) async {
    final response = await client.get(
      Uri.parse('${Urls.shop}/$id'),
      headers: {
        'Content-Type': 'application/json',
      },
    );

    if (response.statusCode == 200) {
      return ShopModel.fromJson(jsonDecode(response.body));
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<ProductModel> addProduct(
      {required String token,
      required String title,
      required String description,
      required int price,
      required bool isFixedPrice,
      required String condition,
      required bool inStock,
      required String status,
      required List<String> images,
      required List<String> colorIds,
      required List<String> sizeIds,
      required List<String> categoryIds,
      required List<String> brandIds,
      required List<String> materialIds,
      required List<String> designIds,
      required String shopId,
      String? videoUrl}) async {
    final response = await client.post(Uri.parse(Urls.product),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer $token',
        },
        body: jsonEncode({
          "title": title,
          "description": description,
          "price": price,
          "status": status,
          "videoUrl": videoUrl,
          "shopId": shopId,
          "condition": condition,
          "isNegotiable": isFixedPrice,
          "inStock": inStock,
          "imageIds": images,
          "categoryIds": categoryIds,
          "brandIds": brandIds,
          "designIds": designIds,
          "sizeIds": sizeIds,
          "colorIds": colorIds,
          "materialIds": materialIds
        }));

    if (response.statusCode == 200) {
      return ProductModel.fromJson(jsonDecode(response.body)['data']);
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<ImageModel> addProductImage(
      {required String token, required String base64Image}) async {
    final response = await client.post(
      Uri.parse(Urls.image),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
      body: jsonEncode(
        {"base64Image": base64Image},
      ),
    );

    if (response.statusCode == 200) {
      return ImageModel.fromJson(jsonDecode(response.body)['data']);
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<ProductModel> deleteProductById(
      {required String token, required String productId}) async {
    final response = await client.delete(
      Uri.parse('${Urls.product}/$productId'),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
    );

    if (response.statusCode == 200) {
      return ProductModel.fromJson(jsonDecode(response.body)['data']);
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }
}
