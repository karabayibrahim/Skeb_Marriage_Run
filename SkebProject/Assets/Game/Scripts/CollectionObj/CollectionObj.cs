using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionObj : MonoBehaviour, ICollectable
{
    public bool StartStatus = false;
    public void DoCollect()
    {
        if (gameObject.tag=="Positive")
        {
            GameManager.Instance.Player.RelationCount+=5;
        }
        else
        {
            GameManager.Instance.Player.RelationCount-=5;
        }
        Destroy(gameObject);

    }

    private void Update()
    {
        transform.Rotate(0, 0.5f, 0);
    }

    void Start()
    {
        GameManager.GameStateChanged += ObjectSpawner;
        StartSpawn();
    }

    private void OnDisable()
    {
        GameManager.GameStateChanged -= ObjectSpawner;
    }
    private void ObjectSpawner()
    {
        var parent = GameManager.Instance.CurrentLevel.transform;
        if (gameObject.tag=="Positive")
        {
            Instantiate(GameManager.Instance.Data.CollectionObjsPositive[GameManager.Instance.GameStateIndex], transform.position, Quaternion.identity,parent);
        }
        else
        {
            Instantiate(GameManager.Instance.Data.CollectionObjsNegative[GameManager.Instance.GameStateIndex], transform.position, Quaternion.identity,parent);
        }
        if (GameManager.Instance.GameStateIndex > 0)
        {
            Destroy(gameObject);
        }
    }

    private void StartSpawn()
    {
        if (!StartStatus)
        {
            var parent = GameManager.Instance.CurrentLevel.transform;
            if (gameObject.tag == "Positive")
            {
                var newObject=Instantiate(GameManager.Instance.Data.CollectionObjsPositive[GameManager.Instance.GameStateIndex], transform.position, Quaternion.identity,parent);
                newObject.StartStatus = true;
            }
            else
            {
                var newObject=Instantiate(GameManager.Instance.Data.CollectionObjsNegative[GameManager.Instance.GameStateIndex], transform.position, Quaternion.identity,parent);
                newObject.StartStatus = true;
            }
            Destroy(gameObject);
        }
    }

}
