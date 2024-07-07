import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../../../setUp/size/app_size.dart';
import '../../bloc/prdoct_filter/product_filter_bloc.dart';
import '../../widgets/filter/bottom_filter_bar.dart';

class PriceFullFilterScreen extends StatefulWidget {
  const PriceFullFilterScreen(
      {super.key, required this.isAdd, required this.onTap});

  final bool isAdd;
  final Function() onTap;

  @override
  _PriceFullFilterScreenState createState() => _PriceFullFilterScreenState();
}

class _PriceFullFilterScreenState extends State<PriceFullFilterScreen> {
  bool _shouldPop = false;

  @override
  Widget build(BuildContext context) {
    RangeValues _currentRangeValues = RangeValues(
        context.watch<ProductFilterBloc>().state.priceMin == -1
            ? 1
            : context.watch<ProductFilterBloc>().state.priceMin,
        context.watch<ProductFilterBloc>().state.priceMax == -1
            ? 10000
            : context.watch<ProductFilterBloc>().state.priceMax);
    return SafeArea(
      child: Scaffold(
        backgroundColor: Theme.of(context).colorScheme.onPrimary,
        body: NotificationListener<ScrollNotification>(
          onNotification: (notification) {
            if (notification is ScrollUpdateNotification && !_shouldPop) {
              if (notification.metrics.pixels < -100) {
                setState(() {
                  _shouldPop = true;
                });
                Navigator.pop(context, true);
              }
            }
            return false;
          },
          child: Column(
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
              Expanded(
                child: SingleChildScrollView(
                  physics: const BouncingScrollPhysics(),
                  child: Padding(
                    padding: const EdgeInsets.symmetric(
                      horizontal: AppSize.smallSize,
                      vertical: AppSize.largeSize,
                    ),
                    child: Column(
                      children: [
                        Row(
                          children: [
                            Column(
                              children: [
                                Container(
                                  width: 85,
                                  alignment: Alignment.center,
                                  padding:
                                      const EdgeInsets.all(AppSize.xSmallSize),
                                  decoration: BoxDecoration(
                                    color:
                                        Theme.of(context).colorScheme.primary,
                                    borderRadius: BorderRadius.circular(
                                        AppSize.xxSmallSize),
                                  ),
                                  child: Text(
                                    _currentRangeValues.start
                                        .round()
                                        .toString(),
                                    style: Theme.of(context)
                                        .textTheme
                                        .titleMedium!
                                        .copyWith(
                                          color: Theme.of(context)
                                              .colorScheme
                                              .onPrimary,
                                        ),
                                  ),
                                ),
                                const SizedBox(height: AppSize.xSmallSize),
                                Text(
                                  "Min",
                                  style: Theme.of(context)
                                      .textTheme
                                      .bodyLarge!
                                      .copyWith(
                                        color: Theme.of(context)
                                            .colorScheme
                                            .onSurface,
                                      ),
                                ),
                              ],
                            ),
                            const Spacer(),
                            Column(
                              children: [
                                Container(
                                  width: 80,
                                  alignment: Alignment.center,
                                  padding:
                                      const EdgeInsets.all(AppSize.xSmallSize),
                                  decoration: BoxDecoration(
                                    color:
                                        Theme.of(context).colorScheme.primary,
                                    borderRadius: BorderRadius.circular(
                                        AppSize.xxSmallSize),
                                  ),
                                  child: Text(
                                    _currentRangeValues.end.round().toString(),
                                    style: Theme.of(context)
                                        .textTheme
                                        .titleMedium!
                                        .copyWith(
                                          color: Theme.of(context)
                                              .colorScheme
                                              .onPrimary,
                                        ),
                                  ),
                                ),
                                const SizedBox(height: AppSize.xSmallSize),
                                Text(
                                  "Max",
                                  style: Theme.of(context)
                                      .textTheme
                                      .bodyLarge!
                                      .copyWith(
                                        color: Theme.of(context)
                                            .colorScheme
                                            .onSurface,
                                      ),
                                ),
                              ],
                            ),
                          ],
                        ),
                        const SizedBox(height: AppSize.xSmallSize),
                        RangeSlider(
                          values: _currentRangeValues,
                          min: 1,
                          max: 100000,
                          divisions: 20,
                          onChanged: (RangeValues values) {
                            context.read<ProductFilterBloc>().add(
                                  SetPriceRangeEvent(
                                    values.start.round().toDouble(),
                                    values.end.round().toDouble(),
                                  ),
                                );
                            setState(() {
                              _currentRangeValues = values;
                            });
                          },
                        ),
                        SizedBox(
                            height: MediaQuery.of(context).size.height * 0.575),
                      ],
                    ),
                  ),
                ),
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
                      context
                          .read<ProductFilterBloc>()
                          .add(ClearPriceRangeEvent());
                    },
                    onTapResult: () {
                      widget.onTap();
                      Navigator.pop(context, true);
                    }),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
