import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:meta/meta.dart';

part 'scroll_event.dart';
part 'scroll_state.dart';

class ScrollBloc extends Bloc<ScrollEvent, ScrollState> {
  ScrollBloc()
      : super(
          const ScrollState(),
        ) {
    on<ScrollEventInitial>(_onInitial);
    on<ToggleVisibilityEvent>(_onToggleVisibility);
  }

  void _onInitial(ScrollEventInitial event, Emitter<ScrollState> emit) {
    emit(state.copyWith(isVisible: true));
  }

  void _onToggleVisibility(
      ToggleVisibilityEvent event, Emitter<ScrollState> emit) {
    if (state.isVisible == event.isVisible) return;
    emit(state.copyWith(isVisible: event.isVisible));
  }
}
