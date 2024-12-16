using UnityEngine;
using Valve.VR;

public class SteamVRControllerInput : MonoBehaviour
{
    // �� SteamVR �������� SteamVR �����༭���д����Ķ�����
    public SteamVR_Action_Boolean triggerPressAction;

    // ������Դ�����ֻ�����
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.Any;

    // ���ƵĶ��������ɫ��
    public Transform cameraTransform;  // �������
    public Transform sceneRoot;        // ��Ҫ��ת�ĳ���������
    public float rotationSpeed = 30f;  // ��ת�ٶ�
    public KeyCode rotatekey = KeyCode.R;
    private bool isRotating = false;
    // �ƶ��ٶ�

    public float moveSpeed = 10f;

    void Update()
    {
        Debug.Log("zhixing");

        if (triggerPressAction.stateDown | Input.GetKeyDown(rotatekey)) // ��ⰴ������
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
        // ��ȡ���Y��
        Vector3 cameraYAxis = cameraTransform.up;

        // �����Y����ת����
        sceneRoot.RotateAround(cameraTransform.position, cameraYAxis, rotationSpeed * Time.deltaTime);
    }
}

