using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invaders : MonoBehaviour
{
    public invader[] prefabs;
    public int rows = 11;
    public int columns = 5;

    private void Awake()
    {
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
}
