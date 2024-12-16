using UnityEngine;
using UnityEngine.XR;

public class HMDTracking : MonoBehaviour
{
    public float realWorldRotationGain = 0.1f; // ��ת����
    public float realWorldPositionGain = 0.1f; // λ������

    private Quaternion previousHmdRotation;
    private Vector3 previousHmdPosition;

    void Start()
    {
        // ��ʼ����һ֡�� HMD λ�ú���ת
        previousHmdRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);
        previousHmdPosition = InputTracking.GetLocalPosition(XRNode.CenterEye);
    }

    void Update()
    {
        // ��ȡ��ǰ HMD ����ת��λ��
        Quaternion currentHmdRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);
        Vector3 currentHmdPosition = InputTracking.GetLocalPosition(XRNode.CenterEye);

        // ������ת��λ�õĲ�ֵ
        Quaternion rotationDelta = Quaternion.Inverse(previousHmdRotation) * currentHmdRotation;
        Vector3 positionDelta = currentHmdPosition - previousHmdPosition;

        // Ӧ������
        Quaternion gainedRotation = Quaternion.Lerp(Quaternion.identity, rotationDelta, realWorldRotationGain);
        Vector3 gainedPosition = positionDelta * realWorldPositionGain;

        // �����������ת��λ��
        transform.rotation = transform.rotation * gainedRotation;
        transform.position += gainedPosition;

        // ������һ֡�� HMD λ�ú���ת
        previousHmdRotation = currentHmdRotation;
        previousHmdPosition = currentHmdPosition;
    }
}
