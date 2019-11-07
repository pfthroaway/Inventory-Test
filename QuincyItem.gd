extends Control

var drag = false

var orphanage: Control

func _ready() -> void:
	orphanage = get_tree().current_scene.find_node("Orphanage")

func _on_TextureRect_gui_input(event: InputEvent) -> void:
	if event is InputEventMouseButton and event.pressed and event.button_index == 1:
		if not drag:
			if orphanage.get_child_count() == 0:
				drag = true
				self.mouse_filter = Control.MOUSE_FILTER_IGNORE
				get_parent().remove_child(self)
				orphanage.add_child(self)

func _process(delta: float) -> void:
	if drag:
		#move it one pixel below the click collision area to click other things
		rect_global_position = get_viewport().get_mouse_position() + Vector2(1, 1)



