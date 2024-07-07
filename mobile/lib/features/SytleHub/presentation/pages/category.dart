import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/svg.dart';
import 'package:persistent_bottom_nav_bar/persistent_tab_view.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/category_entity.dart';

import '../../../../core/utils/captilizations.dart';
import '../../../../setUp/size/app_size.dart';
import '../bloc/product/product_bloc.dart';
import '../bloc/scroll/scroll_bloc.dart';
import '../widgets/category_chip.dart';
import '../widgets/category_swap_chip.dart';
import '../widgets/search.dart';
import '../widgets/sub_category_list.dart';
import 'product_list.dart';

const image =
    "https://images.unsplash.com/photo-1539571696357-5a69c17a67c6?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";

class CategoryScreen extends StatefulWidget {
  const CategoryScreen({super.key});

  @override
  State<CategoryScreen> createState() => _CategoryScreenState();
}

class _CategoryScreenState extends State<CategoryScreen> {
  @override
  void initState() {
    super.initState();
    _scrollController.addListener(_scrollListener);
  }

  final ScrollController _scrollController = ScrollController();
  Timer? _scrollEndTimer;
  int currentIndex = 0;

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

  final TextEditingController searchController = TextEditingController();

  @override
  void dispose() {
    _scrollController.removeListener(_scrollListener);
    _scrollController.dispose();
    super.dispose();
  }

  void gotoProductList(List<CategoryEntity> categories) {
    PersistentNavBarNavigator.pushNewScreenWithRouteSettings(
      context,
      settings: const RouteSettings(name: '/contest/standing'),
      withNavBar: false,
      screen: ProductList(categories: categories),
      pageTransitionAnimation: PageTransitionAnimation.fade,
    );
  }

  void onChipTap(int index) {
    setState(() {
      currentIndex = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        backgroundColor: Theme.of(context).colorScheme.onPrimary,
        body: Column(
          children: [
            Padding(
              padding: const EdgeInsets.all(AppSize.smallSize),
              child: Row(
                children: [
                  const CircleAvatar(
                    backgroundImage: NetworkImage(image),
                    radius: 22.5,
                  ),
                  const SizedBox(width: AppSize.smallSize),
                  Search(
                    title: "What are you looking for?",
                    controller: searchController,
                  ),
                  const SizedBox(width: AppSize.smallSize),
                  SvgPicture.asset(
                    "assets/icons/notifaction.svg",
                    height: 32,
                  ),
                ],
              ),
            ),
            if (context.watch<ProductBloc>().state.domainStatus ==
                DomainStatus.loading)
              const Expanded(child: Center(child: CircularProgressIndicator())),
            if (context.watch<ProductBloc>().state.domainStatus ==
                DomainStatus.success)
              Container(
                height: 35,
                padding: const EdgeInsets.only(
                    left: AppSize.smallSize, right: AppSize.smallSize),
                decoration: BoxDecoration(
                  border: Border(
                    bottom: BorderSide(
                      color: Theme.of(context)
                          .colorScheme
                          .onSurface
                          .withOpacity(0.1),
                      width: 0.5,
                    ),
                  ),
                ),
                child: ListView.builder(
                  scrollDirection: Axis.horizontal,
                  itemCount: context.watch<ProductBloc>().state.domains.length,
                  itemBuilder: (context, index) {
                    return Padding(
                      padding: const EdgeInsets.only(right: AppSize.mediumSize),
                      child: AnimatedContainer(
                        duration: const Duration(milliseconds: 300),
                        curve: Curves.easeInOut,
                        child: CategorySwapChip(
                          text: Captilizations.capitalize(context
                              .watch<ProductBloc>()
                              .state
                              .domains[index]
                              .name),
                          onTap: () => onChipTap(index),
                          isActive: index == currentIndex,
                        ),
                      ),
                    );
                  },
                ),
              ),
            if (context.watch<ProductBloc>().state.domainStatus ==
                DomainStatus.success)
              Expanded(
                child: GestureDetector(
                  onHorizontalDragEnd: (details) {
                    int length =
                        context.read<ProductBloc>().state.domains.length;
                    if (details.primaryVelocity! > 0) {
                      if (currentIndex > 0) {
                        onChipTap(currentIndex - 1);
                      }
                    } else {
                      if (currentIndex < length - 1) {
                        onChipTap(currentIndex + 1);
                      }
                    }
                  },
                  child: ListView.builder(
                    padding: const EdgeInsets.all(AppSize.smallSize),
                    controller: _scrollController,
                    itemCount: context
                        .watch<ProductBloc>()
                        .state
                        .domains[currentIndex]
                        .subDomain
                        .length,
                    itemBuilder: (context, index) {
                      return SubCategoryList(
                        title: Captilizations.capitalizeFirstOfEach(context
                            .watch<ProductBloc>()
                            .state
                            .domains[currentIndex]
                            .subDomain[index]
                            .name),
                        subCategories: context
                            .watch<ProductBloc>()
                            .state
                            .domains[currentIndex]
                            .subDomain[index]
                            .category
                            .map((e) => CategoryChip(
                                name: Captilizations.capitalizeFirstOfEach(
                                    e.name),
                                image: e.image,
                                onTap: () {
                                  gotoProductList(context
                                      .read<ProductBloc>()
                                      .state
                                      .domains[currentIndex]
                                      .subDomain[index]
                                      .category);
                                }))
                            .toList(),
                      );
                    },
                  ),
                ),
              ),
          ],
        ),
      ),
    );
  }
}
