import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:image_cropper/image_cropper.dart';
import 'package:image_picker/image_picker.dart';
import 'package:style_hub/features/SytleHub/presentation/bloc/user/user_bloc.dart';
import 'package:style_hub/features/SytleHub/presentation/widgets/common/custom_input_field_product.dart';

import '../../../../setUp/size/app_size.dart';
import '../../domain/entities/product/image_entity.dart';
import '../bloc/prdoct_filter/product_filter_bloc.dart';
import '../bloc/shop/shop_bloc.dart';
import '../widgets/filter/half_brand_filter.dart';
import '../widgets/filter/half_color_filter.dart';
import '../widgets/filter/half_design_filter.dart';
import '../widgets/filter/half_material_filter.dart';
import '../widgets/filter/half_size_filter.dart';
import 'filter/brand.dart';
import 'filter/color.dart';
import 'filter/design.dart';
import 'filter/material.dart';
import 'filter/size.dart';
import 'my_image_list.dart';

enum Filters { color, material, size, brand, price, location, design, all }

enum OnSummit { publish, draft }

class AddProductScreen extends StatefulWidget {
  const AddProductScreen({super.key, required this.shopId});

  final String shopId;

  @override
  State<AddProductScreen> createState() => _AddProductScreenState();
}

class _AddProductScreenState extends State<AddProductScreen> {
  TextEditingController titleController = TextEditingController();
  TextEditingController descriptionController = TextEditingController();
  TextEditingController priceController = TextEditingController();
  TextEditingController quantityController = TextEditingController();
  TextEditingController videoUrl = TextEditingController();

  bool isPriceFixed = true;
  bool inStock = true;
  String condition = 'new';

  String titleError = '';
  String descriptionError = '';
  String priceError = '';
  String quantityError = '';
  String videoUrlError = '';

  final List<XFile> _imageFiles = [];
  final ImagePicker _picker = ImagePicker();
  final Set<ImageEntity> _imageEntities = {};

  void validateTitle(String value) {
    if (value.isEmpty) {
      setState(() {
        titleError = 'Title is required';
      });
    } else {
      setState(() {
        titleError = '';
      });
    }
  }

  void validateDescription(String value) {
    if (value.isEmpty) {
      setState(() {
        descriptionError = 'Description is required';
      });
    } else {
      setState(() {
        descriptionError = '';
      });
    }
  }

  void validatePrice(String value) {
    if (value.isEmpty) {
      setState(() {
        priceError = 'Price is required';
      });
    } else {
      setState(() {
        priceError = '';
      });
    }
  }

  void validateQuantity(String value) {
    if (value.isEmpty) {
      setState(() {
        quantityError = 'Quantity is required';
      });
    } else {
      setState(() {
        quantityError = '';
      });
    }
  }

  void validateVideoUrl(String value) {
    if (value.isNotEmpty) {
      if (value.contains('https://www.youtube.com/watch?v=')) {
        setState(() {
          videoUrl.text = 'Video Url is not valid';
        });
      }
    }
  }

  Future<void> _pickImages() async {
    final pickedFiles = await _picker.pickMultiImage();
    if (pickedFiles.isNotEmpty) {
      for (var pickedFile in pickedFiles) {
        final croppedFile = await _cropImage(pickedFile);
        if (croppedFile != null) {
          setState(() {
            _imageFiles.add(croppedFile);
          });
        }
      }
    }
  }

  Future<void> _takePicture() async {
    final pickedFile = await _picker.pickImage(source: ImageSource.camera);
    if (pickedFile != null) {
      final croppedFile = await _cropImage(pickedFile);
      if (croppedFile != null) {
        setState(() {
          _imageFiles.add(croppedFile);
        });
      }
    }
  }

  void displayBottomSheet(BuildContext context, Filters filterType) {
    showModalBottomSheet<void>(
      isScrollControlled: true,
      useSafeArea: true,
      backgroundColor: Theme.of(context).colorScheme.onPrimary,
      useRootNavigator: true,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.only(
          topLeft: Radius.circular(AppSize.xxxSmallSize),
          topRight: Radius.circular(AppSize.xxxSmallSize),
        ),
      ),
      context: context,
      builder: (BuildContext context) {
        return GestureDetector(
          onVerticalDragUpdate: (details) async {
            if (details.delta.dy < -5) {
              final data = await Navigator.push(
                context,
                PageRouteBuilder(
                  pageBuilder: (context, animation, secondaryAnimation) =>
                      filterType == Filters.color
                          ? const ColorFullFilterScreen(isAdd: true)
                          : filterType == Filters.material
                              ? const MaterialFullFilterScreen(isAdd: true)
                              : filterType == Filters.size
                                  ? const SizeFullFilterScreen(isAdd: true)
                                  : filterType == Filters.design
                                      ? const DesignFullFilterScreen(
                                          isAdd: true)
                                      : filterType == Filters.brand
                                          ? const BrandFullFilterScreen(
                                              isAdd: true)
                                          : const SizedBox(),
                  transitionsBuilder:
                      (context, animation, secondaryAnimation, child) {
                    const begin = Offset(0.0, 1.0);
                    const end = Offset.zero;
                    const curve = Curves.ease;
                    var tween = Tween(begin: begin, end: end)
                        .chain(CurveTween(curve: curve));
                    var offsetAnimation = animation.drive(tween);

                    return SlideTransition(
                      position: offsetAnimation,
                      child: child,
                    );
                  },
                  transitionDuration: const Duration(milliseconds: 300),
                ),
              );
              if (data != null && data == true) {
                Navigator.pop(context);
              }
            }
            if (details.delta.dy > 5) {
              Navigator.pop(context);
            }
          },
          child: filterType == Filters.color
              ? HalfColorFilterDisplay(isAdd: false, onTap: () {})
              : filterType == Filters.material
                  ? HalfMaterialFilterDisplay(isAdd: false, onTap: () {})
                  : filterType == Filters.size
                      ? HalfSizeFilterDisplay(isAdd: true, onTap: () {})
                      : filterType == Filters.brand
                          ? HalfBrandFilterDisplay(isAdd: false, onTap: () {})
                          : filterType == Filters.design
                              ? HalfDesignFilterDisplay(
                                  isAdd: false, onTap: () {})
                              : const SizedBox(),
        );
      },
    );
  }

  Future<XFile?> _cropImage(XFile pickedFile) async {
    final croppedFile = await ImageCropper().cropImage(
      sourcePath: pickedFile.path,
      compressFormat: ImageCompressFormat.jpg,
      compressQuality: 100,
      uiSettings: [
        AndroidUiSettings(
          toolbarTitle: 'Cropper',
          toolbarColor: Theme.of(context).colorScheme.secondary,
          toolbarWidgetColor: Colors.white,
          initAspectRatio: CropAspectRatioPreset.square,
          lockAspectRatio: false,
          aspectRatioPresets: [
            CropAspectRatioPreset.square,
          ],
        ),
        IOSUiSettings(
          title: 'Cropper',
          aspectRatioPresets: [
            CropAspectRatioPreset.square,
          ],
        ),
      ],
    );

    return croppedFile != null ? XFile(croppedFile.path) : null;
  }

  void _removeImage(int index) {
    setState(() {
      _imageFiles.removeAt(index);
    });
  }

  void _showImageSourceActionSheet(BuildContext context) {
    showModalBottomSheet(
      context: context,
      backgroundColor: Theme.of(context).colorScheme.onPrimary,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.only(
          topLeft: Radius.circular(AppSize.xxxSmallSize),
          topRight: Radius.circular(AppSize.xxxSmallSize),
        ),
      ),
      builder: (BuildContext context) {
        return SafeArea(
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
                          'Select Image Source',
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
              Wrap(
                children: <Widget>[
                  ListTile(
                    leading: const Icon(Icons.photo_library),
                    title: const Text('Gallery'),
                    onTap: () {
                      Navigator.of(context).pop();
                      _pickImages();
                    },
                  ),
                  ListTile(
                    leading: const Icon(Icons.photo_camera),
                    title: const Text('Camera'),
                    onTap: () {
                      Navigator.of(context).pop();
                      _takePicture();
                    },
                  ),
                  ListTile(
                    leading: const Icon(Icons.photo_camera_back),
                    title: const Text('Existing Images'),
                    onTap: () async {
                      Navigator.of(context).pop();
                      final Set<ImageEntity>? result =
                          await Navigator.of(context).push(
                        MaterialPageRoute<Set<ImageEntity>>(
                          builder: (context) =>
                              MyImageList(selectedImages: _imageEntities),
                        ),
                      );

                      if (result != null) {
                        setState(() {
                          _imageEntities.clear();
                          _imageEntities.addAll(result);
                        });
                      }
                    },
                  ),
                ],
              ),
            ],
          ),
        );
      },
    );
  }

  void _showFixedPriceSourceActionSheet(
      BuildContext context, bool isPriceFixedContent) {
    showModalBottomSheet(
      context: context,
      backgroundColor: Theme.of(context).colorScheme.onPrimary,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.only(
          topLeft: Radius.circular(AppSize.xxxSmallSize),
          topRight: Radius.circular(AppSize.xxxSmallSize),
        ),
      ),
      builder: (BuildContext context) {
        return SafeArea(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              // Header section with title and close button
              Container(
                padding: const EdgeInsets.only(
                  left: AppSize.smallSize,
                  bottom: AppSize.xSmallSize,
                  right: AppSize.smallSize,
                  top: AppSize.xSmallSize,
                ),
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
                    // Title
                    Expanded(
                      child: Center(
                        child: Text(
                          isPriceFixedContent
                              ? 'Is Fixed Price'
                              : 'Is Product in Stock',
                          style: const TextStyle(
                            fontSize: 18,
                            fontWeight: FontWeight.w600,
                          ),
                        ),
                      ),
                    ),
                    // Close button
                    GestureDetector(
                      onTap: () {
                        Navigator.pop(context);
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
              // Content section with options
              Container(
                padding: const EdgeInsets.only(
                  top: AppSize.xLargeSize,
                  bottom: AppSize.xLargeSize,
                ),
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
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    GestureDetector(
                      onTap: () {
                        if (isPriceFixedContent) {
                          setState(() {
                            isPriceFixed = false;
                          });
                        } else {
                          setState(() {
                            inStock = false;
                          });
                        }

                        Navigator.pop(context, true);
                      },
                      child: Container(
                        alignment: Alignment.center,
                        width: 120,
                        padding: const EdgeInsets.symmetric(
                          horizontal: AppSize.smallSize,
                          vertical: AppSize.xSmallSize,
                        ),
                        margin: const EdgeInsets.only(right: AppSize.smallSize),
                        decoration: BoxDecoration(
                          color: Theme.of(context).colorScheme.onSurface,
                          borderRadius:
                              BorderRadius.circular(AppSize.xxSmallSize),
                        ),
                        child: Text(
                          "No",
                          style: Theme.of(context)
                              .textTheme
                              .bodyMedium!
                              .copyWith(
                                color: Theme.of(context).colorScheme.onPrimary,
                              ),
                        ),
                      ),
                    ),
                    const SizedBox(width: AppSize.smallSize),
                    GestureDetector(
                      onTap: () {
                        if (isPriceFixedContent) {
                          setState(() {
                            isPriceFixed = true;
                          });
                        } else {
                          setState(() {
                            inStock = true;
                          });
                        }
                        Navigator.pop(context, true);
                      },
                      child: Container(
                        alignment: Alignment.center,
                        width: 120,
                        padding: const EdgeInsets.symmetric(
                          horizontal: AppSize.smallSize,
                          vertical: AppSize.xSmallSize,
                        ),
                        margin: const EdgeInsets.only(right: AppSize.smallSize),
                        decoration: BoxDecoration(
                          color: Theme.of(context).colorScheme.primary,
                          borderRadius:
                              BorderRadius.circular(AppSize.xxSmallSize),
                        ),
                        child: Text(
                          "Yes",
                          style: Theme.of(context)
                              .textTheme
                              .bodyMedium!
                              .copyWith(
                                color: Theme.of(context).colorScheme.onPrimary,
                              ),
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ],
          ),
        );
      },
    );
  }

  void _showCondtionActionSheet(BuildContext context) {
    showModalBottomSheet(
      context: context,
      backgroundColor: Theme.of(context).colorScheme.onPrimary,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.only(
          topLeft: Radius.circular(AppSize.xxxSmallSize),
          topRight: Radius.circular(AppSize.xxxSmallSize),
        ),
      ),
      builder: (BuildContext context) {
        return SafeArea(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              // Header section with title and close button
              Container(
                padding: const EdgeInsets.only(
                  left: AppSize.smallSize,
                  bottom: AppSize.xSmallSize,
                  right: AppSize.smallSize,
                  top: AppSize.xSmallSize,
                ),
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
                    // Title
                    const Expanded(
                      child: Center(
                        child: Text(
                          'Product Condition',
                          style: TextStyle(
                            fontSize: 18,
                            fontWeight: FontWeight.w600,
                          ),
                        ),
                      ),
                    ),
                    // Close button
                    GestureDetector(
                      onTap: () {
                        Navigator.pop(context);
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
              // Content section with options
              Container(
                padding: const EdgeInsets.only(
                  top: AppSize.xLargeSize,
                  bottom: AppSize.xLargeSize,
                ),
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
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    GestureDetector(
                      onTap: () {
                        setState(() {
                          condition = 'fairly used';
                        });
                        Navigator.pop(context, true);
                      },
                      child: Container(
                        alignment: Alignment.center,
                        padding: const EdgeInsets.symmetric(
                          horizontal: AppSize.smallSize,
                          vertical: AppSize.xSmallSize,
                        ),
                        margin: const EdgeInsets.only(right: AppSize.smallSize),
                        decoration: BoxDecoration(
                          color: Theme.of(context).colorScheme.primary,
                          borderRadius:
                              BorderRadius.circular(AppSize.xxSmallSize),
                        ),
                        child: Text(
                          "Fairly Used",
                          style: Theme.of(context)
                              .textTheme
                              .bodyMedium!
                              .copyWith(
                                color: Theme.of(context).colorScheme.onPrimary,
                              ),
                        ),
                      ),
                    ),
                    const SizedBox(width: AppSize.xSmallSize),
                    GestureDetector(
                      onTap: () {
                        setState(() {
                          condition = 'used';
                        });
                        Navigator.pop(context, true);
                      },
                      child: Container(
                        alignment: Alignment.center,
                        padding: const EdgeInsets.symmetric(
                          horizontal: AppSize.smallSize,
                          vertical: AppSize.xSmallSize,
                        ),
                        margin: const EdgeInsets.only(right: AppSize.smallSize),
                        decoration: BoxDecoration(
                          color: Theme.of(context).colorScheme.onSurface,
                          borderRadius:
                              BorderRadius.circular(AppSize.xxSmallSize),
                        ),
                        child: Text(
                          "Used",
                          style: Theme.of(context)
                              .textTheme
                              .bodyMedium!
                              .copyWith(
                                color: Theme.of(context).colorScheme.onPrimary,
                              ),
                        ),
                      ),
                    ),
                    const SizedBox(width: AppSize.xSmallSize),
                    GestureDetector(
                      onTap: () {
                        setState(() {
                          condition = 'new';
                        });
                        Navigator.pop(context, true);
                      },
                      child: Container(
                        alignment: Alignment.center,
                        padding: const EdgeInsets.symmetric(
                          horizontal: AppSize.smallSize,
                          vertical: AppSize.xSmallSize,
                        ),
                        margin: const EdgeInsets.only(right: AppSize.smallSize),
                        decoration: BoxDecoration(
                          color: Theme.of(context).colorScheme.primary,
                          borderRadius:
                              BorderRadius.circular(AppSize.xxSmallSize),
                        ),
                        child: Text(
                          "New",
                          style: Theme.of(context)
                              .textTheme
                              .bodyMedium!
                              .copyWith(
                                color: Theme.of(context).colorScheme.onPrimary,
                              ),
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ],
          ),
        );
      },
    );
  }

  Widget builderDetails(String title, Widget value) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      mainAxisAlignment: MainAxisAlignment.start,
      mainAxisSize: MainAxisSize.min,
      children: [
        Text(
          title,
          style: Theme.of(context).textTheme.titleMedium!.copyWith(
                color: Theme.of(context).colorScheme.onSurface,
              ),
        ),
        const SizedBox(height: AppSize.xSmallSize),
        value,
      ],
    );
  }

  void onSubmitPress(OnSummit onSummitType) {
    context.read<ShopBloc>().add(
          AddProductEvent(
            title: titleController.text,
            description: descriptionController.text,
            price: int.parse(priceController.text),
            videoUrl: videoUrl.text,
            inStock: inStock,
            condition: condition,
            token: context.read<UserBloc>().state.user?.token ?? "",
            isFixedPrice: isPriceFixed,
            shopId: widget.shopId,
            fileImages: _imageFiles,
            status: onSummitType == OnSummit.publish ? 'active' : 'draft',
            images: _imageEntities.toList(),
            colorIds:
                context.read<ProductFilterBloc>().state.selectedColors.toList(),
            sizeIds:
                context.read<ProductFilterBloc>().state.selectedSizes.toList(),
            categoryIds: [],
            brandIds:
                context.read<ProductFilterBloc>().state.selectedBrands.toList(),
            materialIds: context
                .read<ProductFilterBloc>()
                .state
                .selectedMaterials
                .toList(),
            designIds: context
                .read<ProductFilterBloc>()
                .state
                .selectedDesigns
                .toList(),
          ),
        );
  }

  void onClear() {
    titleController.clear();
    descriptionController.clear();
    priceController.clear();
    quantityController.clear();
    videoUrl.clear();
    _imageFiles.clear();
    _imageEntities.clear();
    isPriceFixed = true;
    inStock = true;
    condition = 'new';
    context.read<ProductFilterBloc>().add(ClearAllEvent());
  }

  @override
  Widget build(BuildContext context) {
    if (context.watch<ShopBloc>().state.addProductStatus ==
        AddProductStatus.success) {
      ScaffoldMessenger.of(context).showSnackBar(const SnackBar(
        content: Center(
            child: Text('Added product successfully',
                textAlign: TextAlign.center)),
      ));
    }
    return SafeArea(
      child: Scaffold(
        backgroundColor: Theme.of(context).colorScheme.onPrimary,
        body: Column(
          children: [
            Padding(
              padding: const EdgeInsets.all(AppSize.smallSize),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  GestureDetector(
                    onTap: () => Navigator.pop(context),
                    child: Icon(
                      Icons.arrow_back_outlined,
                      size: 32,
                      color: Theme.of(context).colorScheme.onSurface,
                    ),
                  ),
                  Text(
                    "Add Product",
                    style: Theme.of(context).textTheme.titleMedium!.copyWith(
                          color: Theme.of(context).colorScheme.onSurface,
                        ),
                  ),
                  const SizedBox(width: AppSize.xLargeSize),
                ],
              ),
            ),
            Expanded(
              child: SingleChildScrollView(
                child: Column(
                  children: [
                    Container(
                      height: 2,
                      margin: const EdgeInsets.only(top: AppSize.xSmallSize),
                      color: Theme.of(context).colorScheme.primaryContainer,
                    ),
                    Padding(
                      padding: const EdgeInsets.all(AppSize.smallSize),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          builderDetails(
                            "Title",
                            CustomInputFieldProduct(
                              controller: titleController,
                              hintText: 'Write name of product',
                              errorText: titleError,
                              onChanged: validateTitle,
                            ),
                          ),
                          builderDetails(
                            "Description",
                            CustomInputFieldProduct(
                              controller: descriptionController,
                              hintText: 'Write a description',
                              errorText: descriptionError,
                              onChanged: validateDescription,
                              maxLines: 5,
                            ),
                          ),
                          Row(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Expanded(
                                child: builderDetails(
                                  "Price",
                                  CustomInputFieldProduct(
                                    controller: priceController,
                                    hintText: '1000',
                                    onChanged: validatePrice,
                                    errorText: priceError,
                                    keyboardType: TextInputType.number,
                                  ),
                                ),
                              ),
                              const SizedBox(width: AppSize.smallSize),
                              Expanded(
                                child: builderDetails(
                                  "Fixed Price",
                                  GestureDetector(
                                    onTap: () {
                                      _showFixedPriceSourceActionSheet(
                                          context, true);
                                    },
                                    child: Container(
                                      width: double.infinity,
                                      padding: const EdgeInsets.symmetric(
                                        horizontal: AppSize.smallSize,
                                        vertical: AppSize.smallSize,
                                      ),
                                      decoration: BoxDecoration(
                                        color: Theme.of(context)
                                            .colorScheme
                                            .primaryContainer,
                                        borderRadius: BorderRadius.circular(
                                            AppSize.xSmallSize),
                                      ),
                                      child: Text(
                                        isPriceFixed ? 'Yes' : 'No',
                                        style: Theme.of(context)
                                            .textTheme
                                            .titleMedium!
                                            .copyWith(
                                              color: Theme.of(context)
                                                  .colorScheme
                                                  .secondary,
                                            ),
                                      ),
                                    ),
                                  ),
                                ),
                              ),
                            ],
                          ),
                          Row(
                            children: [
                              Expanded(
                                child: builderDetails(
                                  "Conditions",
                                  GestureDetector(
                                    onTap: () {
                                      _showCondtionActionSheet(context);
                                    },
                                    child: Container(
                                      width: double.infinity,
                                      padding: const EdgeInsets.symmetric(
                                        horizontal: AppSize.smallSize,
                                        vertical: AppSize.smallSize,
                                      ),
                                      decoration: BoxDecoration(
                                        color: Theme.of(context)
                                            .colorScheme
                                            .primaryContainer,
                                        borderRadius: BorderRadius.circular(
                                            AppSize.xSmallSize),
                                      ),
                                      child: Text(
                                        condition == 'new'
                                            ? 'New'
                                            : condition == 'used'
                                                ? 'Used'
                                                : 'Fairly Used',
                                        style: Theme.of(context)
                                            .textTheme
                                            .titleMedium!
                                            .copyWith(
                                              color: Theme.of(context)
                                                  .colorScheme
                                                  .secondary,
                                            ),
                                      ),
                                    ),
                                  ),
                                ),
                              ),
                              const SizedBox(width: AppSize.smallSize),
                              Expanded(
                                child: builderDetails(
                                  "In Stock",
                                  GestureDetector(
                                    onTap: () {
                                      _showFixedPriceSourceActionSheet(
                                          context, false);
                                    },
                                    child: Container(
                                      width: double.infinity,
                                      padding: const EdgeInsets.symmetric(
                                        horizontal: AppSize.smallSize,
                                        vertical: AppSize.smallSize,
                                      ),
                                      decoration: BoxDecoration(
                                        color: Theme.of(context)
                                            .colorScheme
                                            .primaryContainer,
                                        borderRadius: BorderRadius.circular(
                                            AppSize.xSmallSize),
                                      ),
                                      child: Text(
                                        inStock ? 'Yes' : 'No',
                                        style: Theme.of(context)
                                            .textTheme
                                            .titleMedium!
                                            .copyWith(
                                              color: Theme.of(context)
                                                  .colorScheme
                                                  .secondary,
                                            ),
                                      ),
                                    ),
                                  ),
                                ),
                              ),
                            ],
                          ),
                          const SizedBox(height: AppSize.mediumSize),
                          builderDetails(
                            "Upload Images",
                            Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                GestureDetector(
                                  onTap: () =>
                                      _showImageSourceActionSheet(context),
                                  child: Container(
                                    padding: const EdgeInsets.symmetric(
                                      horizontal: AppSize.smallSize,
                                      vertical: AppSize.smallSize,
                                    ),
                                    decoration: BoxDecoration(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .primaryContainer,
                                      borderRadius: BorderRadius.circular(
                                          AppSize.xSmallSize),
                                    ),
                                    child: Row(
                                      children: [
                                        Icon(Icons.upload,
                                            color: Theme.of(context)
                                                .colorScheme
                                                .secondary),
                                        const SizedBox(
                                            width: AppSize.xSmallSize),
                                        Text("Upload Images",
                                            style: Theme.of(context)
                                                .textTheme
                                                .titleMedium!
                                                .copyWith(
                                                  color: Theme.of(context)
                                                      .colorScheme
                                                      .secondary,
                                                )),
                                      ],
                                    ),
                                  ),
                                ),
                                const SizedBox(height: AppSize.smallSize),
                                _imageFiles.isNotEmpty ||
                                        _imageEntities.isNotEmpty
                                    ? GridView.builder(
                                        shrinkWrap: true,
                                        physics:
                                            const NeverScrollableScrollPhysics(),
                                        gridDelegate:
                                            const SliverGridDelegateWithFixedCrossAxisCount(
                                          crossAxisCount: 4,
                                          crossAxisSpacing: AppSize.xSmallSize,
                                          mainAxisSpacing: AppSize.xSmallSize,
                                        ),
                                        itemCount: _imageFiles.length +
                                            _imageEntities.length,
                                        itemBuilder: (context, index) {
                                          if (index < _imageFiles.length) {
                                            return Container(
                                              decoration: BoxDecoration(
                                                color: Theme.of(context)
                                                    .colorScheme
                                                    .primaryContainer,
                                                borderRadius:
                                                    BorderRadius.circular(
                                                        AppSize.xSmallSize),
                                              ),
                                              clipBehavior: Clip.hardEdge,
                                              child: Stack(
                                                children: [
                                                  Image.file(
                                                    File(_imageFiles[index]
                                                        .path),
                                                    fit: BoxFit.cover,
                                                    width: double.infinity,
                                                    height: double.infinity,
                                                  ),
                                                  Positioned(
                                                    top: -4,
                                                    right: -4,
                                                    child: IconButton(
                                                      icon: const Icon(
                                                        Icons.cancel,
                                                        color: Colors.red,
                                                      ),
                                                      onPressed: () =>
                                                          _removeImage(index),
                                                    ),
                                                  ),
                                                ],
                                              ),
                                            );
                                          } else {
                                            if (_imageEntities.isNotEmpty) {
                                              final image = _imageEntities
                                                  .elementAt(index -
                                                      _imageFiles.length);
                                              return Container(
                                                decoration: BoxDecoration(
                                                  color: Theme.of(context)
                                                      .colorScheme
                                                      .primaryContainer,
                                                  borderRadius:
                                                      BorderRadius.circular(
                                                          AppSize.xSmallSize),
                                                ),
                                                clipBehavior: Clip.hardEdge,
                                                child: Stack(
                                                  children: [
                                                    Image.network(
                                                      _imageEntities
                                                          .elementAt(index -
                                                              _imageFiles
                                                                  .length)
                                                          .imageUri,
                                                      fit: BoxFit.cover,
                                                      width: double.infinity,
                                                      height: double.infinity,
                                                      filterQuality:
                                                          FilterQuality.low,
                                                    ),
                                                    Positioned(
                                                      top: -4,
                                                      right: -4,
                                                      child: IconButton(
                                                        icon: const Icon(
                                                          Icons.cancel,
                                                          color: Colors.red,
                                                        ),
                                                        onPressed: () {
                                                          setState(() {
                                                            _imageEntities
                                                                .remove(image);
                                                          });
                                                        },
                                                      ),
                                                    ),
                                                  ],
                                                ),
                                              );
                                            }
                                          }

                                          return const SizedBox.shrink();
                                        },
                                      )
                                    : const SizedBox.shrink(),
                              ],
                            ),
                          ),
                          const SizedBox(height: AppSize.smallSize),
                          builderDetails(
                            "Video Url (Optional)",
                            CustomInputFieldProduct(
                              controller: videoUrl,
                              hintText: 'video link',
                              onChanged: validateVideoUrl,
                              errorText: videoUrlError,
                            ),
                          ),
                          builderDetails(
                            "Brand ${context.watch<ProductFilterBloc>().state.selectedBrands.isEmpty ? '' : '(${context.watch<ProductFilterBloc>().state.selectedBrands.length})'}",
                            Column(
                              children: [
                                GestureDetector(
                                  onTap: () {
                                    displayBottomSheet(context, Filters.brand);
                                  },
                                  child: Container(
                                    padding: const EdgeInsets.symmetric(
                                      horizontal: AppSize.smallSize,
                                      vertical: AppSize.smallSize,
                                    ),
                                    decoration: BoxDecoration(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .primaryContainer,
                                      borderRadius: BorderRadius.circular(
                                          AppSize.xSmallSize),
                                    ),
                                    child: Row(
                                      children: [
                                        Icon(Icons.branding_watermark,
                                            color: Theme.of(context)
                                                .colorScheme
                                                .secondary),
                                        const SizedBox(
                                            width: AppSize.xSmallSize),
                                        Text(
                                          "Select Brand",
                                          style: Theme.of(context)
                                              .textTheme
                                              .titleMedium!
                                              .copyWith(
                                                color: Theme.of(context)
                                                    .colorScheme
                                                    .secondary,
                                              ),
                                        ),
                                      ],
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                          const SizedBox(height: AppSize.mediumSize),
                          builderDetails(
                            "Colors ${context.watch<ProductFilterBloc>().state.selectedColors.isEmpty ? '' : '(${context.watch<ProductFilterBloc>().state.selectedColors.length})'}",
                            Column(
                              children: [
                                GestureDetector(
                                  onTap: () {
                                    displayBottomSheet(context, Filters.color);
                                  },
                                  child: Container(
                                    padding: const EdgeInsets.symmetric(
                                      horizontal: AppSize.smallSize,
                                      vertical: AppSize.smallSize,
                                    ),
                                    decoration: BoxDecoration(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .primaryContainer,
                                      borderRadius: BorderRadius.circular(
                                          AppSize.xSmallSize),
                                    ),
                                    child: Row(
                                      children: [
                                        Icon(Icons.color_lens,
                                            color: Theme.of(context)
                                                .colorScheme
                                                .secondary),
                                        const SizedBox(
                                            width: AppSize.xSmallSize),
                                        Text(
                                          "Select Color",
                                          style: Theme.of(context)
                                              .textTheme
                                              .titleMedium!
                                              .copyWith(
                                                color: Theme.of(context)
                                                    .colorScheme
                                                    .secondary,
                                              ),
                                        ),
                                      ],
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                          const SizedBox(height: AppSize.mediumSize),
                          builderDetails(
                            "Designs ${context.watch<ProductFilterBloc>().state.selectedDesigns.isEmpty ? '' : '(${context.watch<ProductFilterBloc>().state.selectedDesigns.length})'}",
                            Column(
                              children: [
                                GestureDetector(
                                  onTap: () {
                                    displayBottomSheet(context, Filters.design);
                                  },
                                  child: Container(
                                    padding: const EdgeInsets.symmetric(
                                      horizontal: AppSize.smallSize,
                                      vertical: AppSize.smallSize,
                                    ),
                                    decoration: BoxDecoration(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .primaryContainer,
                                      borderRadius: BorderRadius.circular(
                                          AppSize.xSmallSize),
                                    ),
                                    child: Row(
                                      children: [
                                        Icon(Icons.design_services,
                                            color: Theme.of(context)
                                                .colorScheme
                                                .secondary),
                                        const SizedBox(
                                            width: AppSize.xSmallSize),
                                        Text(
                                          "Select Design",
                                          style: Theme.of(context)
                                              .textTheme
                                              .titleMedium!
                                              .copyWith(
                                                color: Theme.of(context)
                                                    .colorScheme
                                                    .secondary,
                                              ),
                                        ),
                                      ],
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                          const SizedBox(height: AppSize.mediumSize),
                          builderDetails(
                            "Materials ${context.watch<ProductFilterBloc>().state.selectedMaterials.isEmpty ? '' : '(${context.watch<ProductFilterBloc>().state.selectedMaterials.length})'}",
                            Column(
                              children: [
                                GestureDetector(
                                  onTap: () {
                                    displayBottomSheet(
                                        context, Filters.material);
                                  },
                                  child: Container(
                                    padding: const EdgeInsets.symmetric(
                                      horizontal: AppSize.smallSize,
                                      vertical: AppSize.smallSize,
                                    ),
                                    decoration: BoxDecoration(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .primaryContainer,
                                      borderRadius: BorderRadius.circular(
                                          AppSize.xSmallSize),
                                    ),
                                    child: Row(
                                      children: [
                                        Icon(Icons.texture,
                                            color: Theme.of(context)
                                                .colorScheme
                                                .secondary),
                                        const SizedBox(
                                            width: AppSize.xSmallSize),
                                        Text(
                                          "Select Materials",
                                          style: Theme.of(context)
                                              .textTheme
                                              .titleMedium!
                                              .copyWith(
                                                color: Theme.of(context)
                                                    .colorScheme
                                                    .secondary,
                                              ),
                                        ),
                                      ],
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                          const SizedBox(height: AppSize.mediumSize),
                          builderDetails(
                            "Sizes ${context.watch<ProductFilterBloc>().state.selectedSizes.isEmpty ? '' : '(${context.watch<ProductFilterBloc>().state.selectedSizes.length})'}",
                            Column(
                              children: [
                                GestureDetector(
                                  onTap: () {
                                    displayBottomSheet(context, Filters.size);
                                  },
                                  child: Container(
                                    padding: const EdgeInsets.symmetric(
                                      horizontal: AppSize.smallSize,
                                      vertical: AppSize.smallSize,
                                    ),
                                    decoration: BoxDecoration(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .primaryContainer,
                                      borderRadius: BorderRadius.circular(
                                          AppSize.xSmallSize),
                                    ),
                                    child: Row(
                                      children: [
                                        Icon(Icons.crop_3_2_outlined,
                                            color: Theme.of(context)
                                                .colorScheme
                                                .secondary),
                                        const SizedBox(
                                            width: AppSize.xSmallSize),
                                        Text(
                                          "Select Sizes",
                                          style: Theme.of(context)
                                              .textTheme
                                              .titleMedium!
                                              .copyWith(
                                                color: Theme.of(context)
                                                    .colorScheme
                                                    .secondary,
                                              ),
                                        ),
                                      ],
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                          const SizedBox(height: AppSize.smallSize),
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
                            onTap: onClear,
                            child: Text(
                              "Clear All",
                              style: Theme.of(context)
                                  .textTheme
                                  .bodyMedium!
                                  .copyWith(
                                    color:
                                        Theme.of(context).colorScheme.primary,
                                  ),
                            ),
                          ),
                          Row(
                            children: [
                              GestureDetector(
                                onTap: () {
                                  onSubmitPress(OnSummit.draft);
                                },
                                child: Container(
                                  alignment: Alignment.center,
                                  padding: const EdgeInsets.symmetric(
                                      horizontal: AppSize.smallSize,
                                      vertical: AppSize.xSmallSize),
                                  margin: const EdgeInsets.only(
                                      right: AppSize.smallSize),
                                  decoration: BoxDecoration(
                                    color:
                                        Theme.of(context).colorScheme.onSurface,
                                    borderRadius: BorderRadius.circular(
                                        AppSize.xxSmallSize),
                                  ),
                                  child: Text(
                                    "Draft",
                                    style: Theme.of(context)
                                        .textTheme
                                        .bodyMedium!
                                        .copyWith(
                                          color: Theme.of(context)
                                              .colorScheme
                                              .onPrimary,
                                        ),
                                  ),
                                ),
                              ),
                              const SizedBox(width: AppSize.xSmallSize),
                              GestureDetector(
                                onTap: () {
                                  onSubmitPress(OnSummit.publish);
                                  Navigator.pop(context);
                                },
                                child: Container(
                                  alignment: Alignment.center,
                                  padding: const EdgeInsets.symmetric(
                                      horizontal: AppSize.smallSize,
                                      vertical: AppSize.xSmallSize),
                                  margin: const EdgeInsets.only(
                                      right: AppSize.smallSize),
                                  decoration: BoxDecoration(
                                    color:
                                        Theme.of(context).colorScheme.primary,
                                    borderRadius: BorderRadius.circular(
                                        AppSize.xxSmallSize),
                                  ),
                                  child: Text(
                                    "Publish",
                                    style: Theme.of(context)
                                        .textTheme
                                        .bodyMedium!
                                        .copyWith(
                                          color: Theme.of(context)
                                              .colorScheme
                                              .onPrimary,
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
              ),
            ),
          ],
        ),
      ),
    );
  }
}
