using UnityEngine;
using Valve.VR;

public class CameraMoveOnTrigger : MonoBehaviour
{
    public SteamVR_Action_Boolean triggerPressAction;  // 绑定触发器的 SteamVR 动作
    public SteamVR_Input_Sources handType;             // 指定手柄（左手或右手）
    public Transform cameraRoot;                       // CameraRoot 的 Transform
    public float moveSpeed = 2f;                       // 移动速度

    void Update()
    {
        if (triggerPressAction.GetState(handType))
        {
            // 获取 CameraRoot 的前进方向
            Vector3 forwardDirection = new Vector3(cameraRoot.forward.x, 0, cameraRoot.forward.z).normalized;

            // 根据前进方向和速度更新 CameraRoot 的位置
            cameraRoot.position += forwardDirection * moveSpeed * Time.deltaTime;

            Debug.Log("CameraRoot is moving forward!");
        }
    }
}
