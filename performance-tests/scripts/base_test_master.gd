class_name BaseTestMaster
extends Node2D

var team1: Array[BaseTestShip] = []
var team2: Array[BaseTestShip] = []

signal start_battle
signal end_battle

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var i = 0
	var all_ships := get_parent().get_children()
	for ship in all_ships:
		# skip self or anything not a ship
		if (ship == self):
			continue
		# setup teams
		if (i%2 > 0):
			ship.team = 1
			team1.append(ship)
		else:
			ship.team = 2
			team2.append(ship)
		ship.die.connect(ship_die)
		i += 1

func get_opposing(team: int) -> Array[BaseTestShip]:
	if (team == 1):
		return team2
	else:
		return team1

func _unhandled_key_input(event: InputEvent) -> void:
	if (event is InputEventKey):
		if (event.keycode == KEY_SPACE):
			start_battle.emit()

func ship_die(dead_ship: BaseTestShip, ship_team: int) -> void:
	if (ship_team == 1):
		team1.remove_at(team1.find(dead_ship))
	else:
		team2.remove_at(team2.find(dead_ship))

func _process(delta: float) -> void:
	if (team1.is_empty() || team2.is_empty()):
		end_battle.emit()
