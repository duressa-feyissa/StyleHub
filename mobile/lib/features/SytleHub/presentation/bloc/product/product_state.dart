part of 'product_bloc.dart';

enum ColorStatus { initial, loading, success, failure }

enum SizeStatus { initial, loading, success, failure }

enum CategoryStatus { initial, loading, success, failure }

enum BrandStatus { initial, loading, success, failure }

enum MaterialStatus { initial, loading, success, failure }

enum LocationStatus { initial, loading, success, failure }

enum DesignStatus { initial, loading, success, failure }

enum DomainStatus { initial, loading, success, failure }

enum ProductStatus { initial, loading, success, failure }

enum FilteredProductStatus { initial, loading, success, failure }


class ProductState extends Equatable {
  final List<ColorEntity> colors;
  final ColorStatus colorStatus;
  final List<SizeEntity> sizes;
  final SizeStatus sizeStatus;
  final List<CategoryEntity> categories;
  final CategoryStatus categoryStatus;
  final List<BrandEntity> brands;
  final BrandStatus brandStatus;
  final List<MaterialEntity> materials;
  final MaterialStatus materialStatus;
  final List<LocationEntity> locations;
  final LocationStatus locationStatus;
  final List<DesignEntity> designs;
  final DesignStatus designStatus;
  final List<DomainEntity> domains;
  final DomainStatus domainStatus;
  final List<ProductEntity> products;
  final ProductStatus productStatus;
  final FilteredProductStatus filteredProductStatus;
  final List<ProductEntity> filteredProducts;

  const ProductState({
    this.colors = const <ColorEntity>[],
    this.colorStatus = ColorStatus.initial,
    this.sizes = const <SizeEntity>[],
    this.sizeStatus = SizeStatus.initial,
    this.categories = const <CategoryEntity>[],
    this.categoryStatus = CategoryStatus.initial,
    this.brands = const <BrandEntity>[],
    this.brandStatus = BrandStatus.initial,
    this.materials = const <MaterialEntity>[],
    this.materialStatus = MaterialStatus.initial,
    this.locations = const <LocationEntity>[],
    this.locationStatus = LocationStatus.initial,
    this.designs = const <DesignEntity>[],
    this.designStatus = DesignStatus.initial,
    this.domains = const <DomainEntity>[],
    this.domainStatus = DomainStatus.initial,
    this.products = const <ProductEntity>[],
    this.productStatus = ProductStatus.initial,
    this.filteredProductStatus = FilteredProductStatus.initial,
    this.filteredProducts = const <ProductEntity>[],
  });

  ProductState copyWith({
    List<ColorEntity>? colors,
    ColorStatus? colorStatus,
    List<SizeEntity>? sizes,
    SizeStatus? sizeStatus,
    List<CategoryEntity>? categories,
    CategoryStatus? categoryStatus,
    List<BrandEntity>? brands,
    BrandStatus? brandStatus,
    List<MaterialEntity>? materials,
    MaterialStatus? materialStatus,
    List<LocationEntity>? locations,
    LocationStatus? locationStatus,
    List<DesignEntity>? designs,
    DesignStatus? designStatus,
    List<DomainEntity>? domains,
    DomainStatus? domainStatus,
    List<ProductEntity>? products,
    ProductStatus? productStatus,
    FilteredProductStatus? filteredProductStatus,
    List<ProductEntity>? filteredProducts,
  }) {
    return ProductState(
      colors: colors ?? this.colors,
      colorStatus: colorStatus ?? this.colorStatus,
      sizes: sizes ?? this.sizes,
      sizeStatus: sizeStatus ?? this.sizeStatus,
      categories: categories ?? this.categories,
      categoryStatus: categoryStatus ?? this.categoryStatus,
      brands: brands ?? this.brands,
      brandStatus: brandStatus ?? this.brandStatus,
      materials: materials ?? this.materials,
      materialStatus: materialStatus ?? this.materialStatus,
      locations: locations ?? this.locations,
      locationStatus: locationStatus ?? this.locationStatus,
      designs: designs ?? this.designs,
      designStatus: designStatus ?? this.designStatus,
      domains: domains ?? this.domains,
      domainStatus: domainStatus ?? this.domainStatus,
      products: products ?? this.products,
      productStatus: productStatus ?? this.productStatus,
      filteredProductStatus: filteredProductStatus ?? this.filteredProductStatus,
      filteredProducts: filteredProducts ?? this.filteredProducts,
    );
  }

  @override
  List<Object?> get props => [
        colors,
        colorStatus,
        sizes,
        sizeStatus,
        categories,
        categoryStatus,
        brands,
        brandStatus,
        materials,
        materialStatus,
        locations,
        locationStatus,
        designs,
        designStatus,
        domains,
        domainStatus,
        products,
        productStatus,
        filteredProductStatus,
        filteredProducts,
      ];
}
