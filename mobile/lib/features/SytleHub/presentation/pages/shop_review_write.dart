import 'package:flutter/material.dart';
import 'package:flutter_rating_stars/flutter_rating_stars.dart';

import '../../../../setUp/size/app_size.dart';
import '../widgets/common/app_bar_one.dart';

class ShopReviewWrite extends StatefulWidget {
  const ShopReviewWrite({super.key, required this.shopId});
  final String shopId;

  @override
  State<ShopReviewWrite> createState() => _ShopReviewWriteState();
}

class _ShopReviewWriteState extends State<ShopReviewWrite> {
  double selectedRating = 0;
  @override
  Widget build(BuildContext context) {
    Widget builderDetails(String title, Widget value) {
      return Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            title,
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.bold,
              color: Theme.of(context).colorScheme.onSurface,
            ),
          ),
          const SizedBox(height: AppSize.xSmallSize),
          value,
        ],
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
                      builderDetails(
                        "Your Review",
                        Container(
                          decoration: BoxDecoration(
                            color:
                                Theme.of(context).colorScheme.primaryContainer,
                            borderRadius:
                                BorderRadius.circular(AppSize.smallSize),
                          ),
                          padding: const EdgeInsets.all(AppSize.smallSize),
                          child: TextField(
                            maxLines: 5,
                            decoration: InputDecoration(
                              hintText: 'Write your review...',
                              hintStyle: Theme.of(context)
                                  .textTheme
                                  .bodyMedium!
                                  .copyWith(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .secondary),
                              border: InputBorder.none,
                            ),
                          ),
                        ),
                      ),
                      const SizedBox(height: AppSize.mediumSize),
                      builderDetails(
                        "Rating",
                        RatingStars(
                          axis: Axis.horizontal,
                          value: selectedRating,
                          starCount: 5,
                          starSize: 40,
                          onValueChanged: (value) {
                            setState(() {
                              selectedRating = value;
                            });
                          },
                          valueLabelColor: const Color(0xff9b9b9b),
                          valueLabelTextStyle: const TextStyle(
                              color: Colors.white,
                              fontWeight: FontWeight.w400,
                              fontStyle: FontStyle.normal,
                              fontSize: 12.0),
                          valueLabelRadius: 10,
                          maxValue: 5,
                          starSpacing: 8,
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
                      ),
                    ],
                  ),
                ),
              ),
            ),
            GestureDetector(
              onTap: () {},
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
                    'Add a review',
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
