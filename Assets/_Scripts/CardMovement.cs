using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    // CardMovement

    public Vector2 _offsetToMousePos, _originalPos;

    public bool _isPlaced = false;

    public Vector3 _correctPos;

    bool _isMoving, _isCollide;

    float _yOffset = 0.35f;

    float _zOffset = 0.01f;

    public static CardMovement Instance;

    Transform _pos;

    public int indexNumber;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _originalPos = transform.position;
        
    }

    private void Update()
    {
        if (!_isMoving) return;
        var MousePos = GetMousePos();

        transform.position = MousePos - _offsetToMousePos;
    }

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {

        if (!_isPlaced)
        {
            _isMoving = true;

            _offsetToMousePos = GetMousePos() - (Vector2)transform.position;

            transform.GetChild(0).GetChild(0).GetComponent<Canvas>().sortingOrder = 3;

        }
    }

    private void OnMouseUp()
    {
        if (_isCollide && enabled)
        {
            indexNumber = BlockManager.Instance.AllCorrectPositions.IndexOf(_pos);
                if (BlockManager.Instance.AllBlockManager[indexNumber].Count <= 7)
                {
                    if (_isMoving)
                    {
                        SufflingCards.Instance.NextMove();
                    }
                    transform.position = _correctPos;
                     Soundmanager.Instance.PlaySound(Soundmanager.Sound.correct);
                _isMoving = false;

                    transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .15f).OnComplete(() =>
                   {
                       transform.DOScale(new Vector3(1, 1, 1), .15f).OnComplete(() =>
                       {
                           transform.GetChild(0).GetChild(0).GetComponent<Canvas>().sortingOrder = 2;

                           indexNumber = BlockManager.Instance.AllCorrectPositions.IndexOf(_pos);
                           if (indexNumber != -1)
                           {
                               BlockManager.Instance.AllCorrectPositions[indexNumber] = transform;

                               if (transform.CompareTag("Card"))
                               {

                                   if (indexNumber == 0)
                                   {
                                   
                                       BlockManager.Instance.Block1.Add(gameObject);
                                       Debug.Log("Block1 Count" + BlockManager.Instance.Block1.Count);
                                       AdditionofCards.Instance.CardAdditions(indexNumber);
                                       transform.SetParent(BlockManager.Instance.allFourBlockList[indexNumber]);
                                  
                                   }

                                   else if (indexNumber == 1)
                                   {
                                   
                                       BlockManager.Instance.Block2.Add(gameObject);
                                   //  Debug.Log("Tried To call");
                                   AdditionofCards.Instance.CardAdditions(indexNumber);
                                   //  Debug.Log("call Done");
                                   transform.SetParent(BlockManager.Instance.allFourBlockList[indexNumber]);
                                  
                                   }

                                   else if (indexNumber == 2)
                                   {
                                   
                                       BlockManager.Instance.Block3.Add(gameObject);
                                       AdditionofCards.Instance.CardAdditions(indexNumber);
                                       transform.SetParent(BlockManager.Instance.allFourBlockList[indexNumber]);
                                  
                                   }

                                   else if (indexNumber == 3)
                                   {
                                   
                                       BlockManager.Instance.Block4.Add(gameObject);
                                       AdditionofCards.Instance.CardAdditions(indexNumber);
                                       transform.SetParent(BlockManager.Instance.allFourBlockList[indexNumber]);
                                  
                                   }
                               }
                           }

                       });
                   });
                    _isPlaced = true;
                    enabled = false;
                //SetLimit();
                Invoke("SetLimit",1);
            }
                else
                {
                if (enabled)
                {

                    // SetLimit();
                    Invoke("SetLimit", 1);
                    Soundmanager.Instance.PlaySound(Soundmanager.Sound.wrong);
                    transform.position = _originalPos;
                    _isMoving = false;
                    _isPlaced = false;
                }
                }
        }
        else
        {
            if (enabled)
            {
                Soundmanager.Instance.PlaySound(Soundmanager.Sound.wrong);
                transform.position = _originalPos;
                _isMoving = false;
                _isPlaced = false;
            }
        }
    }


        public void OnTriggerEnter2D(Collider2D collision)
        {
        if(collision.gameObject.CompareTag("Discard") && OnDiscard.instance.discardedCardCount < 5)
        {
            OnDiscardCard();
        }
        else if (BlockManager.Instance.AllCorrectPositions.Contains(collision.gameObject.transform))
        {
            if (collision.gameObject.CompareTag("Card"))
            {
              Debug.Log("  :   " + collision.gameObject.name);
                _correctPos = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - _yOffset, collision.gameObject.transform.position.z - _zOffset);
               _pos = collision.gameObject.transform;
                _isCollide = true;
            }
            else
            {
                 Debug.Log("  :   " + collision.gameObject.name);
                _correctPos = collision.gameObject.transform.position;
               
              _pos = collision.gameObject.transform;
                _isCollide = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Discard") && OnDiscard.instance.discardedCardCount < 2)
        {
            OnDiscardCard();
        }
        else if (BlockManager.Instance.AllCorrectPositions.Contains(other.gameObject.transform))
        {
            Debug.Log("  :   " + other.gameObject.name);
            if (other.gameObject.CompareTag("Card"))
            {
                _correctPos = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y - _yOffset, other.gameObject.transform.position.z - _zOffset);
               _pos = other.gameObject.transform;
                _isCollide = true;
            }
            else
            {
                Debug.Log("  :   " + other.gameObject.name);
                _correctPos = other.gameObject.transform.position;
               
               _pos = other.gameObject.transform;
                _isCollide = true;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isCollide = false;
    }

    public void SetLimit()
    {
       // Debug.Log("SETLIMIT " + BlockManager.Instance.Block1.Count + " " + BlockManager.Instance.Block2.Count + " " + BlockManager.Instance.Block3.Count + " " + BlockManager.Instance.Block4.Count);

        if (BlockManager.Instance.Block1.Count > 7 && BlockManager.Instance.Block2.Count > 7 && BlockManager.Instance.Block3.Count > 7 && BlockManager.Instance.Block4.Count > 7)
        {
            Debug.Log("gameOver");
            UiManager.instance.OpenGameOver();
        }
    }

    private void OnDiscardCard()
    {
        OnDiscard.instance.discardedCardCount++;
        OnDiscard.instance.SetCardColor();
        
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .25f).OnComplete(() =>
        {
            transform.DOScale(new Vector3(0, 0, 0), .3f).OnComplete(() =>
            {
                Soundmanager.Instance.PlaySound(Soundmanager.Sound.discard);
                Destroy(gameObject);
                SufflingCards.Instance.NextMove();
            });
        });
    }
}
