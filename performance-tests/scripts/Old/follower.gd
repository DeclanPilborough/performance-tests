@abstract class_name Follower extends Node2D
@abstract func receive_orders(cells: Array) -> void
@abstract func receive_followers(new_followers: Array) -> void

var followers: Array
var cells: Array[GridCell]
