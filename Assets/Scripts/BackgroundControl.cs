using UnityEngine;
using System.Collections;

public class BackgroundControl : MonoBehaviour {

    public GameObject background1;
    public GameObject background2;
	// Use this for initialization
	void Start ()
    {



	} // end start
	

	// Update is called once per frame
	void Update ()
    {
        float tempx = background1.transform.position.x - 0.1f;
        float tempy = background1.transform.position.y;
        float tempz = background1.transform.position.z;
        background1.transform.position = new Vector3(tempx, tempy, tempz);

        tempx = background2.transform.position.x - 0.1f;
        tempy = background2.transform.position.y;
        tempz = background2.transform.position.z;
        background2.transform.position = new Vector3(tempx,tempy,tempz);

        if(background1.transform.position.x <= -40)
            background1.transform.position = new Vector3(39.9f, 0.0f, 1.0f);

        if (background2.transform.position.x <= -40)
            background2.transform.position = new Vector3(39.9f, 0.0f, 1.0f);

	} // end update

} // end class
