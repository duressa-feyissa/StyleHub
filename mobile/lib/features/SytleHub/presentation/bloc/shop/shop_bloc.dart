import 'dart:convert';
import 'dart:io';

import 'package:bloc/bloc.dart';
import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:equatable/equatable.dart';
import 'package:image_picker/image_picker.dart';
import 'package:stream_transform/stream_transform.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/image_entity.dart';
import 'package:style_hub/features/SytleHub/domain/entities/shop/review_entity.dart';
import 'package:style_hub/features/SytleHub/domain/entities/shop/working_hour_entity.dart';
import 'package:style_hub/features/SytleHub/domain/usecases/shop/add_image.dart'
    as add_image;
import 'package:style_hub/features/SytleHub/domain/usecases/shop/add_product.dart'
    as add_product;
import 'package:style_hub/features/SytleHub/domain/usecases/shop/delete_product.dart'
    as delete_product;
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop.dart'
    as get_shop;
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop_by_id.dart'
    as get_shop_by_id;
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop_products.dart'
    as get_shop_products;
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop_products_images.dart'
    as get_shop_products_images;
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop_products_video.dart'
    as get_shop_products_video;
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop_reviews.dart'
    as get_shop_reviews;
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop_working_hour.dart'
    as get_shop_working_hour;

import '../../../domain/entities/product/product_entity.dart';
import '../../../domain/entities/shop/shop_entity.dart';

part 'shop_event.dart';
part 'shop_state.dart';

const throttleDuration = Duration(milliseconds: 100);

EventTransformer<E> throttleDroppable<E>(Duration duration) {
  return (events, mapper) {
    return droppable<E>().call(events.throttle(duration), mapper);
  };
}

class ShopBloc extends Bloc<ShopEvent, ShopState> {
  final get_shop_by_id.GetShopByIdUseCase getShopByIdUseCase;
  final get_shop_products_images.GetShopProductsImageUseCase
      getShopProductsImageUseCase;
  final get_shop_products_video.GetShopProductsVideoUseCase
      getShopProductsVideoUseCase;
  final get_shop_products.GetShopProductUseCase getShopProductUseCase;
  final get_shop_reviews.GetShopReviewUseCase getShopReviewUseCase;
  final get_shop_working_hour.GetShopWorkingHourUseCase
      getShopWorkingHourUseCase;
  final get_shop.GetShopUseCase getShopUseCase;
  final add_image.AddImageUseCase addImageUseCase;
  final add_product.AddProductsUseCase addProductsUseCase;
  final delete_product.DeleteProductByIdUseCase deleteProductByIdUseCase;

  ShopBloc({
    required this.getShopByIdUseCase,
    required this.getShopProductsImageUseCase,
    required this.getShopProductsVideoUseCase,
    required this.getShopProductUseCase,
    required this.getShopReviewUseCase,
    required this.getShopWorkingHourUseCase,
    required this.getShopUseCase,
    required this.addImageUseCase,
    required this.addProductsUseCase,
    required this.deleteProductByIdUseCase,
  }) : super(const ShopState()) {
    on<GetAllShopEvent>(_getAllShop,
        transformer: throttleDroppable(throttleDuration));

    on<GetShopByIdEvent>(_getShopById,
        transformer: throttleDroppable(throttleDuration));

    on<GetShopProductsImagesEvent>(_getShopProductsImages,
        transformer: throttleDroppable(throttleDuration));

    on<GetShopProductsVideosEvent>(_getShopProductsVideo,
        transformer: throttleDroppable(throttleDuration));
    on<GetShopProductsEvent>(_getShopProducts,
        transformer: throttleDroppable(throttleDuration));
    on<GetShopReviewsEvent>(_getShopReviews,
        transformer: throttleDroppable(throttleDuration));
    on<GetShopWorkingHoursEvent>(_getShopWorkingHours,
        transformer: throttleDroppable(throttleDuration));
    on<GetMyShopEvent>(_getMyShop,
        transformer: throttleDroppable(throttleDuration));
    on<AddProductEvent>(_addProduct,
        transformer: throttleDroppable(throttleDuration));
    on<DeleteProductEvent>(_deleteProduct,
        transformer: throttleDroppable(throttleDuration));
  }

  void _getAllShop(
    GetAllShopEvent event,
    Emitter<ShopState> emit,
  ) async {
    if (state.shopsListStatus == ShopsListStatus.loaded) {
      return;
    }
    emit(state.copyWith(shopsListStatus: ShopsListStatus.loading));
    final result = await getShopUseCase(get_shop.Params(
      token: event.token,
      search: event.search,
      category: event.category,
      rating: event.rating,
      verified: event.verified,
      active: event.active,
      ownerId: event.ownerId,
      latitudes: event.latitudes,
      longitudes: event.longitudes,
      radiusInKilometers: event.radiusInKilometers,
      condition: event.condition,
      sortBy: event.sortBy,
      sortOrder: event.sortOrder,
      skip: state.shops.isEmpty ? 0 : state.shops.length,
      limit: 10,
    ));
    result.fold(
        (failure) =>
            emit(state.copyWith(shopsListStatus: ShopsListStatus.failure)),
        (shops) {
      if (shops.isEmpty) {
        emit(state.copyWith(shopsListStatus: ShopsListStatus.loaded));
        return;
      }
      Map<String, ShopEntity> shopMap = {...state.shops};
      for (var shop in shops) {
        shopMap[shop.id] = shop;
      }
      emit(
        state.copyWith(
            shopsListStatus: ShopsListStatus.success, shops: shopMap),
      );
    });
  }

  void _getShopById(
    GetShopByIdEvent event,
    Emitter<ShopState> emit,
  ) async {
    emit(state.copyWith(shopStatus: ShopStatus.loading));
    final result = await getShopByIdUseCase(get_shop_by_id.Params(
      id: event.id,
    ));
    result
        .fold((failure) => emit(state.copyWith(shopStatus: ShopStatus.failure)),
            (shop) {
      Map<String, ShopEntity> shopMap = {...state.shops};
      shopMap[shop.id] = shop;
      emit(
        state.copyWith(shopStatus: ShopStatus.success, shops: shopMap),
      );
    });
  }

  void _getShopProductsImages(
    GetShopProductsImagesEvent event,
    Emitter<ShopState> emit,
  ) async {
    if (!state.shops.containsKey(event.shopId) ||
        state.shopProductImageStatus == ShopProductImageStatus.loaded) {
      return;
    }

    emit(
        state.copyWith(shopProductImageStatus: ShopProductImageStatus.loading));
    final result =
        await getShopProductsImageUseCase(get_shop_products_images.Params(
      shopId: event.shopId,
      skip: state.shops[event.shopId]!.images.isEmpty
          ? 0
          : state.shops[event.shopId]!.images.length,
      limit: 10,
    ));
    result.fold(
      (failure) => emit(state.copyWith(
          shopProductImageStatus: ShopProductImageStatus.failure)),
      (images) {
        if (images.isEmpty) {
          emit(state.copyWith(
              shopProductImageStatus: ShopProductImageStatus.loaded));
          return;
        }
        List<ImageEntity> newImages = [
          ...state.shops[event.shopId]!.images,
          ...images,
        ];

        Map<String, ShopEntity> newShops = {...state.shops};
        newShops[event.shopId] =
            newShops[event.shopId]!.copyWith(images: newImages);

        emit(state.copyWith(
            shops: newShops,
            shopProductImageStatus: ShopProductImageStatus.success));
      },
    );
  }

  void _getShopProductsVideo(
    GetShopProductsVideosEvent event,
    Emitter<ShopState> emit,
  ) async {
    if (!state.shops.containsKey(event.shopId) ||
        state.shopProductVideoStatus == ShopProductVideoStatus.loaded) {
      return;
    }

    emit(
        state.copyWith(shopProductVideoStatus: ShopProductVideoStatus.loading));

    final result =
        await getShopProductsVideoUseCase(get_shop_products_video.Params(
      shopId: event.shopId,
      skip: state.shops[event.shopId]!.videos.isEmpty
          ? 0
          : state.shops[event.shopId]!.videos.length,
      limit: 10,
    ));

    result.fold(
      (failure) => emit(state.copyWith(
          shopProductVideoStatus: ShopProductVideoStatus.failure)),
      (videos) {
        if (videos.isEmpty) {
          emit(state.copyWith(
              shopProductVideoStatus: ShopProductVideoStatus.loaded));
          return;
        }

        List<String> newVideos = [
          ...state.shops[event.shopId]!.videos,
          ...videos,
        ];

        Map<String, ShopEntity> newShops = {...state.shops};
        newShops[event.shopId] =
            newShops[event.shopId]!.copyWith(videos: newVideos);

        emit(state.copyWith(
            shops: newShops,
            shopProductVideoStatus: ShopProductVideoStatus.success));
      },
    );
  }

  void _getShopProducts(
    GetShopProductsEvent event,
    Emitter<ShopState> emit,
  ) async {
    if (!state.shops.containsKey(event.shopId) ||
        state.shopProductsStatus == ShopProductsStatus.loaded) {
      return;
    }

    emit(state.copyWith(shopProductsStatus: ShopProductsStatus.loading));

    final result = await getShopProductUseCase(get_shop_products.Params(
        shopId: event.shopId,
        sortBy: event.sortBy,
        sortOrder: event.sortOrder,
        limit: 10,
        skip: state.shops[event.shopId]!.products.isEmpty
            ? 0
            : state.shops[event.shopId]!.products.length));
    result.fold(
      (failure) =>
          emit(state.copyWith(shopProductsStatus: ShopProductsStatus.failure)),
      (products) {
        if (products.isEmpty) {
          emit(state.copyWith(shopProductsStatus: ShopProductsStatus.loaded));
          return;
        }
        List<ProductEntity> newProducts = [
          ...state.shops[event.shopId]!.products
        ];
        newProducts.addAll(products);
        Map<String, ShopEntity> newShops = {...state.shops};
        ShopEntity updatedShop =
            newShops[event.shopId]!.copyWith(products: newProducts);
        newShops[event.shopId] = updatedShop;
        emit(state.copyWith(
            shops: newShops, shopProductsStatus: ShopProductsStatus.success));
      },
    );
  }

  void _getShopReviews(
    GetShopReviewsEvent event,
    Emitter<ShopState> emit,
  ) async {
    if (!state.shops.containsKey(event.shopId) ||
        state.shopProductReviewStatus == ShopProductReviewStatus.loaded) {
      return;
    }
    emit(state.copyWith(
        shopProductReviewStatus: ShopProductReviewStatus.loading));

    final result = await getShopReviewUseCase(get_shop_reviews.Params(
        shopId: event.shopId,
        sortBy: event.sortBy,
        sortOrder: event.sortOrder,
        skip: state.shops[event.shopId]!.reviews.isEmpty
            ? 0
            : state.shops[event.shopId]!.reviews.length));

    result.fold(
      (failure) => emit(state.copyWith(
          shopProductReviewStatus: ShopProductReviewStatus.failure)),
      (reviews) {
        if (reviews.isEmpty) {
          emit(state.copyWith(
              shopProductReviewStatus: ShopProductReviewStatus.loaded));
          return;
        }

        List<ReviewEntity> newReviews = [
          ...state.shops[event.shopId]!.reviews,
          ...reviews
        ];
        Map<String, ShopEntity> newShops = {...state.shops};
        newShops[event.shopId] = newShops[event.shopId]!.copyWith(
          reviews: newReviews,
        );
        emit(state.copyWith(
            shops: newShops,
            shopProductReviewStatus: ShopProductReviewStatus.success));
      },
    );
  }

  void _getShopWorkingHours(
    GetShopWorkingHoursEvent event,
    Emitter<ShopState> emit,
  ) async {
    if (!state.shops.containsKey(event.shopId) ||
        state.shopProductWorkStatus == ShopProductWorkStatus.loaded) {
      return;
    }
    emit(state.copyWith(shopProductWorkStatus: ShopProductWorkStatus.loading));

    final result = await getShopWorkingHourUseCase(get_shop_working_hour.Params(
      shopId: event.shopId,
    ));

    result.fold(
      (failure) => emit(
          state.copyWith(shopProductWorkStatus: ShopProductWorkStatus.failure)),
      (workinghours) {
        List<WorkingHourEntity> newWorkingHours = [
          ...state.shops[event.shopId]!.workingHours,
          ...workinghours
        ];

        Map<String, ShopEntity> newShops = {...state.shops};
        newShops[event.shopId] = newShops[event.shopId]!.copyWith(
          workingHours: newWorkingHours,
        );

        emit(state.copyWith(
            shops: newShops,
            shopProductWorkStatus: ShopProductWorkStatus.loaded));
      },
    );
  }

  void _getMyShop(
    GetMyShopEvent event,
    Emitter<ShopState> emit,
  ) async {
    if (state.shopMyProductsStatus == ShopMyProductsStatus.loaded) {
      return;
    }
    emit(state.copyWith(shopMyProductsStatus: ShopMyProductsStatus.loading));
    final result = await getShopUseCase(get_shop.Params(
      token: event.token ?? '',
      ownerId: event.userId,
      skip: 0,
      limit: 10,
    ));
    result.fold(
        (failure) => emit(
            state.copyWith(shopMyProductsStatus: ShopMyProductsStatus.failure)),
        (shops) {
      if (shops.isEmpty) {
        emit(
            state.copyWith(shopMyProductsStatus: ShopMyProductsStatus.failure));
        return;
      }

      Map<String, ShopEntity> shopMap = {...state.shops};
      shopMap[shops.first.id] = shops.first;

      emit(
        state.copyWith(
            shopMyProductsStatus: ShopMyProductsStatus.success,
            shops: shopMap,
            myShopId: shops.first.id),
      );
    });
  }

  void _addProduct(
    AddProductEvent event,
    Emitter<ShopState> emit,
  ) async {
    List<String> imagesBase64 = [];
    List<String> imagesIds = [];
    emit(state.copyWith(addProductStatus: AddProductStatus.loading));

    for (var image in event.fileImages) {
      final base64Image = await _getBase64Image(image);
      final result = await addImageUseCase(add_image.Params(
        token: event.token,
        base64Image: base64Image,
      ));
      result.fold(
        (failure) {
          emit(state.copyWith(addProductStatus: AddProductStatus.failure));
          return;
        },
        (image) {
          imagesBase64.add(base64Image);
          imagesIds.add(image.id);
        },
      );
      if (state.addProductStatus == AddProductStatus.failure) {
        return;
      }
    }

    final result = await addProductsUseCase(add_product.Params(
      token: event.token,
      shopId: event.shopId,
      title: event.title,
      description: event.description,
      price: event.price,
      condition: event.condition,
      inStock: event.inStock,
      status: event.status,
      videoUrl: event.videoUrl,
      isFixedPrice: event.isFixedPrice,
      colorIds: event.colorIds,
      sizeIds: event.sizeIds,
      categoryIds: event.categoryIds,
      brandIds: event.brandIds,
      materialIds: event.materialIds,
      designIds: event.designIds,
      images: imagesIds + event.images.map((e) => e.id).toList(),
    ));

    result.fold(
      (failure) {
        emit(state.copyWith(addProductStatus: AddProductStatus.failure));
      },
      (product) {
        Map<String, ShopEntity> newShops = {...state.shops};
        List<ProductEntity> newProducts = [
          product,
          ...newShops[event.shopId]!.products,
        ];
        newShops[event.shopId] =
            newShops[event.shopId]!.copyWith(products: newProducts);
        emit(state.copyWith(
          shops: newShops,
          addProductStatus: AddProductStatus.success,
        ));
      },
    );
  }

  Future<String> _getBase64Image(XFile image) async {
    final bytes = await File(image.path).readAsBytes();
    return base64Encode(bytes);
  }

  void _deleteProduct(
    DeleteProductEvent event,
    Emitter<ShopState> emit,
  ) async {
    Map<String, ShopEntity> newShops = {...state.shops};
    List<ProductEntity> newProducts = [
      ...newShops[state.myShopId]!.products,
    ];
    ProductEntity? removedProduct;
    removedProduct = newProducts.firstWhere(
      (element) => element.id == event.productId,
    );
    newProducts.remove(removedProduct);

    newShops[state.myShopId!] =
        newShops[state.myShopId]!.copyWith(products: newProducts);
    emit(state.copyWith(
      shops: newShops,
      deleteProductStatus: DeleteProductStatus.loading,
    ));

    final result = await deleteProductByIdUseCase(delete_product.Params(
      id: event.productId,
      token: event.token,
    ));

    result.fold(
      (failure) {
        if (removedProduct != null) {
          newProducts.add(removedProduct);
          newShops[state.myShopId!] =
              newShops[state.myShopId]!.copyWith(products: newProducts);
        }
        emit(state.copyWith(
            deleteProductStatus: DeleteProductStatus.failure, shops: newShops));
      },
      (product) {
        emit(state.copyWith(deleteProductStatus: DeleteProductStatus.success));
      },
    );
  }
}
