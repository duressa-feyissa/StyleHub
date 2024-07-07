import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../../../core/utils/captilizations.dart';
import '../../../../../setUp/size/app_size.dart';
import '../../../domain/entities/product/design_entity.dart';
import '../../bloc/prdoct_filter/product_filter_bloc.dart';
import '../../bloc/product/product_bloc.dart';
import '../../widgets/filter/bottom_filter_bar.dart';
import '../../widgets/search.dart';

class DesignFullFilterScreen extends StatefulWidget {
  const DesignFullFilterScreen({super.key, this.isAdd, this.onTap});

  final bool? isAdd;
  final Function()? onTap;
  @override
  _DesignFullFilterScreenState createState() => _DesignFullFilterScreenState();
}

class _DesignFullFilterScreenState extends State<DesignFullFilterScreen> {
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
    List<DesignEntity> filterDesign() {
      List<DesignEntity> designs = context.watch<ProductBloc>().state.designs;
      if (searchController.text.isEmpty) return designs;
      return designs
          .where((design) => design.name
              .toLowerCase()
              .contains(searchController.text.toLowerCase()))
          .toList();
    }

    List<DesignEntity> designs = filterDesign();
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
                      title: "Search Design",
                      controller: searchController,
                    ),
                  ],
                ),
              ),
              Expanded(
                child: Column(
                  children: [
                    if (context.watch<ProductBloc>().state.designStatus ==
                        DesignStatus.loading)
                      const Expanded(
                          child: Center(child: CircularProgressIndicator())),
                    if (context.watch<ProductBloc>().state.designStatus ==
                        DesignStatus.success)
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
                                        .designStatus ==
                                    DesignStatus.success)
                                  for (int index = 0;
                                      index < designs.length;
                                      index++)
                                    Container(
                                      margin: const EdgeInsets.only(
                                          top: AppSize.smallSize),
                                      child: Column(
                                        children: [
                                          (context
                                                  .watch<ProductFilterBloc>()
                                                  .state
                                                  .selectedDesigns
                                                  .contains(designs[index].id))
                                              ? GestureDetector(
                                                  onTap: () {
                                                    final id =
                                                        designs[index].id;
                                                    context
                                                        .read<
                                                            ProductFilterBloc>()
                                                        .add(
                                                            RemoveSelectedDesignEvent(
                                                                id));
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
                                                          designs[index].name),
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
                                                    final id =
                                                        designs[index].id;
                                                    context
                                                        .read<
                                                            ProductFilterBloc>()
                                                        .add(
                                                            AddSelectedDesignEvent(
                                                                id));
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
                                                                designs[index]
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
                    if (widget.onTap != null && widget.onTap != null)
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
                                  .add(ClearSelectedDesignsEvent());
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
