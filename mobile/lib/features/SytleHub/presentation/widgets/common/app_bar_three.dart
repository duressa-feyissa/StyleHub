import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:persistent_bottom_nav_bar/persistent_tab_view.dart';
import 'package:style_hub/features/SytleHub/presentation/pages/add_product.dart';
import 'package:style_hub/setUp/size/app_size.dart';

import '../../bloc/shop/shop_bloc.dart';
import '../../bloc/user/user_bloc.dart';

class AppBarThree extends StatelessWidget {
  const AppBarThree({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    if (context.watch<UserBloc>().state.user != null) {}
    return Padding(
      padding: const EdgeInsets.all(AppSize.smallSize),
      child: Row(
        children: [
          Row(
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              GestureDetector(
                onTap: () => Navigator.pop(context),
                child: Icon(
                  Icons.arrow_back_outlined,
                  size: 32,
                  color: Theme.of(context).colorScheme.onSurface,
                ),
              ),
              const SizedBox(width: AppSize.smallSize),
              CircleAvatar(
                backgroundImage: NetworkImage(
                  context
                      .watch<ShopBloc>()
                      .state
                      .shops[context.read<ShopBloc>().state.myShopId]!
                      .logo,
                ) as ImageProvider,
                radius: 22.5,
              ),
              const SizedBox(width: AppSize.smallSize),
              Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisAlignment: MainAxisAlignment.start,
                children: [
                  Text(
                    context
                        .watch<ShopBloc>()
                        .state
                        .shops[context.read<ShopBloc>().state.myShopId]!
                        .name,
                    style: Theme.of(context).textTheme.titleMedium!.copyWith(
                          color: Theme.of(context).colorScheme.onSurface,
                        ),
                  ),
                  Text(
                    "${context.watch<ShopBloc>().state.shops[context.read<ShopBloc>().state.myShopId]!.city}, ${context.watch<ShopBloc>().state.shops[context.read<ShopBloc>().state.myShopId]!.country}",
                    style: Theme.of(context).textTheme.bodySmall!.copyWith(
                          color: Theme.of(context).colorScheme.secondary,
                        ),
                  ),
                ],
              ),
            ],
          ),
          const Spacer(),
          GestureDetector(
            onTap: () {
              PersistentNavBarNavigator.pushNewScreenWithRouteSettings(
                context,
                settings: const RouteSettings(name: '/product/add'),
                withNavBar: false,
                screen: AddProductScreen(
                    shopId: context.read<ShopBloc>().state.myShopId ?? ""),
                pageTransitionAnimation: PageTransitionAnimation.fade,
              );
            },
            child: Container(
              alignment: Alignment.center,
              padding: const EdgeInsets.symmetric(
                horizontal: AppSize.smallSize - 2,
                vertical: AppSize.xSmallSize,
              ),
              decoration: BoxDecoration(
                color: Theme.of(context).colorScheme.primary,
                borderRadius: BorderRadius.circular(AppSize.xxSmallSize),
              ),
              child: Text(
                "New",
                style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                      color: Theme.of(context).colorScheme.onPrimary,
                    ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
