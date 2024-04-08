using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invaders : MonoBehaviour
{
    public invader[] prefabs;
    public int rows = 11;
    public int columns = 5;

    public float speed = 0.5f;

    private Vector3 _direc = Vector2.right;
    private void Awake()
    {
      // spawn the mobs in row and column by calling those prefabs
        for(int row = 0; row< rows; row++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width/2, -height/2);
            Vector3 rowPos = new Vector3(centering.x,centering.y + (row*2.0f),0.0f);

            for (int column = 0; column < columns; column++)
            {
                invader inVader = Instantiate(this.prefabs[row], this.transform);
                Vector3 pos = rowPos;
                pos.x += column * 2.0f;
                inVader.transform.position = pos;
            }
        }
    }

    private void Update()
    {
        // spawn the mobs that moving to right
        this.transform.position += _direc * this.speed * Time.deltaTime;

        // set the min and max of x coordinate point to let the mobs stop
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector2.right);

        // spawn loop that detect left and right edge
        foreach(Transform invader in this.transform)
        {
            if(! invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            // if move to max right coordinate x move downward by 1
            if(_direc == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f))
            {
                AdvanceRow();
            }

            else if (_direc == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }

    //flip the direction of the mobs
    void AdvanceRow()
    {
        _direc *= -1.0f;

        Vector3 pos = transform.position;

        pos.y -= 1.0f;

        this.transform.position = pos;
    }
}
