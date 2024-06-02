extends Control

# 引入yamlDotNet脚本
var yamlDotNet = load("res://Script/UniLib/yamlExtension.cs")

# 导出属性
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


signal NewOrOpenFile

# 当节点第一次进入场景树时调用
func _ready():
	pass


# 每帧调用一次，'delta' 是自上一帧以来的经过时间。
func _process(delta):
	pass

# 打开音乐文件
func _open_music_file():
	print("OpenFile")
	var file_dialog = FileDialog.new()
	file_dialog.set_access(FileDialog.ACCESS_FILESYSTEM)
	file_dialog.set_title("Open Music File")
	file_dialog.add_filter("*.mp3")
	print(OS.get_environment("USER"))
	# 根据不同的操作系统设置路径
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
		
	# 当文件被选中时调用_music_file_selected函数
	file_dialog.connect("file_selected", Callable(self, "_music_file_selected"))
	add_child(file_dialog)
	file_dialog.popup_centered(Vector2(1000,900))


func _music_file_selected(path):
	# 显示选中的音乐文件路径
	PathLabel.text = str(path)
	print("Selected music file: " + path)
	var file_type_chars = path.right(4)
	# 播放音频文件
	var MusicFile = FileAccess.open(path,FileAccess.READ)
	var stream
	if(file_type_chars==".mp3"):
		stream = AudioStreamMP3.new()
	else:
		printerr("错误的文件类型")

	# 读取音频文件头并设置音频流
	stream.data = MusicFile.get_buffer(MusicFile.get_length())
	MusicPlayer.stream = stream
	EVarible.MusicStream = stream
	MusicPlayer.play()

# 保存文件
func _save_file(path):
	print(path)
	yamlDotNet.Test()
	# 构建音乐信息的字典数据
	ChartData = {
		"MusicName":_MusicName.text,
		"ChartName":_ChartName.text,
		"ArtistName":_ArtistName.text,
		"DefaultBPM":_DefaultBPM.text
	}
	# 设置默认BPM并转换字典数据为YAML字符串
	EVarible.bpm = int(_DefaultBPM.text)
	var yamlString = yamlDotNet.ConvertJsonToYaml(JSON.stringify(ChartData))
	print(yamlString)
	# 打开文件，保存转换后的yaml字符串
	var File = FileAccess.open(path,FileAccess.WRITE)
	File.store_string(yamlString)
	File.close()
	queue_free()    #关闭窗口

# 点击创建按钮时调用
func _on_create_button_down():
	# 打开保存文件的对话框
	var file_dialog = FileDialog.new()
	file_dialog.set_access(FileDialog.ACCESS_FILESYSTEM)
	file_dialog.file_mode = FileDialog.FILE_MODE_SAVE_FILE
	file_dialog.set_title("Save Chart File")
	file_dialog.add_filter("*.dcc")

	# 根据不同的操作系统设置路径
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
		
	# 当文件被选中时调用_save_file函数
	file_dialog.connect("file_selected", Callable(self, "_save_file"))
	add_child(file_dialog)
	file_dialog.popup_centered(Vector2(1000,900))
	emit_signal("NewOrOpenFile")

# 点击取消按钮时调用
func _on_cancel_button_down():
	queue_free()
