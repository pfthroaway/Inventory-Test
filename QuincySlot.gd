extends Control

var orphanage: Control

func _ready() -> void:
	orphanage = get_tree().current_scene.find_node("Orphanage")

func _on_TextureRect_gui_input(event: InputEvent) -> void:
	if event is InputEventMouseButton and event.pressed and event.button_index == 1:
		if orphanage.get_child_count() > 0:
			var item: Control = orphanage.get_child(0)
			item.mouse_filter = Control.MOUSE_FILTER_PASS
			item.drag = false
			item.rect_global_position = Vector2.ZERO
			orphanage.remove_child(item)
			add_child(item)
