using UnityEngine;
using UnityEngine.InputSystem;

public class testbutton: MonoBehaviour
{
    public InputActionProperty buttonPressAction; // ��Ӧ��ť����

    void Start()
    {
        buttonPressAction.action.Enable();
    }

    void Update()
    {
        // ��� ButtonPress ����
        if (buttonPressAction.action.WasPressedThisFrame())
        {
            Debug.Log("ButtonPress ����������");
        }
    }

    private void OnDestroy()
    {
        buttonPressAction.action.Disable();
    }
}
