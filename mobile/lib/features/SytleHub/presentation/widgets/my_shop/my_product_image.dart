import 'package:flutter/material.dart';
import 'package:style_hub/setUp/size/app_size.dart';

import '../../../domain/entities/product/product_entity.dart';
import '../common/show_image.dart';

class MyProductImage extends StatefulWidget {
  const MyProductImage({super.key, required this.product});

  final ProductEntity product;

  @override
  State<MyProductImage> createState() => _MyProductImageState();
}

class _MyProductImageState extends State<MyProductImage> {
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

    return ConstrainedBox(
      constraints: BoxConstraints(
        maxWidth: width,
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          SizedBox(
            width: width,
            height: width,
            child: Container(
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(AppSize.xxSmallSize),
                color: Theme.of(context).colorScheme.primaryContainer,
              ),
              child: ShowImage(image: widget.product.images[0].imageUri),
            ),
          ),
        ],
      ),
    );
  }
}
