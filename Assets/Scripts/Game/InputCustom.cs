using UnityEngine;
using UnityEngine.InputSystem;

public class InputCustom : MonoBehaviour
{
    private Vector2 positionOfMouse;
    private bool isClicked;
    
    public void OnMoveMouse(InputAction.CallbackContext context)
    {
        positionOfMouse = context.ReadValue<Vector2>();
    }
    
    public void OnClick(InputAction.CallbackContext context)
    {
        isClicked = context.ReadValueAsButton();
    }
    
    public Vector2 GetPositionOfMouse()
    {
        return positionOfMouse;
    }
    public bool IsClicked()
    {
        var alter = false;
        if (isClicked)
        {
            isClicked = false;
            alter = true;
        }
        return alter;
    }
}