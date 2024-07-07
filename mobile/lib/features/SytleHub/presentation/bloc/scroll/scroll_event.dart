part of 'scroll_bloc.dart';

@immutable
sealed class ScrollEvent {}

class ScrollEventInitial extends ScrollEvent {}

class ToggleVisibilityEvent extends ScrollEvent {
  final bool isVisible;

  ToggleVisibilityEvent({required this.isVisible});
}