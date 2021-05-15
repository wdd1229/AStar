using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StarType
{
    Walk,Stop
}
public class AStarNode 
{
    //格子对象的坐标
    public int x;
    public int y;

    //父节点
    public AStarNode FeaterNode;
    //寻路消耗
    public int node_f;
    //离起点的距离
    public float node_g;
    //离终点的距离
    public int node_h;

    public  StarType startype;

    public AStarNode(int x,int y,StarType type)
    {
        this.x = x;
        this.y = y;
        this.startype = type;
    }
}
