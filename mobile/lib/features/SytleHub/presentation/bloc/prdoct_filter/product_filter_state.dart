part of 'product_filter_bloc.dart';

class ProductFilterState extends Equatable {
  final Set<String> selectedColors;
  final Set<String> selectedSizes;
  final Set<String> selectedCategories;
  final Set<String> selectedBrands;
  final Set<String> selectedMaterials;
  final Set<String> selectedDesigns;
  final String target;
  final String condition;
  final String search;
  final String sort;
  final String order;
  final double priceMin;
  final double priceMax;
  final double latitute;
  final double longitude;
  final double distance;
  final String location;

  const ProductFilterState({
    this.selectedColors = const <String>{},
    this.selectedSizes = const <String>{},
    this.selectedCategories = const <String>{},
    this.selectedBrands = const <String>{},
    this.selectedMaterials = const <String>{},
    this.selectedDesigns = const <String>{},
    this.priceMin = -1,
    this.priceMax = -1,
    this.location = "",
    this.latitute = 0,
    this.longitude = 0,
    this.distance = 0,
    this.target = "",
    this.condition = "",
    this.search = "",
    this.sort = "",
    this.order = "",
  });

  ProductFilterState copyWith(
      {Set<String>? selectedColors,
      Set<String>? selectedSizes,
      Set<String>? selectedCategories,
      Set<String>? selectedBrands,
      Set<String>? selectedMaterials,
      Set<String>? selectedDesigns,
      double? priceMin,
      double? priceMax,
      String? location,
      double? latitute,
      double? longitude,
      double? distance,
      String? target,
      String? condition,
      String? search,
      String? sort,
      String? order}) {
    return ProductFilterState(
        selectedColors: selectedColors ?? this.selectedColors,
        selectedSizes: selectedSizes ?? this.selectedSizes,
        selectedCategories: selectedCategories ?? this.selectedCategories,
        selectedBrands: selectedBrands ?? this.selectedBrands,
        selectedMaterials: selectedMaterials ?? this.selectedMaterials,
        selectedDesigns: selectedDesigns ?? this.selectedDesigns,
        priceMin: priceMin ?? this.priceMin,
        priceMax: priceMax ?? this.priceMax,
        location: location ?? this.location,
        latitute: latitute ?? this.latitute,
        longitude: longitude ?? this.longitude,
        distance: distance ?? this.distance,
        target: target ?? this.target,
        condition: condition ?? this.condition,
        search: search ?? this.search,
        sort: sort ?? this.sort,
        order: order ?? this.order);
  }

  @override
  List<Object> get props => [
        selectedColors,
        selectedSizes,
        selectedCategories,
        selectedBrands,
        selectedMaterials,
        selectedDesigns,
        priceMin,
        priceMax,
        location,
        latitute,
        longitude,
        distance,
        target,
        condition,
        search,
        sort,
        order
      ];
}
