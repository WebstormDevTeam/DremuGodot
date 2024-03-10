extends TouchScreenButton

func _input(event):
	if event is InputEventScreenTouch:
		if event.pressed:
			#print("touch")
			pass



# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _on_pressed():
	print("Touched")
