using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SufflingCards : MonoBehaviour
{
    // SufflingCards
    public List<int> Deck = new List<int>();

    public static SufflingCards Instance;

    public int _suffleRandomIndex, _nextIndex, cardRange = 3;

    public Transform _suffleCardParent,_carryCardParent;

    [SerializeField] CardManager _card;
   
    private CardManager _suffleCard;
    private CardManager _carryForwadCard;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Deck[0] = 2; Deck[1] = 4; Deck[2] = 8; Deck[3] = 16; Deck[4] = 32; Deck[5] = 64; Deck[6] = 128; Deck[7] = 256; Deck[8] = 512; Deck[9] = 1024; Deck[10] = 2048;
        InstatiateFirstTime();
    }

    void InstatiateFirstTime()
    {
        _nextIndex = Random.Range(0, cardRange);

        Debug.Log("NextIndex : " + _nextIndex);

        _carryForwadCard = Instantiate(_card, _carryCardParent);

        _carryForwadCard.transform.GetChild(0).GetChild(0).GetComponent<Canvas>().sortingOrder = 2;

        _carryForwadCard.name = Deck[_nextIndex].ToString();
        _carryForwadCard.SetData(_nextIndex);
        _carryForwadCard.transform.position += new Vector3(0.35f, 0, 0);

        _suffleRandomIndex = Random.Range(0, 3);

        Debug.Log("SuffleIndex : " + _suffleRandomIndex);

        _suffleCard = Instantiate(_card, _suffleCardParent);
        _suffleCard.SetData(_suffleRandomIndex);
        _suffleCard.name = Deck[_suffleRandomIndex].ToString();
        _suffleCard.GetComponent<BoxCollider2D>().enabled = false;
    }


    public void NextMove()
    {
            _nextIndex = _suffleRandomIndex;
            _carryForwadCard = Instantiate(_card, _carryCardParent);

            _carryForwadCard.transform.GetChild(0).GetChild(0).GetComponent<Canvas>().sortingOrder = 2;

            _carryForwadCard.name = Deck[_nextIndex].ToString();
            _carryForwadCard.SetData(_nextIndex);
            _carryForwadCard.transform.position += new Vector3(0.35f, 0, 0);

            _suffleRandomIndex = Random.Range(0, cardRange);
            _suffleCard.SetData(_suffleRandomIndex);
            _suffleCard.name = Deck[_suffleRandomIndex].ToString();
            _suffleCard.GetComponent<BoxCollider2D>().enabled = false;
       
    }
}
