class_name Grid extends Node2D

@export var row_count: int
@export var coloumn_count: int
@export var cell_size = 32

var cells: Dictionary[String, GridCell]
var rng = RandomNumberGenerator.new()

func _ready() -> void:
	cells = {}
	var coloumn_current = 0
	var row_current = 0
	for i in coloumn_count:
		row_current = 0
		for j in row_count:
			var cell_key: String = String.num_int64(coloumn_current) + "/" + String.num_int64(row_current)
			cells[cell_key] = GridCell.new()
			cells[cell_key].pos = Vector2(coloumn_current, row_current)
			cells[cell_key].world_pos = Vector2(coloumn_current * cell_size, row_current * cell_size)
			row_current += 1
		coloumn_current += 1

func get_cell_from_position(pos: Vector2) -> GridCell:
	@warning_ignore("integer_division")
	var x = int(pos.x) / cell_size
	@warning_ignore("integer_division")
	var y = int(pos.y) / cell_size
	var key: String = String.num_int64(x) + "/" + String.num_int64(y)
	return cells[key]

func ship_move(ship: NewBaseTestShip) -> void:
	var cell = get_cell_from_position(ship.global_position)
	if (ship.current_grid_cell != cell):
		ship.current_grid_cell.remove_ship(ship)
		cell.add_ship(ship)
		ship.current_grid_cell = cell

func get_surrounding(ship: NewBaseTestShip) -> Array[NewBaseTestShip]:
	var surrounds: Array[NewBaseTestShip] = ship.current_grid_cell.get_ships()
	for cell in get_neighbouring_cells(ship.current_grid_cell):
		surrounds.append_array(cell.get_ships())
	return surrounds

func get_neighbouring_cells(cell: GridCell) -> Array[GridCell]:
	var keys: String
	var array: Array[GridCell]
	
	keys = String.num_int64(int(cell.pos.x)) + "/" + String.num_int64(int(cell.pos.y - 1))
	if (cells.has(keys)):
		array.append(cells[keys])
	
	keys = String.num_int64(int(cell.pos.x)) + "/" + String.num_int64(int(cell.pos.y + 1))
	if (cells.has(keys)):
		array.append(cells[keys])
	
	keys = String.num_int64(int(cell.pos.x - 1)) + "/" + String.num_int64(int(cell.pos.y))
	if (cells.has(keys)):
		array.append(cells[keys])
	
	keys = String.num_int64(int(cell.pos.x + 1)) + "/" + String.num_int64(int(cell.pos.y))
	if (cells.has(keys)):
		array.append(cells[keys])
	
	return array

func get_random() -> GridCell:
	@warning_ignore("integer_division")
	var coloumn = rng.randi() % coloumn_count
	@warning_ignore("integer_division")
	var row = rng.randi() % row_count
	var key: String = String.num_int64(coloumn) + "/" + String.num_int64(row)
	return cells[key]
