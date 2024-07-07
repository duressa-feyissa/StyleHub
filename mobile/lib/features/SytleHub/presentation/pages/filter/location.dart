import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../../../core/utils/captilizations.dart';
import '../../../../../setUp/size/app_size.dart';
import '../../../domain/entities/product/location_entity.dart';
import '../../bloc/prdoct_filter/product_filter_bloc.dart';
import '../../bloc/product/product_bloc.dart';
import '../../widgets/filter/bottom_filter_bar.dart';
import '../../widgets/search.dart';

class LocationFullFilterScreen extends StatefulWidget {
  const LocationFullFilterScreen({super.key, this.isAdd, this.onTap});

  final bool? isAdd;
  final Function()? onTap;

  @override
  _LocationFullFilterScreenState createState() =>
      _LocationFullFilterScreenState();
}

class _LocationFullFilterScreenState extends State<LocationFullFilterScreen> {
  bool _shouldPop = false;

  TextEditingController searchController = TextEditingController();

  @override
  void initState() {
    super.initState();

    searchController.addListener(_onSearchTextChanged);
  }

  @override
  void dispose() {
    searchController.dispose();
    super.dispose();
  }

  void _onSearchTextChanged() {
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    List<LocationEntity> filterLocation() {
      List<LocationEntity> locations =
          context.watch<ProductBloc>().state.locations;
      if (searchController.text.isEmpty) return locations;
      return locations
          .where((location) => location.name
              .toLowerCase()
              .contains(searchController.text.toLowerCase()))
          .toList();
    }

    List<LocationEntity> locations = filterLocation();

    return SafeArea(
      child: Scaffold(
        backgroundColor: Theme.of(context).colorScheme.onPrimary,
        body: NotificationListener<ScrollNotification>(
          onNotification: (notification) {
            if (notification is ScrollUpdateNotification && !_shouldPop) {
              if (notification.metrics.pixels < -120) {
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
                padding: const EdgeInsets.symmetric(
                  horizontal: AppSize.smallSize,
                  vertical: AppSize.smallSize,
                ),
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
                  children: [
                    GestureDetector(
                      onTap: () => Navigator.pop(context, true),
                      child: Icon(
                        Icons.close_rounded,
                        size: 32,
                        color: Theme.of(context).colorScheme.onSurface,
                      ),
                    ),
                    const SizedBox(width: AppSize.smallSize),
                    Search(
                      title: "Search Location",
                      controller: searchController,
                    ),
                  ],
                ),
              ),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    if (context.watch<ProductBloc>().state.locationStatus ==
                        LocationStatus.loading)
                      const Expanded(
                          child: Center(child: CircularProgressIndicator())),
                    if (context.watch<ProductBloc>().state.locationStatus ==
                        LocationStatus.success)
                      Expanded(
                          child: Padding(
                        padding: const EdgeInsets.symmetric(
                            horizontal: AppSize.xSmallSize),
                        child: SingleChildScrollView(
                          physics: const BouncingScrollPhysics(),
                          child: Wrap(spacing: AppSize.smallSize, children: [
                            for (int index = 0;
                                index < locations.length;
                                index++)
                              Container(
                                  margin: const EdgeInsets.only(
                                      top: AppSize.smallSize),
                                  child: (GestureDetector(
                                    onTap: () {
                                      final location = locations[index];

                                      context.read<ProductFilterBloc>().add(
                                          SetLocationEvent(
                                              location.id,
                                              location.latitude,
                                              location.longitude,
                                              40));
                                    },
                                    child: Container(
                                      padding: const EdgeInsets.symmetric(
                                          horizontal: AppSize.smallSize,
                                          vertical: AppSize.xxSmallSize),
                                      decoration: (context
                                                      .watch<
                                                          ProductFilterBloc>()
                                                      .state
                                                      .latitute ==
                                                  locations[index].latitude &&
                                              context
                                                      .watch<
                                                          ProductFilterBloc>()
                                                      .state
                                                      .longitude ==
                                                  locations[index].longitude)
                                          ? BoxDecoration(
                                              color: Theme.of(context)
                                                  .colorScheme
                                                  .onSurface,
                                              borderRadius:
                                                  BorderRadius.circular(
                                                      AppSize.xxSmallSize),
                                              border: Border.all(
                                                color: Theme.of(context)
                                                    .colorScheme
                                                    .onSurface,
                                              ),
                                            )
                                          : BoxDecoration(
                                              borderRadius:
                                                  BorderRadius.circular(
                                                      AppSize.xxSmallSize),
                                              border: Border.all(
                                                color: Theme.of(context)
                                                    .colorScheme
                                                    .surfaceContainerHigh,
                                              ),
                                            ),
                                      child: Text(
                                        Captilizations.capitalize(
                                            locations[index].name),
                                        style: (context
                                                        .watch<
                                                            ProductFilterBloc>()
                                                        .state
                                                        .latitute ==
                                                    locations[index].latitude &&
                                                context
                                                        .watch<
                                                            ProductFilterBloc>()
                                                        .state
                                                        .longitude ==
                                                    locations[index].longitude)
                                            ? Theme.of(context)
                                                .textTheme
                                                .bodyMedium!
                                                .copyWith(
                                                    color: Theme.of(context)
                                                        .colorScheme
                                                        .onPrimary)
                                            : Theme.of(context)
                                                .textTheme
                                                .bodyMedium!
                                                .copyWith(
                                                  color: Theme.of(context)
                                                      .colorScheme
                                                      .onSurface,
                                                ),
                                      ),
                                    ),
                                  ))),
                          ]),
                        ),
                      )),
                    if (widget.onTap != null && widget.isAdd != null)
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
                            isAdd: widget.isAdd!,
                            onTapClear: () {
                              context
                                  .read<ProductFilterBloc>()
                                  .add(ClearLocationEvent());
                            },
                            onTapResult: () {
                              widget.onTap!();
                              Navigator.pop(context, true);
                            }),
                      ),
                  ],
                ),
              )
            ],
          ),
        ),
      ),
    );
  }
}
