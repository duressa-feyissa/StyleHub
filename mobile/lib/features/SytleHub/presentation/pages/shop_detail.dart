import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_rating_stars/flutter_rating_stars.dart';
import 'package:style_hub/features/SytleHub/presentation/widgets/common/app_bar_one.dart';
import 'package:style_hub/features/SytleHub/presentation/widgets/shop/product_list.dart';
import 'package:style_hub/features/SytleHub/presentation/widgets/shop/shop_review.dart';

import '../../../../setUp/size/app_size.dart';
import '../bloc/shop/shop_bloc.dart';
import '../widgets/shop/shop_about.dart';
import '../widgets/shop/shop_product_images.dart';

class ShopDetail extends StatefulWidget {
  const ShopDetail({super.key, required this.shopId});

  final String shopId;

  @override
  State<ShopDetail> createState() => _ShopDetailState();
}

class _ShopDetailState extends State<ShopDetail> {
  int selectedTab = 0;

  @override
  void initState() {
    super.initState();
    context.read<ShopBloc>().add(GetShopProductsEvent(shopId: widget.shopId));
    context
        .read<ShopBloc>()
        .add(GetShopWorkingHoursEvent(shopId: widget.shopId));
    context.read<ShopBloc>().add(GetShopReviewsEvent(shopId: widget.shopId));
    context
        .read<ShopBloc>()
        .add(GetShopProductsImagesEvent(shopId: widget.shopId));
  }

  @override
  Widget build(BuildContext context) {
    List<String> tabs = ["Products", "About", "Photos", "Review"];

    void onChangeTab(int index) {
      setState(() {
        selectedTab = index;
      });
    }

    Widget getSelectedTabContent() {
      switch (selectedTab) {
        case 0:
          return ShopProductList(shopId: widget.shopId);
        case 1:
          return ShopAbout(shopId: widget.shopId);
        case 2:
          return ShopProductImages(shopId: widget.shopId);
        case 3:
          return ShopReview(shopId: widget.shopId);
        default:
          return ShopProductList(shopId: widget.shopId);
      }
    }

    return SafeArea(
      child: Scaffold(
        backgroundColor: Theme.of(context).colorScheme.onPrimary,
        body: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const AppBarOne(),
              SizedBox(
                height: 230,
                child: Stack(
                  children: [
                    if (context
                            .read<ShopBloc>()
                            .state
                            .shops[widget.shopId]!
                            .banner !=
                        null)
                      Container(
                        height: 180,
                        width: double.infinity,
                        decoration: BoxDecoration(
                          color: Theme.of(context).colorScheme.secondary,
                        ),
                        child: Image.network(
                          context
                              .read<ShopBloc>()
                              .state
                              .shops[widget.shopId]!
                              .banner!,
                          fit: BoxFit.cover,
                        ),
                      ),
                    if (context
                            .read<ShopBloc>()
                            .state
                            .shops[widget.shopId]!
                            .banner ==
                        null)
                      Container(
                        height: 180,
                        width: double.infinity,
                        decoration: BoxDecoration(
                          color: Theme.of(context).colorScheme.secondary,
                        ),
                      ),
                    Positioned(
                      left: AppSize.smallSize,
                      bottom: 0,
                      child: Container(
                        alignment: Alignment.center,
                        child: CircleAvatar(
                          radius: 55,
                          backgroundImage: NetworkImage(
                            context
                                .read<ShopBloc>()
                                .state
                                .shops[widget.shopId]!
                                .logo,
                          ),
                          backgroundColor:
                              Theme.of(context).colorScheme.primaryContainer,
                        ),
                      ),
                    )
                  ],
                ),
              ),
              Padding(
                padding: const EdgeInsets.all(AppSize.smallSize),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      context.read<ShopBloc>().state.shops[widget.shopId]!.name,
                      style: Theme.of(context).textTheme.titleLarge!.copyWith(
                            color: Theme.of(context).colorScheme.onSurface,
                          ),
                    ),
                    RatingStars(
                      axis: Axis.horizontal,
                      value: context
                          .read<ShopBloc>()
                          .state
                          .shops[widget.shopId]!
                          .rating
                          .toDouble(),
                      starCount: 5,
                      starSize: 20,
                      valueLabelColor: const Color(0xff9b9b9b),
                      valueLabelTextStyle: const TextStyle(
                          color: Colors.white,
                          fontWeight: FontWeight.w400,
                          fontStyle: FontStyle.normal,
                          fontSize: 12.0),
                      valueLabelRadius: 10,
                      maxValue: 5,
                      starSpacing: 2,
                      maxValueVisibility: true,
                      valueLabelVisibility: false,
                      animationDuration: const Duration(milliseconds: 1000),
                      valueLabelPadding: const EdgeInsets.symmetric(
                          vertical: 1, horizontal: 8),
                      valueLabelMargin: const EdgeInsets.only(right: 8),
                      starOffColor: const Color(0xffe7e8ea),
                      starColor: Colors.yellow,
                      angle: 12,
                    ),
                    Text(
                      "${context.read<ShopBloc>().state.shops[widget.shopId]!.city} | ${context.read<ShopBloc>().state.shops[widget.shopId]!.state}",
                      style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                            color: Theme.of(context).colorScheme.secondary,
                          ),
                    ),
                    const SizedBox(height: AppSize.mediumSize),
                    Row(
                      children: [
                        Container(
                          width: 150,
                          padding: const EdgeInsets.symmetric(
                            horizontal: AppSize.smallSize,
                            vertical: AppSize.xSmallSize,
                          ),
                          decoration: BoxDecoration(
                            color: Theme.of(context).colorScheme.onSurface,
                            borderRadius:
                                BorderRadius.circular(AppSize.xxSmallSize),
                          ),
                          child: Center(
                            child: Row(
                              crossAxisAlignment: CrossAxisAlignment.center,
                              mainAxisAlignment: MainAxisAlignment.center,
                              children: [
                                Icon(
                                  Icons.call,
                                  color:
                                      Theme.of(context).colorScheme.onPrimary,
                                ),
                                const SizedBox(width: AppSize.xxSmallSize),
                                Text(
                                  "Call Now",
                                  style: Theme.of(context)
                                      .textTheme
                                      .titleSmall!
                                      .copyWith(
                                        color: Theme.of(context)
                                            .colorScheme
                                            .onPrimary,
                                      ),
                                ),
                              ],
                            ),
                          ),
                        ),
                        const SizedBox(width: AppSize.smallSize),
                        Container(
                          width: 150,
                          padding: const EdgeInsets.symmetric(
                            horizontal: AppSize.smallSize,
                            vertical: AppSize.xSmallSize,
                          ),
                          decoration: BoxDecoration(
                            color: Theme.of(context).colorScheme.primary,
                            borderRadius:
                                BorderRadius.circular(AppSize.xxSmallSize),
                          ),
                          child: Center(
                            child: Row(
                              crossAxisAlignment: CrossAxisAlignment.center,
                              mainAxisAlignment: MainAxisAlignment.center,
                              children: [
                                Icon(
                                  Icons.message,
                                  color:
                                      Theme.of(context).colorScheme.onPrimary,
                                ),
                                const SizedBox(width: AppSize.xxSmallSize),
                                Text(
                                  "Message",
                                  style: Theme.of(context)
                                      .textTheme
                                      .titleSmall!
                                      .copyWith(
                                        color: Theme.of(context)
                                            .colorScheme
                                            .onPrimary,
                                      ),
                                ),
                              ],
                            ),
                          ),
                        )
                      ],
                    )
                  ],
                ),
              ),
              Container(
                width: double.infinity,
                height: AppSize.xSmallSize + 2,
                color: Theme.of(context).colorScheme.primaryContainer,
              ),
              Container(
                margin:
                    const EdgeInsets.symmetric(vertical: AppSize.xSmallSize),
                height: 50,
                padding:
                    const EdgeInsets.symmetric(horizontal: AppSize.smallSize),
                child: Center(
                  child: ListView.builder(
                    itemCount: tabs.length,
                    scrollDirection: Axis.horizontal,
                    itemBuilder: (BuildContext context, int index) {
                      return GestureDetector(
                        onTap: () => onChangeTab(index),
                        child: Container(
                          decoration: BoxDecoration(
                            color: selectedTab == index
                                ? Theme.of(context).colorScheme.secondary
                                : Theme.of(context).colorScheme.onPrimary,
                            borderRadius:
                                BorderRadius.circular(AppSize.xLargeSize),
                          ),
                          padding: const EdgeInsets.symmetric(
                            horizontal: AppSize.smallSize,
                            vertical: AppSize.xSmallSize,
                          ),
                          margin: const EdgeInsets.only(right: 16),
                          child: Center(
                            child: Text(
                              tabs[index],
                              style: Theme.of(context)
                                  .textTheme
                                  .titleSmall!
                                  .copyWith(
                                      color: selectedTab == index
                                          ? Theme.of(context)
                                              .colorScheme
                                              .onPrimary
                                          : Theme.of(context)
                                              .colorScheme
                                              .secondary),
                            ),
                          ),
                        ),
                      );
                    },
                  ),
                ),
              ),
              Container(
                width: double.infinity,
                height: AppSize.xSmallSize + 2,
                color: Theme.of(context).colorScheme.primaryContainer,
              ),
              Padding(
                padding: const EdgeInsets.all(AppSize.smallSize),
                child: getSelectedTabContent(),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
