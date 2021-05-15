using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A行寻路管理器
/// </summary>
public class AStarMgr :BaseManager<AStarMgr>
{

    private int mapW;
    private int mapH;

    public  AStarNode[,] nodes;
    //开启 列表
    private List<AStarNode> openList=new List<AStarNode>();
    //关闭 列表
    private List<AStarNode> closeList=new List<AStarNode>();

    /// <summary>
    /// 初始化地图信息
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    public void InitMapInfo(int w,int h)
    {
        this.mapW = w;
        this.mapH = h;
        //申明容器可以有多少个格子
        nodes = new AStarNode[w,h];
        //根据宽高 创建格子
        for (int i = 0; i < w; i++)
        {
            for (int k = 0; k < h; k++)
            {
                AStarNode node = new AStarNode(i, k, Random.Range(0, 100) < 20 ? StarType.Stop : StarType.Walk);
                nodes[i, k] = node;
            }
        }
    }
    public List<AStarNode> FindPath(Vector2 strarPos,Vector2 endPos)
    {
        //实际项目 传入的往往是坐标中的位置
        //我们这互留换算的步骤，直接认为他是传进来的格子坐标

        //首先判断是否符合
        if(strarPos.x<0||strarPos.x>=mapW||strarPos.y<0||strarPos.y>=mapH||
            endPos.x<0||endPos.x>=mapW||endPos.y<0||endPos.y>=mapH)
             
        return null;
        //判断不是阻挡的情况下
        //应该得到起始点和终点的 对应格子
        AStarNode start = nodes[(int)strarPos.x, (int)strarPos.y];
        AStarNode end = nodes[(int)endPos.x, (int)endPos.y];
        if (start.startype==StarType.Stop||end.startype==StarType.Stop)
        {
            Debug.LogError("开始或者结束点是阻挡");
            return null;
        }

        closeList.Clear();
        openList.Clear();

        //把开始放入关闭列表中
        start.FeaterNode = null;
        start.node_f = 0;
        start.node_g = 0;
        start.node_h = 0;
        closeList.Add(start);

        while (true)
        {
            //从起点开始 找周围的点 并存放到开启列表中
            //左上
            FindNearlyNodeToOpenList(start.x - 1, start.y - 1, 1.4f, start, end);
            //上
            FindNearlyNodeToOpenList(start.x, start.y - 1, 1, start, end);
            //右上
            FindNearlyNodeToOpenList(start.x + 1, start.y - 1, 1.4f, start, end);
            //左
            FindNearlyNodeToOpenList(start.x - 1, start.y, 1, start, end);
            //右
            FindNearlyNodeToOpenList(start.x + 1, start.y, 1, start, end);
            //左下
            FindNearlyNodeToOpenList(start.x - 1, start.y + 1, 1.4f, start, end);
            //下
            FindNearlyNodeToOpenList(start.x, start.y + 1, 1, start, end);
            //右下
            FindNearlyNodeToOpenList(start.x + 1, start.y + 1, 1.4f, start, end);

            if (openList.Count==0)
            {
                Debug.Log("死路");
                return null;
            }


            //选出开启列表中 寻找消耗最小的点
            openList.Sort(SortOpenList);
            //放入关闭列表中 然后在从开启列表中移除
            closeList.Add(openList[0]);
            //找到这个点 又变成新的起点 进行下一次寻路
            start = openList[0];

            openList.RemoveAt(0);

            if (start == end)
            {
                //找完了 找到路径
                List<AStarNode> path = new List<AStarNode>();
                path.Add(end);
                while (end.FeaterNode!=null)
                {
                    path.Add(end.FeaterNode);
                    end = end.FeaterNode;
                }
                path.Reverse();
                return path;
            }
        }
      

        
       
    } 
    /// <summary>
    /// 排序函数
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private int SortOpenList(AStarNode a, AStarNode b)
    {
        if (a.node_f>b.node_f)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    private void FindNearlyNodeToOpenList(int x,int y,float g,AStarNode father,AStarNode end)
    {
        //边界判断
        if (x<0||x>=mapW ||
            y<0||y>=mapH)
        {
            return;
        }
        //在范围内 再去取点
        AStarNode node = nodes[x, y];
        //判断这些点 是否是边界 是否是阻挡 是否在开启列表和关闭列表里面，如果都不是 再放入开启列表
        if (node==null || node.startype == StarType.Stop||
            closeList.Contains(node)||openList.Contains(node))
        {
            return ;
        }
        //计算F值
        //f=g+h
        //纪录父对象
        node.FeaterNode = father;
        //计算g   我离起点的距离 就是我父亲距离起点的纪录+我距离我父亲的距离
        node.node_g = father.node_g+g;
        node.node_h = Mathf.Abs(end.x - node.x) + Mathf.Abs(end.y - node.y);
        node.node_f = (int)(node.node_g + node.node_h);

        //如果通过上面的合法验证 就存放到开启列表里
        openList.Add(node);
    }
}
