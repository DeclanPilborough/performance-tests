class_name Points
extends RefCounted

enum point_types { moveto, protect, destroy }

class Point extends RefCounted:
	var point: Vector2
	var point_type: point_types
	func _init(point: Vector2, type: point_types) -> void:
		self.point = point
		self.point_type = type

var points: Array[Point]

func _init(new_points: Array[Point]) -> void:
	points = new_points
