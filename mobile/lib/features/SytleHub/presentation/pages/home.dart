import 'dart:async';
import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/svg.dart';

import '../../../../setUp/size/app_size.dart';
import '../bloc/product/product_bloc.dart';
import '../bloc/scroll/scroll_bloc.dart';
import '../widgets/button.dart';
import '../widgets/category_chip.dart';
import '../widgets/product.dart';
import '../widgets/search.dart';

const image =
    "https://images.unsplash.com/photo-1539571696357-5a69c17a67c6?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";

class Home extends StatefulWidget {
  const Home({super.key});

  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
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
              Row(
                children: [
                  const CircleAvatar(
                    backgroundImage: NetworkImage(image),
                    radius: 22.5,
                  ),
                  const Spacer(),
                  SvgPicture.asset(
                    "assets/icons/notifaction.svg",
                    height: 32,
                  ),
                ],
              ),
              const SizedBox(height: AppSize.smallSize),
              Row(
                children: [
                  Search(
                    title: "What are you looking for?",
                    controller: searchController,
                  ),
                  const SizedBox(width: AppSize.smallSize),
                  Container(
                    alignment: Alignment.center,
                    width: 45,
                    height: 45,
                    decoration: BoxDecoration(
                      color: Theme.of(context).colorScheme.primary,
                      borderRadius: BorderRadius.circular(AppSize.xSmallSize),
                    ),
                    child: Icon(
                      Icons.location_on_outlined,
                      color: Theme.of(context).colorScheme.onPrimary,
                    ),
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
                                          onTap: () {},
                                        ))
                                    .toList()),
                          if (width >= 700)
                            SizedBox(
                              height: 108,
                              child: ListView.builder(
                                scrollDirection: Axis.horizontal,
                                itemCount: 28,
                                itemBuilder: (context, index) {
                                  return Padding(
                                    padding: const EdgeInsets.only(right: 16),
                                    child: CategoryChip(
                                        name: "category",
                                        image: image,
                                        onTap: () {}),
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
