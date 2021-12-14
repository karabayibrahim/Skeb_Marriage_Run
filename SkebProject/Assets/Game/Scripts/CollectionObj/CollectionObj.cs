using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionObj : MonoBehaviour, ICollectable
{
    public void DoCollect()
    {
        if (gameObject.tag=="Positive")
        {
            GameManager.Instance.Player.RelationCount++;
        }
        else
        {
            GameManager.Instance.Player.RelationCount--;
        }
        Destroy(gameObject);

    }

    private void Update()
    {
        transform.Rotate(0, 1f, 0);
    }

    void Start()
    {
        GameManager.GameStateChanged += ObjectSpawner;
    }

    private void OnDisable()
    {
        GameManager.GameStateChanged -= ObjectSpawner;
    }
    private void ObjectSpawner()
    {
        if (gameObject.tag=="Positive")
        {
            Instantiate(GameManager.Instance.Data.CollectionObjsPositive[GameManager.Instance.GameStateIndex], transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(GameManager.Instance.Data.CollectionObjsNegative[GameManager.Instance.GameStateIndex], transform.position, Quaternion.identity);
        }
        if (GameManager.Instance.GameStateIndex > 0)
        {
            Destroy(gameObject);
        }
    }

}
