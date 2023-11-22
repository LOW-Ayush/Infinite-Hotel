extends Node2D

@export
var moveSpeed: float
var moveDistance: Vector2
var coverEnabled: bool

# Called when the node enters the scene tree for the first time.
func _ready():
	moveSpeed = 2
	coverEnabled = false
	moveDistance = Vector2(10,10)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _input(event):
	while event.is_action_pressed("Forward"):
		translate(Vector2(10,-10))
		return
		
	if event.is_action_pressed("Back"):
		print("back")
	
	if event.is_action_pressed("Left"):
		print("Left")
	
	if event.is_action_pressed("Right"):
		print("Right")
	
	if event.is_action_pressed("Cover"):
		coverEnabled = true
		
	if event.is_action_pressed("Reload"):
		print("Reload")
