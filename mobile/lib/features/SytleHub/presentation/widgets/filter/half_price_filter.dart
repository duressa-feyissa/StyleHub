import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../../../setUp/size/app_size.dart';
import '../../bloc/prdoct_filter/product_filter_bloc.dart';
import 'bottom_filter_bar.dart';

class HalfPriceFilterDisplay extends StatefulWidget {
  const HalfPriceFilterDisplay(
      {super.key, required this.isAdd, required this.onTap});

  final bool isAdd;
  final Function() onTap;

  @override
  State<HalfPriceFilterDisplay> createState() => _HalfPriceFilterDisplayState();
}

class _HalfPriceFilterDisplayState extends State<HalfPriceFilterDisplay> {
  @override
  Widget build(BuildContext context) {
    RangeValues _currentRangeValues = RangeValues(
        context.watch<ProductFilterBloc>().state.priceMin == -1
            ? 1
            : context.watch<ProductFilterBloc>().state.priceMin,
        context.watch<ProductFilterBloc>().state.priceMax == -1
            ? 10000
            : context.watch<ProductFilterBloc>().state.priceMax);

    return SingleChildScrollView(
      child: Column(
        children: [
          Column(
            children: <Widget>[
              Container(
                padding: const EdgeInsets.only(
                    left: AppSize.smallSize,
                    bottom: AppSize.xSmallSize,
                    right: AppSize.smallSize,
                    top: AppSize.xSmallSize),
                decoration: BoxDecoration(
                  border: Border(
                    bottom: BorderSide(
                      color: Theme.of(context)
                          .colorScheme
                          .onSurface
                          .withOpacity(0.1),
                      width: 0.5,
                    ),
                  ),
                ),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    const Expanded(
                      child: Center(
                        child: Text(
                          'Price',
                          style: TextStyle(
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
                          borderRadius:
                              BorderRadius.circular(AppSize.mediumSize),
                          border: Border.all(
                            color: Theme.of(context)
                                .colorScheme
                                .surfaceContainerHigh,
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
              const SizedBox(height: AppSize.largeSize),
              Padding(
                padding:
                    const EdgeInsets.symmetric(horizontal: AppSize.smallSize),
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
                          child: Text(
                              _currentRangeValues.start.round().toString(),
                              style: Theme.of(context)
                                  .textTheme
                                  .titleSmall!
                                  .copyWith(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .onPrimary)),
                        ),
                        const SizedBox(height: AppSize.xSmallSize),
                        Text(
                          "Min",
                          style: Theme.of(context)
                              .textTheme
                              .bodyLarge!
                              .copyWith(
                                  color:
                                      Theme.of(context).colorScheme.onSurface),
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
                          child: Text(
                              _currentRangeValues.end.round().toString(),
                              style: Theme.of(context)
                                  .textTheme
                                  .titleSmall!
                                  .copyWith(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .onPrimary)),
                        ),
                        const SizedBox(height: AppSize.xSmallSize),
                        Text(
                          "Max",
                          style: Theme.of(context)
                              .textTheme
                              .bodyLarge!
                              .copyWith(
                                  color:
                                      Theme.of(context).colorScheme.onSurface),
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
              const SizedBox(height: AppSize.xLargeSize),
            ],
          ),
          Container(
              padding: const EdgeInsets.all(AppSize.smallSize),
              decoration: BoxDecoration(
                border: Border(
                  top: BorderSide(
                    color: Theme.of(context)
                        .colorScheme
                        .onSurface
                        .withOpacity(0.1),
                    width: 0.5,
                  ),
                ),
              ),
              child: BottomFilterBar(
                isAdd: widget.isAdd,
                onTapClear: () {
                  context.read<ProductFilterBloc>().add(ClearPriceRangeEvent());
                },
                onTapResult: () {
                  widget.onTap();
                  Navigator.pop(context, true);
                },
              ))
        ],
      ),
    );
  }
}
