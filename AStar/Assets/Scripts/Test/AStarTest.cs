using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarTest : MonoBehaviour
{
    public  Material red;
    public  Material yellow;
    public  Material green;
    public Material Nomal;
    //几行几列
    public int H=5;
    public int Y=5;


    List<AStarNode> list;
    //偏移量
    public int offsetX=2;
    public int offsetY=-2;
    //坐标位置
    public int x=-3;
    public int y=5;

    private Vector2 beginPos = Vector2.right * -1;

    private Dictionary<string, GameObject> cubes = new Dictionary<string, GameObject>();
    void Start()
    {
        //初始化
        AStarMgr.GetInstance().InitMapInfo(Y, H);

        for (int i = 0; i <H; ++i)
        {
            for (int k = 0; k < Y; ++k)
            {
                //创建一个一个立方体
                GameObject game = GameObject.CreatePrimitive(PrimitiveType.Cube);
                game.transform.position = new Vector3(x + i * offsetX, y+ k *offsetY,0);
                //存储名字
                game.name = i + "_" + k;
                cubes.Add(game.name, game);

                //得到格子 判断他是不是阻挡
                AStarNode node = AStarMgr.GetInstance().nodes[i, k];
                if (node.startype==StarType.Stop)
                {
                    game.GetComponent<MeshRenderer>().material = red;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //进行射线检测
            RaycastHit info;
            //得到屏幕鼠标位置触发的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out info,1000))
            {
                if (beginPos==Vector2.right*-1)
                {
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            cubes[list[i].x + "_" + list[i].y].GetComponent<MeshRenderer>().material = Nomal;
                        }
                    }
                    //清理上一次的路径
                    string[] strs = info.collider.gameObject.name.Split('_');

                    beginPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
                    //吧点击到的对象改成黄色
                    info.collider.gameObject.GetComponent<MeshRenderer>().material = yellow;
                }
                else
                {
                    string[]strs=info.collider.gameObject.name.Split('_');

                    Vector2 endPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));


                    //寻路
                   
                    list = AStarMgr.GetInstance().FindPath(beginPos, endPos);
                    //避免死路 的时候黄色不变
                    cubes[(int)beginPos.x + "_" + (int)beginPos.y].GetComponent<MeshRenderer>().material = Nomal;
                    if (list!=null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            cubes[list[i].x + "_" + list[i].y].GetComponent<MeshRenderer>().material = green;
                        }
                    }
                   
                    //清除开始点 把它变成初始值
                    beginPos = Vector2.right * -1;
                }
            }
        }
    }
}
