using UnityEngine;
using System.Collections;
using System;

public abstract class Redirector : MonoBehaviour
{
    [HideInInspector]
    public GlobalConfiguration globalConfiguration;

    [HideInInspector]
    public RedirectionManager redirectionManager;

    [HideInInspector]
    public MovementManager movementManager;

    void Awake()
    {
        globalConfiguration = GetComponentInParent<GlobalConfiguration>();
        redirectionManager = GetComponent<RedirectionManager>();
        movementManager = GetComponent<MovementManager>();
    }

    /// <summary>
    /// Applies redirection based on the algorithm.
    /// </summary>
    public abstract void InjectRedirection();

    /// <summary>
    /// Applies rotation to Redirected User. The neat thing about calling it this way is that we can keep track of gains applied.
    /// </summary>
    /// <param name="rotationInDegrees"></param>
    protected void InjectRotation(float rotationInDegrees)
    {
        if (rotationInDegrees != 0)
        {
            transform.RotateAround(Utilities.FlattenedPos3D(redirectionManager.headTransform.position), Vector3.up, rotationInDegrees);
            GetComponentInChildren<KeyboardController>().SetLastRotation(rotationInDegrees);
            if (redirectionManager.deltaDir != 0)
                redirectionManager.globalConfiguration.statisticsLogger.Event_Rotation_Gain(redirectionManager.movementManager.avatarId, rotationInDegrees / redirectionManager.deltaDir, rotationInDegrees);
                Debug.Log(rotationInDegrees / redirectionManager.deltaDir);
                Debug.Log("这里是旋转增益");
        }
    }


    /// <summary>
    /// Applies curvature to Redirected User. The neat thing about calling it this way is that we can keep track of gains applied.
    /// </summary>
    /// <param name="rotationInDegrees"></param>
    protected void InjectCurvature(float rotationInDegrees)
    {
        if (rotationInDegrees != 0)
        {                                
            transform.RotateAround(Utilities.FlattenedPos3D(redirectionManager.headTransform.position), Vector3.up, rotationInDegrees);
            
            GetComponentInChildren<KeyboardController>().SetLastCurvature(rotationInDegrees);
            if (redirectionManager.deltaPos.magnitude != 0)
                redirectionManager.globalConfiguration.statisticsLogger.Event_Curvature_Gain(redirectionManager.movementManager.avatarId, rotationInDegrees / redirectionManager.deltaPos.magnitude, rotationInDegrees);
                Debug.Log(rotationInDegrees / redirectionManager.deltaPos.magnitude);
                Debug.Log("这里是曲线增益");
        }
    }
    public bool Vector3IsNan(Vector3 v) {
        return float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);
    }
    /// <summary>
    /// Applies rotation to Redirected User. The neat thing about calling it this way is that we can keep track of gains applied.
    /// </summary>
    /// <param name="translation"></param>
    protected void InjectTranslation(Vector3 translation)
    {
        if (translation.magnitude > 0)
        {
            transform.Translate(translation, Space.World);
            
            if (redirectionManager.deltaPos.magnitude != 0)
                redirectionManager.globalConfiguration.statisticsLogger.Event_Translation_Gain(redirectionManager.movementManager.avatarId, Mathf.Sign(Vector3.Dot(translation, redirectionManager.deltaPos)) * translation.magnitude / redirectionManager.deltaPos.magnitude, Utilities.FlattenedPos2D(translation));
                Debug.Log("这里是平移增益");
                Debug.Log(Mathf.Sign(Vector3.Dot(translation, redirectionManager.deltaPos)) * translation.magnitude / redirectionManager.deltaPos.magnitude);
        }
    }
    public virtual void GetPriority()
    { }
}
