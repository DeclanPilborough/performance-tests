extends Node2D

@export var spawn_pos: Vector2

# need a list of the ships that are spawned, so that they can be counted
var type1list: Array
var type2list: Array

# preload new ships types
var type1: PackedScene = preload("res://performance-tests/scenes/new_base_test_ship.tscn")
var type2: PackedScene = preload("res://performance-tests/scenes/new_base_test_ship.tscn")

func spawn_ships() -> void:
	var t1new = $Type1Input.value - type1list.size()
	if (t1new <= 0):
		t1new = $Type1Input.value
		for ship in type1list:
			ship.queue_free()
		type1list.clear()
	var t2new = $Type2Input.value - type2list.size()
	
	for i in t1new:
		var ship = type1.instantiate()
		get_parent().add_child(ship)
		var rows = floori(type1list.size() / 50)
		var xpos = rows * 10
		var ypos = ((type1list.size() % 50) + 1) * 10
		ship.global_position = Vector2(xpos, ypos)
		type1list.append(ship)
	
	for i in t2new:
		var ship = type2.instantiate()
		get_parent().add_child(ship)
		ship.global_position = spawn_pos
