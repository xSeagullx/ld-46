using UnityEngine;

namespace Sources.Interfaces {
public enum DirectionalInput {
  NONE,
  LEFT,
  RIGHT
};

public interface InputAccessor {
  
  Vector2 GetMousePosition();
  DirectionalInput GetDirectionalInput();
}
}