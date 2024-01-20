using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // BlockManager

    public List<Transform> AllCorrectPositions;
    public List<Transform> allFourBlockList;

    public List<GameObject> Block1;
    public List<GameObject> Block2;
    public List<GameObject> Block3;
    public List<GameObject> Block4;

    public List<List<GameObject>> AllBlockManager = new List<List<GameObject>>();

    public static BlockManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AllBlockManager.Add(Block1);
        AllBlockManager.Add(Block2);
        AllBlockManager.Add(Block3);
        AllBlockManager.Add(Block4);
        // AllBlockManager[0] = Block1; AllBlockManager[1] = Block2; AllBlockManager[2] = Block3; AllBlockManager[3] = Block4;
    }
}
