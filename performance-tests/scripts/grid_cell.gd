class_name GridCell extends RefCounted

var ships: Array[NewBaseTestShip]
var pos: Vector2
var world_pos: Vector2

func get_ships() -> Array[NewBaseTestShip]:
	return ships

func add_ship(ship: NewBaseTestShip) -> void:
	ships.append(ship)
	ship.current_grid_cell = self

func remove_ship(ship: NewBaseTestShip) -> void:
	ships.remove_at(ships.find(ship))
