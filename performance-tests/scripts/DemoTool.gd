extends Node

@export var NumberInput: SpinBox
@export var StartButton: Button
@export var SimRunner: Node

func button_pressed() -> void:
	SimRunner.StartSim(NumberInput.value)
	StartButton.queue_free()
	NumberInput.queue_free()

func _unhandled_key_input(event: InputEvent) -> void:
	if event is InputEventKey:
		if event.pressed and event.keycode == KEY_ESCAPE:
			get_tree().reload_current_scene()
