import 'package:flutter/material.dart';
import 'package:shimmer/shimmer.dart';

import '../../../../../setUp/size/app_size.dart';

class FilterShimmer extends StatelessWidget {
  const FilterShimmer({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 30,
      clipBehavior: Clip.hardEdge,
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(AppSize.xxSmallSize),
      ),
      child: Shimmer.fromColors(
          baseColor: Theme.of(context).colorScheme.onTertiaryContainer,
          highlightColor: Theme.of(context).colorScheme.tertiary,
          child: Container(
            color: Theme.of(context).colorScheme.onPrimary,
          )),
    );
  }
}
