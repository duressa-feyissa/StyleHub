import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../../../core/utils/captilizations.dart';
import '../../../../../setUp/size/app_size.dart';
import '../../bloc/prdoct_filter/product_filter_bloc.dart';
import '../../bloc/product/product_bloc.dart';
import '../shimmer/filter.dart';
import 'bottom_filter_bar.dart';

class HalfColorFilterDisplay extends StatelessWidget {
  const HalfColorFilterDisplay(
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
                      'Colors${context.watch<ProductFilterBloc>().state.selectedColors.isEmpty ? '' : ' (${context.watch<ProductFilterBloc>().state.selectedColors.length})'}',
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
          if (context.watch<ProductBloc>().state.colorStatus ==
              ColorStatus.loading)
            const SizedBox(height: AppSize.largeSize),
          if (context.watch<ProductBloc>().state.colorStatus ==
              ColorStatus.loading)
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
            child: Wrap(spacing: AppSize.largeSize, children: [
              if (context.watch<ProductBloc>().state.colorStatus ==
                  ColorStatus.success)
                for (int index = 0;
                    index <
                        min(
                          24,
                          context.watch<ProductBloc>().state.colors.length,
                        );
                    index++)
                  Container(
                    margin: const EdgeInsets.only(top: AppSize.smallSize),
                    child: Column(
                      children: [
                        (context
                                .watch<ProductFilterBloc>()
                                .state
                                .selectedColors
                                .contains(context
                                    .watch<ProductBloc>()
                                    .state
                                    .colors[index]
                                    .id))
                            ? GestureDetector(
                                onTap: () {
                                  final colorId = context
                                      .read<ProductBloc>()
                                      .state
                                      .colors[index]
                                      .id;
                                  context
                                      .read<ProductFilterBloc>()
                                      .add(RemoveSelectedColorEvent(colorId));
                                },
                                child: Column(
                                  children: [
                                    Container(
                                      width: 24,
                                      height: 24,
                                      decoration: BoxDecoration(
                                        color: Color(int.parse(
                                          "FF${context.watch<ProductBloc>().state.colors[index].hexCode.substring(1)}",
                                          radix: 16,
                                        )),
                                        borderRadius: BorderRadius.circular(12),
                                      ),
                                      child: const Center(
                                        child: Icon(
                                          Icons.check,
                                          color: Colors.white,
                                          size: 16,
                                        ),
                                      ),
                                    ),
                                    Text(
                                      Captilizations.capitalize(context
                                          .watch<ProductBloc>()
                                          .state
                                          .colors[index]
                                          .name),
                                      style: Theme.of(context)
                                          .textTheme
                                          .bodyMedium!
                                          .copyWith(
                                            color: Theme.of(context)
                                                .colorScheme
                                                .onSurface,
                                          ),
                                    ),
                                  ],
                                ),
                              )
                            : GestureDetector(
                                onTap: () {
                                  final colorId = context
                                      .read<ProductBloc>()
                                      .state
                                      .colors[index]
                                      .id;
                                  context
                                      .read<ProductFilterBloc>()
                                      .add(AddSelectedColorEvent(colorId));
                                },
                                child: Column(
                                  children: [
                                    Container(
                                      width: 24,
                                      height: 24,
                                      decoration: BoxDecoration(
                                        color: Color(int.parse(
                                          "FF${context.watch<ProductBloc>().state.colors[index].hexCode.substring(1)}",
                                          radix: 16,
                                        )),
                                        borderRadius: BorderRadius.circular(12),
                                      ),
                                    ),
                                    Text(
                                      Captilizations.capitalize(context
                                          .watch<ProductBloc>()
                                          .state
                                          .colors[index]
                                          .name),
                                      style: Theme.of(context)
                                          .textTheme
                                          .bodyMedium!
                                          .copyWith(
                                            color: Theme.of(context)
                                                .colorScheme
                                                .onSurface,
                                          ),
                                    )
                                  ],
                                ),
                              ),
                      ],
                    ),
                  ),
            ]),
          ),
          if (context.watch<ProductBloc>().state.brandStatus ==
              BrandStatus.loading)
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
                      .add(ClearSelectedColorsEvent());
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
