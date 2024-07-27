import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/svg.dart';
import 'package:persistent_bottom_nav_bar/persistent_tab_view.dart';
import 'package:style_hub/features/SytleHub/presentation/pages/my_shop.dart';
import 'package:style_hub/setUp/size/app_size.dart';

import '../../bloc/shop/shop_bloc.dart';

class AppBarOne extends StatelessWidget {
  const AppBarOne({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(AppSize.smallSize),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          GestureDetector(
            onTap: () => Navigator.pop(context),
            child: Icon(
              Icons.arrow_back_outlined,
              size: 32,
              color: Theme.of(context).colorScheme.onSurface,
            ),
          ),
          const Spacer(),
          Row(
            children: [
              ElevatedButton.icon(
                onPressed: () {
                  if (context.read<ShopBloc>().state.myShopId != null) {
                    PersistentNavBarNavigator.pushNewScreenWithRouteSettings(
                      context,
                      settings: const RouteSettings(name: '/shop/review'),
                      withNavBar: false,
                      screen: const MyShopScreen(),
                      pageTransitionAnimation: PageTransitionAnimation.fade,
                    );
                  }
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: Theme.of(context).colorScheme.primary,
                  foregroundColor: Theme.of(context).colorScheme.onPrimary,
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(20),
                  ),
                  elevation: 1,
                  padding:
                      const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
                ),
                icon: const Icon(Icons.storefront, size: 20),
                label: Text(
                  'My Shop',
                  style: Theme.of(context).textTheme.titleSmall!.copyWith(
                        color: Theme.of(context).colorScheme.onPrimary,
                      ),
                ),
              ),
              const SizedBox(width: AppSize.smallSize),
              SvgPicture.asset(
                "assets/icons/notifaction.svg",
                height: 32,
              ),
            ],
          ),
        ],
      ),
    );
  }
}
