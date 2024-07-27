import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:style_hub/features/SytleHub/presentation/widgets/common/app_bar_one.dart';

import '../../../../core/utils/captilizations.dart';
import '../../../../setUp/size/app_size.dart';
import '../../domain/entities/product/category_entity.dart';
import '../bloc/prdoct_filter/product_filter_bloc.dart';
import '../bloc/product/product_bloc.dart';
import '../bloc/scroll/scroll_bloc.dart';
import '../bloc/user/user_bloc.dart';
import '../widgets/common/button.dart';
import '../widgets/filter/all_filter.dart';
import '../widgets/filter/half_brand_filter.dart';
import '../widgets/filter/half_color_filter.dart';
import '../widgets/filter/half_design_filter.dart';
import '../widgets/filter/half_location_filter.dart';
import '../widgets/filter/half_material_filter.dart';
import '../widgets/filter/half_price_filter.dart';
import '../widgets/filter/half_size_filter.dart';
import '../widgets/common/product.dart';
import 'filter/brand.dart';
import 'filter/color.dart';
import 'filter/design.dart';
import 'filter/location.dart';
import 'filter/material.dart';
import 'filter/price.dart';
import 'filter/size.dart';

enum Filters { color, material, size, brand, price, location, design, all }

class ProductList extends StatefulWidget {
  const ProductList({super.key, required this.categories});

  final List<CategoryEntity> categories;

  @override
  State<ProductList> createState() => _ProductListState();
}

class _ProductListState extends State<ProductList> {
  int currentIndex = 0;
  @override
  void initState() {
    super.initState();
    context.read<ProductFilterBloc>().add(ClearAllEvent());
    context.read<ProductBloc>().add(GetFilteredProductsEvent(
          token: context.read<UserBloc>().state.user?.token ?? "",
          categoryIds: [widget.categories[currentIndex].id],
        ));

    _scrollController.addListener(_scrollListener);
  }

  final ScrollController _scrollController = ScrollController();
  Timer? _scrollEndTimer;
  final TextEditingController searchController = TextEditingController();

  void _scrollListener() {
    if (_scrollEndTimer != null && _scrollEndTimer!.isActive) {
      _scrollEndTimer!.cancel();
    }

    if (_scrollController.position.userScrollDirection ==
        ScrollDirection.reverse) {
      context.read<ScrollBloc>().add(ToggleVisibilityEvent(isVisible: false));
    } else if (_scrollController.position.userScrollDirection ==
        ScrollDirection.forward) {
      context.read<ScrollBloc>().add(ToggleVisibilityEvent(isVisible: true));
    }
  }

  @override
  void dispose() {
    _scrollController.removeListener(_scrollListener);
    _scrollController.dispose();
    super.dispose();
  }

  bool isExpanded = false;

  void onfilterMaterial() {
    context.read<ProductFilterBloc>().add(ClearAllEvent());
    context.read<ProductBloc>().add(GetFilteredProductsEvent(
          token: context.read<UserBloc>().state.user?.token ?? "",
          materialIds: context
              .read<ProductFilterBloc>()
              .state
              .selectedMaterials
              .toList(),
          categoryIds: [widget.categories[currentIndex].id],
        ));
  }

  void onfilterSize() {
    context.read<ProductFilterBloc>().add(ClearAllEvent());
    context.read<ProductBloc>().add(GetFilteredProductsEvent(
          token: context.read<UserBloc>().state.user?.token ?? "",
          sizeIds:
              context.read<ProductFilterBloc>().state.selectedSizes.toList(),
          categoryIds: [widget.categories[currentIndex].id],
        ));
  }

  void onfilterBrand() {
    context.read<ProductFilterBloc>().add(ClearAllEvent());
    context.read<ProductBloc>().add(GetFilteredProductsEvent(
          token: context.read<UserBloc>().state.user?.token ?? "",
          brandIds:
              context.read<ProductFilterBloc>().state.selectedBrands.toList(),
          categoryIds: [widget.categories[currentIndex].id],
        ));
  }

  void onfilterColor() {
    context.read<ProductFilterBloc>().add(ClearAllEvent());
    context.read<ProductBloc>().add(GetFilteredProductsEvent(
          token: context.read<UserBloc>().state.user?.token ?? "",
          colorIds:
              context.read<ProductFilterBloc>().state.selectedColors.toList(),
          categoryIds: [widget.categories[currentIndex].id],
        ));
  }

  void onfilterLocation() {
    context.read<ProductFilterBloc>().add(ClearAllEvent());
    context.read<ProductBloc>().add(GetFilteredProductsEvent(
          token: context.read<UserBloc>().state.user?.token ?? "",
          latitudes: context.read<ProductFilterBloc>().state.latitute,
          longitudes: context.read<ProductFilterBloc>().state.longitude,
          radiusInKilometers: context.read<ProductFilterBloc>().state.distance,
          categoryIds: [widget.categories[currentIndex].id],
        ));
  }

  void onfilterDesign() {
    context.read<ProductFilterBloc>().add(ClearAllEvent());
    context.read<ProductBloc>().add(GetFilteredProductsEvent(
          token: context.read<UserBloc>().state.user?.token ?? "",
          designIds:
              context.read<ProductFilterBloc>().state.selectedDesigns.toList(),
          categoryIds: [widget.categories[currentIndex].id],
        ));
  }

  void onfilterPrice() {
    context.read<ProductFilterBloc>().add(ClearAllEvent());
    context.read<ProductBloc>().add(GetFilteredProductsEvent(
          token: context.read<UserBloc>().state.user?.token ?? "",
          minPrice: context.read<ProductFilterBloc>().state.priceMin,
          maxPrice: context.read<ProductFilterBloc>().state.priceMax,
          categoryIds: [widget.categories[currentIndex].id],
        ));
  }

  void onfilter() {
    context.read<ProductFilterBloc>().add(ClearAllEvent());
    context.read<ProductBloc>().add(GetFilteredProductsEvent(
          token: context.read<UserBloc>().state.user?.token ?? "",
          categoryIds: [widget.categories[currentIndex].id],
          materialIds: context
              .read<ProductFilterBloc>()
              .state
              .selectedMaterials
              .toList(),
          sortBy: context.read<ProductFilterBloc>().state.sort,
          sortOrder: context.read<ProductFilterBloc>().state.order,
          designIds:
              context.read<ProductFilterBloc>().state.selectedDesigns.toList(),
          sizeIds:
              context.read<ProductFilterBloc>().state.selectedSizes.toList(),
          brandIds:
              context.read<ProductFilterBloc>().state.selectedBrands.toList(),
          colorIds:
              context.read<ProductFilterBloc>().state.selectedColors.toList(),
          latitudes: context.read<ProductFilterBloc>().state.latitute,
          longitudes: context.read<ProductFilterBloc>().state.longitude,
          radiusInKilometers: context.read<ProductFilterBloc>().state.distance,
          minPrice: context.read<ProductFilterBloc>().state.priceMin,
          maxPrice: context.read<ProductFilterBloc>().state.priceMax,
        ));
  }

  void onChangeCategory(int index) {
    context.read<ProductFilterBloc>().add(ClearAllEvent());
    print(index);
    context.read<ProductBloc>().add(GetFilteredProductsEvent(
          token: context.read<UserBloc>().state.user?.token ?? "",
          categoryIds: [widget.categories[index].id],
        ));
    setState(() {
      currentIndex = index;
    });
  }

  void displayBottomSheet(BuildContext context, Filters filterType) {
    showModalBottomSheet<void>(
      isScrollControlled: true,
      useSafeArea: true,
      backgroundColor: Theme.of(context).colorScheme.onPrimary,
      useRootNavigator: true,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.only(
          topLeft: Radius.circular(AppSize.xxxSmallSize),
          topRight: Radius.circular(AppSize.xxxSmallSize),
        ),
      ),
      context: context,
      builder: (BuildContext context) {
        return GestureDetector(
          onVerticalDragUpdate: (details) async {
            if (details.delta.dy < -5) {
              final data = await Navigator.push(
                context,
                PageRouteBuilder(
                  pageBuilder: (context, animation, secondaryAnimation) =>
                      filterType == Filters.color
                          ? ColorFullFilterScreen(
                              isAdd: false, onTap: onfilterColor)
                          : filterType == Filters.price
                              ? PriceFullFilterScreen(
                                  isAdd: false, onTap: onfilterPrice)
                              : filterType == Filters.material
                                  ? MaterialFullFilterScreen(
                                      isAdd: false, onTap: onfilterMaterial)
                                  : filterType == Filters.size
                                      ? SizeFullFilterScreen(
                                          isAdd: false, onTap: onfilterSize)
                                      : filterType == Filters.design
                                          ? DesignFullFilterScreen(
                                              isAdd: false,
                                              onTap: onfilterDesign)
                                          : filterType == Filters.location
                                              ? LocationFullFilterScreen(
                                                  isAdd: false,
                                                  onTap: onfilterLocation)
                                              : filterType == Filters.brand
                                                  ? BrandFullFilterScreen(
                                                      isAdd: false,
                                                      onTap: onfilterBrand)
                                                  : SafeArea(
                                                      child: Scaffold(
                                                          backgroundColor:
                                                              Theme.of(context)
                                                                  .colorScheme
                                                                  .onPrimary,
                                                          body: AllFilterDisplay(
                                                              isAdd: false,
                                                              onTap: onfilter)),
                                                    ),
                  transitionsBuilder:
                      (context, animation, secondaryAnimation, child) {
                    const begin = Offset(0.0, 1.0);
                    const end = Offset.zero;
                    const curve = Curves.ease;
                    var tween = Tween(begin: begin, end: end)
                        .chain(CurveTween(curve: curve));
                    var offsetAnimation = animation.drive(tween);

                    return SlideTransition(
                      position: offsetAnimation,
                      child: child,
                    );
                  },
                  transitionDuration: const Duration(milliseconds: 300),
                ),
              );
              if (data != null && data == true) {
                Navigator.pop(context);
              }
            }
            if (details.delta.dy > 5) {
              Navigator.pop(context);
            }
          },
          child: filterType == Filters.color
              ? HalfColorFilterDisplay(isAdd: false, onTap: onfilterColor)
              : filterType == Filters.price
                  ? HalfPriceFilterDisplay(isAdd: false, onTap: onfilterPrice)
                  : filterType == Filters.material
                      ? HalfMaterialFilterDisplay(
                          isAdd: false, onTap: onfilterMaterial)
                      : filterType == Filters.size
                          ? HalfSizeFilterDisplay(
                              isAdd: false, onTap: onfilterSize)
                          : filterType == Filters.brand
                              ? HalfBrandFilterDisplay(
                                  isAdd: false, onTap: onfilterBrand)
                              : filterType == Filters.location
                                  ? HalfLocationFilterDisplay(
                                      isAdd: false, onTap: onfilterLocation)
                                  : filterType == Filters.design
                                      ? HalfDesignFilterDisplay(
                                          isAdd: false, onTap: onfilterDesign)
                                      : AllFilterDisplay(
                                          isAdd: false, onTap: onfilter),
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        backgroundColor: Theme.of(context).colorScheme.onPrimary,
        body: Column(
          children: [
            const AppBarOne(),
            Container(
                height: 2,
                color: Theme.of(context).colorScheme.primaryContainer),
            Expanded(
              child: Padding(
                padding: const EdgeInsets.all(AppSize.smallSize),
                child: CustomScrollView(
                  controller: _scrollController,
                  slivers: [
                    SliverToBoxAdapter(
                      child: Wrap(
                        spacing: AppSize.smallSize,
                        crossAxisAlignment: WrapCrossAlignment.center,
                        runSpacing: AppSize.xSmallSize,
                        children: [
                          GestureDetector(
                            onTap: () =>
                                displayBottomSheet(context, Filters.all),
                            child: Row(
                              mainAxisSize: MainAxisSize.min,
                              children: [
                                Icon(
                                  Icons.filter_alt_outlined,
                                  color:
                                      Theme.of(context).colorScheme.onSurface,
                                ),
                                const SizedBox(width: 2),
                                Text(
                                  "All Filters",
                                  style: Theme.of(context)
                                      .textTheme
                                      .titleMedium!
                                      .copyWith(
                                        color: Theme.of(context)
                                            .colorScheme
                                            .onSurface,
                                      ),
                                ),
                              ],
                            ),
                          ),

                          // vertial line
                          Container(
                            height: 20,
                            width: 1,
                            color: Theme.of(context).colorScheme.onSurface,
                          ),

                          GestureDetector(
                            onTap: () =>
                                displayBottomSheet(context, Filters.brand),
                            child: Text(
                              "Brand",
                              style: Theme.of(context)
                                  .textTheme
                                  .titleMedium!
                                  .copyWith(
                                    color:
                                        Theme.of(context).colorScheme.onSurface,
                                  ),
                            ),
                          ),

                          GestureDetector(
                            onTap: () =>
                                displayBottomSheet(context, Filters.color),
                            child: Text(
                              "Color",
                              style: Theme.of(context)
                                  .textTheme
                                  .titleMedium!
                                  .copyWith(
                                    color:
                                        Theme.of(context).colorScheme.onSurface,
                                  ),
                            ),
                          ),
                          GestureDetector(
                            onTap: () =>
                                displayBottomSheet(context, Filters.design),
                            child: Text(
                              "Design",
                              style: Theme.of(context)
                                  .textTheme
                                  .titleMedium!
                                  .copyWith(
                                    color:
                                        Theme.of(context).colorScheme.onSurface,
                                  ),
                            ),
                          ),
                          GestureDetector(
                            onTap: () =>
                                displayBottomSheet(context, Filters.location),
                            child: Text(
                              "Location",
                              style: Theme.of(context)
                                  .textTheme
                                  .titleMedium!
                                  .copyWith(
                                    color:
                                        Theme.of(context).colorScheme.onSurface,
                                  ),
                            ),
                          ),
                          GestureDetector(
                            onTap: () =>
                                displayBottomSheet(context, Filters.material),
                            child: Text(
                              "Material",
                              style: Theme.of(context)
                                  .textTheme
                                  .titleMedium!
                                  .copyWith(
                                    color:
                                        Theme.of(context).colorScheme.onSurface,
                                  ),
                            ),
                          ),

                          GestureDetector(
                            onTap: () =>
                                displayBottomSheet(context, Filters.price),
                            child: Text(
                              "Price",
                              style: Theme.of(context)
                                  .textTheme
                                  .titleMedium!
                                  .copyWith(
                                    color:
                                        Theme.of(context).colorScheme.onSurface,
                                  ),
                            ),
                          ),

                          GestureDetector(
                            onTap: () =>
                                displayBottomSheet(context, Filters.size),
                            child: Text(
                              "Size",
                              style: Theme.of(context)
                                  .textTheme
                                  .titleMedium!
                                  .copyWith(
                                    color:
                                        Theme.of(context).colorScheme.onSurface,
                                  ),
                            ),
                          ),
                        ],
                      ),
                    ),
                    const SliverToBoxAdapter(
                      child: SizedBox(height: AppSize.smallSize),
                    ),
                    SliverAppBar(
                      pinned: true,
                      automaticallyImplyLeading: false,
                      forceMaterialTransparency: true,
                      backgroundColor: Theme.of(context).colorScheme.onPrimary,
                      toolbarHeight: 55,
                      flexibleSpace: Container(
                        color: Theme.of(context).colorScheme.onPrimary,
                        child: ListView.builder(
                          padding:
                              const EdgeInsets.only(bottom: AppSize.smallSize),
                          scrollDirection: Axis.horizontal,
                          itemCount: widget.categories.length,
                          itemBuilder: (context, index) {
                            return ChipButton(
                              text: Captilizations.capitalizeFirstOfEach(
                                  widget.categories[index].name),
                              onTap: () {
                                if (currentIndex != index) {
                                  onChangeCategory(index);
                                }
                              },
                              isActive: index == currentIndex,
                            );
                          },
                        ),
                      ),
                    ),
                    if (context
                            .watch<ProductBloc>()
                            .state
                            .filteredProductStatus ==
                        FilteredProductStatus.loading)
                      const SliverToBoxAdapter(
                        child: Center(
                          child: CircularProgressIndicator(),
                        ),
                      ),
                    if (context
                            .watch<ProductBloc>()
                            .state
                            .filteredProductStatus ==
                        FilteredProductStatus.success)
                      SliverToBoxAdapter(
                        child: Wrap(
                          spacing: AppSize.smallSize,
                          runSpacing: AppSize.smallSize,
                          children: context
                              .watch<ProductBloc>()
                              .state
                              .filteredProducts
                              .map((e) => Product(
                                    product: e,
                                  ))
                              .toList(),
                        ),
                      ),
                  ],
                ),
              ),
            )
          ],
        ),
      ),
    );
  }
}
