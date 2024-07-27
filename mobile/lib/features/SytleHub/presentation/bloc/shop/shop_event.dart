part of 'shop_bloc.dart';

sealed class ShopEvent extends Equatable {
  const ShopEvent();

  @override
  List<Object> get props => [];
}

class GetAllShopEvent extends ShopEvent {
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

  const GetAllShopEvent({
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
  });
}

class GetShopByIdEvent extends ShopEvent {
  final String id;

  const GetShopByIdEvent({
    required this.id,
  });
}

class GetShopProductsImagesEvent extends ShopEvent {
  final String shopId;

  const GetShopProductsImagesEvent({
    required this.shopId,
  });
}

class GetShopProductsVideosEvent extends ShopEvent {
  final String shopId;

  const GetShopProductsVideosEvent({
    required this.shopId,
  });
}

class GetShopReviewsEvent extends ShopEvent {
  final String shopId;
  final String? userId;
  final String? sortBy;
  final String? sortOrder;
  final int? rating;

  const GetShopReviewsEvent({
    required this.shopId,
    this.userId,
    this.sortBy,
    this.sortOrder,
    this.rating,
  });
}

class GetShopProductsEvent extends ShopEvent {
  final String shopId;
  final String? sortBy;
  final String? sortOrder;

  const GetShopProductsEvent({
    required this.shopId,
    this.sortBy,
    this.sortOrder,
  });
}

class GetShopWorkingHoursEvent extends ShopEvent {
  final String shopId;

  const GetShopWorkingHoursEvent({
    required this.shopId,
  });
}

class GetMyShopEvent extends ShopEvent {
  final String userId;
  final String? token;

  const GetMyShopEvent({
    required this.userId,
    this.token,
  });
}

class AddProductEvent extends ShopEvent {
  final String token;
  final String title;
  final String description;
  final int price;
  final bool isFixedPrice;
  final String condition;
  final bool inStock;
  final String status;
  final String? videoUrl;
  final String shopId;
  final List<XFile> fileImages;
  final List<ImageEntity> images;
  final List<String> colorIds;
  final List<String> sizeIds;
  final List<String> categoryIds;
  final List<String> brandIds;
  final List<String> materialIds;
  final List<String> designIds;

  const AddProductEvent({
    required this.token,
    required this.title,
    required this.description,
    required this.price,
    required this.isFixedPrice,
    required this.shopId,
    required this.condition,
    required this.inStock,
    required this.fileImages,
    required this.images,
    required this.colorIds,
    required this.sizeIds,
    required this.categoryIds,
    required this.brandIds,
    required this.materialIds,
    required this.designIds,
    required this.status,
    this.videoUrl,
  });
}

class DeleteProductEvent extends ShopEvent {
  final String productId;
  final String token;

  const DeleteProductEvent({
    required this.productId,
    required this.token,
  });
}