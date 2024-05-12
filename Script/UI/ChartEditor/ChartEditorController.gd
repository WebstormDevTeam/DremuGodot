extends Control

@export var CreateDialogPrefeb:PackedScene

# Called when the node enters the scene tree for the first time.
func _ready():
	var pop = $Panel/MenuBar/MenuButton.get_popup()
	pop.connect("id_pressed",Callable(self,"_on_menu_item_pressed"))
	pass

func _on_menu_item_pressed(id):
	match id:
		0:
			var dialog = CreateDialogPrefeb.instantiate()
			$Panel.add_child(dialog)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
