import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:meta/meta.dart';

part 'product_filter_event.dart';
part 'product_filter_state.dart';

class ProductFilterBloc extends Bloc<ProductFilterEvent, ProductFilterState> {
  ProductFilterBloc() : super(const ProductFilterState()) {
    // color
    on<AddSelectedColorEvent>((event, emit) {
      emit(state.copyWith(
        selectedColors: Set<String>.from(state.selectedColors)
          ..add(event.color),
      ));
    });
    on<RemoveSelectedColorEvent>((event, emit) {
      emit(state.copyWith(
        selectedColors: Set<String>.from(state.selectedColors)
          ..remove(event.color),
      ));
    });

    on<ClearSelectedColorsEvent>((event, emit) {
      emit(state.copyWith(selectedColors: const <String>{}));
    });

    // material
    on<AddSelectedMaterialEvent>((event, emit) {
      emit(state.copyWith(
        selectedMaterials: Set<String>.from(state.selectedMaterials)
          ..add(event.material),
      ));
    });

    on<RemoveSelectedMaterialEvent>((event, emit) {
      emit(state.copyWith(
        selectedMaterials: Set<String>.from(state.selectedMaterials)
          ..remove(event.material),
      ));
    });

    on<ClearSelectedMaterialsEvent>((event, emit) {
      emit(state.copyWith(selectedMaterials: const <String>{}));
    });

    // size
    on<AddSelectedSizeEvent>((event, emit) {
      emit(state.copyWith(
        selectedSizes: Set<String>.from(state.selectedSizes)..add(event.size),
      ));
    });

    on<RemoveSelectedSizeEvent>((event, emit) {
      emit(state.copyWith(
        selectedSizes: Set<String>.from(state.selectedSizes)
          ..remove(event.size),
      ));
    });

    on<ClearSelectedSizesEvent>((event, emit) {
      emit(state.copyWith(selectedSizes: const <String>{}));
    });

    // brand
    on<AddSelectedBrandEvent>((event, emit) {
      emit(state.copyWith(
        selectedBrands: Set<String>.from(state.selectedBrands)
          ..add(event.brand),
      ));
    });

    on<RemoveSelectedBrandEvent>((event, emit) {
      emit(state.copyWith(
        selectedBrands: Set<String>.from(state.selectedBrands)
          ..remove(event.brand),
      ));
    });

    on<ClearSelectedBrandsEvent>((event, emit) {
      emit(state.copyWith(selectedBrands: const <String>{}));
    });

    // design
    on<AddSelectedDesignEvent>((event, emit) {
      emit(state.copyWith(
        selectedDesigns: Set<String>.from(state.selectedDesigns)
          ..add(event.design),
      ));
    });

    on<RemoveSelectedDesignEvent>((event, emit) {
      emit(state.copyWith(
        selectedDesigns: Set<String>.from(state.selectedDesigns)
          ..remove(event.design),
      ));
    });

    on<ClearSelectedDesignsEvent>((event, emit) {
      emit(state.copyWith(selectedDesigns: const <String>{}));
    });

    // price
    on<SetPriceRangeEvent>((event, emit) {
      emit(state.copyWith(priceMin: event.priceMin, priceMax: event.priceMax));
    });

    on<ClearPriceRangeEvent>((event, emit) {
      emit(state.copyWith(priceMin: 1, priceMax: 10000));
    });

    // conditions
    on<SetConditionEvent>((event, emit) {
      emit(state.copyWith(condition: event.condition));
    });

    on<ClearConditionEvent>((event, emit) {
      emit(state.copyWith(condition: ""));
    });

    on<ClearTargetEvent>((event, emit) {
      emit(state.copyWith(target: ""));
    });

    // location

    on<SetLocationEvent>((event, emit) {
      emit(state.copyWith(
          location: event.location,
          latitute: event.latitute,
          longitude: event.longitude,
          distance: event.distance));
    });

    on<ClearLocationEvent>((event, emit) {
      emit(state.copyWith(
          location: "", latitute: null, longitude: null, distance: null));
    });

    // sort
    on<SetSortByEvent>((event, emit) {
      emit(state.copyWith(sort: event.sortBy, order: event.order));
    });

    on<ClearSortOrderEvent>((event, emit) {
      emit(state.copyWith(sort: "", order: ""));
    });

    // clear all
    on<ClearAllEvent>((event, emit) {
      emit(const ProductFilterState());
    });
  }
}
