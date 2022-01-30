using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrCell : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private Color _acceptColor = new Color(0, 1, 0.2f, 0.2f);
    private Color _errorColor = new Color(1, 0, 0, 0.6f);
    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    public bool FindFreeCells()
    {
        LayerMask mask = LayerMask.GetMask("Chunk");
        Vector3 start = transform.position;
        Vector3 direction = transform.forward;
        RaycastHit2D hit = Physics2D.Raycast(start, direction, 1, mask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<Chunk>().IsOpen)
            {
                _sprite.color = _acceptColor;
                return true;
            }
           
        }
        _sprite.color = _errorColor;
        return false;
    }
}
