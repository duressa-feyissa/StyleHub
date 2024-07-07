import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../../../../../../core/utils/captilizations.dart';
import '../../../bloc/prdoct_filter/product_filter_bloc.dart';
import '../../../bloc/product/product_bloc.dart';
import '../../../../../../setUp/size/app_size.dart';

class DesignFilterContent extends StatelessWidget {
  const DesignFilterContent({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Wrap(
      spacing: AppSize.smallSize,
      children: [
        if (context.watch<ProductBloc>().state.designStatus ==
            DesignStatus.success)
          for (int index = 0;
              index <
                  min(
                    8,
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
                              color: Theme.of(context).colorScheme.onSurface,
                              borderRadius:
                                  BorderRadius.circular(AppSize.xxSmallSize),
                              border: Border.all(
                                color: Theme.of(context).colorScheme.onSurface,
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
                                    color:
                                        Theme.of(context).colorScheme.onPrimary,
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
                                borderRadius:
                                    BorderRadius.circular(AppSize.xxSmallSize),
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
    );
  }
}
