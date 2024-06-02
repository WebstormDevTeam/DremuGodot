extends Control

var yamlDotNet = load("res://Script/UniLib/yamlExtension.cs")

@export var CreateDialogPrefeb:PackedScene
@export var beatMap:VBoxContainer
@export var audioPlayer:AudioStreamPlayer

var codeEdit:CodeEdit
var codePreFixes = ["\\"]
var _Path
var _SystemPath
var ChartData
var n = 4
var BeatLines = []
var SongLong
# Called when the node enters the scene tree for the first time.
func _ready():
	var pop = $Panel/MenuBar/MenuButton.get_popup()
	pop.connect("id_pressed",Callable(self,"_on_menu_item_pressed"))
	codeEdit = $Panel/CodeEdit
	codeEdit.code_completion_prefixes = codePreFixes
	codeEdit.add_code_completion_option(CodeEdit.KIND_VARIABLE,"Chart","Chart: ")
	
	add_beat_line()
	print(EVarible.bpm)
	pass

func _on_menu_item_pressed(id):
	match id:
		0:
			var dialog = CreateDialogPrefeb.instantiate()
			dialog.z_index = 20
			$Panel.add_child(dialog)
		1:
			_open_file()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _on_open():
	print("opened")


func add_beat_line():
	beatMap.add_theme_constant_override("separation",200/n)
	connect("NewOrOpenFile",Callable(self,"_on_open"))
	#beatMap.add_spacer(true)
	for i in range(10):
		var line = ColorRect.new()
		line.custom_minimum_size = Vector2(200,2)#添加节拍线
		#四个节拍线把节拍线的颜色改变
		if i%4 == 0:
			line.color = Color(0.34,0.36,0.39)
			print(line.color)
		beatMap.add_child(line)
		BeatLines.append(line)
	pass

func _open_file():
	var file_dialog = FileDialog.new()
	file_dialog.set_access(FileDialog.ACCESS_FILESYSTEM)
	file_dialog.file_mode = FileDialog.FILE_MODE_OPEN_FILE
	file_dialog.set_title("Open Chart File")
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
		
	
	file_dialog.connect("file_selected", Callable(self, "_open_chart_file"))
	add_child(file_dialog)
	file_dialog.popup_centered(Vector2(1000,900))
	EVarible.IsNewOrCreatedChart = true

func _open_chart_file(path):
	_Path = path
	var File = FileAccess.open(path,FileAccess.READ_WRITE)
	ChartData = JSON.parse_string(yamlDotNet.ConvertYamlToJson(File.get_as_text()))
	EVarible.bpm = int(ChartData["DefaultBPM"])
	print(ChartData)
	
	pass

func _on_code_text_Changed():
	pass


func _on_code_edit_code_completion_requested():
	
	pass # Replace with function body.
