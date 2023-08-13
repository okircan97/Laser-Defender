using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is to make background move.
public class BackgroundScroller : MonoBehaviour
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////

    [SerializeField] float backgroundScrollSpeed = 0.05f;
    Material myMaterial;
    Vector2 offSet;


    //////////////////////////////////
    ///////// START & UPDATE /////////
    //////////////////////////////////
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0, backgroundScrollSpeed);
    }

    void Update()
    {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
