import 'dart:async';
import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:persistent_bottom_nav_bar/persistent_tab_view.dart';
import 'package:style_hub/features/SytleHub/presentation/pages/product_list.dart';

import '../../../../setUp/size/app_size.dart';
import '../bloc/product/product_bloc.dart';
import '../bloc/scroll/scroll_bloc.dart';
import '../widgets/common/button.dart';
import '../widgets/common/category_chip.dart';
import '../widgets/common/app_bar_two.dart';
import '../widgets/common/product.dart';
import '../widgets/common/search.dart';

class HomeScreen extends StatefulWidget {
  const HomeScreen({super.key});

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  @override
  void initState() {
    super.initState();

    _scrollController.addListener(_scrollListener);
  }

  final ScrollController _scrollController = ScrollController();
  Timer? _scrollEndTimer;

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

  @override
  Widget build(BuildContext context) {
    const List<String> categories = [
      "For you",
      "Trending",
      "New",
      "Popular",
      "Recommended",
    ];
    double width = MediaQuery.of(context).size.width;

    int numbersDisplayed = 0;
    if (width <= 240) {
      numbersDisplayed = 4;
    } else if (width <= 340) {
      numbersDisplayed = 6;
    } else if (width <= 450) {
      numbersDisplayed = 12;
    } else if (width <= 550) {
      numbersDisplayed = 10;
    } else if (width <= 650) {
      numbersDisplayed = 12;
    } else if (width <= 750) {
      numbersDisplayed = 14;
    } else {
      numbersDisplayed = 20;
    }

    return SafeArea(
      child: Scaffold(
        backgroundColor: Theme.of(context).colorScheme.onPrimary,
        body: Padding(
          padding: const EdgeInsets.all(AppSize.smallSize),
          child: Column(
            children: [
              const AppBarTwo(),
              const SizedBox(height: AppSize.smallSize),
              Row(
                children: [
                  Search(
                    title: "What are you looking for?",
                    controller: searchController,
                  ),
                ],
              ),
              const SizedBox(height: AppSize.smallSize),
              Expanded(
                child: CustomScrollView(
                  controller: _scrollController,
                  slivers: [
                    SliverToBoxAdapter(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        mainAxisAlignment: MainAxisAlignment.start,
                        children: [
                          Row(
                            children: [
                              Text(
                                "Explore",
                                style: TextStyle(
                                  fontSize: 18,
                                  fontWeight: FontWeight.w500,
                                  height: 1.25,
                                  color:
                                      Theme.of(context).colorScheme.onSurface,
                                ),
                              ),
                              const Spacer(),
                              Text(
                                "View All",
                                style: Theme.of(context)
                                    .textTheme
                                    .bodySmall!
                                    .copyWith(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .secondary,
                                    ),
                              ),
                            ],
                          ),
                          const SizedBox(height: AppSize.smallSize),
                          if (width < 700)
                            Wrap(
                                spacing: AppSize.smallSize,
                                runSpacing: AppSize.smallSize,
                                children: context
                                    .watch<ProductBloc>()
                                    .state
                                    .categories
                                    .sublist(
                                        0,
                                        min(
                                            context
                                                .watch<ProductBloc>()
                                                .state
                                                .categories
                                                .length,
                                            numbersDisplayed))
                                    .map((e) => CategoryChip(
                                          name: e.name,
                                          image: e.image,
                                          onTap: () {
                                            PersistentNavBarNavigator
                                                .pushNewScreenWithRouteSettings(
                                              context,
                                              settings: const RouteSettings(
                                                  name: '/productList'),
                                              withNavBar: false,
                                              screen: ProductList(
                                                  categories: context
                                                      .read<ProductBloc>()
                                                      .state
                                                      .categories),
                                              pageTransitionAnimation:
                                                  PageTransitionAnimation.fade,
                                            );
                                          },
                                        ))
                                    .toList()),
                          if (width >= 700)
                            SizedBox(
                              height: 108,
                              child: ListView.builder(
                                scrollDirection: Axis.horizontal,
                                itemCount: context
                                    .watch<ProductBloc>()
                                    .state
                                    .categories
                                    .length,
                                itemBuilder: (context, index) {
                                  return Padding(
                                    padding: const EdgeInsets.only(right: 16),
                                    child: CategoryChip(
                                        name: context
                                            .watch<ProductBloc>()
                                            .state
                                            .categories[index]
                                            .name,
                                        image: context
                                            .watch<ProductBloc>()
                                            .state
                                            .categories[index]
                                            .image,
                                        onTap: () {
                                          PersistentNavBarNavigator
                                              .pushNewScreenWithRouteSettings(
                                            context,
                                            settings: const RouteSettings(
                                                name: '/productList'),
                                            withNavBar: false,
                                            screen: ProductList(
                                                categories: context
                                                    .read<ProductBloc>()
                                                    .state
                                                    .categories),
                                            pageTransitionAnimation:
                                                PageTransitionAnimation.fade,
                                          );
                                        }),
                                  );
                                },
                              ),
                            ),
                        ],
                      ),
                    ),
                    const SliverToBoxAdapter(
                        child: SizedBox(height: AppSize.mediumSize)),
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
                          itemCount: categories.length,
                          itemBuilder: (context, index) {
                            return ChipButton(
                              text: categories[index],
                              onTap: () {},
                              isActive: index == 0,
                            );
                          },
                        ),
                      ),
                    ),
                    SliverToBoxAdapter(
                      child: Wrap(
                        spacing: AppSize.smallSize,
                        runSpacing: AppSize.smallSize,
                        children: context
                            .watch<ProductBloc>()
                            .state
                            .products
                            .map((e) => Product(
                                  product: e,
                                ))
                            .toList(),
                      ),
                    ),
                  ],
                ),
              )
            ],
          ),
        ),
      ),
    );
  }
}
