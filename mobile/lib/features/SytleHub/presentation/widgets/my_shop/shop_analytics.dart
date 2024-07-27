import 'package:flutter/material.dart';
import 'package:style_hub/setUp/size/app_size.dart';

class AnalyticsCard extends StatelessWidget {
  final int totalFollowers;
  final int totalReviews;
  final int totalFavorites;
  final int totalProducts;
  final int totalContacts;
  final int totalViews;

  const AnalyticsCard({
    super.key,
    required this.totalFollowers,
    required this.totalReviews,
    required this.totalFavorites,
    required this.totalProducts,
    required this.totalContacts,
    required this.totalViews,
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

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        builderDetails(
          "Total Summary",
          Column(
            mainAxisAlignment: MainAxisAlignment.start,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  AnalyticsItem(
                    icon: Icons.people,
                    label: "Followers",
                    value: totalFollowers,
                  ),
                  const SizedBox(width: AppSize.smallSize),
                  AnalyticsItem(
                    icon: Icons.rate_review,
                    label: "Reviews",
                    value: totalReviews,
                  ),
                ],
              ),
              const SizedBox(height: AppSize.smallSize),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  AnalyticsItem(
                    icon: Icons.favorite,
                    label: "Favorites",
                    value: totalFavorites,
                  ),
                  const SizedBox(width: AppSize.smallSize),
                  AnalyticsItem(
                    icon: Icons.shopping_bag,
                    label: "Products",
                    value: totalProducts,
                  ),
                ],
              ),
              const SizedBox(height: AppSize.smallSize),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  AnalyticsItem(
                    icon: Icons.contact_mail,
                    label: "Contacts",
                    value: totalContacts,
                  ),
                  const SizedBox(width: AppSize.smallSize),
                  AnalyticsItem(
                    icon: Icons.visibility,
                    label: "Views",
                    value: totalViews,
                  ),
                ],
              ),
            ],
          ),
        )
      ],
    );
  }
}

class AnalyticsItem extends StatelessWidget {
  final IconData icon;
  final String label;
  final int value;

  const AnalyticsItem({
    super.key,
    required this.icon,
    required this.label,
    required this.value,
  });

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: Container(
        padding: const EdgeInsets.all(12),
        decoration: BoxDecoration(
          gradient: LinearGradient(
            colors: [
              Theme.of(context).colorScheme.primary,
              Theme.of(context).colorScheme.secondary
            ],
            begin: Alignment.topLeft,
            end: Alignment.bottomRight,
          ),
          borderRadius: BorderRadius.circular(AppSize.xSmallSize),
          boxShadow: [
            BoxShadow(
              color: Colors.black.withOpacity(0.2),
              blurRadius: 10,
              offset: const Offset(0, 5),
            ),
          ],
        ),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  label,
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                    fontWeight: FontWeight.w600,
                    fontSize: 16,
                  ),
                ),
                Text(
                  value.toString(),
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onPrimary,
                    fontWeight: FontWeight.bold,
                    fontSize: 28,
                  ),
                ),
              ],
            ),
            Container(
              padding: const EdgeInsets.all(AppSize.xSmallSize),
              decoration: BoxDecoration(
                color: Theme.of(context).colorScheme.primaryContainer,
                shape: BoxShape.circle,
              ),
              child: Icon(
                icon,
                size: 24,
                color: Theme.of(context).colorScheme.secondary,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
