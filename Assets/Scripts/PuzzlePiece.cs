using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    private bool dragging,placed;
    private Vector2 offset,original_pos;
    public Camera Canvas_camera;
    AudioManager audiomanager;
    private PuzzleSlot _slot;
    StarScript star;
    

    public void Init(PuzzleSlot slot)
    {
        _renderer.sprite = slot.Renderer.sprite;
        _slot = slot;
    }
    void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
        //original_pos = transform.parent.transform.localPosition;
        //Debug.Log(this.transform.parent.transform.localPosition.x);
        Canvas_camera = GameObject.FindWithTag("canvas_camera").GetComponent<Camera>();
        star = FindObjectOfType<StarScript>();
    }
    void Update()
    {
        if (!dragging) return;
        var mousePosition= GetMousePos();
        transform.position = mousePosition - offset;
    }
     void OnMouseDown()
     {
         dragging = true;
         offset = GetMousePos() - (Vector2)transform.position;
         //pickup sound
     }
    void OnMouseUp()

    {
        if (_slot != null)
        {
            if (Vector2.Distance(transform.position, _slot.transform.position) < 2)
            {
               star.progress();
                transform.position = _slot.transform.position;
                audiomanager.Play("puzzleplaced");
                audiomanager.Play(_slot.name);
                
            }
           
           //else
           // {

                //transform.position = original_pos;
                //Debug.Log(transform.position);
                
                //drop sound
            //}
        }
        dragging = false;


    }
    Vector2 GetMousePos()
    {
        return Canvas_camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
