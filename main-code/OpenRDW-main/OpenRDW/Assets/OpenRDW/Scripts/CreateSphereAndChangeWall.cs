using UnityEngine;
using System.Collections;

public class CreateSphereAndChangeWall : MonoBehaviour
{

    public float newangle=-20f;
    public float angle ;// 指定的Y轴正方向的角度
    /*public float distance = 0.1f;  // 距离当前位置的距离*/
    public float distance = 1f;  // 距离当前位置的距离
    public float minA = 100f;
    public float maxA = 151f;

    public GameObject cube;
    public GameObject sphere;
    public float sphereSize = 0.5f;  // 球体的缩放大小
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


        // 根据原点,角度,半径获取物体的位置.  
        Quaternion rotation = Quaternion.Euler(0, angle+15, 0);
        /*Quaternion rotation = Quaternion.Euler(0,newangle, 0);*/
        Vector3 offset = rotation * Vector3.forward * distance; 
        Vector3 newPosition = position + offset;
        newPosition.y += 0.5f;

        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        
        sphere.transform.localScale = new Vector3(sphereSize, sphereSize, sphereSize);
        // 创建一个新的材质并设置颜色
        Renderer sphererenderer = sphere.GetComponent<Renderer>();
        Material wallmaterial = new Material(Shader.Find("Standard"));
        wallmaterial.color = sphereColor;
        // 将颜色设置为红色或其他指定颜色
        // 应用材质到球体
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
                // 创建一个新的材质并设置颜色
                Material spherematerial = new Material(Shader.Find("Standard"));
                spherematerial.color = wallColor;

                // 应用材质到墙体
                wallrenderer.material = spherematerial;
            }
        }

    }


}
