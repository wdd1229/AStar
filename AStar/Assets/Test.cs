using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : BasePanel
{
    // Start is called before the first frame update
    void Start()
    {

        //UIManager.GetInstance().ShowPanel<Test>("UI/MainPanel", E_UI_Layer.Mid,null);
        //GetControl<Button>("Button");
        TeST("1", "+", "2");
    }
    public void TeST(string s ,string a, string b)
    {
        string str = 1 + a + 2;
        Debug.Log(($"{str}"));
    }
   
    public override void ShowMe()
    {
        base.ShowMe();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PoolMgr.GetInstance().GetObj("Cube",(o)=> { o.transform.position=new Vector3(2,2,2);o.AddComponent<PoolPush>(); });
        }
        if (Input.GetMouseButtonDown(1))
        {
            ResMgr.GetInstance().LoadAsync<GameObject>("Cube" ,o=> { o.name = "张鹏笨蛋"; });
            ResMgr.GetInstance().Load<GameObject>("Cube").gameObject.name="王鹏笨蛋";
        }
    }
}
