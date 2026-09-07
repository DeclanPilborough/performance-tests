class_name NewBaseTestShip
extends Follower

@export var speed: int
@export var damage: int
@export var health: int
@export var engagement_distance: int

var current_grid_cell: GridCell
var spot_to_attack := 0
var attack_position
var attack = false
var target: NewBaseTestShip
var team: int

func _ready() -> void:
	current_grid_cell = GridMaster.get_cell_from_position(global_position)
	current_grid_cell.add_ship(self)

func _process(delta: float) -> void:
	if (attack):
		var pos = attack_position.world_pos
		global_position.x = move_toward(global_position.x, pos.x, delta * speed)
		global_position.y = move_toward(global_position.y, pos.y, delta * speed)
		GridMaster.ship_move(self)
		
		if (target == null && global_position.distance_squared_to(pos) < engagement_distance):
			for ship in GridMaster.get_surrounding(self):
				if (ship.team != team):
					target = ship
					break
		elif (target != null):
			target.health -= damage
	if (health <= 0):
		queue_free()

@warning_ignore("shadowed_variable_base_class")
func receive_orders(cells: Array) -> void:
	attack_position = cells[spot_to_attack]

@warning_ignore("unused_parameter")
func receive_followers(new_followers: Array) -> void:
	pass
