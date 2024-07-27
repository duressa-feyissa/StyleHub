import 'package:flutter/material.dart';
import 'package:flutter_rating_stars/flutter_rating_stars.dart';
import 'package:style_hub/features/SytleHub/domain/entities/shop/review_entity.dart';
import 'package:timeago/timeago.dart' as timeago;

import '../../../../../core/utils/captilizations.dart';
import '../../../../../setUp/size/app_size.dart';

class ReviewWidget extends StatefulWidget {
  final ReviewEntity review;

  const ReviewWidget({super.key, required this.review});

  @override
  _ReviewWidgetState createState() => _ReviewWidgetState();
}

class _ReviewWidgetState extends State<ReviewWidget> {
  bool isExpanded = false;

  @override
  Widget build(BuildContext context) {
    final review = widget.review;
    final text = Captilizations.capitalize(review.review);
    final isTextLong = text.length > 100;

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Row(
          children: [
            Container(
              width: 45,
              height: 45,
              padding: EdgeInsets.all(review.image != null ? 0 : 6),
              decoration: BoxDecoration(
                color: Theme.of(context).colorScheme.primaryContainer,
                shape: BoxShape.circle,
              ),
              child: Image(
                image: review.image != null
                    ? NetworkImage(review.image!) as ImageProvider
                    : const AssetImage(
                        "assets/images/Screens/person.png",
                      ),
                fit: BoxFit.cover,
              ),
            ),
            const SizedBox(width: AppSize.smallSize),
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Text(
                  "${Captilizations.capitalize(review.firstName)} ${Captilizations.capitalize(review.lastName)}",
                  style: Theme.of(context)
                      .textTheme
                      .titleMedium!
                      .copyWith(color: Theme.of(context).colorScheme.onSurface),
                ),
                Row(
                  children: [
                    RatingStars(
                      axis: Axis.horizontal,
                      value: review.rating.toDouble(),
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
                    const SizedBox(width: AppSize.xSmallSize),
                    Text(
                      timeago.format(review.createdAt),
                      style: Theme.of(context).textTheme.bodySmall!.copyWith(
                          color: Theme.of(context).colorScheme.secondary),
                    ),
                  ],
                ),
              ],
            ),
          ],
        ),
        const SizedBox(height: AppSize.xSmallSize),
        if (isTextLong)
          Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                isExpanded ? text : '${text.substring(0, 100)}...',
                textAlign: TextAlign.justify,
                style: Theme.of(context)
                    .textTheme
                    .bodyMedium!
                    .copyWith(color: Theme.of(context).colorScheme.secondary),
              ),
              InkWell(
                onTap: () {
                  setState(() {
                    isExpanded = !isExpanded;
                  });
                },
                child: Text(
                  isExpanded ? 'Read less' : 'Read more',
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.primary,
                  ),
                ),
              ),
            ],
          )
        else
          Text(
            text,
            textAlign: TextAlign.justify,
            style: Theme.of(context)
                .textTheme
                .bodyMedium!
                .copyWith(color: Theme.of(context).colorScheme.secondary),
          ),
      ],
    );
  }
}
