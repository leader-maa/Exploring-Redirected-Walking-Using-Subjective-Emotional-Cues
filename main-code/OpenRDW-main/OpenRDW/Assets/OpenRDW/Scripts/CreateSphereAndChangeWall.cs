using UnityEngine;
using System.Collections;

public class CreateSphereAndChangeWall : MonoBehaviour
{

    public float newangle=-20f;
    public float angle ;// ָ����Y��������ĽǶ�
    /*public float distance = 0.1f;  // ���뵱ǰλ�õľ���*/
    public float distance = 1f;  // ���뵱ǰλ�õľ���
    public float minA = 100f;
    public float maxA = 151f;

    public GameObject cube;
    public GameObject sphere;
    public float sphereSize = 0.5f;  // ��������Ŵ�С
    public Color sphereColor = Color.green;
    public Color wallColor = Color.red;
    GameObject[] walls;
    // Use this for initialization
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        angle = Random.Range(minA, maxA);
        CreateSphere();
        ChangeWall();
        Debug.Log(angle);
    }

    void Update()
    {
        
    }

    public void CreateSphere()
    {
        Vector3 position = transform.position;


        // ����ԭ��,�Ƕ�,�뾶��ȡ�����λ��.  
        Quaternion rotation = Quaternion.Euler(0, angle+15, 0);
        /*Quaternion rotation = Quaternion.Euler(0,newangle, 0);*/
        Vector3 offset = rotation * Vector3.forward * distance; 
        Vector3 newPosition = position + offset;
        newPosition.y += 0.5f;

        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        
        sphere.transform.localScale = new Vector3(sphereSize, sphereSize, sphereSize);
        // ����һ���µĲ��ʲ�������ɫ
        Renderer sphererenderer = sphere.GetComponent<Renderer>();
        Material wallmaterial = new Material(Shader.Find("Standard"));
        wallmaterial.color = sphereColor;
        // ����ɫ����Ϊ��ɫ������ָ����ɫ
        // Ӧ�ò��ʵ�����
        sphererenderer.material = wallmaterial;
        GameObject obj = (GameObject)GameObject.Instantiate(sphere, newPosition, rotation);

    }
    public void ChangeWall()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {
            Renderer wallrenderer = wall.GetComponent<Renderer>();
            if (wallrenderer != null)
            {
                // ����һ���µĲ��ʲ�������ɫ
                Material spherematerial = new Material(Shader.Find("Standard"));
                spherematerial.color = wallColor;

                // Ӧ�ò��ʵ�ǽ��
                wallrenderer.material = spherematerial;
            }
        }

    }


}
