using UnityEngine;
using System.Collections;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
public class CameraShareScript : MonoBehaviour
{
    public float edgeDelta = 10;                        //edge width border
    public float rmEdgeDelta = 500;
    public float speed = 5;
    public Vector3 spritePos;


    public Vector3 Dir;

    public GameObject Circle;
    private bool isEdge = false;
    // Use this for initialization
    void Start()
    {
        spritePos = Circle.transform.position;
        Dir = spritePos - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        spritePos = Circle.transform.position;
        Dir = spritePos - transform.position;

        //Debug.Log(Input.mousePosition.x+", "+Input.mousePosition.y+": "+(Screen.width-rmEdgeDelta)+", "+(Screen.height-rmEdgeDelta));

        if (Mathf.Abs(Dir.x) < 9 && Mathf.Abs(Dir.y) < 4.5 && !isEdge)
        {
            //Debug.Log("Not Edging");
            if (Input.mousePosition.x >= Screen.width - edgeDelta)      // Check if on the right edge

                { transform.position += Vector3.right * Time.deltaTime * speed; }

            else if (Input.mousePosition.x <= edgeDelta)      // Check if on the left edge

                { transform.position += Vector3.left * Time.deltaTime * speed; }

            else if (Input.mousePosition.y >= Screen.height - edgeDelta)      // Check if on the top edge

                { transform.position += Vector3.up * Time.deltaTime * speed; }

            else if (Input.mousePosition.y <= edgeDelta)      // Check if on the bottom edge

                { transform.position += Vector3.down * Time.deltaTime * speed; }
            
            else if(!(isEdge && Input.mousePosition.x <= Screen.width - rmEdgeDelta
                && (Mathf.Abs(Input.mousePosition.x) + Mathf.Abs(Input.mousePosition.y)) >= rmEdgeDelta
                && Input.mousePosition.y <= Screen.height - rmEdgeDelta)) { transform.Translate(Dir.x * speed / 100, Dir.y * speed / 100, 0); }

        }
        else
        {
            //Debug.Log("EDGING");
            isEdge = true;
            
            if (isEdge && Input.mousePosition.x <= (Screen.width - rmEdgeDelta)
                && Input.mousePosition.x >= rmEdgeDelta && Input.mousePosition.y >= rmEdgeDelta
                && Input.mousePosition.y <= (Screen.height - rmEdgeDelta))
            {
                transform.Translate(Dir.x * speed / 100, Dir.y * speed / 100, 0);
            }
            if(Mathf.Abs(Dir.x) + Mathf.Abs(Dir.y) < .1f)
            {
                isEdge = false;
            }

        }
        //transform.Translate(Dir.x, Dir.y, 0);
        //Debug.Log(Time.deltaTime);
    }
}