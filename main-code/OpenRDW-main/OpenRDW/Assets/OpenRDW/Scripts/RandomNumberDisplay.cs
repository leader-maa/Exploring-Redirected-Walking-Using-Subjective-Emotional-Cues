using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomNumberDisplay : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // ����UI�е�Text���

    private void Start()
    {
        // ÿ֡�������������ʾ
        textMeshPro.text += ("please turn ");
        
    }
}

