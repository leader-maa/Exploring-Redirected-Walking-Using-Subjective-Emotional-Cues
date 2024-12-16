using UnityEngine;
using Valve.VR;

public class CameraMoveOnTrigger : MonoBehaviour
{
    public SteamVR_Action_Boolean triggerPressAction;  // �󶨴������� SteamVR ����
    public SteamVR_Input_Sources handType;             // ָ���ֱ������ֻ����֣�
    public Transform cameraRoot;                       // CameraRoot �� Transform
    public float moveSpeed = 2f;                       // �ƶ��ٶ�

    void Update()
    {
        if (triggerPressAction.GetState(handType))
        {
            // ��ȡ CameraRoot ��ǰ������
            Vector3 forwardDirection = new Vector3(cameraRoot.forward.x, 0, cameraRoot.forward.z).normalized;

            // ����ǰ��������ٶȸ��� CameraRoot ��λ��
            cameraRoot.position += forwardDirection * moveSpeed * Time.deltaTime;

            Debug.Log("CameraRoot is moving forward!");
        }
    }
}
