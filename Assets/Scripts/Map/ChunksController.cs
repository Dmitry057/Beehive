using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof (InstantChunks))]
public class ChunksController : MonoBehaviour
{
    private List<Chunk> Chunks;
    private InstantChunks _IC;
     
    private void Start()
    {
        _IC = GetComponent<InstantChunks>();
        Chunks = _IC.GetChunks();

        //PlayerPrefs.DeleteAll();

        HideAllChunks();
        ShowOpenedChunks();
    }
    private void HideAllChunks()
    {
        foreach(Chunk chunk in Chunks)
        {
            chunk.HideChunk();
        }
    }
    private void ShowOpenedChunks()
    {
        foreach(Chunk chunk in Chunks)
        {
            ShowFirstChunk(chunk, 199);
            if (PlayerPrefs.GetInt("chunk" + chunk.Id.ToString()) == 1)
            {
                chunk.IsHidden = false;
                chunk.UnlockChunk();
            }
        }
    }
   
    private void ShowFirstChunk(Chunk chunk, int centerId)
    {
        if (centerId == chunk.Id)
        {
            chunk.IsHidden = false;
            chunk.UnlockChunk();
            
        }
    }
    
}
