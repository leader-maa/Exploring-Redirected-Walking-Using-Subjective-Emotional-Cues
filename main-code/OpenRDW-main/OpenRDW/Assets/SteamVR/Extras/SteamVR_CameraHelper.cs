using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SpatialTracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Valve.VR;

namespace Valve.VR
{
    public class SteamVR_CameraHelper : MonoBehaviour
    {
        public float realWorldRotationGain = 1.0f;
        public float realWorldTranslationGain = 1.0f;// 调整实际世界中头部旋转与虚拟世界中物体旋转之间的映射增益
        public float fakeWorldTranslationGain = 0.01f;
        public float total = 0.5f;
        public TrackedPoseDriver trackedPoseDriver;
         // 用于保存上一次的增益旋转
        public Quaternion initialRotationOffset; 
       
        public Quaternion lastHmdRotation; // 用于保存上一次的HMD旋转
        public bool _isText = false;
        public bool tag = false;
        public bool isCameraLocked = false;
        public GameObject textBox;
        public Vector3 initialCameraPosition;
        public Quaternion lockedRotation;
        public Quaternion hmdRotation;
        public Vector3 hmdEulerAngles;
        public Vector3 cameraEulerAngles;
        public Vector3 initEulerAngles;
        public Quaternion gainedRotation;
        private Quaternion previousHmdRotation;
        private Vector3 previousHmdPosition;

       
        private Vector3 previousPosition;
        private Quaternion previousRotation;

        private SteamVR_Behaviour_Pose pose;

        void Start()
        {
#if OPENVR_XR_API && UNITY_LEGACY_INPUT_HELPERS
            if (this.gameObject.GetComponent<UnityEngine.SpatialTracking.TrackedPoseDriver>() == null)
            {
                this.gameObject.AddComponent<UnityEngine.SpatialTracking.TrackedPoseDriver>();
                // 获取 TrackedPoseDriver 组件

#endif
            }
            previousHmdRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);
            previousHmdPosition = InputTracking.GetLocalPosition(XRNode.CenterEye);
            trackedPoseDriver = GetComponent<TrackedPoseDriver>();
            textBox = GameObject.Find("xuanzhuan");
            _isText = false;
            initialRotationOffset = InputTracking.GetLocalRotation(XRNode.CenterEye);

            cameraEulerAngles = initialRotationOffset.eulerAngles;
           /* textBox.SetActive(_isText);*/
            Debug.Log("执行了旋转初始化");
            Debug.Log(_isText);

            // 获取 SteamVR 设备行为
            pose = GetComponent<SteamVR_Behaviour_Pose>();
            if (pose == null)
            {
                Debug.LogError("SteamVR_Behaviour_Pose 未找到！");
                return;
            }

            // 初始化前一帧的位置和旋转
            previousPosition = pose.transform.localPosition;
            previousRotation = pose.transform.localRotation;
        }
        
        private void ApplyRotation()
        {
            Quaternion currentHmdRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);

            // 计算HMD旋转增量
            Quaternion rotationDelta = Quaternion.Inverse(lastHmdRotation) * currentHmdRotation;

            // 应用增益因子
            Quaternion gainedRotationDelta = Quaternion.Slerp(Quaternion.identity, rotationDelta, realWorldRotationGain);

            // 应用增益后的旋转增量到主摄像机
            transform.rotation = transform.rotation * gainedRotationDelta;

            // 保存当前HMD旋转用于下一帧计算
            lastHmdRotation = currentHmdRotation;

            // Debugging the rotations
            Debug.Log("HMD Rotation: " + currentHmdRotation.eulerAngles);
            Debug.Log("Gained Rotation Delta: " + gainedRotationDelta.eulerAngles);
            Debug.Log("Camera Rotation: " + transform.rotation.eulerAngles);
        }
        /*  void Update()
          {
                  // 获取头显的旋转
                  Quaternion hmdRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);

                  // 计算增益后的旋转
                  Quaternion gainedRotation = Quaternion.Euler(hmdRotation.eulerAngles * realWorldRotationGain);
                  //Quaternion desiredRotation = Quaternion.Lerp(transform.rotation, hmdRotation, realWorldRotationGain);
              // 应用增益后的旋转到主摄像机
                  transform.rotation = gainedRotation;

                  // Debugging the rotations
                  Debug.Log("HMD Rotation: " + hmdRotation.eulerAngles);
                  Debug.Log("Gained Rotation: " + gainedRotation.eulerAngles);

          }*/
        /*   void Update() //单纯的乘法会出现翻转问题  Slerp函数每启作用  不知道什么原因
           {
               // 获取头显的旋转
               Quaternion hmdRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);

               // 使用 Lerp 函数将物体的旋转平滑地过渡到头显的旋转，应用增益
               Quaternion desiredRotation = Quaternion.Euler(hmdRotation.eulerAngles * realWorldRotationGain);

           // 应用旋转到物体
              transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0,60,0)), Time.deltaTime);


               // Debugging the rotations
               //Debug.Log("HMD Rotation: " + hmdRotation.eulerAngles);
               Debug.Log("Rotation: " + transform.eulerAngles.y);
               //Debug.Log("Gained Rotation: " + desiredRotation.eulerAngles);
        }*/
        public void LockCamera()
        {
            // 锁定摄像机的当前旋转方向
            
            tag = true;
            Debug.Log("Camera Locked");
        }

        public void UnlockCamera()
        {
            // 解除锁定，恢复HMD控制
            tag = false;
            Debug.Log("Camera Unlocked");
        }


        /* void Update()// 使用四元数还避免上下翻转
         {
             hmdRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);
             if (Input.GetKeyDown(KeyCode.B))
             {
                 if (tag)
                 {
                     UnlockCamera();

                     if (Input.GetKeyDown(KeyCode.K))
                     {
                         Debug.Log("输入了K");
                         _isText = !_isText;
                         textBox.SetActive(_isText);
                     }
                 }

                 // 获取HMD的本地位置
                 else
                 {
                     LockCamera();
                 }
             }
             if (tag)
             {
                 initEulerAngles = initialRotationOffset.eulerAngles;

                  initialRotationOffset= hmdRotation;

                 // 将HMD的旋转分解为欧拉角
                 hmdEulerAngles = hmdRotation.eulerAngles;


                 // 对每个轴的旋转应用增益
                 //hmdEulerAngles.x *= realWorldRotationGain;
                 cameraEulerAngles.x = hmdRotation.x;
                 cameraEulerAngles.y +=(hmdEulerAngles.y-initEulerAngles.y) * fakeWorldTranslationGain;
                 cameraEulerAngles.z = hmdRotation.z;
                 //hmdEulerAngles.z *= realWorldRotationGain;


                 // 将增益后的欧拉角转换回四元数
                 gainedRotation = Quaternion.Euler(cameraEulerAngles);

                 // 应用增益后的旋转到相机
                 transform.rotation = gainedRotation;
             }
             else
             {
                 initEulerAngles = initialRotationOffset.eulerAngles;

                 initialRotationOffset = hmdRotation;

                 // 将HMD的旋转分解为欧拉角
                 hmdEulerAngles = hmdRotation.eulerAngles;

                 // 对每个轴的旋转应用增益
                 //hmdEulerAngles.x *= realWorldRotationGain;
                 cameraEulerAngles.x = hmdRotation.x;
                 cameraEulerAngles.y += (hmdEulerAngles.y - initEulerAngles.y) * realWorldRotationGain;
                 cameraEulerAngles.z = hmdRotation.z;
                 //hmdEulerAngles.z *= realWorldRotationGain;

                 // 将增益后的欧拉角转换回四元数
                 gainedRotation = Quaternion.Euler(cameraEulerAngles);

                 // 应用增益后的旋转到相机
                 transform.rotation = gainedRotation;

             }



             Vector3 hmdPosition = InputTracking.GetLocalPosition(XRNode.CenterEye);

             hmdPosition.x *= realWorldTranslationGain;
             *//*hmdPosition.y *= realWorldTranslationGain;*//*
             hmdPosition.z *= realWorldTranslationGain;
             transform.position = initialCameraPosition + hmdPosition;
         }*/
        void Update()
        {
            if (pose == null) return;

            // 获取当前 HMD 的位置和旋转
            Vector3 currentPosition = pose.transform.localPosition;
            Quaternion currentRotation = pose.transform.localRotation;

            // 计算位置和旋转增量
            Vector3 positionDelta = (currentPosition - previousPosition) * realWorldTranslationGain;
            Quaternion rotationDelta = Quaternion.Lerp(Quaternion.identity,
                                                       Quaternion.Inverse(previousRotation) * currentRotation,
                                                       realWorldRotationGain);

            // 应用增量到当前对象
            transform.position += positionDelta;
            transform.rotation *= rotationDelta;

            // 更新前一帧数据
            previousPosition = currentPosition;
            previousRotation = currentRotation;
        }
    }
}

        
                
        

        
                              

    

