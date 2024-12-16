using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // 在 Inspector 中将 VideoPlayer 拖拽到此字段

    void Start()
    {
        // 确保 VideoPlayer 组件已正确设置
        if (videoPlayer != null)
        {
            // 开始播放视频
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("VideoPlayer component is not assigned.");
        }
    }
}