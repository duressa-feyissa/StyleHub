import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/image_entity.dart';
import 'package:style_hub/setUp/size/app_size.dart';

import '../bloc/shop/shop_bloc.dart';
import '../widgets/common/show_image.dart';

class MyImageList extends StatefulWidget {
  const MyImageList({super.key, required this.selectedImages});

  final Set<ImageEntity> selectedImages;

  @override
  _MyImageListState createState() => _MyImageListState();
}

class _MyImageListState extends State<MyImageList> {
  final Set<ImageEntity> selectedImages = {};

  @override
  void initState() {
    super.initState();
    selectedImages.addAll(widget.selectedImages);
  }

  @override
  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    if (width <= 340) {
    } else if (width <= 500) {
      width = (width - (16 * 3)) / 2;
    } else if (width <= 700) {
      width = (width - (16 * 4)) / 3;
    } else if (width <= 900) {
      width = (width - (16 * 5)) / 4;
    } else if (width <= 1100) {
      width = (width - (16 * 6)) / 5;
    } else if (width <= 1100) {
      width = (width - (16 * 7)) / 6;
    } else if (width <= 1400) {
      width = (width - (16 * 8)) / 7;
    } else {
      width = (width - (16 * 9)) / 8;
    }

    final String shopId = context.read<ShopBloc>().state.myShopId ?? "";

    return SafeArea(
      child: Scaffold(
        body: Column(
          children: [
            Padding(
              padding: const EdgeInsets.all(AppSize.smallSize),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  GestureDetector(
                    onTap: () => Navigator.pop(context, selectedImages),
                    child: Icon(
                      Icons.arrow_back_outlined,
                      size: 32,
                      color: Theme.of(context).colorScheme.onSurface,
                    ),
                  ),
                  Text(
                    "My Images",
                    style: Theme.of(context).textTheme.titleMedium!.copyWith(
                          color: Theme.of(context).colorScheme.onSurface,
                        ),
                  ),
                  const SizedBox(width: AppSize.xLargeSize),
                ],
              ),
            ),
            Expanded(
              child: Column(
                children: [
                  Container(
                    height: 2,
                    margin: const EdgeInsets.only(top: AppSize.xSmallSize),
                    color: Theme.of(context).colorScheme.primaryContainer,
                  ),
                  const SizedBox(height: AppSize.smallSize),
                  Wrap(
                    spacing: AppSize.smallSize,
                    runSpacing: AppSize.smallSize,
                    children: context
                        .watch<ShopBloc>()
                        .state
                        .shops[shopId]!
                        .images
                        .map(
                          (e) => GestureDetector(
                            onTap: () => _toggleSelection(e),
                            child: ConstrainedBox(
                              constraints: BoxConstraints(
                                maxWidth: width,
                                maxHeight: width,
                              ),
                              child: Container(
                                width: double.infinity,
                                height: double.infinity,
                                color: Theme.of(context)
                                    .colorScheme
                                    .primaryContainer,
                                child: Stack(
                                  children: [
                                    ShowImage(image: e.imageUri),
                                    if (selectedImages.contains(e))
                                      Positioned(
                                        top: 8,
                                        right: 8,
                                        child: Icon(
                                          Icons.check_circle,
                                          color: Theme.of(context)
                                              .colorScheme
                                              .secondary,
                                        ),
                                      ),
                                  ],
                                ),
                              ),
                            ),
                          ),
                        )
                        .toList(),
                  )
                ],
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
              child: Row(
                crossAxisAlignment: CrossAxisAlignment.center,
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  GestureDetector(
                      onTap: () {
                        setState(() {
                          selectedImages.clear();
                        });
                      },
                      child: Text("Clear All",
                          style: Theme.of(context)
                              .textTheme
                              .bodyMedium!
                              .copyWith(
                                  color:
                                      Theme.of(context).colorScheme.primary))),
                  Row(
                    children: [
                      GestureDetector(
                        onTap: () {
                          if (selectedImages.isNotEmpty) {
                            Navigator.pop(context, selectedImages);
                          } else {
                            showDialog(
                                context: context,
                                builder: (context) => SimpleDialog(
                                      title: const Text("No image selected"),
                                      children: [
                                        SimpleDialogOption(
                                          onPressed: () =>
                                              Navigator.pop(context),
                                          child: const Text("OK"),
                                        ),
                                      ],
                                    ));
                          }
                        },
                        child: Container(
                          alignment: Alignment.center,
                          padding: const EdgeInsets.symmetric(
                              horizontal: AppSize.smallSize,
                              vertical: AppSize.xSmallSize),
                          margin:
                              const EdgeInsets.only(right: AppSize.smallSize),
                          decoration: BoxDecoration(
                            color: selectedImages.isNotEmpty
                                ? Theme.of(context).colorScheme.primary
                                : Theme.of(context).colorScheme.secondary,
                            borderRadius:
                                BorderRadius.circular(AppSize.xxSmallSize),
                          ),
                          child: Text(
                            selectedImages.isNotEmpty
                                ? "Add Image (${selectedImages.length})"
                                : "Add Image",
                            style: Theme.of(context)
                                .textTheme
                                .bodyMedium!
                                .copyWith(
                                  color:
                                      Theme.of(context).colorScheme.onPrimary,
                                ),
                          ),
                        ),
                      ),
                    ],
                  ),
                ],
              ),
            )
          ],
        ),
      ),
    );
  }

  void _toggleSelection(ImageEntity imageUri) {
    setState(() {
      if (selectedImages.contains(imageUri)) {
        selectedImages.remove(imageUri);
      } else {
        selectedImages.add(imageUri);
      }
    });
  }
}
