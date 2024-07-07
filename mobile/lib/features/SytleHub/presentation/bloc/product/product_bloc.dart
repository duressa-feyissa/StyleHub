import 'package:bloc/bloc.dart';
import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter/material.dart';
import 'package:stream_transform/stream_transform.dart';
import 'package:style_hub/features/SytleHub/domain/usecases/product/get_brand.dart'
    as brand_usecase;
import 'package:style_hub/features/SytleHub/domain/usecases/product/get_category.dart'
    as category_usecase;
import 'package:style_hub/features/SytleHub/domain/usecases/product/get_color.dart'
    as color_usecase;
import 'package:style_hub/features/SytleHub/domain/usecases/product/get_design.dart'
    as design_usecase;
import 'package:style_hub/features/SytleHub/domain/usecases/product/get_domain.dart'
    as domain_usecase;
import 'package:style_hub/features/SytleHub/domain/usecases/product/get_location.dart'
    as location_usecase;
import 'package:style_hub/features/SytleHub/domain/usecases/product/get_material.dart'
    as material_usecase;
import 'package:style_hub/features/SytleHub/domain/usecases/product/get_product.dart'
    as product_usecase;
import 'package:style_hub/features/SytleHub/domain/usecases/product/get_size.dart'
    as size_usecase;

import '../../../../../core/use_cases/usecase.dart';
import '../../../domain/entities/product/brand_entity.dart';
import '../../../domain/entities/product/category_entity.dart';
import '../../../domain/entities/product/color_entity.dart';
import '../../../domain/entities/product/design_entity.dart';
import '../../../domain/entities/product/domain_entity.dart';
import '../../../domain/entities/product/location_entity.dart';
import '../../../domain/entities/product/material_entity.dart';
import '../../../domain/entities/product/product_entity.dart';
import '../../../domain/entities/product/size_entity.dart';

part 'product_event.dart';
part 'product_state.dart';

const throttleDuration = Duration(milliseconds: 100);

EventTransformer<E> throttleDroppable<E>(Duration duration) {
  return (events, mapper) {
    return droppable<E>().call(events.throttle(duration), mapper);
  };
}

class ProductBloc extends Bloc<ProductEvent, ProductState> {
  final color_usecase.GetColorsUseCase getColorsUseCase;
  final brand_usecase.GetBrandsUseCase getBrandsUseCase;
  final material_usecase.GetMaterialsUseCase getMaterialsUseCase;
  final size_usecase.GetSizesUseCase getSizesUseCase;
  final category_usecase.GetCategoriesUseCase getCategoriesUseCase;
  final location_usecase.GetLocationUseCase getLocationUseCase;
  final design_usecase.GetDesignsUseCase getDesignsUseCase;
  final domain_usecase.GetDomainsUseCase getDomainsUseCase;
  final product_usecase.GetProductsUseCase getProductsUseCase;

  ProductBloc({
    required this.getColorsUseCase,
    required this.getBrandsUseCase,
    required this.getMaterialsUseCase,
    required this.getSizesUseCase,
    required this.getCategoriesUseCase,
    required this.getLocationUseCase,
    required this.getDesignsUseCase,
    required this.getDomainsUseCase,
    required this.getProductsUseCase,
  }) : super(const ProductState()) {
    on<GetColorsEvent>(_onGetColors,
        transformer: throttleDroppable(throttleDuration));

    on<GetBrandsEvent>(_onGetBrands,
        transformer: throttleDroppable(throttleDuration));
    on<GetMaterialsEvent>(_onGetMaterials,
        transformer: throttleDroppable(throttleDuration));
    on<GetSizesEvent>(_onGetSizes,
        transformer: throttleDroppable(throttleDuration));
    on<GetCategoriesEvent>(_onGetCategories,
        transformer: throttleDroppable(throttleDuration));
    on<GetLocationsEvent>(_onGetLocations,
        transformer: throttleDroppable(throttleDuration));
    on<GetDesignsEvent>(_onGetDesigns,
        transformer: throttleDroppable(throttleDuration));
    on<GetDomainsEvent>(_onGetDomains,
        transformer: throttleDroppable(throttleDuration));
    on<GetProductsEvent>(_onGetProducts,
        transformer: throttleDroppable(throttleDuration));
    on<GetFilteredProductsEvent>(_onGetFilteredProducts,
        transformer: throttleDroppable(throttleDuration));
  }

  void _onGetProducts(
      GetProductsEvent event, Emitter<ProductState> emit) async {
    emit(state.copyWith(productStatus: ProductStatus.loading));

    final result = await getProductsUseCase(
      product_usecase.Params(
        search: event.search,
        colorIds: event.colorIds,
        sizeIds: event.sizeIds,
        categoryIds: event.categoryIds,
        brandIds: event.brandIds,
        designIds: event.designIds,
        materialIds: event.materialIds,
        isNegotiable: event.isNegotiable,
        minPrice: event.minPrice,
        maxPrice: event.maxPrice,
        minQuantity: event.minQuantity,
        maxQuantity: event.maxQuantity,
        latitudes: event.latitudes,
        longitudes: event.longitudes,
        radiusInKilometers: event.radiusInKilometers,
        condition: event.condition,
        sortBy: event.sortBy,
        sortOrder: event.sortOrder,
        skip: event.skip,
        limit: event.limit,
      ),
    );

    result.fold(
      (failure) => emit(state.copyWith(productStatus: ProductStatus.failure)),
      (products) => emit(state.copyWith(
          products: products, productStatus: ProductStatus.success)),
    );
  }

  void _onGetFilteredProducts(
      GetFilteredProductsEvent event, Emitter<ProductState> emit) async {
    emit(state.copyWith(filteredProductStatus: FilteredProductStatus.loading));

    final result = await getProductsUseCase(
      product_usecase.Params(
        search: event.search,
        colorIds: event.colorIds,
        sizeIds: event.sizeIds,
        categoryIds: event.categoryIds,
        brandIds: event.brandIds,
        designIds: event.designIds,
        materialIds: event.materialIds,
        isNegotiable: event.isNegotiable,
        minPrice: event.minPrice,
        maxPrice: event.maxPrice,
        minQuantity: event.minQuantity,
        maxQuantity: event.maxQuantity,
        latitudes: event.latitudes,
        longitudes: event.longitudes,
        radiusInKilometers: event.radiusInKilometers,
        condition: event.condition,
        sortBy: event.sortBy,
        sortOrder: event.sortOrder,
        skip: event.skip,
        limit: event.limit,
      ),
    );

    result.fold(
      (failure) => emit(
          state.copyWith(filteredProductStatus: FilteredProductStatus.failure)),
      (products) => emit(state.copyWith(
          filteredProducts: products,
          filteredProductStatus: FilteredProductStatus.success)),
    );
  }

  void _onGetColors(GetColorsEvent event, Emitter<ProductState> emit) async {
    emit(state.copyWith(colorStatus: ColorStatus.loading));

    final result = await getColorsUseCase(NoParams());

    result.fold(
      (failure) => emit(state.copyWith(colorStatus: ColorStatus.failure)),
      (colors) => emit(
          state.copyWith(colors: colors, colorStatus: ColorStatus.success)),
    );
  }

  void _onGetBrands(GetBrandsEvent event, Emitter<ProductState> emit) async {
    emit(state.copyWith(brandStatus: BrandStatus.loading));

    final result = await getBrandsUseCase(NoParams());

    result.fold(
      (failure) => emit(state.copyWith(brandStatus: BrandStatus.failure)),
      (brands) => emit(
          state.copyWith(brands: brands, brandStatus: BrandStatus.success)),
    );
  }

  void _onGetMaterials(
      GetMaterialsEvent event, Emitter<ProductState> emit) async {
    emit(state.copyWith(materialStatus: MaterialStatus.loading));

    final result = await getMaterialsUseCase(NoParams());

    result.fold(
      (failure) => emit(state.copyWith(materialStatus: MaterialStatus.failure)),
      (materials) => emit(state.copyWith(
          materials: materials, materialStatus: MaterialStatus.success)),
    );
  }

  void _onGetSizes(GetSizesEvent event, Emitter<ProductState> emit) async {
    emit(state.copyWith(sizeStatus: SizeStatus.loading));

    final result = await getSizesUseCase(NoParams());

    result.fold(
      (failure) => emit(state.copyWith(sizeStatus: SizeStatus.failure)),
      (sizes) =>
          emit(state.copyWith(sizes: sizes, sizeStatus: SizeStatus.success)),
    );
  }

  void _onGetCategories(
      GetCategoriesEvent event, Emitter<ProductState> emit) async {
    emit(state.copyWith(categoryStatus: CategoryStatus.loading));

    final result = await getCategoriesUseCase(NoParams());

    result.fold(
      (failure) => emit(state.copyWith(categoryStatus: CategoryStatus.failure)),
      (categories) => emit(state.copyWith(
          categories: categories, categoryStatus: CategoryStatus.success)),
    );
  }

  void _onGetLocations(
      GetLocationsEvent event, Emitter<ProductState> emit) async {
    emit(state.copyWith(locationStatus: LocationStatus.loading));

    final result = await getLocationUseCase(NoParams());

    result.fold(
      (failure) => emit(state.copyWith(locationStatus: LocationStatus.failure)),
      (locations) => emit(state.copyWith(
          locations: locations, locationStatus: LocationStatus.success)),
    );
  }

  void _onGetDesigns(GetDesignsEvent event, Emitter<ProductState> emit) async {
    emit(state.copyWith(designStatus: DesignStatus.loading));

    final result = await getDesignsUseCase(NoParams());

    result.fold(
      (failure) => emit(state.copyWith(designStatus: DesignStatus.failure)),
      (designs) => emit(
          state.copyWith(designs: designs, designStatus: DesignStatus.success)),
    );
  }

  void _onGetDomains(GetDomainsEvent event, Emitter<ProductState> emit) async {
    emit(state.copyWith(domainStatus: DomainStatus.loading));

    final result = await getDomainsUseCase(NoParams());

    result.fold(
      (failure) => emit(state.copyWith(domainStatus: DomainStatus.failure)),
      (domains) => emit(
          state.copyWith(domains: domains, domainStatus: DomainStatus.success)),
    );
  }
}
