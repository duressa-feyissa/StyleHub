import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../../../core/utils/captilizations.dart';
import '../../../../../setUp/size/app_size.dart';
import '../../bloc/prdoct_filter/product_filter_bloc.dart';
import '../../bloc/product/product_bloc.dart';
import '../shimmer/filter.dart';
import 'bottom_filter_bar.dart';

class HalfDesignFilterDisplay extends StatelessWidget {
  const HalfDesignFilterDisplay(
      {super.key, required this.isAdd, required this.onTap});

  final bool isAdd;
  final Function() onTap;

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Container(
            padding: const EdgeInsets.only(
                left: AppSize.smallSize,
                bottom: AppSize.xSmallSize,
                right: AppSize.smallSize,
                top: AppSize.xSmallSize),
            decoration: BoxDecoration(
              border: Border(
                bottom: BorderSide(
                  color:
                      Theme.of(context).colorScheme.onSurface.withOpacity(0.1),
                  width: 0.5,
                ),
              ),
            ),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Expanded(
                  child: Center(
                    child: Text(
                      'Designs${context.watch<ProductFilterBloc>().state.selectedDesigns.isEmpty ? '' : ' (${context.watch<ProductFilterBloc>().state.selectedDesigns.length})'}',
                      style: const TextStyle(
                        fontSize: 18,
                        fontWeight: FontWeight.w600,
                      ),
                    ),
                  ),
                ),
                GestureDetector(
                  onTap: () {
                    Navigator.pop(context, true);
                  },
                  child: Container(
                    padding: const EdgeInsets.all(AppSize.xxSmallSize),
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(AppSize.mediumSize),
                      border: Border.all(
                        color:
                            Theme.of(context).colorScheme.surfaceContainerHigh,
                      ),
                    ),
                    child: Icon(
                      Icons.close,
                      color: Theme.of(context).colorScheme.onSurface,
                    ),
                  ),
                ),
              ],
            ),
          ),
          if (context.watch<ProductBloc>().state.designStatus ==
              DesignStatus.loading)
            const SizedBox(height: AppSize.largeSize),
          if (context.watch<ProductBloc>().state.designStatus ==
              DesignStatus.loading)
            for (int index = 0; index < 6; index++)
              Container(
                  margin: const EdgeInsets.only(bottom: AppSize.smallSize),
                  padding:
                      const EdgeInsets.symmetric(horizontal: AppSize.smallSize),
                  child: const FilterShimmer()),
          Padding(
            padding: const EdgeInsets.only(
                bottom: AppSize.smallSize,
                left: AppSize.smallSize,
                right: AppSize.smallSize),
            child: Wrap(
              spacing: AppSize.smallSize,
              children: [
                if (context.watch<ProductBloc>().state.designStatus ==
                    DesignStatus.success)
                  for (int index = 0;
                      index <
                          min(
                            24,
                            context.watch<ProductBloc>().state.designs.length,
                          );
                      index++)
                    Container(
                      margin: const EdgeInsets.only(top: AppSize.smallSize),
                      child: Column(
                        children: [
                          (context
                                  .watch<ProductFilterBloc>()
                                  .state
                                  .selectedDesigns
                                  .contains(context
                                      .watch<ProductBloc>()
                                      .state
                                      .designs[index]
                                      .id))
                              ? GestureDetector(
                                  onTap: () {
                                    final sizeId = context
                                        .read<ProductBloc>()
                                        .state
                                        .designs[index]
                                        .id;
                                    context
                                        .read<ProductFilterBloc>()
                                        .add(RemoveSelectedDesignEvent(sizeId));
                                  },
                                  child: Container(
                                    padding: const EdgeInsets.symmetric(
                                        horizontal: AppSize.smallSize,
                                        vertical: AppSize.xxSmallSize),
                                    decoration: BoxDecoration(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .onSurface,
                                      borderRadius: BorderRadius.circular(
                                          AppSize.xxSmallSize),
                                      border: Border.all(
                                        color: Theme.of(context)
                                            .colorScheme
                                            .onSurface,
                                      ),
                                    ),
                                    child: Text(
                                      Captilizations.capitalize(context
                                          .watch<ProductBloc>()
                                          .state
                                          .designs[index]
                                          .name),
                                      style: Theme.of(context)
                                          .textTheme
                                          .bodyMedium!
                                          .copyWith(
                                            color: Theme.of(context)
                                                .colorScheme
                                                .onPrimary,
                                          ),
                                    ),
                                  ),
                                )
                              : GestureDetector(
                                  onTap: () {
                                    final brandId = context
                                        .read<ProductBloc>()
                                        .state
                                        .designs[index]
                                        .id;
                                    context
                                        .read<ProductFilterBloc>()
                                        .add(AddSelectedDesignEvent(brandId));
                                  },
                                  child: Container(
                                      padding: const EdgeInsets.symmetric(
                                          horizontal: AppSize.smallSize,
                                          vertical: AppSize.xxSmallSize),
                                      decoration: BoxDecoration(
                                        borderRadius: BorderRadius.circular(
                                            AppSize.xxSmallSize),
                                        border: Border.all(
                                          color: Theme.of(context)
                                              .colorScheme
                                              .surfaceContainerHigh,
                                        ),
                                      ),
                                      child: Text(
                                        Captilizations.capitalize(context
                                            .watch<ProductBloc>()
                                            .state
                                            .designs[index]
                                            .name),
                                        style: Theme.of(context)
                                            .textTheme
                                            .bodyMedium!
                                            .copyWith(
                                              color: Theme.of(context)
                                                  .colorScheme
                                                  .onSurface,
                                            ),
                                      )),
                                ),
                        ],
                      ),
                    ),
                const SizedBox(height: AppSize.smallSize),
              ],
            ),
          ),
          if (context.watch<ProductBloc>().state.designStatus ==
              DesignStatus.loading)
            const LinearProgressIndicator(),
          Container(
            padding: const EdgeInsets.all(AppSize.smallSize),
            decoration: BoxDecoration(
              border: Border(
                top: BorderSide(
                  color:
                      Theme.of(context).colorScheme.onSurface.withOpacity(0.1),
                  width: 0.5,
                ),
              ),
            ),
            child: BottomFilterBar(
                isAdd: isAdd,
                onTapClear: () {
                  context
                      .read<ProductFilterBloc>()
                      .add(ClearSelectedDesignsEvent());
                },
                onTapResult: () {
                  onTap();
                  Navigator.pop(context, true);
                }),
          )
        ],
      ),
    );
  }
}
