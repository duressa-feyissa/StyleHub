import 'package:flutter/material.dart';
import 'package:style_hub/setUp/size/app_size.dart';

import 'rating_overview.dart';
import 'shop_analytics.dart';

class MyShopAnalytic extends StatelessWidget {
  const MyShopAnalytic({super.key, required this.shopId});

  final String shopId;

  @override
  Widget build(BuildContext context) {
    return const Column(
      children: [
        AnalyticsCard(
          totalFollowers: 0,
          totalReviews: 0,
          totalFavorites: 0,
          totalProducts: 0,
          totalContacts: 0,
          totalViews: 0,
        ),
        SizedBox(height: AppSize.mediumSize),
        RatingOverviewWidget(
          averageRating: 4.0,
          totalReviews: 52,
          ratingsCount: {
            5: 40,
            4: 8,
            3: 3,
            2: 1,
            1: 0,
          },
        ),
      ],
    );
  }
}
