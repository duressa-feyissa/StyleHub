import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/product_entity.dart';

import '../../../../../setUp/size/app_size.dart';
import '../../bloc/shop/shop_bloc.dart';
import '../common/product.dart';

class ShopProductList extends StatelessWidget {
  final String shopId;
  const ShopProductList({super.key, required this.shopId});
  @override
  Widget build(BuildContext context) {
    List<ProductEntity> products =
        context.watch<ShopBloc>().state.shops[shopId]?.products ?? [];

    if (context.read<ShopBloc>().state.shopProductImageStatus ==
        ShopProductImageStatus.loading) {
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

    return Wrap(
        spacing: AppSize.smallSize,
        runSpacing: AppSize.smallSize,
        children: products
            .map((product) => Product(
                  product: product,
                ))
            .toList());
  }
}
