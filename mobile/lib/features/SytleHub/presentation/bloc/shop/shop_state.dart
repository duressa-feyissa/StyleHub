part of 'shop_bloc.dart';

enum ShopProductsStatus { initial, loading, success, failure, loaded }

enum ShopStatus { initial, loading, success, failure, loaded }

enum ShopProductStatus { initial, loading, success, failure, loaded }

enum ShopProductReviewStatus { initial, loading, success, failure, loaded }

enum ShopProductImageStatus { initial, loading, success, failure, loaded }

enum ShopProductVideoStatus { initial, loading, success, failure, loaded }

enum ShopProductWorkStatus { initial, loading, success, failure, loaded }

enum ShopsListStatus { initial, loading, success, failure, loaded }

enum ShopMyProductsStatus { initial, loading, success, failure, loaded }

enum DeleteProductStatus { initial, loading, success, failure, loaded }

enum AddProductStatus { initial, loading, success, failure, loaded }

class ShopState extends Equatable {
  final ShopStatus shopStatus;
  final ShopProductsStatus shopProductsStatus;
  final ShopProductStatus shopProductStatus;
  final ShopProductReviewStatus shopProductReviewStatus;
  final ShopProductImageStatus shopProductImageStatus;
  final ShopProductVideoStatus shopProductVideoStatus;
  final ShopProductWorkStatus shopProductWorkStatus;
  final ShopsListStatus shopsListStatus;
  final Map<String, ShopEntity> shops;
  final ShopMyProductsStatus shopMyProductsStatus;
  final String? myShopId;
  final DeleteProductStatus deleteProductStatus;
  final AddProductStatus addProductStatus;

  const ShopState({
    this.deleteProductStatus = DeleteProductStatus.initial,
    this.shopStatus = ShopStatus.initial,
    this.shopProductsStatus = ShopProductsStatus.initial,
    this.shopProductStatus = ShopProductStatus.initial,
    this.shopProductReviewStatus = ShopProductReviewStatus.initial,
    this.shopProductImageStatus = ShopProductImageStatus.initial,
    this.shopProductVideoStatus = ShopProductVideoStatus.initial,
    this.shopProductWorkStatus = ShopProductWorkStatus.initial,
    this.shopsListStatus = ShopsListStatus.initial,
    this.shopMyProductsStatus = ShopMyProductsStatus.initial,
    this.addProductStatus = AddProductStatus.initial,
    this.myShopId,
    this.shops = const <String, ShopEntity>{},
  });

  @override
  List<Object> get props => [
        shopStatus,
        shopProductsStatus,
        shopProductStatus,
        shopProductReviewStatus,
        shopProductImageStatus,
        shopProductVideoStatus,
        shopProductWorkStatus,
        shopsListStatus,
        shops,
        shopMyProductsStatus,  
        deleteProductStatus,      
        addProductStatus,
      ];

  ShopState copyWith({
    ShopStatus? shopStatus,
    ShopProductsStatus? shopProductsStatus,
    ShopProductStatus? shopProductStatus,
    ShopProductReviewStatus? shopProductReviewStatus,
    ShopProductImageStatus? shopProductImageStatus,
    ShopProductVideoStatus? shopProductVideoStatus,
    ShopProductWorkStatus? shopProductWorkStatus,
    ShopsListStatus? shopsListStatus,
    AddProductStatus? addProductStatus,
    Map<String, ShopEntity>? shops,
    ShopMyProductsStatus? shopMyProductsStatus,
    String? myShopId,
    DeleteProductStatus? deleteProductStatus,
  }) {
    return ShopState(
      shopStatus: shopStatus ?? this.shopStatus,
      shopProductsStatus: shopProductsStatus ?? this.shopProductsStatus,
      shopProductStatus: shopProductStatus ?? this.shopProductStatus,
      shopProductReviewStatus:
          shopProductReviewStatus ?? this.shopProductReviewStatus,
      shopProductImageStatus:
          shopProductImageStatus ?? this.shopProductImageStatus,
      shopProductVideoStatus:
          shopProductVideoStatus ?? this.shopProductVideoStatus,
      shopProductWorkStatus:
          shopProductWorkStatus ?? this.shopProductWorkStatus,
      shopsListStatus: shopsListStatus ?? this.shopsListStatus,
      shops: shops ?? this.shops,
      shopMyProductsStatus: shopMyProductsStatus ?? this.shopMyProductsStatus,
      myShopId: myShopId ?? this.myShopId,
      deleteProductStatus: deleteProductStatus ?? this.deleteProductStatus,
      addProductStatus: addProductStatus ?? this.addProductStatus,
    );
  }
}
