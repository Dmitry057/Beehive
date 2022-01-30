using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Buyer : MonoBehaviour
{
    [HideInInspector] 
    public bool BlockClicks = true;

   
    public bool isChoosen = false;

    [SerializeField]
    private CanvasAtributes _CanvasAttributes;


    [HideInInspector] public UserResources userResources;

    [HideInInspector] public List<Chunk> choosedChunks = new List<Chunk>();

    private List<CostRes> costRes;

    private void Start()
    {
        BlockClicks = true;
        _CanvasAttributes.ShovelButton.onClick.AddListener(StartToSelectChunks);
    }
    public void CheckOnCost(List<CostRes> costResources)
    {
        isChoosen = true;
        costRes = costResources;
        bool a = true;
        foreach(CostRes costRes in costResources)
        {
            foreach(Resource userRes in userResources.resources)
            {
                if (userRes.name == costRes.name)
                {
                    a &= costRes.CheckOnProsperity(userRes.value / choosedChunks.Count);
                }
            }
        }

        if (!a)
        {
            _CanvasAttributes.BoughtUnsuccesfull();
        }
        else
        {
            _CanvasAttributes.BoughtSuccesfull();
        }
    }
    public void StartToSelectChunks()
    {
        BlockClicks = false;
        _CanvasAttributes.ShowShovel();
    }
    public void Return()
    {
        isChoosen = false;
        BlockClicks = true;
        foreach (Chunk chunk in choosedChunks)
        {
            chunk.LockChunk();
            
        }
        choosedChunks.Clear();
    }
    public void SellOut()
    {
        isChoosen = false;
        foreach (CostRes costRes in costRes)
        {
            foreach (Resource userRes in userResources.resources)
            {
                if (userRes.name == costRes.name)
                {
                    userRes.SetValue(-costRes.value * choosedChunks.Count);
                }
            }
        }
        foreach(Chunk chunk in choosedChunks)
        {
            chunk.UnlockChunk();
        }
        BlockClicks = true;
        choosedChunks.Clear();
    }
   
    
}
[System.Serializable]
public class CostRes
{
    public string name;
    public int value;

    public bool CheckOnProsperity(int userResValue) => userResValue >= value;

}
