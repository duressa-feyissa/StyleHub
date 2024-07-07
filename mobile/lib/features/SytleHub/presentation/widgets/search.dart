import 'package:flutter/material.dart';

import '../../../../setUp/size/app_size.dart';

class Search extends StatefulWidget {
  const Search({super.key, required this.controller, required this.title});

  final TextEditingController controller;
  final String title;

  @override
  _SearchState createState() => _SearchState();
}

class _SearchState extends State<Search> {
  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: TextField(
        controller: widget.controller,
        decoration: InputDecoration(
          contentPadding: const EdgeInsets.all(AppSize.xSmallSize),
          hintText: widget.title,
          hintStyle: Theme.of(context)
              .textTheme
              .bodyMedium!
              .copyWith(color: Theme.of(context).colorScheme.outline),
          prefixIcon: Icon(
            Icons.search,
            color: Theme.of(context).colorScheme.outline,
          ),
          filled: true,
          fillColor: Theme.of(context).colorScheme.primaryContainer,
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(AppSize.xSmallSize),
            borderSide: BorderSide.none,
          ),
        ),
      ),
    );
  }
}
