using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampControl : MonoBehaviour,IClampCollect
{
    public void DoClampCollect(Vector3 position)
    {
        var Player = GameManager.Instance.Player;
        if (position.x>=Player.transform.position.x)
        {
            Player.IsClampRight = true;
        }
        else if (position.x<=Player.transform.position.x)
        {
            Player.IsClampLeft = true;

        }
    }

    public void DoClampCollectExit(Vector3 position)
    {
        var Player = GameManager.Instance.Player;
        if (position.x >= Player.transform.position.x)
        {
            Player.IsClampRight = false;
        }
        else if (position.x <= Player.transform.position.x)
        {
            Player.IsClampLeft = false;

        }
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
