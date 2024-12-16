using UnityEngine;
using UnityEngine.InputSystem;

public class testbutton: MonoBehaviour
{
    public InputActionProperty buttonPressAction; // 对应按钮输入

    void Start()
    {
        buttonPressAction.action.Enable();
    }

    void Update()
    {
        // 检测 ButtonPress 动作
        if (buttonPressAction.action.WasPressedThisFrame())
        {
            Debug.Log("ButtonPress 动作触发！");
        }
    }

    private void OnDestroy()
    {
        buttonPressAction.action.Disable();
    }
}
