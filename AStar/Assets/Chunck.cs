using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Chunck : MonoBehaviour
{

   
    /// <summary>
    /// 1.用list存储我们的定点和面熟信息    /// </summary>
    //顶点信息
    List<Vector3> vectors = new List<Vector3>();
    //面数信息
    List<int> trilos = new List<int>();
    Mesh mesh;
    void Start()
    {
        GameObject a = new GameObject();
      
        //3.将我们的信息放在一个mesh上
        mesh = new Mesh();
        mesh.name = "Chunck";
        //添加定点信息和面数信息
        AddCubeFront();
        AddCubeBack();
        AddCubeRight();
        //mesh.SetVertices(vectors.ToArray()); //mesh.vertices = vectors.ToArray();  直接对mesh.vertices和mesh.triangles进行赋值是老版本的Untiy输入网格数据的方法，这种方式并不准确，因为mesh.triangles里存储的顶点索引信息并不一定是构成三角形，
                                             //也可能是Lines，Quad等方式，所以为了区分primitive（图元）的类型，新增了SetIndices函数，用于区别图元的类型。

        mesh.triangles = trilos.ToArray();
        //4.合成
        mesh.RecalculateBounds();//重新计算包围体
        mesh.RecalculateNormals(); //重新计算网格的法线
        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshFilter>().mesh = mesh;

        MonoMgr.GetInstance().AddUpdateListener(MyUpdate);
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 2.建立一个某种规则存放定点信息和三角面的合理方法
    /// </summary>
    void AddCubeFront()
    {

        trilos.Add(2 + vectors.Count);
        trilos.Add(1 + vectors.Count);
        trilos.Add(0 + vectors.Count);
        trilos.Add(0 + vectors.Count);
        trilos.Add(3 + vectors.Count);
        trilos.Add(2 + vectors.Count);

        vectors.Add(new Vector3(0, 0, 0));
        vectors.Add(new Vector3(0, 0, 1));
        vectors.Add(new Vector3(0, 1, 1));
        vectors.Add(new Vector3(0, 1, 0));

       
    }
    void AddCubeBack()
    {
       

        trilos.Add(2 + vectors.Count);
        trilos.Add(1 + vectors.Count);
        trilos.Add(0 + vectors.Count);
        trilos.Add(0 + vectors.Count);
        trilos.Add(3 + vectors.Count);
        trilos.Add(2 + vectors.Count);

        vectors.Add(new Vector3(0, 0, 0));
        vectors.Add(new Vector3(0, 0, 1));
        vectors.Add(new Vector3(0, 1, 1));
        vectors.Add(new Vector3(0, 1, 0));
    }
    void AddCubeRight()
    {


        trilos.Add(2 + vectors.Count);
        trilos.Add(1 + vectors.Count);
        trilos.Add(0 + vectors.Count);
        trilos.Add(0 + vectors.Count);
        trilos.Add(3 + vectors.Count);
        trilos.Add(2 + vectors.Count);

        vectors.Add(new Vector3(0, 0, 1));
        vectors.Add(new Vector3(-1, 0, 1));
        vectors.Add(new Vector3(-1, 1, 1));
        vectors.Add(new Vector3(0, 1, 1));
    }
    void AddCubeLeft()
    {


        trilos.Add(2 + vectors.Count);
        trilos.Add(1 + vectors.Count);
        trilos.Add(0 + vectors.Count);
        trilos.Add(0 + vectors.Count);
        trilos.Add(3 + vectors.Count);
        trilos.Add(2 + vectors.Count);

        vectors.Add(new Vector3(-1, 0, 0));
        vectors.Add(new Vector3(-1, 0, 0));
        vectors.Add(new Vector3(-1, 1, 0));
        vectors.Add(new Vector3(0, 1, 0));
    }

    public void MyUpdate()
    {
        Debug.Log("aa");
    }
}
