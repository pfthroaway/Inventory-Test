extends Control

var drag = false

var orphanage: Control

var parent_slot

export (Texture) var texture

func _ready() -> void:
	$TextureRect.texture = texture
	orphanage = get_tree().current_scene.find_node("Orphanage")

func _on_TextureRect_gui_input(event: InputEvent) -> void:
	print(self)
	if event is InputEventMouseButton and event.pressed and event.button_index == 1:
		parent_slot = get_parent()
		if not drag:
			if orphanage.get_child_count() > 0:
				var orphan_item: Control = orphanage.get_child(0)
				orphan_item.mouse_filter = Control.MOUSE_FILTER_PASS
				orphan_item.drag = false
				orphan_item.rect_global_position = Vector2.ZERO
				orphanage.remove_child(orphan_item)
				self.get_parent().add_child(orphan_item)
				self.get_parent().remove_child(self)
				orphanage.add_child(self)
				self.drag = true









				print("swapped!")
			elif orphanage.get_child_count() == 0:
				drag = true
				self.mouse_filter = Control.MOUSE_FILTER_IGNORE
				get_parent().remove_child(self)
				orphanage.add_child(self)

func _process(delta: float) -> void:
	if drag:
		#move it one pixel below the click collision area to click other things
		rect_global_position = get_viewport().get_mouse_position() + Vector2(1, 1)



