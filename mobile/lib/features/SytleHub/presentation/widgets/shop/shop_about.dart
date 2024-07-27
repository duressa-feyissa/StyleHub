import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:share_plus/share_plus.dart';

import '../../../../../core/utils/captilizations.dart';
import '../../../../../setUp/size/app_size.dart';
import '../../../domain/entities/shop/working_hour_entity.dart';
import '../../bloc/shop/shop_bloc.dart';

class ShopAbout extends StatelessWidget {
  final String shopId;
  const ShopAbout({super.key, required this.shopId});

  @override
  Widget build(BuildContext context) {
    void shareContent(String content) {
      Share.share(content);
    }

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

    Widget buildSocialMedia(Map<String, String> socialMediaLinks) {
      Map<String, String> socialMediaLinksList = {
        'facebook': 'assets/icons/facebook.svg',
        'instagram': 'assets/icons/instagram.svg',
        'linkedin': 'assets/icons/linkedin.svg',
        'telegram': 'assets/icons/telegram.svg',
        'tiktok': 'assets/icons/tiktok.svg',
        'whatsapp': 'assets/icons/whatsapp.svg',
        'youtube': 'assets/icons/youtube.svg',
      };
      return Wrap(
        spacing: AppSize.xSmallSize,
        runSpacing: AppSize.xSmallSize,
        children: socialMediaLinks.entries.map((entry) {
          return GestureDetector(
            onTap: () => shareContent(entry.value),
            child: Container(
              padding: const EdgeInsets.all(AppSize.smallSize),
              decoration: BoxDecoration(
                color: Theme.of(context).colorScheme.primaryContainer,
                borderRadius: BorderRadius.circular(AppSize.xSmallSize),
              ),
              child: SvgPicture.asset(
                socialMediaLinksList[entry.key]!,
                height: 24.0,
                width: 24.0,
              ),
            ),
          );
        }).toList(),
      );
    }

    Widget buildWorkingHours(List<WorkingHourEntity> workingHours) {
      List<String> daysOfWeek = [
        'sunday',
        'monday',
        'tuesday',
        'wednesday',
        'thursday',
        'friday',
        'saturday'
      ];

      Map<String, WorkingHourEntity?> hoursMap = {
        for (var day in daysOfWeek) day: null,
      };
      for (var hour in workingHours) {
        hoursMap[hour.day.toLowerCase()] = hour;
      }

      List<WorkingHourEntity?> availableHours = workingHours
          .where((hour) => daysOfWeek.contains(hour.day.toLowerCase()))
          .toList();

      return Column(
        children: availableHours.map((hour) {
          IconData timeIcon;
          String timeText = '';

          switch (hour!.time.toLowerCase()) {
            case 'morning':
              timeIcon = Icons.wb_sunny;
              timeText = Captilizations.capitalize(hour.time);
              break;
            case 'afternoon':
              timeIcon = Icons.wb_cloudy;
              timeText = Captilizations.capitalize(hour.time);
              break;
            case 'evening':
              timeIcon = Icons.nightlight_round;
              timeText = Captilizations.capitalize(hour.time);
              break;
            case 'all day':
              timeIcon = Icons.access_time;
              timeText = 'All day';
              break;
            default:
              timeIcon = Icons.access_time;
              timeText = Captilizations.capitalize(hour.time);
          }

          return Container(
            padding: const EdgeInsets.all(AppSize.smallSize),
            margin: const EdgeInsets.only(bottom: AppSize.xSmallSize),
            decoration: BoxDecoration(
              color: Theme.of(context).colorScheme.primaryContainer,
              borderRadius: BorderRadius.circular(AppSize.xSmallSize),
            ),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Row(
                  children: [
                    Icon(
                      timeIcon,
                      color: Theme.of(context).colorScheme.secondary,
                    ),
                    const SizedBox(width: AppSize.smallSize),
                    Text(
                      Captilizations.capitalize(hour.day),
                      style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                            color: Theme.of(context).colorScheme.onSurface,
                          ),
                    ),
                  ],
                ),
                Text(
                  timeText,
                  style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                        color: Theme.of(context).colorScheme.secondary,
                      ),
                ),
              ],
            ),
          );
        }).toList(),
      );
    }

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        builderDetails(
          'Description',
          Text(
            Captilizations.capitalize(
                context.watch<ShopBloc>().state.shops[shopId]!.description),
            textAlign: TextAlign.justify,
            style: Theme.of(context)
                .textTheme
                .bodyMedium!
                .copyWith(color: Theme.of(context).colorScheme.secondary),
          ),
        ),
        const SizedBox(height: AppSize.mediumSize),
        if (context
            .watch<ShopBloc>()
            .state
            .shops[shopId]!
            .workingHours
            .isNotEmpty)
          builderDetails(
            'Working Hours',
            buildWorkingHours(
                context.watch<ShopBloc>().state.shops[shopId]!.workingHours),
          ),
        const SizedBox(height: AppSize.mediumSize),
        if (context
            .watch<ShopBloc>()
            .state
            .shops[shopId]!
            .socialMediaLinks
            .isNotEmpty)
          builderDetails(
            'Social Medias',
            buildSocialMedia(context
                .watch<ShopBloc>()
                .state
                .shops[shopId]!
                .socialMediaLinks),
          ),
      ],
    );
  }
}
