using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;
public class InstantChunks : MonoBehaviour
{
    public GameObject ChunkPrefab;
    [SerializeField] private UserResources Resources;

    [SerializeField] private float offset = 5;

    [MinMaxSlider(-100, 100)]
    [SerializeField] private Vector2 xAxis;

    [MinMaxSlider(-100, 100)]
    [SerializeField] private Vector2 yAxis;


    private int _layer = 0;
    private int _id;

    private List<Chunk> _chunksMap = new List<Chunk>();
    
    private void Awake()
    {
        CreateNet();
    }

    private void CreateNet()
    {
        float currentX = xAxis.x;
        float currentY = yAxis.x;

        while (currentY <yAxis.y)
        {
            while(currentX < xAxis.y)
            {
                
                GameObject obj = Instantiate(ChunkPrefab, new Vector3(currentX, currentY, 0), Quaternion.identity);
                obj.transform.SetParent(transform);

                Chunk chunk = obj.GetComponent<Chunk>();
                chunk.Id = _id;
                chunk.res = Resources;
                _chunksMap.Add(chunk);
               
                currentX += offset;
                _id++;
            }

            currentX = _layer % 2 == 0 ? xAxis.x - offset * 0.5f: xAxis.x;
           
            currentY += offset-1;
            _layer++;
        }
        SetNearChunks();
    }
    private void SetNearChunks()
    { 
        foreach(Chunk neededChunk in _chunksMap)
        {
            foreach(Chunk currentChunk in _chunksMap)
            {
                if (Vector3.Distance(neededChunk.transform.position, currentChunk.transform.position) <= offset && neededChunk != currentChunk)
                {
                    currentChunk.ChunksNearMe.Add(neededChunk);
                }
            }
        }
    }
    public List<Chunk> GetChunks() => _chunksMap;
}
