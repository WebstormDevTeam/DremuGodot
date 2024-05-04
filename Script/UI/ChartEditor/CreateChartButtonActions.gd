extends Control


@export var PathLabel:Label
@export var MusicStream:AudioStream
@export var MusicPlayer:AudioStreamPlayer2D

@export var _MusicName:LineEdit
@export var _ChartName:LineEdit
@export var _ArtistName:LineEdit
@export var _DefaultBPM:LineEdit

var _Path

var _SystemPath

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
#	file_dialog.add_filter("*.wav")——见下
	print(OS.get_environment("USER"))
	# 
	if OS.get_name()=="Windows":
		if OS.has_environment("USERNAME"):
			var username = OS.get_environment("USERNAME")
			_SystemPath = "C:\\Users\\"+username+"\\"
			file_dialog.set_current_dir(_SystemPath+"Music")
			# 如果'USERNAME'不存在，尝试'USER'环境变量（通常在Linux和macOS上使用）
		elif OS.has_environment("USER"):
			var username = OS.get_environment("USER")
			_SystemPath = "C:\\Users\\"+username+"\\"
			file_dialog.set_current_dir(_SystemPath+"Music")
	elif OS.get_name()=="macOS":
		if OS.has_environment("USERNAME"):
			var username = OS.get_environment("USERNAME")
			_SystemPath = "/Users/"+username+"/"
			file_dialog.set_current_dir(_SystemPath+"Music")
			# 如果'USERNAME'不存在，尝试'USER'环境变量（通常在Linux和macOS上使用）
		elif OS.has_environment("USER"):
			var username = OS.get_environment("USER")
			_SystemPath = "/Users/"+username+"/"
			file_dialog.set_current_dir(_SystemPath+"Music")
		
	
	file_dialog.connect("file_selected", Callable(self, "_music_file_selected"))
	add_child(file_dialog)
	file_dialog.popup_centered(Vector2(1000,900))


func _music_file_selected(path):
	PathLabel.text = path
	print("Selected music file: " + path)
	var file_type_chars = path.right(4)
	#播放音频
	var MusicFile = FileAccess.open(path,FileAccess.READ)
	var stream
	if(file_type_chars==".mp3"):
		stream = AudioStreamMP3.new()
#	会将wav文件头读取为音频文件，因此需要修改
#	elif(file_type_chars==".wav"):
#		stream = AudioStreamWAV.new()
	print(file_type_chars)
	stream.data = MusicFile.get_buffer(MusicFile.get_length())
	MusicPlayer.stream = stream
	MusicPlayer.play()
	
	


func _on_create_button_down():
#	var yaml = preload("res://godot-yaml-Windows_x64/gdyaml.gdns").new() TODO:如何用插件转换为YAML
	var File = FileAccess.open("D:\\GodotTester\\CodeWTF.txt",FileAccess.WRITE)
	File.store_string(_MusicName.text+'\n')
	File.store_string(_ChartName.text+'\n')
	File.store_string(_ArtistName.text+'\n')
	File.store_string(_DefaultBPM.text+'\n')
	File.close()
