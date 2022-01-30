using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField]
    private GameObject Lock;

    [SerializeField]
    private Color LockedColor;

    [SerializeField]
    private Color UnlockedColor;

    [SerializeField]
    private Color HiddedColor;


    [SerializeField]
    private Color SelectedColor;

    public int Id;

    [SerializeField]
    private List<CostRes> CostReses;

    [HideInInspector]
    public bool IsOpen = false;

    [HideInInspector]
    public bool IsHidden;

    [SerializeField]
    private bool IsChoosen;

   // [HideInInspector]
    public List<Chunk> ChunksNearMe;

    [HideInInspector]
    public UserResources res;

    private Buyer buyer;
    private void Start()
    {
        buyer = res.buyer;
    }
    public void ButtonFunction()
    {
        if (!IsHidden && !IsOpen && !buyer.BlockClicks)
        {
            if (IsChoosen) Unchoose();
            else TryToBuy();
        } 
    }
    public void UnlockChunk()   //Show unlocked chunk
    {
        if (IsHidden == false)
        {
            IsOpen = true;
            IsChoosen = false;

            SetColor(UnlockedColor, false);
            ShowNearChunks();
            PlayerPrefs.SetInt("chunk" + Id.ToString(), 1);
        }
    }

    public void LockChunk()     //Show locked chunk
    {
        IsChoosen = false;
        IsHidden = false;
        IsOpen = false;

        SetColor(LockedColor, true);
    }
    public void SelectChunk()
    {
        IsChoosen = true;
        IsHidden = false;
        IsOpen = false;

        SetColor(SelectedColor, true);
        buyer.choosedChunks.Add(this);
        buyer.CheckOnCost(CostReses);
    }
    public void HideChunk()
    {
        IsHidden = true;
        SetColor(HiddedColor, false);
    }

    public void TryToBuy()
    {
        bool isNearOpen = false;
        foreach (Chunk chunk in ChunksNearMe)
        {
            isNearOpen |= chunk.IsChoosen;
            Debug.Log(isNearOpen);
        }
        isNearOpen |= !buyer.isChoosen;
        Debug.Log(isNearOpen + "final" + buyer.isChoosen);

        if (isNearOpen)
        {
            SelectChunk();
        }
    }
    public void Unchoose()
    {
        IsChoosen = false;
        if (buyer.choosedChunks.Count > 1)
        {
            buyer.choosedChunks.Remove(this);
            buyer.CheckOnCost(CostReses);
        }
        else
        {
            buyer.isChoosen = false;
            buyer.choosedChunks.Clear();
        }
        LockChunk();
    }
    private void SetColor(Color color, bool HaveLock)
    {
        GetComponent<SpriteRenderer>().color = color;
        Lock.SetActive(HaveLock);
    }
    public void ShowNearChunks()
    {
        foreach (Chunk chunkNearMe in ChunksNearMe)
        {
            if (!chunkNearMe.IsOpen)
            {
                chunkNearMe.LockChunk();
            }
        }
    }
}
