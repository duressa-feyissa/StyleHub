import 'dart:math';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../../../../../../core/utils/captilizations.dart';
import '../../../bloc/prdoct_filter/product_filter_bloc.dart';
import '../../../bloc/product/product_bloc.dart';
import '../../../../../../setUp/size/app_size.dart';

class MaterialFilterContent extends StatelessWidget {
  const MaterialFilterContent({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Wrap(
      spacing: AppSize.smallSize,
      children: [
        if (context.watch<ProductBloc>().state.materialStatus ==
            MaterialStatus.success)
          for (int index = 0;
              index <
                  min(
                    8,
                    context.watch<ProductBloc>().state.materials.length,
                  );
              index++)
            Container(
              margin: const EdgeInsets.only(top: AppSize.smallSize),
              child: Column(
                children: [
                  (context
                          .watch<ProductFilterBloc>()
                          .state
                          .selectedMaterials
                          .contains(context
                              .watch<ProductBloc>()
                              .state
                              .materials[index]
                              .id))
                      ? GestureDetector(
                          onTap: () {
                            final sizeId = context
                                .read<ProductBloc>()
                                .state
                                .materials[index]
                                .id;
                            context
                                .read<ProductFilterBloc>()
                                .add(RemoveSelectedMaterialEvent(sizeId));
                          },
                          child: Container(
                            padding: const EdgeInsets.symmetric(
                                horizontal: AppSize.smallSize,
                                vertical: AppSize.xxSmallSize),
                            decoration: BoxDecoration(
                              color:
                                  Theme.of(context).colorScheme.onSurface,
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
                                  .materials[index]
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
                                .materials[index]
                                .id;
                            context
                                .read<ProductFilterBloc>()
                                .add(AddSelectedMaterialEvent(brandId));
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
                                    .materials[index]
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
