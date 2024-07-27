import 'package:flutter/material.dart';
import 'package:timeago/timeago.dart' as timeago;

import '../../../domain/entities/product/product_entity.dart';
import 'show_image.dart';

class MyProduct extends StatelessWidget {
  const MyProduct({super.key, required this.product});

  final ProductEntity product;

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

    void gotoProductDetail(ProductEntity product) {
      // PersistentNavBarNavigator.pushNewScreenWithRouteSettings(
      //   context,
      //   settings: const RouteSettings(name: '/product/detail'),
      //   withNavBar: false,
      //   screen: ProductDetail(product: product),
      //   pageTransitionAnimation: PageTransitionAnimation.fade,
      // );
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
              color: Theme.of(context).colorScheme.primaryContainer,
              child: ShowImage(image: product.images[0].imageUri),
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
                timeago.format(product.createdAt),
                style: Theme.of(context)
                    .textTheme
                    .bodyMedium!
                    .copyWith(color: Theme.of(context).colorScheme.secondary),
              ),
            ],
          )
        ],
      ),
    );
  }
}
