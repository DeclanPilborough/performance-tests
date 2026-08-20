class_name BaseTestShip
extends Node2D

var team: int = 0
var health: float = 500
var damage: float = 10
var speed: int = 10
var engagement_distance: int = 100

var master: BaseTestMaster
var opponent: BaseTestShip
var isFighting: bool = false
var isDead: bool = false

signal die(BaseTestShip, int)

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	master = get_parent().get_node("Master")
	master.start_battle.connect(start_battle)
	master.end_battle.connect(delete_self)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if (isFighting && opponent != null):
		if (global_position.distance_squared_to(opponent.global_position) < engagement_distance):
			fire(delta)
		else:
			var newX = move_toward(global_position.x, opponent.global_position.x, speed*delta)
			var newY = move_toward(global_position.y, opponent.global_position.y, speed*delta)
			var newPos: Vector2 = Vector2(newX, newY)
			global_position = newPos
	elif (opponent == null):
		opponent = find_opponent()

# start the battle
func start_battle() -> void:
	isFighting = true
	opponent = find_opponent()

func fire(mult: float) -> void:
	opponent.take_damage(damage * mult)

func take_damage(damage: float) -> void:
	health -= damage
	if (health <= 0 && !isDead):
		isFighting = false
		isDead = true;
		die.emit(self, team)
		self.queue_free()

func delete_self() -> void:
	self.queue_free()

func find_opponent() -> BaseTestShip:
	var opposing_team = master.get_opposing(team)
	var newOpponent: BaseTestShip = opposing_team.pick_random() if (opposing_team.size() > 0) else null
	return newOpponent
