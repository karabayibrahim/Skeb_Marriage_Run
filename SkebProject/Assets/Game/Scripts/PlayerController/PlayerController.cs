using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    public RelationStatus _relationStatus;
    public AgeStatus _ageStatus;
    public Human Male;
    public Human Female;
    public float Speed;
    public float swerveSpeed;
    public static Action WalkAction;
    public static Action IdleAction;
    public float MoveFactorX => _moveFactorX;

    private float _reletionCount;
    private float _lastFrameFingerPositionX;
    private float _moveFactorX;
    public float RelationCount
    {
        get
        {
            return _reletionCount;
        }
        set
        {
            if (RelationCount == value)
            {
                return;
            }
            _reletionCount = value;
            OnRelationCountControl();
            OnProgressBarControl();
        }
    }

    private void OnProgressBarControl()
    {
        if (RelationCount>=0)
        {
            GameManager.Instance.UIManager.Bar.fillAmount = RelationCount / 100f;
        }
        else
        {
            
        }
    }

    private void OnRelationCountControl()
    {
        if (RelationCount<-100)
        {
            RelationCount = -100;
        }

        else if (RelationCount >= -100 && RelationCount < -50)
        {
            RelationCount = -100;
            RelationStatus = RelationStatus.TERRIBLE;
        }

        else if (RelationCount >= -50 && RelationCount < 0)
        {
            RelationStatus = RelationStatus.BAD;
        }

        else if (RelationCount >= 0 && RelationCount < 50)
        {
            RelationStatus = RelationStatus.NORMAL;
        }
        else if (RelationCount >= 50 && RelationCount < 75)
        {
            RelationStatus = RelationStatus.GOOD;
        }
        else if (RelationCount>=75 && RelationCount <100)
        {
            RelationStatus = RelationStatus.EXCELLENT;
        }
        else if (RelationCount>=100)
        {
            RelationCount = 100;
        }
    }

    public AgeStatus AgeStatus
    {
        get
        {
            return _ageStatus;
        }
        set
        {
            if (AgeStatus == value)
            {
                return;
            }
            _ageStatus = value;
            OnAgeStatusChanged();
        }
    }


    public RelationStatus RelationStatus
    {
        get
        {
            return _relationStatus;
        }
        set
        {
            if (RelationStatus == value)
            {
                return;
            }
            _relationStatus = value;
            OnRelationStatusChanged();
        }
    }


    private void OnRelationStatusChanged()
    {
        switch (RelationStatus)
        {
            case RelationStatus.TERRIBLE:
                GameManager.Instance.UIManager.Bar.color = Color.black;
                GameManager.Instance.UIManager.StatusTextAdjust("Terrible", Color.black);
                Speed = 6f;
                break;
            case RelationStatus.BAD:
                GameManager.Instance.UIManager.Bar.color = Color.gray;
                GameManager.Instance.UIManager.StatusTextAdjust("Bad", Color.gray);
                Speed = 8f;
                break;
            case RelationStatus.NORMAL:
                GameManager.Instance.UIManager.Bar.color = Color.red;
                GameManager.Instance.UIManager.StatusTextAdjust("Normal", Color.red);
                CouplePositionAdjust(4, -4);
                gameObject.GetComponent<BoxCollider>().size = new Vector3(11f, 12f, 1f);
                Speed = 10f;
                break;
            case RelationStatus.GOOD:
                GameManager.Instance.UIManager.Bar.color = Color.yellow;
                GameManager.Instance.UIManager.StatusTextAdjust("Good", Color.yellow);
                CouplePositionAdjust(1.4f, -1.4f);
                gameObject.GetComponent<BoxCollider>().size = new Vector3(6f, 12f, 1f);
                Speed = 15f;
                break;
            case RelationStatus.EXCELLENT:
                GameManager.Instance.UIManager.Bar.color = Color.green;
                GameManager.Instance.UIManager.StatusTextAdjust("Excellent", Color.green);
                Speed = 20f;
                break;
            default:
                break;
        }
    }

    private void OnAgeStatusChanged()
    {
        switch (AgeStatus)
        {
            case AgeStatus.YOUNG:
                GameManager.Instance.GameStateIndex = 0;
                GameManager.GameStateChanged?.Invoke();
                CharacterChanged();
                break;
            case AgeStatus.ADULT:
                GameManager.Instance.GameStateIndex = 1;
                GameManager.GameStateChanged?.Invoke();
                CharacterChanged();
                break;
            case AgeStatus.OLD:
                GameManager.Instance.GameStateIndex = 2;
                GameManager.GameStateChanged?.Invoke();
                CharacterChanged();
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ICollectable>() != null)
        {
            other.gameObject.GetComponent<ICollectable>().DoCollect();
        }
    }

    void Start()
    {
        RelationStatus = RelationStatus.NORMAL;
        GameManager.Instance.UIManager.Bar.fillAmount = RelationCount / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSystem();
        float swerveAmount = Time.deltaTime * swerveSpeed * MoveFactorX;
        transform.Translate(swerveAmount, 0, 0);
    }

    private void MoveSystem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX = Input.mousePosition.x;
            transform.Translate(0, 0, Speed * Time.deltaTime);
            WalkAction?.Invoke();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;
            IdleAction?.Invoke();
        }
    }

    private void CharacterChanged()
    {
        Destroy(Male);
        Destroy(Female);
        Male = Instantiate(GameManager.Instance.Data.Males[GameManager.Instance.GameStateIndex], transform.position, Quaternion.identity);
        Female = Instantiate(GameManager.Instance.Data.Females[GameManager.Instance.GameStateIndex], transform.position, Quaternion.identity);
    }

    private void CouplePositionAdjust(float _malePos, float _femalePos)
    {
        Male.transform.DOLocalMove(new Vector3(_malePos, 0, 0), 0.5f).SetEase(Ease.OutCubic);
        Female.transform.DOLocalMove(new Vector3(_femalePos, 0, 0), 0.5f).SetEase(Ease.OutCubic);
    }
}
