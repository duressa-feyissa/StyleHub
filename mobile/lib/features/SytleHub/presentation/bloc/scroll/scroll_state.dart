part of 'scroll_bloc.dart';

class ScrollState extends Equatable {
  final bool isVisible;

  const ScrollState({this.isVisible = true});

  @override
  List<Object?> get props => [isVisible];

  ScrollState copyWith({bool? isVisible}) {
    return ScrollState(
      isVisible: isVisible ?? this.isVisible,
    );
  }
}
