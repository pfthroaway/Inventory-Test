extends Control

var orphanage: Control

func _ready() -> void:
	orphanage = get_tree().current_scene.find_node("Orphanage")

func _on_TextureRect_gui_input(event: InputEvent) -> void:
	if event is InputEventMouseButton and event.pressed and event.button_index == 1:
#		var has_quincy_node = self.has_node("QuincyItem")
#		if orphanage.get_child_count() > 0 and has_quincy_node:
#			var orphan_item: Control = orphanage.get_child(0)
#			orphan_item.mouse_filter = Control.MOUSE_FILTER_PASS
#			orphan_item.drag = false
#			orphan_item.rect_global_position = Vector2.ZERO
#			orphanage.remove_child(orphan_item)
#			var quincy_item = self.get_node("QuincyItem")
#			self.remove_child(quincy_item)
#			orphanage.add_child(quincy_item)
#			self.add_child(orphan_item)
#			print("swapped!")
		if orphanage.get_child_count() > 0:
			var item: Control = orphanage.get_child(0)
			item.mouse_filter = Control.MOUSE_FILTER_PASS
			item.drag = false
			item.rect_global_position = Vector2.ZERO
			orphanage.remove_child(item)
			add_child(item)

