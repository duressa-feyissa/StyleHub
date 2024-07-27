import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:persistent_bottom_nav_bar/persistent_tab_view.dart';
import 'package:style_hub/features/SytleHub/presentation/widgets/shop/review_widget.dart';
import 'package:style_hub/setUp/size/app_size.dart';

import '../bloc/shop/shop_bloc.dart';
import '../widgets/common/app_bar_one.dart';
import 'shop_review_write.dart';

class ShopReviewScreen extends StatelessWidget {
  const ShopReviewScreen({super.key, required this.shopId});

  final String shopId;

  @override
  Widget build(BuildContext context) {
    if (context.watch<ShopBloc>().state.shopProductReviewStatus ==
        ShopProductReviewStatus.loading) {
      return SafeArea(
        child: Scaffold(
          backgroundColor: Theme.of(context).colorScheme.onPrimary,
          body: Column(
            children: [
              const SizedBox(height: AppSize.largeSize),
              Image.asset('assets/images/Screens/review.png'),
              const SizedBox(height: AppSize.smallSize),
              Padding(
                padding:
                    const EdgeInsets.symmetric(horizontal: AppSize.smallSize),
                child: Text('No reviews yet. Be the first to review this shop',
                    textAlign: TextAlign.center,
                    style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                        color: Theme.of(context).colorScheme.secondary)),
              ),
              const SizedBox(height: AppSize.largeSize),
              Container(
                width: double.infinity,
                height: 55,
                decoration: BoxDecoration(
                  color: Theme.of(context).colorScheme.primary,
                  borderRadius: BorderRadius.circular(AppSize.xxSmallSize),
                ),
                child: Center(
                  child: Text(
                    'Write a review',
                    style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                        color: Theme.of(context).colorScheme.onPrimary),
                  ),
                ),
              ),
            ],
          ),
        ),
      );
    }

    return SafeArea(
      child: Scaffold(
        backgroundColor: Theme.of(context).colorScheme.onPrimary,
        body: Column(
          children: [
            const AppBarOne(),
            Divider(
              color: Theme.of(context).colorScheme.primaryContainer,
              thickness: 2,
            ),
            Expanded(
              child: Padding(
                padding:
                    const EdgeInsets.symmetric(horizontal: AppSize.smallSize),
                child: SingleChildScrollView(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      for (var review in context
                          .watch<ShopBloc>()
                          .state
                          .shops[shopId]!
                          .reviews)
                        Container(
                            margin: const EdgeInsets.only(
                                bottom: AppSize.mediumSize),
                            child: ReviewWidget(review: review)),
                    ],
                  ),
                ),
              ),
            ),
            // Divider(
            //   color: Theme.of(context).colorScheme.primaryContainer,
            //   thickness: 2,
            // ),
            GestureDetector(
              onTap: () {
                PersistentNavBarNavigator.pushNewScreenWithRouteSettings(
                  context,
                  settings: const RouteSettings(name: '/shop/review'),
                  withNavBar: false,
                  screen: ShopReviewWrite(shopId: shopId),
                  pageTransitionAnimation: PageTransitionAnimation.fade,
                );
              },
              child: Container(
                width: double.infinity,
                height: 60,
                margin: const EdgeInsets.all(AppSize.smallSize),
                decoration: BoxDecoration(
                  color: Theme.of(context).colorScheme.primary,
                  borderRadius: BorderRadius.circular(AppSize.xxSmallSize),
                ),
                child: Center(
                  child: Text(
                    'Write a review',
                    style: Theme.of(context).textTheme.titleSmall!.copyWith(
                        color: Theme.of(context).colorScheme.onPrimary),
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
