class_name Leader extends Node2D

@export var calc_delay: float
@export var followers: Array[Follower]

var _delay_counter: float = 0.0
var _fighting = false

# find cells and send to followers
func _process(delta: float) -> void:
	if (!_fighting): return
	_delay_counter += delta
	if (_delay_counter >= calc_delay):
		_delay_counter = 0.0
		var cells := [GridMaster.get_random(), GridMaster.get_random(), GridMaster.get_random()]
		
		for follower in followers:
			follower.receive_orders(cells)

func get_team(team_ships: Array) -> void:
	@warning_ignore("integer_division")
	var block_amount = team_ships.size() / followers.size()
	var start = 0
	var end = block_amount
	for follower in followers:
		follower.receive_followers(team_ships.slice(start, end))
		start += block_amount
		end += block_amount

func start_battle() -> void:
	_fighting = true
