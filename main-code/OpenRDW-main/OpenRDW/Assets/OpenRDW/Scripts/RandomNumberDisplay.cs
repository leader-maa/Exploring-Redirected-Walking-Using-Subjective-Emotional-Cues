using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomNumberDisplay : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // 引用UI中的Text组件

    private void Start()
    {
        // 每帧更新随机数的显示
        textMeshPro.text += ("please turn ");
        
    }
}

