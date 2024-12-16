using UnityEngine;
using Valve.VR;

public class SteamVRControllerInput : MonoBehaviour
{
    // 绑定 SteamVR 动作（在 SteamVR 动作编辑器中创建的动作）
    public SteamVR_Action_Boolean triggerPressAction;

    // 输入来源：左手或右手
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.Any;

    // 控制的对象（例如角色）
    public Transform cameraTransform;  // 相机对象
    public Transform sceneRoot;        // 需要旋转的场景根对象
    public float rotationSpeed = 30f;  // 旋转速度
    public KeyCode rotatekey = KeyCode.R;
    private bool isRotating = false;
    // 移动速度

    public float moveSpeed = 10f;

    void Update()
    {
        Debug.Log("zhixing");

        if (triggerPressAction.stateDown | Input.GetKeyDown(rotatekey)) // 检测按键按下
            {
                isRotating = !isRotating;
                Debug.Log("Trigger Press Detected!");
            }
        if (isRotating && sceneRoot != null && cameraTransform != null)
        {
            RotateScene();
        }

        
        
    }
    void RotateScene()
    {
        // 获取相机Y轴
        Vector3 cameraYAxis = cameraTransform.up;

        // 绕相机Y轴旋转场景
        sceneRoot.RotateAround(cameraTransform.position, cameraYAxis, rotationSpeed * Time.deltaTime);
    }
}

