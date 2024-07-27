import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/product_entity.dart';
import 'package:timeago/timeago.dart' as timeago;

import '../../../../../setUp/size/app_size.dart';
import '../../bloc/shop/shop_bloc.dart';
import '../../bloc/user/user_bloc.dart';
import '../common/my_product.dart';
import 'my_product_image.dart';

class MyShopProductList extends StatelessWidget {
  final String shopId;
  const MyShopProductList({super.key, required this.shopId});
  @override
  Widget build(BuildContext context) {
    List<ProductEntity> products =
        context.watch<ShopBloc>().state.shops[shopId]?.products ?? [];

    if (context.read<ShopBloc>().state.shopProductsStatus ==
        ShopProductsStatus.loading) {
      return const Center(
        child: CircularProgressIndicator(),
      );
    }

    if (products.isEmpty) {
      return Center(
        child: Text(
          'No products found',
          style: Theme.of(context)
              .textTheme
              .bodyMedium!
              .copyWith(color: Theme.of(context).colorScheme.secondary),
        ),
      );
    }

    return SingleChildScrollView(
      child: Wrap(
          spacing: AppSize.smallSize,
          runSpacing: AppSize.smallSize,
          children: products
              .map((product) => GestureDetector(
                    onTap: () {
                      _showUnarchiveBottomSheet(context, product);
                    },
                    child: MyProduct(
                      product: product,
                    ),
                  ))
              .toList()),
    );
  }

  void _showUnarchiveBottomSheet(BuildContext context, ProductEntity product) {
    showModalBottomSheet(
      useSafeArea: true,
      backgroundColor: Theme.of(context).colorScheme.onPrimary,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.only(
          topLeft: Radius.circular(AppSize.xxxSmallSize),
          topRight: Radius.circular(AppSize.xxxSmallSize),
        ),
      ),
      context: context,
      builder: (BuildContext context) {
        return SingleChildScrollView(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              Container(
                padding: const EdgeInsets.only(
                    left: AppSize.smallSize,
                    bottom: AppSize.xSmallSize,
                    right: AppSize.smallSize,
                    top: AppSize.xSmallSize),
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
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    const Expanded(
                      child: Center(
                        child: Text(
                          'Product Detail',
                          style: TextStyle(
                            fontSize: 18,
                            fontWeight: FontWeight.w600,
                          ),
                        ),
                      ),
                    ),
                    GestureDetector(
                      onTap: () {
                        Navigator.pop(context, true);
                      },
                      child: Container(
                        padding: const EdgeInsets.all(AppSize.xxSmallSize),
                        decoration: BoxDecoration(
                          borderRadius:
                              BorderRadius.circular(AppSize.mediumSize),
                          border: Border.all(
                            color: Theme.of(context)
                                .colorScheme
                                .surfaceContainerHigh,
                          ),
                        ),
                        child: Icon(
                          Icons.close,
                          color: Theme.of(context).colorScheme.onSurface,
                        ),
                      ),
                    ),
                  ],
                ),
              ),
              Container(
                padding: const EdgeInsets.symmetric(
                    horizontal: AppSize.smallSize,
                    vertical: AppSize.mediumSize),
                child: Column(
                  mainAxisSize: MainAxisSize.min,
                  mainAxisAlignment: MainAxisAlignment.start,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Row(
                      mainAxisAlignment: MainAxisAlignment.start,
                      mainAxisSize: MainAxisSize.max,
                      children: [
                        MyProductImage(product: product),
                        const SizedBox(width: AppSize.smallSize),
                        Expanded(
                          child: Column(
                            mainAxisAlignment: MainAxisAlignment.start,
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Text(
                                "ETB ${product.price}",
                                style: Theme.of(context)
                                    .textTheme
                                    .titleMedium!
                                    .copyWith(
                                      color:
                                          Theme.of(context).colorScheme.primary,
                                    ),
                              ),
                              Text(
                                product.title,
                                softWrap: true,
                                style: const TextStyle(
                                  fontSize: 18,
                                  fontWeight: FontWeight.w700,
                                ),
                              ),
                              Text(
                                timeago.format(product.createdAt),
                                style: Theme.of(context)
                                    .textTheme
                                    .bodyMedium!
                                    .copyWith(
                                        color: Theme.of(context)
                                            .colorScheme
                                            .secondary),
                              ),
                              const SizedBox(height: AppSize.smallSize),
                              Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  Text(
                                    product.description.length > 50
                                        ? '${product.description.substring(0, 50)}...'
                                        : product.description,
                                    textAlign: TextAlign.justify,
                                    style: Theme.of(context)
                                        .textTheme
                                        .bodyMedium!
                                        .copyWith(
                                          color: Theme.of(context)
                                              .colorScheme
                                              .secondary,
                                        ),
                                  ),
                                  const SizedBox(height: AppSize.xSmallSize),
                                  InkWell(
                                    onTap: () {},
                                    child: Container(
                                      padding: const EdgeInsets.symmetric(
                                          horizontal: AppSize.xSmallSize,
                                          vertical: AppSize.xxSmallSize),
                                      decoration: BoxDecoration(
                                        color: Theme.of(context)
                                            .colorScheme
                                            .primary
                                            .withOpacity(0.1),
                                        borderRadius: BorderRadius.circular(
                                            AppSize.xxSmallSize),
                                      ),
                                      child: Text(
                                        'View more',
                                        style: TextStyle(
                                          color: Theme.of(context)
                                              .colorScheme
                                              .secondary,
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
                        onTap: () {},
                        child: Text("Edit Product",
                            style: Theme.of(context)
                                .textTheme
                                .bodyMedium!
                                .copyWith(
                                    color: Theme.of(context)
                                        .colorScheme
                                        .primary))),
                    Row(
                      children: [
                        GestureDetector(
                          onTap: () {
                            context.read<ShopBloc>().add(
                                  DeleteProductEvent(
                                      productId: product.id,
                                      token: context
                                              .read<UserBloc>()
                                              .state
                                              .user
                                              ?.token ??
                                          ''),
                                );
                            Navigator.pop(context);
                          },
                          child: Container(
                            alignment: Alignment.center,
                            padding: const EdgeInsets.symmetric(
                                horizontal: AppSize.smallSize,
                                vertical: AppSize.xSmallSize),
                            margin:
                                const EdgeInsets.only(right: AppSize.smallSize),
                            decoration: BoxDecoration(
                              color: Theme.of(context).colorScheme.primary,
                              borderRadius:
                                  BorderRadius.circular(AppSize.xxSmallSize),
                            ),
                            child: Text(
                              "Delete",
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
                        GestureDetector(
                          onTap: () {},
                          child: Container(
                            alignment: Alignment.center,
                            padding: const EdgeInsets.symmetric(
                                horizontal: AppSize.smallSize,
                                vertical: AppSize.xSmallSize),
                            margin:
                                const EdgeInsets.only(right: AppSize.smallSize),
                            decoration: BoxDecoration(
                              color: Theme.of(context).colorScheme.onSurface,
                              borderRadius:
                                  BorderRadius.circular(AppSize.xxSmallSize),
                            ),
                            child: Text(
                              "Archieve",
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
        );
      },
    );
  }
}
