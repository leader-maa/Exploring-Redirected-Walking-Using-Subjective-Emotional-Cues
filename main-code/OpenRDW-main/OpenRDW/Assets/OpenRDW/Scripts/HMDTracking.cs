using UnityEngine;
using UnityEngine.XR;

public class HMDTracking : MonoBehaviour
{
    public float realWorldRotationGain = 0.1f; // 旋转增益
    public float realWorldPositionGain = 0.1f; // 位置增益

    private Quaternion previousHmdRotation;
    private Vector3 previousHmdPosition;

    void Start()
    {
        // 初始化上一帧的 HMD 位置和旋转
        previousHmdRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);
        previousHmdPosition = InputTracking.GetLocalPosition(XRNode.CenterEye);
    }

    void Update()
    {
        // 获取当前 HMD 的旋转和位置
        Quaternion currentHmdRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);
        Vector3 currentHmdPosition = InputTracking.GetLocalPosition(XRNode.CenterEye);

        // 计算旋转和位置的差值
        Quaternion rotationDelta = Quaternion.Inverse(previousHmdRotation) * currentHmdRotation;
        Vector3 positionDelta = currentHmdPosition - previousHmdPosition;

        // 应用增益
        Quaternion gainedRotation = Quaternion.Lerp(Quaternion.identity, rotationDelta, realWorldRotationGain);
        Vector3 gainedPosition = positionDelta * realWorldPositionGain;

        // 更新相机的旋转和位置
        transform.rotation = transform.rotation * gainedRotation;
        transform.position += gainedPosition;

        // 更新上一帧的 HMD 位置和旋转
        previousHmdRotation = currentHmdRotation;
        previousHmdPosition = currentHmdPosition;
    }
}
