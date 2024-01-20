using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class AdditionofCards : MonoBehaviour
{
    // AdditionofCards

    int _lastIndex, _cardIndex, _secondLastIndex;
    public int _cardValue;
    [SerializeField] CardManager _carryCard;
    private CardManager _carryForwadCard;
    public int _score = 0;
    int count = 1;

    public static AdditionofCards Instance;


    private void Awake()
    {
        Instance = this;
    }

    public void CardAdditions(int _index)
    {

        if (BlockManager.Instance.AllBlockManager[_index].Count > 1)
        {

            _lastIndex = BlockManager.Instance.AllBlockManager[_index].Count - 1;

            _secondLastIndex = BlockManager.Instance.AllBlockManager[_index].Count - 2;

            _cardValue = int.Parse(BlockManager.Instance.AllBlockManager[_index][_lastIndex].name) * 2;


            _cardIndex = SufflingCards.Instance.Deck.IndexOf(_cardValue); 
            //  Debug.Log(Block1[_lastIndex] == Block1[_secondLastIndex]);
            if (BlockManager.Instance.AllBlockManager[_index][_lastIndex].name == BlockManager.Instance.AllBlockManager[_index][_secondLastIndex].name)
            {
                BlockManager.Instance.AllBlockManager[_index][_lastIndex].transform.DOScale(new Vector3(1.3f,1.3f,1.3f) , .15f).OnComplete(() =>
                { 
                     BlockManager.Instance.AllBlockManager[_index][_lastIndex].transform.DOMoveY(BlockManager.Instance.AllBlockManager[_index][_lastIndex].transform.position.y + 0.35f, .15f).OnComplete(() =>
                     { 
                        BlockManager.Instance.AllBlockManager[_index][_lastIndex].transform.DOScale(new Vector3(1, 1, 1), .15f).OnComplete(() =>
                        {
                            Destroy(BlockManager.Instance.AllBlockManager[_index][_lastIndex]);
                            Destroy(BlockManager.Instance.AllBlockManager[_index][_secondLastIndex]);
                            //BlockManager.Instance.AllBlockManager[_index][_lastIndex].SetActive(false);
                            //BlockManager.Instance.AllBlockManager[_index][_secondLastIndex].SetActive(false);
                            if (BlockManager.Instance.AllBlockManager[_index].Count > 0)
                            {
                                 _carryForwadCard = Instantiate(_carryCard, BlockManager.Instance.AllBlockManager[_index][_lastIndex].transform.position, Quaternion.identity, BlockManager.Instance.allFourBlockList[_index]);
                                BlockManager.Instance.AllCorrectPositions[_index] = _carryForwadCard.transform;
                                Debug.Log(_carryForwadCard.gameObject.name + " if");
                            }
                            else
                            {
                                 _carryForwadCard = Instantiate(_carryCard, BlockManager.Instance.allFourBlockList[_index].position, Quaternion.identity, BlockManager.Instance.allFourBlockList[_index]);
                                 BlockManager.Instance.AllCorrectPositions[_index] = _carryForwadCard.transform;
                                Debug.Log(_carryForwadCard.gameObject.name + " else");
                            }


                                BlockManager.Instance.AllBlockManager[_index].RemoveAt(_lastIndex);
                                BlockManager.Instance.AllBlockManager[_index].RemoveAt(_secondLastIndex);
                                BlockManager.Instance.AllBlockManager[_index].Add(_carryForwadCard.gameObject);
                                _carryForwadCard.transform.GetChild(0).GetChild(0).GetComponent<Canvas>().sortingOrder = 2;
                                _carryForwadCard.SetData(_cardIndex);
                                _carryForwadCard.name = SufflingCards.Instance.Deck[_cardIndex].ToString();

                                //count = 1;
                                Debug.Log("card Value " + _cardValue);
                                if(_cardValue > SufflingCards.Instance.Deck[SufflingCards.Instance.cardRange-1])
                                {
                                     SufflingCards.Instance.cardRange++;
                                }
                                _score += (_cardValue)*count;
                                UiManager.instance.scoreCard.text = "Score : "+ _score.ToString();
                                Debug.Log("Count" + count);
                                count++;
                                 if (_cardValue == 2048)
                                 {
                                    _carryForwadCard.transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), .15f).OnComplete(() =>
                                    {
                                        _carryForwadCard.transform.DORotate(new Vector3(0, 0, 360), 2).OnComplete(() =>
                                        { 
                                            Destroy(_carryForwadCard);
                                            BlockManager.Instance.AllBlockManager[_index].Remove(_carryForwadCard.gameObject);
                                            BlockManager.Instance.AllCorrectPositions[_index] = BlockManager.Instance.AllBlockManager[_index][_lastIndex].transform;
                                        });
                                    });
                                 }

                            CardAdditions(_index);
                        });
                    });
                });
            }

        }
        else
        {
            count = 1;
        }
    }
}
       