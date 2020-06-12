using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tiling : MonoBehaviour
{
    public int offsetX = 2;
    public bool hasARight = false;
    public bool hasALeft = false;
    public bool reverseScale = false;
    private float spriteWidth = 0f;
    private Camera cam;
    private Transform myTranform;
    // Start is called ;before the first frame update
    void Start()
    {
        cam = Camera.main;
        myTranform = transform;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        spriteWidth = renderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasARight==false || hasALeft==false)
        {
            float camHorizontal = cam.orthographicSize * Screen.width / Screen.height;
            float posRight = (myTranform.position.x + spriteWidth / 2) - camHorizontal;
            float posLeft= (myTranform.position.x - spriteWidth / 2) +  camHorizontal;
            if (cam.transform.position.x>=posRight-offsetX && hasARight==false)
            {
                MakeNew(1);
                hasARight = true;
            }
            else if (cam.transform.position.x <= posLeft + offsetX && hasALeft == false)
            {
                MakeNew(-1);
                hasALeft = true;
            }
        }
    }
    void MakeNew(int rightorleft)
    {
        Vector3 newPos = new Vector3(myTranform.position.x + spriteWidth * rightorleft, myTranform.position.y, myTranform.position.z);
        Transform newSprite = Instantiate(myTranform, newPos, myTranform.rotation);
        if (reverseScale==true)
        {
            newSprite.localScale = new Vector3(newSprite.localScale.x * -1, newSprite.localScale.y, newSprite.localScale.z);
        }
        newSprite.parent = myTranform.parent;
        if (rightorleft>0)
        {
            newSprite.GetComponent<Tiling>().hasALeft = true;
        }
        else
        {
            newSprite.GetComponent<Tiling>().hasARight = true;

        }
    }
}
