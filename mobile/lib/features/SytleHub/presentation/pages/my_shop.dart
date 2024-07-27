import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:style_hub/features/SytleHub/presentation/widgets/common/app_bar_three.dart';
import 'package:style_hub/features/SytleHub/presentation/widgets/my_shop/product_draft_list.dart';

import '../../../../setUp/size/app_size.dart';
import '../bloc/shop/shop_bloc.dart';
import '../widgets/my_shop/my_shop_analytic.dart';
import '../widgets/my_shop/product_archieve_list.dart';
import '../widgets/my_shop/product_list.dart';
import '../widgets/my_shop/shop_review.dart';
import '../widgets/shop/shop_product_images.dart';

class MyShopScreen extends StatefulWidget {
  const MyShopScreen({super.key});

  @override
  State<MyShopScreen> createState() => _MyShopScreenState();
}

class _MyShopScreenState extends State<MyShopScreen> {
  int currentIndex = 0;

  @override
  void initState() {
    super.initState();
    context.read<ShopBloc>().add(GetShopProductsEvent(
        shopId: context.read<ShopBloc>().state.myShopId ?? ""));
    context.read<ShopBloc>().add(GetShopWorkingHoursEvent(
        shopId: context.read<ShopBloc>().state.myShopId ?? ""));
    context.read<ShopBloc>().add(GetShopReviewsEvent(
        shopId: context.read<ShopBloc>().state.myShopId ?? ""));
    context.read<ShopBloc>().add(GetShopProductsImagesEvent(
        shopId: context.read<ShopBloc>().state.myShopId ?? ""));
  }

  void onChipTap(int index) {
    setState(() {
      currentIndex = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    List<String> tabs = [
      "Analytics",
      "Products",
      "Photos",
      "Reviews",
      "Archives",
      "Drafts"
    ];

    Widget onChangeWidget(int index) {
      switch (index) {
        case 0:
          return MyShopAnalytic(
              shopId: context.read<ShopBloc>().state.myShopId ?? "");
        case 1:
          return MyShopProductList(
              shopId: context.read<ShopBloc>().state.myShopId ?? "");
        case 2:
          return ShopProductImages(
              shopId: context.read<ShopBloc>().state.myShopId ?? "");
        case 3:
          return MyShopReview(
              shopId: context.read<ShopBloc>().state.myShopId ?? "");
        case 4:
          return MyShopProductArchieveList(
              shopId: context.read<ShopBloc>().state.myShopId ?? "");
        case 5:
          return MyShopProductDraftList(
              shopId: context.read<ShopBloc>().state.myShopId ?? "");
        default:
          return Container();
      }
    }

    return SafeArea(
      child: Scaffold(
        backgroundColor: Theme.of(context).colorScheme.onPrimary,
        body: Column(
          children: [
            const AppBarThree(),
            Container(
              height: 40,
              padding: const EdgeInsets.only(
                  left: AppSize.smallSize, right: AppSize.smallSize),
              child: ListView.builder(
                scrollDirection: Axis.horizontal,
                itemCount: tabs.length,
                itemBuilder: (context, index) {
                  return GestureDetector(
                    onTap: () => onChipTap(index),
                    child: Container(
                      decoration: BoxDecoration(
                        color: currentIndex == index
                            ? Theme.of(context).colorScheme.secondary
                            : Theme.of(context).colorScheme.onPrimary,
                        borderRadius: BorderRadius.circular(AppSize.xLargeSize),
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
                                color: currentIndex == index
                                    ? Theme.of(context).colorScheme.onPrimary
                                    : Theme.of(context).colorScheme.secondary,
                              ),
                        ),
                      ),
                    ),
                  );
                },
              ),
            ),
            Container(
              height: 2,
              margin: const EdgeInsets.only(top: AppSize.xSmallSize),
              color: Theme.of(context).colorScheme.primaryContainer,
            ),
            Expanded(
              child: Padding(
                padding: const EdgeInsets.all(AppSize.smallSize),
                child: onChangeWidget(currentIndex),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
