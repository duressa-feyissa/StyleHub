import 'package:flutter/material.dart';

class CategorySwapChip extends StatelessWidget {
  final String text;
  final Function() onTap;
  final bool isActive;

  const CategorySwapChip(
      {super.key,
      required this.text,
      required this.onTap,
      this.isActive = false});

  @override
  Widget build(BuildContext context) {
    if (isActive) {
      return GestureDetector(
        onTap: onTap,
        child: Text(text,
            style: TextStyle(
              color: Theme.of(context).colorScheme.onSurface,
              fontSize: 16,
              fontWeight: FontWeight.w600,
              height: 1.25,
            )),
      );
    } else {
      return GestureDetector(
        onTap: onTap,
        child: Text(text,
            style: Theme.of(context).textTheme.titleSmall!.copyWith(
                  color: Theme.of(context).colorScheme.outline,
                )),
      );
    }
  }
}
