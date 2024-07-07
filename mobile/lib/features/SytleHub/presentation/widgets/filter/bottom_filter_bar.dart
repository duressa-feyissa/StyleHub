import 'package:flutter/material.dart';
import '../../../../../setUp/size/app_size.dart';

class BottomFilterBar extends StatelessWidget {
  const BottomFilterBar(
      {super.key,
      required this.onTapClear,
      required this.onTapResult,
      required this.isAdd});

  final Function() onTapClear;
  final Function() onTapResult;
  final bool isAdd;

  @override
  Widget build(BuildContext context) {
    return Row(
      crossAxisAlignment: CrossAxisAlignment.center,
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        GestureDetector(
            onTap: onTapClear,
            child: Text("Clear",
                style: Theme.of(context)
                    .textTheme
                    .bodyMedium!
                    .copyWith(color: Theme.of(context).colorScheme.onSurface))),
        GestureDetector(
          onTap: onTapResult,
          child: Container(
            alignment: Alignment.center,
            padding: const EdgeInsets.symmetric(
                horizontal: AppSize.smallSize, vertical: AppSize.xSmallSize),
            margin: const EdgeInsets.only(right: AppSize.smallSize),
            decoration: BoxDecoration(
              color: Theme.of(context).colorScheme.primary,
              borderRadius: BorderRadius.circular(AppSize.xxSmallSize),
            ),
            child: Text(isAdd ? "Add" : "Result",
                style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                      color: Theme.of(context).colorScheme.onPrimary,
                    )),
          ),
        ),
      ],
    );
  }
}
