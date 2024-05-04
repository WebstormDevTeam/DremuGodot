extends Control


@export var PathLabel:Label
@export var MusicStream:AudioStream
@export var MusicPlayer:AudioStreamPlayer2D


var _Path


# Called when the node enters the scene tree for the first time.
func _ready():
	pass


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _open_music_file():
	print("OpenFile")
	var file_dialog = FileDialog.new()
	file_dialog.set_access(FileDialog.ACCESS_FILESYSTEM)
	file_dialog.set_title("Open Music File")
	file_dialog.add_filter("*.mp3")
	print(OS.get_environment("USER"))
	# 
	if OS.get_name()=="Window":
		if OS.has_environment("USERNAME"):
			var username = OS.get_environment("USERNAME")
			file_dialog.set_current_dir("C:/Users/"+username+"/Music")
			# 如果'USERNAME'不存在，尝试'USER'环境变量（通常在Linux和macOS上使用）
		elif OS.has_environment("USER"):
			var username = OS.get_environment("USER")
			file_dialog.set_current_dir("C:/Users/"+username+"/Music")
	elif OS.get_name()=="macOS":
		if OS.has_environment("USERNAME"):
			var username = OS.get_environment("USERNAME")
			file_dialog.set_current_dir("/Users/"+username+"/Music")
			# 如果'USERNAME'不存在，尝试'USER'环境变量（通常在Linux和macOS上使用）
		elif OS.has_environment("USER"):
			var username = OS.get_environment("USER")
			file_dialog.set_current_dir("/Users/"+username+"/Music")
		
	
	file_dialog.connect("file_selected", Callable(self, "_music_file_selected"))
	add_child(file_dialog)
	file_dialog.popup_centered(Vector2(1000,900))
	


func _music_file_selected(path):
	PathLabel.text = path
	print("Selected music file: " + path)
	#播放音频
	var MusicFile = FileAccess.open(path,FileAccess.READ)
	var stream = AudioStreamMP3.new()
	stream.data = MusicFile.get_buffer(MusicFile.get_length())
	MusicPlayer.stream = stream
	MusicPlayer.play()
	
	


func _on_create_button_down():
	pass # Replace with function body.
