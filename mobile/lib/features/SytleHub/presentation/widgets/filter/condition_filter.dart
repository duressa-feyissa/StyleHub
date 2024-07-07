import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../../../core/utils/captilizations.dart';
import '../../../../../setUp/size/app_size.dart';
import '../../bloc/prdoct_filter/product_filter_bloc.dart';

class ConditionFilterDisplay extends StatefulWidget {
  const ConditionFilterDisplay({super.key});
  @override
  State<ConditionFilterDisplay> createState() => _CategoryFilterDisplayState();
}

const List<String> conditions = ["New", "Used", "Fairy Used"];

class _CategoryFilterDisplayState extends State<ConditionFilterDisplay> {
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(AppSize.smallSize),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Row(
            children: [
              Text(
                'Condition',
                style: TextStyle(
                  fontSize: 18,
                  fontWeight: FontWeight.w600,
                ),
              ),
            ],
          ),
          const SizedBox(height: AppSize.smallSize),
          Wrap(
            spacing: AppSize.smallSize,
            runSpacing: AppSize.smallSize,
            children: [
              for (int index = 0; index < conditions.length; index++)
                GestureDetector(
                  onTap: () {
                    context
                        .read<ProductFilterBloc>()
                        .add(SetConditionEvent(conditions[index]));
                  },
                  child: Container(
                    padding: const EdgeInsets.symmetric(
                        horizontal: AppSize.smallSize,
                        vertical: AppSize.xxSmallSize),
                    decoration: context
                                .watch<ProductFilterBloc>()
                                .state
                                .condition ==
                            conditions[index]
                        ? BoxDecoration(
                            color: Theme.of(context).colorScheme.onSurface,
                            borderRadius:
                                BorderRadius.circular(AppSize.xxSmallSize),
                            border: Border.all(
                              color: Theme.of(context).colorScheme.onSurface,
                            ),
                          )
                        : BoxDecoration(
                            borderRadius:
                                BorderRadius.circular(AppSize.xxSmallSize),
                            border: Border.all(
                              color: Theme.of(context)
                                  .colorScheme
                                  .surfaceContainerHigh,
                            ),
                          ),
                    child: Text(
                      Captilizations.capitalize(conditions[index]),
                      style: context
                                  .watch<ProductFilterBloc>()
                                  .state
                                  .condition ==
                              conditions[index]
                          ? Theme.of(context).textTheme.bodyMedium!.copyWith(
                              color: Theme.of(context).colorScheme.onPrimary)
                          : Theme.of(context).textTheme.bodyMedium!.copyWith(
                                color: Theme.of(context).colorScheme.onSurface,
                              ),
                    ),
                  ),
                )
            ],
          )
        ],
      ),
    );
  }
}
