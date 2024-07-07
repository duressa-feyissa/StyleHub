import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../../../../core/utils/captilizations.dart';
import '../../../../../../setUp/size/app_size.dart';
import '../../../bloc/prdoct_filter/product_filter_bloc.dart';
import '../../../bloc/product/product_bloc.dart';

class LocationFilterContent extends StatelessWidget {
  const LocationFilterContent({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Wrap(spacing: AppSize.smallSize, children: [
      for (int index = 0;
          index <
              min(
                8,
                context.watch<ProductBloc>().state.locations.length,
              );
          index++)
        Container(
            margin: const EdgeInsets.only(top: AppSize.smallSize),
            child: (GestureDetector(
              onTap: () {
                final location =
                    context.read<ProductBloc>().state.locations[index];

                context.read<ProductFilterBloc>().add(SetLocationEvent(
                    location.id, location.latitude, location.longitude, 40));
              },
              child: Container(
                padding: const EdgeInsets.symmetric(
                    horizontal: AppSize.smallSize,
                    vertical: AppSize.xxSmallSize),
                decoration: (context
                                .watch<ProductFilterBloc>()
                                .state
                                .latitute ==
                            context
                                .watch<ProductBloc>()
                                .state
                                .locations[index]
                                .latitude &&
                        context.watch<ProductFilterBloc>().state.longitude ==
                            context
                                .watch<ProductBloc>()
                                .state
                                .locations[index]
                                .longitude)
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
                  Captilizations.capitalize(
                      context.watch<ProductBloc>().state.locations[index].name),
                  style: (context.watch<ProductFilterBloc>().state.latitute ==
                              context
                                  .watch<ProductBloc>()
                                  .state
                                  .locations[index]
                                  .latitude &&
                          context.watch<ProductFilterBloc>().state.longitude ==
                              context
                                  .watch<ProductBloc>()
                                  .state
                                  .locations[index]
                                  .longitude)
                      ? Theme.of(context).textTheme.bodyMedium!.copyWith(
                          color: Theme.of(context).colorScheme.onPrimary)
                      : Theme.of(context).textTheme.bodyMedium!.copyWith(
                            color: Theme.of(context).colorScheme.onSurface,
                          ),
                ),
              ),
            ))),
    ]);
  }
}
