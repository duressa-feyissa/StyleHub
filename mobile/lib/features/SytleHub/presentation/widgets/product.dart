import 'package:flutter/material.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/product_entity.dart';

import '../../../../setUp/size/app_size.dart';

class Product extends StatelessWidget {
  final ProductEntity product;

  const Product({
    super.key,
    required this.product,
  });

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
            child: Stack(
              children: [
                Container(
                  color: Theme.of(context).colorScheme.primaryContainer,
                  child: Image.network(
                    product.images.first.imageUri,
                    fit: BoxFit.cover,
                  ),
                ),
                Positioned(
                  top: 6,
                  left: 6,
                  child: Container(
                    padding: const EdgeInsets.symmetric(
                      horizontal: AppSize.xSmallSize - 4,
                    ),
                    decoration: BoxDecoration(
                      color: Theme.of(context).colorScheme.onSurface,
                      borderRadius: BorderRadius.circular(AppSize.xxSmallSize),
                    ),
                    child: Text(
                      "New",
                      style: Theme.of(context).textTheme.bodySmall!.copyWith(
                            color: Theme.of(context).colorScheme.onPrimary,
                          ),
                    ),
                  ),
                ),
                Positioned(
                  bottom: 8,
                  right: 6,
                  child: Container(
                    padding: const EdgeInsets.all(AppSize.xxSmallSize),
                    decoration: BoxDecoration(
                      color: Theme.of(context).colorScheme.primaryContainer,
                      shape: BoxShape.circle,
                    ),
                    child: Icon(
                      Icons.favorite_border,
                      color: Theme.of(context).colorScheme.secondary,
                    ),
                  ),
                ),
              ],
            ),
          ),
          Column(
            mainAxisAlignment: MainAxisAlignment.start,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                product.title,
                softWrap: true,
                style: Theme.of(context).textTheme.titleSmall!.copyWith(
                      color: Theme.of(context).colorScheme.onSurface,
                    ),
              ),
              Text(
                "ETB ${product.price}",
                style: Theme.of(context).textTheme.titleSmall!.copyWith(
                      color: Theme.of(context).colorScheme.primary,
                    ),
              ),
              const SizedBox(height: 6),
              Text(
                product.city,
                style: Theme.of(context).textTheme.bodySmall!.copyWith(
                      color: Theme.of(context).colorScheme.secondary,
                    ),
              ),
            ],
          )
        ],
      ),
    );
  }
}
