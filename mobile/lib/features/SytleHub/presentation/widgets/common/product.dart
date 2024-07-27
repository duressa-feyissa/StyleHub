import 'package:flutter/material.dart';
import 'package:persistent_bottom_nav_bar/persistent_tab_view.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/product_entity.dart';
import 'package:style_hub/features/SytleHub/presentation/pages/product_detail.dart';
import 'package:style_hub/features/SytleHub/presentation/widgets/common/show_image.dart';

import '../../../../../setUp/size/app_size.dart';

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

    void gotoProductDetail(ProductEntity product) {
      PersistentNavBarNavigator.pushNewScreenWithRouteSettings(
        context,
        settings: const RouteSettings(name: '/product/detail'),
        withNavBar: false,
        screen: ProductDetail(product: product),
        pageTransitionAnimation: PageTransitionAnimation.fade,
      );
    }

    return GestureDetector(
      onTap: () => gotoProductDetail(product),
      child: ConstrainedBox(
        constraints: BoxConstraints(
          maxWidth: width,
        ),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            SizedBox(
              width: width,
              height: width,
              child: Stack(
                children: [
                  Container(
                    color: Theme.of(context).colorScheme.primaryContainer,
                    child: ShowImage(image: product.images[0].imageUri),
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
                        borderRadius:
                            BorderRadius.circular(AppSize.xxSmallSize),
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
                        color: Theme.of(context).colorScheme.onPrimary,
                        shape: BoxShape.circle,
                      ),
                      child: Icon(
                        product.isFavorite
                            ? Icons.favorite
                            : Icons.favorite_border,
                        color: product.isFavorite
                            ? Theme.of(context).colorScheme.error
                            : Theme.of(context).colorScheme.secondary,
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
                  product.shopInfo.city,
                  style: Theme.of(context).textTheme.bodySmall!.copyWith(
                        color: Theme.of(context).colorScheme.secondary,
                      ),
                ),
              ],
            )
          ],
        ),
      ),
    );
  }
}
