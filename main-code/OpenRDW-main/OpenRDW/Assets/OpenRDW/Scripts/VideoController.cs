using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // �� Inspector �н� VideoPlayer ��ק�����ֶ�

    void Start()
    {
        // ȷ�� VideoPlayer �������ȷ����
        if (videoPlayer != null)
        {
            // ��ʼ������Ƶ
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("VideoPlayer component is not assigned.");
        }
    }
}