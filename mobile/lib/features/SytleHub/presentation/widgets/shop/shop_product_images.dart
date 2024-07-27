import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:style_hub/features/SytleHub/presentation/widgets/common/show_image.dart';
import 'package:style_hub/setUp/size/app_size.dart';

import '../../bloc/shop/shop_bloc.dart';

class ShopProductImages extends StatelessWidget {
  const ShopProductImages({super.key, required this.shopId});
  final String shopId;

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

    void showFullSizeImage(BuildContext context, String imageUri) {
      showDialog(
        context: context,
        useSafeArea: true,
        builder: (BuildContext context) {
          return Dialog(
            backgroundColor: Colors.transparent,
            insetPadding: const EdgeInsets.all(AppSize.smallSize),
            child: Stack(
              children: [
                Center(
                  child: Container(
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(AppSize.smallSize),
                    ),
                    child: ClipRRect(
                      borderRadius: BorderRadius.circular(AppSize.smallSize),
                      child: Image.network(imageUri),
                    ),
                  ),
                ),
                Positioned(
                  top: 0,
                  right: 0,
                  child: IconButton(
                    icon: Container(
                      padding: const EdgeInsets.all(5),
                      decoration: BoxDecoration(
                        color: Colors.black.withOpacity(0.5),
                        borderRadius: BorderRadius.circular(50),
                      ),
                      child: Icon(
                        Icons.close,
                        color: Theme.of(context).colorScheme.primaryContainer,
                        size: 30,
                      ),
                    ),
                    onPressed: () {
                      Navigator.of(context).pop();
                    },
                  ),
                ),
              ],
            ),
          );
        },
      );
    }

    return Wrap(
      spacing: AppSize.smallSize,
      runSpacing: AppSize.smallSize,
      children: context
          .watch<ShopBloc>()
          .state
          .shops[shopId]!
          .images
          .map(
            (e) => GestureDetector(
              onTap: () {
                showFullSizeImage(context, e.imageUri);
              },
              child: ConstrainedBox(
                constraints: BoxConstraints(
                  maxWidth: width,
                  maxHeight: width,
                ),
                child: Container(
                  width: double.infinity,
                  height: double.infinity,
                  color: Theme.of(context).colorScheme.primaryContainer,
                  child: ShowImage(image: e.imageUri),
                ),
              ),
            ),
          )
          .toList(),
    );
  }
}
