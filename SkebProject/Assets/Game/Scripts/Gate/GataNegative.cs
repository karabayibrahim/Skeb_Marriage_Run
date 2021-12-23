using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GataNegative : MonoBehaviour,ICollectable
{
    public float IncreaseAmount;
    public GameObject MySprite;
    public GameObject MyParticle;
    public GameObject MyText;
    public GameObject ArkaPlan;
    public GameObject MaterialObject;
    public void DoCollect()
    {
        GameManager.Instance.Player.RelationCount -= IncreaseAmount;
        var newParticle = Instantiate(GameManager.Instance.Data.Particles[13], transform.position, Quaternion.identity);
        Destroy(newParticle, 2f);
        Destroy(MySprite);
        Destroy(MyParticle);
        Destroy(MyText);
        Destroy(ArkaPlan);
    }

    // Start is called before the first frame update
    void Start()
    {
        AdjustTextObj();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPositionControl();
    }

    private void AdjustTextObj()
    {
        int index = Random.Range(0, GameManager.Instance.TextData.NegativeTexts.Count);
        var newText = Instantiate(GameManager.Instance.TextData.NegativeTexts[index], transform.position, Quaternion.identity,transform);
        newText.transform.localPosition = new Vector3(0, 6f, 0);
        MyText = newText;
        MySprite.GetComponent<MeshRenderer>().material = GameManager.Instance.Data.GateNegativeMaterials[index];
    }

    private void PlayerPositionControl()
    {
        if (transform.position.z < GameManager.Instance.Player.transform.position.z)
        {
            Debug.Log("Yakın");
            var newMaterial = GameManager.Instance.Data.AlphaMat;
            var alpha = 0;
            //DOTween.To(() => alpha, x => alpha = x, 0, 0.5f);
            newMaterial.color = new Color(MaterialObject.GetComponent<Renderer>().material.color.r, MaterialObject.GetComponent<Renderer>().material.color.g, MaterialObject.GetComponent<Renderer>().material.color.b, alpha);
            MaterialObject.GetComponent<MeshRenderer>().material = newMaterial;
            Destroy(this);

        }
    }
}
