import 'dart:convert';

import 'package:http/http.dart' show Client;

import '../../../../../core/errors/exception.dart';
import '../../../../../setUp/url/urls.dart';
import '../../models/product/brand_model.dart';
import '../../models/product/category_model.dart';
import '../../models/product/color_model.dart';
import '../../models/product/design_model.dart';
import '../../models/product/domain_model.dart';
import '../../models/product/location_model.dart';
import '../../models/product/material_model.dart';
import '../../models/product/product_model.dart';
import '../../models/product/size_model.dart';

abstract class ProductRemoteDataSource {
  Future<List<ColorModel>> getColors();
  Future<List<BrandModel>> getBrands();
  Future<List<CategoryModel>> getCategories();
  Future<List<SizeModel>> getSizes();
  Future<List<MaterialModel>> getMaterials();
  Future<List<LocationModel>> getLocations();
  Future<List<DesignModel>> getDesigns();
  Future<List<DomainModel>> getDomains();
  Future<List<ProductModel>> getProducts({
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
    int? skip,
    int? limit,
  });
}

class ProductRemoteDataSourceImpl implements ProductRemoteDataSource {
  final Client client;

  ProductRemoteDataSourceImpl({required this.client});

  @override
  Future<List<ColorModel>> getColors() async {
    final response = await client.get(Uri.parse(Urls.color));
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List)
          .map((e) => ColorModel.fromJson(e))
          .toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<List<BrandModel>> getBrands() async {
    final response = await client.get(Uri.parse(Urls.brand));
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List)
          .map((e) => BrandModel.fromJson(e))
          .toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<List<CategoryModel>> getCategories() async {
    final response = await client.get(Uri.parse(Urls.category));
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List)
          .map((e) => CategoryModel.fromJson(e))
          .toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<List<MaterialModel>> getMaterials() async {
    final response = await client.get(Uri.parse(Urls.material));
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List)
          .map((e) => MaterialModel.fromJson(e))
          .toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<List<SizeModel>> getSizes() async {
    final response = await client.get(Uri.parse(Urls.size));
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List)
          .map((e) => SizeModel.fromJson(e))
          .toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<List<LocationModel>> getLocations() async {
    final response = await client.get(Uri.parse(Urls.location));
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List)
          .map((e) => LocationModel.fromJson(e))
          .toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<List<DesignModel>> getDesigns() async {
    final response = await client.get(Uri.parse(Urls.design));
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List)
          .map((e) => DesignModel.fromJson(e))
          .toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<List<DomainModel>> getDomains() async {
    final response = await client.get(Uri.parse(Urls.domain));
    if (response.statusCode == 200) {
      return DomainModel.fromJsonList(json.decode(response.body));
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<List<ProductModel>> getProducts({
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
    int? skip,
    int? limit,
  }) async {
    String query = '?';

    if (search != null) query += '$search&';
    if (colorIds != null && colorIds.isNotEmpty) {
      for (var colorId in colorIds) {
        query += 'colorIds=$colorId&';
      }
    }
    if (sizeIds != null && sizeIds.isNotEmpty) {
      for (var sizeId in sizeIds) {
        query += 'sizeIds=$sizeId&';
      }
    }
    if (categoryIds != null && categoryIds.isNotEmpty) {
      for (var categoryId in categoryIds) {
        query += 'categoryIds=$categoryId&';
      }
    }
    if (designIds != null && designIds.isNotEmpty) {
      for (var designId in designIds) {
        query += 'designIds=$designId&';
      }
    }
    if (brandIds != null && brandIds.isNotEmpty) {
      for (var brandId in brandIds) {
        query += 'brandIds=$brandId&';
      }
    }
    if (materialIds != null && materialIds.isNotEmpty) {
      for (var materialId in materialIds) {
        query += 'materialIds=$materialId&';
      }
    }
    if (isNegotiable != null) {
      query += 'isNegotiable=$isNegotiable&';
    }
    if (minPrice != null && minPrice != -1) query += 'minPrice=$minPrice&';
    if (maxPrice != null && maxPrice != -1) query += 'maxPrice=$maxPrice&';
    if (minQuantity != null) query += 'minQuantity=$minQuantity&';

    if (maxQuantity != null) {
      query += 'maxQuantity=$maxQuantity&';
    }

    if (latitudes != null && latitudes != 0) query += 'latitudes=$latitudes&';
    if (longitudes != null && longitudes != 0) {
      query += 'longitudes=$longitudes&';
    }
    if (radiusInKilometers != null && radiusInKilometers != 0) {
      query += 'radiusInKilometers=$radiusInKilometers&';
    }
    if (condition != null) query += 'condition=$condition&';
    if (sortBy != null) query += 'sortBy=$sortBy&';
    if (sortOrder != null) query += 'sortOrder=$sortOrder&';
    if (skip != null) query += 'skip=$skip&';
    if (limit != null) query += 'limit=$limit&';

    final uri = Uri.parse(Urls.product + query);
    final response = await client.get(uri);

    if (response.statusCode == 200) {
      return (json.decode(response.body) as List)
          .map((e) => ProductModel.fromJson(e))
          .toList();
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }
}
