using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolPush : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnEnable()
    {
        Invoke("Push", 2);
    }
    /// <summary>
    /// 放入缓存池
    /// </summary>
    public void Push()
    {
        PoolMgr.GetInstance().PushObj(this.gameObject.name,this.gameObject);
        Destroy(this.GetComponentInChildren<PoolPush>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
