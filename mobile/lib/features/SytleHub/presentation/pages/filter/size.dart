import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../../../core/utils/captilizations.dart';
import '../../../../../setUp/size/app_size.dart';
import '../../../domain/entities/product/size_entity.dart';
import '../../bloc/prdoct_filter/product_filter_bloc.dart';
import '../../bloc/product/product_bloc.dart';
import '../../widgets/filter/bottom_filter_bar.dart';
import '../../widgets/search.dart';

class SizeFullFilterScreen extends StatefulWidget {
  const SizeFullFilterScreen({super.key, this.isAdd, this.onTap});

  final bool? isAdd;
  final Function()? onTap;

  @override
  _SizeFullFilterScreenState createState() => _SizeFullFilterScreenState();
}

class _SizeFullFilterScreenState extends State<SizeFullFilterScreen> {
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
    List<SizeEntity> filterSize() {
      List<SizeEntity> sizes = context.watch<ProductBloc>().state.sizes;
      if (searchController.text.isEmpty) return sizes;
      return sizes
          .where((size) => size.name
              .toLowerCase()
              .contains(searchController.text.toLowerCase()))
          .toList();
    }

    List<SizeEntity> sizes = filterSize();

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
            crossAxisAlignment: CrossAxisAlignment.start,
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
                      title: "Search Size",
                      controller: searchController,
                    ),
                  ],
                ),
              ),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    if (context.watch<ProductBloc>().state.sizeStatus ==
                        SizeStatus.loading)
                      const Expanded(
                          child: Center(child: CircularProgressIndicator())),
                    if (context.watch<ProductBloc>().state.sizeStatus ==
                        SizeStatus.success)
                      Expanded(
                        child: Padding(
                          padding: const EdgeInsets.symmetric(
                              horizontal: AppSize.smallSize),
                          child: SingleChildScrollView(
                            physics: const BouncingScrollPhysics(),
                            child: Wrap(
                              spacing: AppSize.smallSize,
                              children: [
                                if (context
                                        .watch<ProductBloc>()
                                        .state
                                        .sizeStatus ==
                                    SizeStatus.success)
                                  for (int index = 0;
                                      index < sizes.length;
                                      index++)
                                    Container(
                                      margin: const EdgeInsets.only(
                                          top: AppSize.smallSize),
                                      child: Column(
                                        children: [
                                          (context
                                                  .watch<ProductFilterBloc>()
                                                  .state
                                                  .selectedSizes
                                                  .contains(sizes[index].id))
                                              ? GestureDetector(
                                                  onTap: () {
                                                    final sizeId =
                                                        sizes[index].id;
                                                    context
                                                        .read<
                                                            ProductFilterBloc>()
                                                        .add(
                                                            RemoveSelectedSizeEvent(
                                                                sizeId));
                                                  },
                                                  child: Container(
                                                    padding: const EdgeInsets
                                                        .symmetric(
                                                        horizontal:
                                                            AppSize.smallSize,
                                                        vertical: AppSize
                                                            .xxSmallSize),
                                                    decoration: BoxDecoration(
                                                      color: Theme.of(context)
                                                          .colorScheme
                                                          .onSurface,
                                                      borderRadius:
                                                          BorderRadius.circular(
                                                              AppSize
                                                                  .xxSmallSize),
                                                      border: Border.all(
                                                        color: Theme.of(context)
                                                            .colorScheme
                                                            .onSurface,
                                                      ),
                                                    ),
                                                    child: Text(
                                                      Captilizations.capitalize(
                                                          sizes[index].name),
                                                      style: Theme.of(context)
                                                          .textTheme
                                                          .bodyMedium!
                                                          .copyWith(
                                                            color: Theme.of(
                                                                    context)
                                                                .colorScheme
                                                                .onPrimary,
                                                          ),
                                                    ),
                                                  ),
                                                )
                                              : GestureDetector(
                                                  onTap: () {
                                                    final sizeId =
                                                        sizes[index].id;
                                                    context
                                                        .read<
                                                            ProductFilterBloc>()
                                                        .add(
                                                            AddSelectedSizeEvent(
                                                                sizeId));
                                                  },
                                                  child: Container(
                                                      padding: const EdgeInsets
                                                          .symmetric(
                                                          horizontal:
                                                              AppSize.smallSize,
                                                          vertical: AppSize
                                                              .xxSmallSize),
                                                      decoration: BoxDecoration(
                                                        borderRadius: BorderRadius
                                                            .circular(AppSize
                                                                .xxSmallSize),
                                                        border: Border.all(
                                                          color: Theme.of(
                                                                  context)
                                                              .colorScheme
                                                              .surfaceContainerHigh,
                                                        ),
                                                      ),
                                                      child: Text(
                                                        Captilizations
                                                            .capitalize(
                                                                sizes[index]
                                                                    .name),
                                                        style: Theme.of(context)
                                                            .textTheme
                                                            .bodyMedium!
                                                            .copyWith(
                                                              color: Theme.of(
                                                                      context)
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
                        ),
                      ),
                    if (widget.isAdd != null && widget.onTap != null)
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
                                  .add(ClearSelectedSizesEvent());
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
