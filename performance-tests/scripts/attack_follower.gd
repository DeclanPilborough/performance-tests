extends Follower

func receive_orders(cells: Array) -> void:
	var spot_to_attack = randi() % cells.size()
	for follower in followers:
		follower.receive_orders(cells)
		follower.spot_to_attack = spot_to_attack
		follower.attack = true

func receive_followers(new_followers: Array) -> void:
	followers = new_followers
