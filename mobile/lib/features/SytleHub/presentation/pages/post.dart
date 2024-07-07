import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../bloc/scroll/scroll_bloc.dart';
import '../widgets/search.dart';

import '../../../../setUp/size/app_size.dart';

const image =
    "https://images.unsplash.com/photo-1539571696357-5a69c17a67c6?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";


class PostScreen extends StatefulWidget {
  const PostScreen({super.key});

  @override
  State<PostScreen> createState() => _PostScreenState();
}

class _PostScreenState extends State<PostScreen> {
  @override
  void initState() {
    super.initState();

    _scrollController.addListener(_scrollListener);
  }

  final ScrollController _scrollController = ScrollController();
  Timer? _scrollEndTimer;

  void _scrollListener() {
    if (_scrollEndTimer != null && _scrollEndTimer!.isActive) {
      _scrollEndTimer!.cancel();
    }

    if (_scrollController.position.userScrollDirection ==
        ScrollDirection.reverse) {
      context.read<ScrollBloc>().add(ToggleVisibilityEvent(isVisible: false));
    } else if (_scrollController.position.userScrollDirection ==
        ScrollDirection.forward) {
      context.read<ScrollBloc>().add(ToggleVisibilityEvent(isVisible: true));
    }
  }

  final TextEditingController searchController = TextEditingController();

  @override
  void dispose() {
    _scrollController.removeListener(_scrollListener);
    _scrollController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        backgroundColor: Theme.of(context).colorScheme.onPrimary,
        body: Padding(
          padding: const EdgeInsets.all(AppSize.smallSize),
          child: Column(
            children: [
              Row(
                children: [
                  const CircleAvatar(
                    backgroundImage: NetworkImage(image),
                    radius: 22.5,
                  ),
                  const SizedBox(width: AppSize.smallSize),
                  Search(
                    title: "What are you looking for?",
                    controller: searchController,
                  ),
                  const SizedBox(width: AppSize.smallSize),
                  Icon(
                    Icons.notifications_none_outlined,
                    size: 32,
                    color: Theme.of(context).colorScheme.onSurface,
                  ),
                ],
              ),
              const SizedBox(height: AppSize.smallSize),
              TextField(
                decoration: InputDecoration(
                  contentPadding: const EdgeInsets.all(AppSize.smallSize),
                  hintText: 'Title',
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(10),
                  ),
                ),
              ),
              const SizedBox(height: AppSize.smallSize),
              TextField(
                maxLines: 5,
                decoration: InputDecoration(
                  contentPadding: const EdgeInsets.all(AppSize.smallSize),
                  hintText: 'Description',
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(10),
                  ),
                ),
              ),
              const SizedBox(height: AppSize.smallSize),
              Row(
                children: [
                  Expanded(
                    child: TextField(
                      decoration: InputDecoration(
                        contentPadding: const EdgeInsets.all(AppSize.smallSize),
                        hintText: 'Price',
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(10),
                        ),
                      ),
                    ),
                  ),
                  const SizedBox(width: AppSize.mediumSize),
                  Expanded(
                    child: TextField(
                      keyboardType: TextInputType.number,
                      decoration: InputDecoration(
                        contentPadding: const EdgeInsets.all(AppSize.smallSize),
                        hintText: 'Quantity',
                        fillColor: Theme.of(context).colorScheme.primary,
                        hintStyle: Theme.of(context)
                            .textTheme
                            .bodyLarge!
                            .copyWith(
                                color: Theme.of(context).colorScheme.outline),
                        border: OutlineInputBorder(
                          borderRadius:
                              BorderRadius.circular(AppSize.xSmallSize),
                        ),
                      ),
                    ),
                  ),
                ],
              )
            ],
          ),
        ),
      ),
    );
  }
}
