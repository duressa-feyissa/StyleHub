import 'package:flutter/material.dart';

import '../../../../setUp/size/app_size.dart';

class ChipButton extends StatelessWidget {
  final String text;
  final Function() onTap;
  final bool isActive;

  const ChipButton(
      {super.key,
      required this.text,
      required this.onTap,
      this.isActive = false});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: onTap,
      child: Container(
        alignment: Alignment.center,
        padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
        margin: const EdgeInsets.only(right: AppSize.smallSize),
        decoration: BoxDecoration(
          color: isActive
              ? Theme.of(context).colorScheme.primary
              : Theme.of(context).colorScheme.primaryContainer,
          borderRadius: BorderRadius.circular(AppSize.xxSmallSize),
        ),
        child: Text(
          text,
          style: isActive
              ? Theme.of(context).textTheme.bodyMedium!.copyWith(
                    color: Theme.of(context).colorScheme.onPrimary,
                  )
              : Theme.of(context).textTheme.bodyMedium!.copyWith(
                    color: Theme.of(context).colorScheme.onSurface,
                  ),
        ),
      ),
    );
  }
}
