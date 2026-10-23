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
	for i in cells:
		set_neighbour_cells(i)

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
	var surrounds: Array = ship.current_grid_cell.get_ships().values()
	for cell in ship.current_grid_cell.neighbours.values():
		surrounds.append_array(cell.get_ships().values())
	return surrounds

func get_random() -> GridCell:
	@warning_ignore("integer_division")
	var coloumn = rng.randi() % coloumn_count
	@warning_ignore("integer_division")
	var row = rng.randi() % row_count
	var key: String = String.num_int64(coloumn) + "/" + String.num_int64(row)
	return cells[key]

func set_neighbour_cells(cell_key: String) -> void:
	var c_pos = cells[cell_key].pos
	var string: String
	var x: int
	var y: int
	
	# top left
	x = c_pos.x - 1
	y = c_pos.y - 1
	string = String.num_int64(x) + "/" + String.num_int64(y)
	if (cells.has(string)):
		cells[cell_key].neighbours[GridCell.cell_neighbour.TopLeft] = cells[string]
	# top
	x = c_pos.x
	string = String.num_int64(x) + "/" + String.num_int64(y)
	if (cells.has(string)):
		cells[cell_key].neighbours[GridCell.cell_neighbour.Top] = cells[string]
	# top right
	x = c_pos.x + 1
	string = String.num_int64(x) + "/" + String.num_int64(y)
	if (cells.has(string)):
		cells[cell_key].neighbours[GridCell.cell_neighbour.TopRight] = cells[string]
	# left
	x = c_pos.x - 1
	y = c_pos.y
	string = String.num_int64(x) + "/" + String.num_int64(y)
	if (cells.has(string)):
		cells[cell_key].neighbours[GridCell.cell_neighbour.Left] = cells[string]
	# right
	x = c_pos.x + 1
	string = String.num_int64(x) + "/" + String.num_int64(y)
	if (cells.has(string)):
		cells[cell_key].neighbours[GridCell.cell_neighbour.Right] = cells[string]
	# bottom left
	x = c_pos.x - 1
	y = c_pos.y + 1
	string = String.num_int64(x) + "/" + String.num_int64(y)
	if (cells.has(string)):
		cells[cell_key].neighbours[GridCell.cell_neighbour.BottomLeft] = cells[string]
	# bottom
	x = c_pos.x
	string = String.num_int64(x) + "/" + String.num_int64(y)
	if (cells.has(string)):
		cells[cell_key].neighbours[GridCell.cell_neighbour.Bottom] = cells[string]
	# bottom right
	x = c_pos.x + 1
	string = String.num_int64(x) + "/" + String.num_int64(y)
	if (cells.has(string)):
		cells[cell_key].neighbours[GridCell.cell_neighbour.BottomRight] = cells[string]
