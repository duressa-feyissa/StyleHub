import 'package:flutter/material.dart';

import '../../../../../setUp/size/app_size.dart';
import 'category_chip.dart';

class SubCategoryList extends StatelessWidget {
  const SubCategoryList({
    super.key,
    required this.title,
    required this.subCategories,
  });

  final String title;
  final List<CategoryChip> subCategories;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(bottom: AppSize.mediumSize),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisAlignment: MainAxisAlignment.start,
        children: [
          Text(
            title,
            style: Theme.of(context).textTheme.titleMedium!.copyWith(
                  color: Theme.of(context).colorScheme.onSurface,
                ),
          ),
          Wrap(
              spacing: AppSize.smallSize,
              runSpacing: AppSize.smallSize,
              children: subCategories.map((e) => e).toList()),
        ],
      ),
    );
  }
}
