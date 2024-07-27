import 'package:flutter/material.dart';
import 'package:share_plus/share_plus.dart';
import 'package:style_hub/core/utils/captilizations.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/product_entity.dart';
import 'package:style_hub/features/SytleHub/presentation/widgets/common/app_bar_one.dart';
import 'package:style_hub/setUp/size/app_size.dart';
import 'package:url_launcher/url_launcher.dart';

import '../widgets/common/show_image.dart';

class ProductDetail extends StatefulWidget {
  const ProductDetail({super.key, required this.product});
  final ProductEntity product;

  @override
  State<ProductDetail> createState() => _ProductDetailState();
}

class _ProductDetailState extends State<ProductDetail> {
  TextEditingController searchController = TextEditingController();
  int selectedIndex = 0;
  @override
  Widget build(BuildContext context) {
    Widget builderDetails(String title, Widget value) {
      return Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            title,
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.bold,
              color: Theme.of(context).colorScheme.onSurface,
            ),
          ),
          const SizedBox(height: AppSize.xSmallSize),
          value,
        ],
      );
    }

    Future<void> makePhoneCall(String phoneNumber) async {
      final Uri launchUri = Uri(
        scheme: 'tel',
        path: phoneNumber,
      );
      await launchUrl(launchUri);
    }

    void shareContent(String content) {
      Share.share(content);
    }

    void changeIndex(int index) {
      setState(() {
        selectedIndex = index;
      });
    }

    return SafeArea(
      child: Scaffold(
        backgroundColor: Theme.of(context).colorScheme.onPrimary,
        body: SingleChildScrollView(
          child: Column(
            children: [
              const AppBarOne(),
              GestureDetector(
                onHorizontalDragEnd: (details) {
                  if (details.primaryVelocity != null &&
                      details.primaryVelocity! > 0) {
                    if (selectedIndex > 0) {
                      changeIndex(selectedIndex - 1);
                    }
                  } else if (details.primaryVelocity != null &&
                      details.primaryVelocity! < 0) {
                    if (selectedIndex < widget.product.images.length - 1) {
                      changeIndex(selectedIndex + 1);
                    }
                  }
                },
                child: ConstrainedBox(
                  constraints: const BoxConstraints(minWidth: 300),
                  child: Container(
                    color: Theme.of(context).colorScheme.primaryContainer,
                    child: Stack(
                      children: [
                        ShowImage(
                            image:
                                widget.product.images[selectedIndex].imageUri),
                        Positioned(
                          top: 10,
                          right: 12,
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
                              style: Theme.of(context)
                                  .textTheme
                                  .bodySmall!
                                  .copyWith(
                                    color:
                                        Theme.of(context).colorScheme.onPrimary,
                                  ),
                            ),
                          ),
                        ),
                        if (widget.product.images.length > 1)
                          Positioned(
                            bottom: 12,
                            left: 0,
                            right: 0,
                            child: Center(
                              child: Row(
                                mainAxisAlignment: MainAxisAlignment.center,
                                children: [
                                  for (int index = 0;
                                      index < widget.product.images.length;
                                      index++)
                                    GestureDetector(
                                      onTap: () {
                                        changeIndex(index);
                                      },
                                      child: Container(
                                        width: selectedIndex == index ? 32 : 24,
                                        height:
                                            selectedIndex == index ? 32 : 24,
                                        margin: const EdgeInsets.only(
                                          left: AppSize.smallSize,
                                        ),
                                        padding: const EdgeInsets.all(
                                            AppSize.xxSmallSize),
                                        decoration: BoxDecoration(
                                          color: Theme.of(context)
                                              .colorScheme
                                              .onPrimary,
                                          borderRadius: const BorderRadius.all(
                                            Radius.circular(100),
                                          ),
                                        ),
                                        child: Image.network(
                                          widget.product.images[index].imageUri,
                                          fit: BoxFit.cover,
                                          width: double.infinity,
                                        ),
                                      ),
                                    ),
                                ],
                              ),
                            ),
                          ),
                      ],
                    ),
                  ),
                ),
              ),
              Padding(
                padding: const EdgeInsets.all(AppSize.smallSize),
                child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            Captilizations.capitalize(widget.product.title),
                            style: TextStyle(
                              fontSize: 18,
                              fontWeight: FontWeight.bold,
                              color: Theme.of(context).colorScheme.onSurface,
                            ),
                          ),
                          Text(
                            'ETB ${widget.product.price}',
                            style: Theme.of(context)
                                .textTheme
                                .titleMedium!
                                .copyWith(
                                    color:
                                        Theme.of(context).colorScheme.primary),
                          ),
                          Text(
                            '${Captilizations.capitalize(widget.product.shopInfo.city)} | ${Captilizations.capitalize(widget.product.shopInfo.state)}',
                            style: Theme.of(context)
                                .textTheme
                                .bodyMedium!
                                .copyWith(
                                  color:
                                      Theme.of(context).colorScheme.secondary,
                                ),
                          ),
                        ],
                      ),
                      const SizedBox(height: AppSize.smallSize),
                      Row(
                        children: [
                          // share
                          GestureDetector(
                            child: Container(
                              width: 60,
                              height: 60,
                              decoration: BoxDecoration(
                                border: Border.all(
                                  color: Theme.of(context)
                                      .colorScheme
                                      .primaryContainer,
                                ),
                                color: Theme.of(context)
                                    .colorScheme
                                    .primaryContainer,
                                borderRadius: const BorderRadius.all(
                                  Radius.circular(100),
                                ),
                              ),
                              child: Icon(
                                widget.product.isFavorite
                                    ? Icons.favorite
                                    : Icons.favorite_border,
                                color: widget.product.isFavorite
                                    ? Theme.of(context).colorScheme.error
                                    : Theme.of(context).colorScheme.secondary,
                              ),
                            ),
                          ),
                          const SizedBox(width: AppSize.smallSize),
                          // share
                          GestureDetector(
                            onTap: () => shareContent(widget.product.title),
                            child: Container(
                              width: 60,
                              height: 60,
                              decoration: BoxDecoration(
                                border: Border.all(
                                  color: Theme.of(context)
                                      .colorScheme
                                      .primaryContainer,
                                ),
                                color: Theme.of(context)
                                    .colorScheme
                                    .primaryContainer,
                                borderRadius: const BorderRadius.all(
                                  Radius.circular(100),
                                ),
                              ),
                              child: const Icon(
                                Icons.share,
                                color: Colors.blue,
                              ),
                            ),
                          ),
                          const SizedBox(width: AppSize.smallSize),
                          // call
                          GestureDetector(
                            onTap: () => makePhoneCall(
                                widget.product.shopInfo.phoneNumber),
                            child: Container(
                              width: 60,
                              height: 60,
                              decoration: BoxDecoration(
                                border: Border.all(
                                  color: Theme.of(context)
                                      .colorScheme
                                      .primaryContainer,
                                ),
                                color: Theme.of(context)
                                    .colorScheme
                                    .primaryContainer,
                                borderRadius: const BorderRadius.all(
                                  Radius.circular(100),
                                ),
                              ),
                              child: const Icon(
                                Icons.call,
                                color: Colors.green,
                              ),
                            ),
                          ),
                          // message
                          const SizedBox(width: AppSize.smallSize),
                          GestureDetector(
                            child: Container(
                              width: 60,
                              height: 60,
                              decoration: BoxDecoration(
                                border: Border.all(
                                  color: Theme.of(context)
                                      .colorScheme
                                      .primaryContainer,
                                ),
                                color: Theme.of(context)
                                    .colorScheme
                                    .primaryContainer,
                                borderRadius: const BorderRadius.all(
                                  Radius.circular(100),
                                ),
                              ),
                              child: const Icon(
                                Icons.message,
                                color: Colors.purple,
                              ),
                            ),
                          ),
                        ],
                      ),
                      const SizedBox(height: AppSize.smallSize),
                      builderDetails(
                        'Description',
                        Text(
                          Captilizations.capitalize(widget.product.description),
                          textAlign: TextAlign.justify,
                          style: Theme.of(context)
                              .textTheme
                              .bodyMedium!
                              .copyWith(
                                  color:
                                      Theme.of(context).colorScheme.secondary),
                        ),
                      ),
                      const SizedBox(height: AppSize.smallSize),
                      if (widget.product.colors.isNotEmpty)
                        builderDetails(
                          'Color',
                          Wrap(
                            spacing: AppSize.mediumSize,
                            runSpacing: AppSize.smallSize,
                            children: [
                              for (int index = 0;
                                  index < widget.product.colors.length;
                                  index++)
                                Column(
                                  children: [
                                    Container(
                                      width: 24,
                                      height: 24,
                                      decoration: BoxDecoration(
                                        color: Color(int.parse(
                                          "FF${widget.product.colors[index].hexCode.substring(1)}",
                                          radix: 16,
                                        )),
                                        borderRadius: BorderRadius.circular(12),
                                      ),
                                    ),
                                    const SizedBox(height: AppSize.xxSmallSize),
                                    Text(
                                      Captilizations.capitalize(
                                          widget.product.colors[index].name),
                                      style: Theme.of(context)
                                          .textTheme
                                          .bodyMedium!
                                          .copyWith(
                                              color: Theme.of(context)
                                                  .colorScheme
                                                  .secondary),
                                    ),
                                  ],
                                ),
                            ],
                          ),
                        ),
                      if (widget.product.colors.isNotEmpty)
                        const SizedBox(height: AppSize.smallSize),
                      if (widget.product.sizes.isNotEmpty)
                        builderDetails(
                          'Size',
                          Wrap(
                            spacing: AppSize.mediumSize,
                            runSpacing: AppSize.smallSize,
                            children: [
                              for (int index = 0;
                                  index < widget.product.sizes.length;
                                  index++)
                                Container(
                                  width: 40,
                                  height: 40,
                                  decoration: BoxDecoration(
                                    border: Border.all(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .primaryContainer,
                                    ),
                                    color: Theme.of(context)
                                        .colorScheme
                                        .primaryContainer,
                                    borderRadius: const BorderRadius.all(
                                      Radius.circular(100),
                                    ),
                                  ),
                                  child: Center(
                                    child: Text(
                                      widget.product.sizes[index].abbreviation
                                          .toUpperCase(),
                                      style: Theme.of(context)
                                          .textTheme
                                          .bodyMedium!
                                          .copyWith(
                                              color: Theme.of(context)
                                                  .colorScheme
                                                  .secondary),
                                    ),
                                  ),
                                ),
                            ],
                          ),
                        ),
                      if (widget.product.sizes.isNotEmpty)
                        const SizedBox(height: AppSize.smallSize),
                      if (widget.product.brands.isNotEmpty)
                        builderDetails(
                          'Brand',
                          Wrap(
                            spacing: AppSize.mediumSize,
                            runSpacing: AppSize.smallSize,
                            children: [
                              for (int index = 0;
                                  index < widget.product.brands.length;
                                  index++)
                                Container(
                                  padding: const EdgeInsets.symmetric(
                                    horizontal: AppSize.xSmallSize,
                                    vertical: AppSize.xxSmallSize,
                                  ),
                                  decoration: BoxDecoration(
                                    border: Border.all(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .primaryContainer,
                                    ),
                                    color: Theme.of(context)
                                        .colorScheme
                                        .primaryContainer,
                                    borderRadius: const BorderRadius.all(
                                      Radius.circular(AppSize.xxSmallSize),
                                    ),
                                  ),
                                  child: Text(
                                    Captilizations.capitalize(
                                        widget.product.brands[index].name),
                                    style: Theme.of(context)
                                        .textTheme
                                        .bodyMedium!
                                        .copyWith(
                                            color: Theme.of(context)
                                                .colorScheme
                                                .secondary),
                                  ),
                                ),
                            ],
                          ),
                        ),
                      if (widget.product.brands.isNotEmpty)
                        const SizedBox(height: AppSize.smallSize),
                      if (widget.product.designs.isNotEmpty)
                        builderDetails(
                          'Design',
                          Wrap(
                            spacing: AppSize.mediumSize,
                            runSpacing: AppSize.smallSize,
                            children: [
                              for (int index = 0;
                                  index < widget.product.designs.length;
                                  index++)
                                Container(
                                  padding: const EdgeInsets.symmetric(
                                    horizontal: AppSize.xSmallSize,
                                    vertical: AppSize.xxSmallSize,
                                  ),
                                  decoration: BoxDecoration(
                                    border: Border.all(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .primaryContainer,
                                    ),
                                    color: Theme.of(context)
                                        .colorScheme
                                        .primaryContainer,
                                    borderRadius: const BorderRadius.all(
                                      Radius.circular(AppSize.xxSmallSize),
                                    ),
                                  ),
                                  child: Text(
                                    Captilizations.capitalize(
                                        widget.product.designs[index].name),
                                    style: Theme.of(context)
                                        .textTheme
                                        .bodyMedium!
                                        .copyWith(
                                            color: Theme.of(context)
                                                .colorScheme
                                                .secondary),
                                  ),
                                ),
                            ],
                          ),
                        ),
                      if (widget.product.designs.isNotEmpty)
                        const SizedBox(height: AppSize.smallSize),
                      if (widget.product.materials.isNotEmpty)
                        builderDetails(
                          'Material',
                          Wrap(
                            spacing: AppSize.mediumSize,
                            runSpacing: AppSize.smallSize,
                            children: [
                              for (int index = 0;
                                  index < widget.product.materials.length;
                                  index++)
                                Container(
                                  padding: const EdgeInsets.symmetric(
                                    horizontal: AppSize.xSmallSize,
                                    vertical: AppSize.xxSmallSize,
                                  ),
                                  decoration: BoxDecoration(
                                    border: Border.all(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .primaryContainer,
                                    ),
                                    color: Theme.of(context)
                                        .colorScheme
                                        .primaryContainer,
                                    borderRadius: const BorderRadius.all(
                                      Radius.circular(AppSize.xxSmallSize),
                                    ),
                                  ),
                                  child: Text(
                                    Captilizations.capitalize(
                                        widget.product.materials[index].name),
                                    style: Theme.of(context)
                                        .textTheme
                                        .bodyMedium!
                                        .copyWith(
                                            color: Theme.of(context)
                                                .colorScheme
                                                .secondary),
                                  ),
                                ),
                            ],
                          ),
                        ),
                      if (widget.product.materials.isNotEmpty)
                        const SizedBox(height: AppSize.smallSize),
                      Container(
                        padding: const EdgeInsets.all(
                          AppSize.xSmallSize,
                        ),
                        decoration: BoxDecoration(
                          border: Border.all(
                            color:
                                Theme.of(context).colorScheme.primaryContainer,
                          ),
                          color: Theme.of(context).colorScheme.primaryContainer,
                          borderRadius: const BorderRadius.all(
                              Radius.circular(AppSize.xxSmallSize)),
                        ),
                        child: Row(
                          mainAxisAlignment: MainAxisAlignment.spaceBetween,
                          children: [
                            Row(
                              crossAxisAlignment: CrossAxisAlignment.center,
                              children: [
                                SizedBox(
                                  width: 40,
                                  height: 40,
                                  child: CircleAvatar(
                                    backgroundColor: Theme.of(context)
                                        .colorScheme
                                        .primaryContainer,
                                    radius: 20,
                                    backgroundImage: NetworkImage(
                                      widget.product.shopInfo.logo,
                                    ),
                                  ),
                                ),
                                const SizedBox(width: 12),
                                Column(
                                  crossAxisAlignment: CrossAxisAlignment.start,
                                  children: [
                                    Text(
                                      Captilizations.capitalize(
                                          widget.product.shopInfo.name),
                                      style: Theme.of(context)
                                          .textTheme
                                          .titleSmall!
                                          .copyWith(
                                            color: Theme.of(context)
                                                .colorScheme
                                                .onSurface,
                                          ),
                                    ),
                                    Text(
                                      Captilizations.capitalize(
                                          widget.product.shopInfo.city),
                                      style: Theme.of(context)
                                          .textTheme
                                          .bodySmall!
                                          .copyWith(
                                            color: Theme.of(context)
                                                .colorScheme
                                                .secondary,
                                          ),
                                    ),
                                  ],
                                ),
                              ],
                            ),
                            ElevatedButton.icon(
                              onPressed: () {},
                              style: ElevatedButton.styleFrom(
                                backgroundColor:
                                    Theme.of(context).colorScheme.primary,
                                foregroundColor:
                                    Theme.of(context).colorScheme.onPrimary,
                                shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(20),
                                ),
                                elevation: 2,
                                padding: const EdgeInsets.symmetric(
                                    horizontal: 12, vertical: 8),
                              ),
                              icon: const Icon(Icons.storefront, size: 20),
                              label: Text(
                                'View Shop',
                                style: Theme.of(context)
                                    .textTheme
                                    .titleSmall!
                                    .copyWith(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .onPrimary,
                                    ),
                              ),
                            ),
                          ],
                        ),
                      )
                    ]),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
