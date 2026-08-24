extends Node2D

enum direction { left, right }

@export var spawn_pos: Vector2
@export var spawn_direction: direction
@export var row_length: int
@export var spacing: int

# need a list of the ships that are spawned, so that they can be counted
var type1list: Array

# preload new ships types
var type1: PackedScene = preload("res://performance-tests/scenes/new_base_test_ship.tscn")

func spawn_ships() -> void:
	var t1new = $Type1Input.value - type1list.size()
	if (t1new <= 0):
		t1new = $Type1Input.value
		for ship in type1list:
			ship.queue_free()
		type1list.clear()
	
	for i in t1new:
		var ship = type1.instantiate()
		get_parent().add_child(ship)
		@warning_ignore("integer_division")
		var rows = type1list.size() / row_length
		var xscalar = rows * spacing
		if (spawn_direction == direction.left):
			xscalar = -xscalar
		var xpos = spawn_pos.x + xscalar
		var ypos = spawn_pos.y + ((type1list.size() % row_length) + 1) * spacing
		ship.global_position = Vector2(xpos, ypos)
		type1list.append(ship)
