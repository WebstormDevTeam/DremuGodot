extends Control

var yamlDotNet = load("res://Script/UniLib/yamlExtension.cs")

@export var PathLabel:Label
@export var MusicStream:AudioStream
@export var MusicPlayer:AudioStreamPlayer2D

@export var _MusicName:LineEdit
@export var _ChartName:LineEdit
@export var _ArtistName:LineEdit
@export var _DefaultBPM:LineEdit

var _Path
var _SystemPath
var ChartData

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
	else:
		printerr("错误的文件类型")
#	会将wav文件头读取为音频文件，因此需要修改
#	elif(file_type_chars==".wav"):
#		stream = AudioStreamWAV.new()
	print(file_type_chars)
	stream.data = MusicFile.get_buffer(MusicFile.get_length())
	MusicPlayer.stream = stream
	MusicPlayer.play()
	


func _save_file(path):
	print(path)
	yamlDotNet.Test()
	ChartData = {
		"MusicName":_MusicName.text,
		"ChartName":_ChartName.text,
		"ArtistName":_ArtistName.text,
		"DefaultBPM":_DefaultBPM.text
	}
	var yamlString = yamlDotNet.ConvertJsonToYaml(JSON.stringify(ChartData))
	print(yamlString)
	# 打开文件，保存转换后的yaml字符串
	var File = FileAccess.open(path,FileAccess.WRITE)
	File.store_string(yamlString)
	File.close()
	queue_free()	#关闭窗口

func _on_create_button_down():
#打开保存文件的对话框
	var file_dialog = FileDialog.new()
	file_dialog.set_access(FileDialog.ACCESS_FILESYSTEM)
	file_dialog.file_mode = FileDialog.FILE_MODE_SAVE_FILE
	file_dialog.set_title("Save Chart File")
	file_dialog.add_filter("*.dcc")

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
		
	
	file_dialog.connect("file_selected", Callable(self, "_save_file"))
	add_child(file_dialog)
	file_dialog.popup_centered(Vector2(1000,900))


func _on_cancel_button_down():
	queue_free()
