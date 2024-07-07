import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../../../setUp/size/app_size.dart';
import '../../bloc/prdoct_filter/product_filter_bloc.dart';

class PriceFilterDisplay extends StatefulWidget {
  const PriceFilterDisplay({super.key});

  @override
  State<PriceFilterDisplay> createState() => _PriceFilterDisplayState();
}

class _PriceFilterDisplayState extends State<PriceFilterDisplay> {
  @override
  Widget build(BuildContext context) {
    RangeValues _currentRangeValues = RangeValues(
        context.watch<ProductFilterBloc>().state.priceMin == -1
            ? 1
            : context.watch<ProductFilterBloc>().state.priceMin,
        context.watch<ProductFilterBloc>().state.priceMax == -1
            ? 10000
            : context.watch<ProductFilterBloc>().state.priceMax);

    return Padding(
      padding: const EdgeInsets.all(AppSize.smallSize),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          const Row(
            children: [
              Text(
                'Price',
                style: TextStyle(
                  fontSize: 18,
                  fontWeight: FontWeight.w600,
                ),
              ),
            ],
          ),
          const SizedBox(height: AppSize.smallSize),
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: AppSize.smallSize),
            child: Row(
              children: [
                Column(
                  children: [
                    Container(
                      width: 80,
                      alignment: Alignment.center,
                      padding: const EdgeInsets.all(AppSize.xSmallSize),
                      decoration: BoxDecoration(
                        color: Theme.of(context).colorScheme.primary,
                        borderRadius:
                            BorderRadius.circular(AppSize.xxSmallSize),
                      ),
                      child: Text(_currentRangeValues.start.round().toString(),
                          style: Theme.of(context)
                              .textTheme
                              .titleSmall!
                              .copyWith(
                                  color:
                                      Theme.of(context).colorScheme.onPrimary)),
                    ),
                    const SizedBox(height: AppSize.xSmallSize),
                    Text(
                      "Min",
                      style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onSurface),
                    ),
                  ],
                ),
                const Spacer(),
                Column(
                  children: [
                    Container(
                      width: 80,
                      alignment: Alignment.center,
                      padding: const EdgeInsets.all(AppSize.xSmallSize),
                      decoration: BoxDecoration(
                        color: Theme.of(context).colorScheme.primary,
                        borderRadius:
                            BorderRadius.circular(AppSize.xxSmallSize),
                      ),
                      child: Text(_currentRangeValues.end.round().toString(),
                          style: Theme.of(context)
                              .textTheme
                              .titleSmall!
                              .copyWith(
                                  color:
                                      Theme.of(context).colorScheme.onPrimary)),
                    ),
                    const SizedBox(height: AppSize.xSmallSize),
                    Text(
                      "Max",
                      style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onSurface),
                    ),
                  ],
                ),
              ],
            ),
          ),
          const SizedBox(height: AppSize.xSmallSize),
          RangeSlider(
            values: _currentRangeValues,
            min: 1,
            max: 100000,
            divisions: 20,
            onChanged: (RangeValues values) {
              context.read<ProductFilterBloc>().add(SetPriceRangeEvent(
                  values.start.round().toDouble(),
                  values.end.round().toDouble()));
              setState(() {
                _currentRangeValues = values;
              });
            },
          ),
        ],
      ),
    );
  }
}
