import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../../../../../../core/utils/captilizations.dart';
import '../../../bloc/prdoct_filter/product_filter_bloc.dart';
import '../../../bloc/product/product_bloc.dart';
import '../../../../../../setUp/size/app_size.dart';

class ColorFilterContent extends StatelessWidget {
  const ColorFilterContent({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Wrap(
        spacing: AppSize.smallSize,
        crossAxisAlignment: WrapCrossAlignment.start,
        children: [
          if (context.watch<ProductBloc>().state.colorStatus ==
              ColorStatus.success)
            for (int index = 0;
                index <
                    min(
                      10,
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
        ]);
  }
}
