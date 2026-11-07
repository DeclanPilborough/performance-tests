class_name GridCell extends RefCounted

var ships: Dictionary
var pos: Vector2
var world_pos: Vector2
enum cell_neighbour { 
	TopLeft, 
	Top, 
	TopRight, 
	Left, 
	Right, 
	BottomLeft, 
	Bottom, 
	BottomRight }
var neighbours: Dictionary[cell_neighbour, GridCell]

func get_ships() -> Dictionary:
	return ships

func add_ship(ship: NewBaseTestShip) -> void:
	ships[ship.to_string()] = ship
	ship.current_grid_cell = self

func remove_ship(ship: NewBaseTestShip) -> void:
	ships.erase(ship.to_string())
