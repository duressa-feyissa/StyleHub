part of 'product_bloc.dart';

@immutable
sealed class ProductEvent {}

class GetColorsEvent extends ProductEvent {
  GetColorsEvent();
}

class GetSizesEvent extends ProductEvent {
  GetSizesEvent();
}

class GetCategoriesEvent extends ProductEvent {
  GetCategoriesEvent();
}

class GetBrandsEvent extends ProductEvent {
  GetBrandsEvent();
}

class GetMaterialsEvent extends ProductEvent {
  GetMaterialsEvent();
}

class GetLocationsEvent extends ProductEvent {
  GetLocationsEvent();
}

class GetDesignsEvent extends ProductEvent {
  GetDesignsEvent();
}

class GetDomainsEvent extends ProductEvent {
  GetDomainsEvent();
}

class GetProductsEvent extends ProductEvent {
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
  final int? skip = 0;
  final int? limit = 10;

  GetProductsEvent({
    this.search,
    this.colorIds,
    this.sizeIds,
    this.categoryIds,
    this.designIds,
    this.brandIds,
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
  });
}

class GetFilteredProductsEvent extends ProductEvent {
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
  final int? skip = 0;
  final int? limit = 10;

  GetFilteredProductsEvent({
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
  });
}