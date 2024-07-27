import 'package:flutter/material.dart';
import 'package:style_hub/setUp/size/app_size.dart';

class RatingOverviewWidget extends StatelessWidget {
  final double averageRating;
  final int totalReviews;
  final Map<int, int> ratingsCount;

  const RatingOverviewWidget({
    super.key,
    required this.averageRating,
    required this.totalReviews,
    required this.ratingsCount,
  });

  @override
  Widget build(BuildContext context) {
    Widget builderDetails(String title, Widget value) {
      return Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            title,
            style: TextStyle(
              fontSize: 18,
              fontWeight: FontWeight.bold,
              color: Theme.of(context).colorScheme.onSurface,
            ),
          ),
          const SizedBox(height: AppSize.xSmallSize),
          value,
        ],
      );
    }

    return builderDetails(
        "Rating Overview",
        Container(
          padding: const EdgeInsets.all(AppSize.smallSize),
          decoration: BoxDecoration(
            color: Theme.of(context).colorScheme.primaryContainer,
            borderRadius: BorderRadius.circular(8),
          ),
          child: Row(
            children: [
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: ratingsCount.entries.map((entry) {
                    return _buildRatingBar(entry.key, entry.value, context);
                  }).toList(),
                ),
              ),
              const SizedBox(width: 20),
              Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Text(
                    averageRating.toStringAsFixed(1),
                    style: const TextStyle(
                      fontSize: 48,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  Row(
                    children: List.generate(5, (index) {
                      return Icon(
                        index < averageRating.round()
                            ? Icons.star
                            : Icons.star_border,
                        color: Colors.amber,
                        size: 30,
                      );
                    }),
                  ),
                  Text(
                    '$totalReviews Reviews',
                    style: const TextStyle(
                      fontSize: 16,
                      color: Colors.grey,
                    ),
                  ),
                ],
              ),
            ],
          ),
        ));
  }

  Widget _buildRatingBar(int starCount, int ratingCount, BuildContext context) {
    return Row(
      children: [
        Text(
          starCount.toString(),
          style: const TextStyle(
            fontSize: 16,
            fontWeight: FontWeight.bold,
          ),
        ),
        const SizedBox(width: 4),
        const Icon(
          Icons.star,
          color: Colors.amber,
          size: 20,
        ),
        const SizedBox(width: 4),
        Expanded(
          child: LinearProgressIndicator(
            value: ratingCount / totalReviews,
            backgroundColor: Colors.grey[300],
            color: Theme.of(context).colorScheme.primary,
          ),
        ),
      ],
    );
  }
}
