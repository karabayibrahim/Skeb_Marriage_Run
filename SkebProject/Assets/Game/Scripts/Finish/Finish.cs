using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour,ICollectable
{
    [System.Obsolete]
    public void DoCollect()
    {
        GameManager.Instance.Player.enabled = false;
        GameManager.FinishEvent?.Invoke();
        gameObject.GetComponent<Collider>().enabled = false;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
