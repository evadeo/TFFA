using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Maitre_mage : MonoBehaviour {

    private MouvementsPP mouvementPPscript;
	private CameraControl cameraScript;
    private bool talking;
    private int currentLine;
    private Rect position_window;
    //private SphereCollider sph_coll;
	private bool instr;
    public string[] textLines;
	public string instructionText;
    public Text talktextGui;
	public Text instructions;
    public KeyCode parler;


    //Check if the player is in the "zone" to talk with the maitre
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "GameController")
        {
            mouvementPPscript = collider.GetComponent<MouvementsPP>();
			cameraScript = collider.GetComponentInChildren<CameraControl>();
            talking = true;
            currentLine = 0;
        }
        else
        {
            talking = false;
        }
    }




	// Use this for initialization
	void Start () {
        //sph_coll = this.GetComponent<SphereCollider>();
		instr = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(talking)
        {
			if (instr)
				instructions.text = instructionText;
			else
				instructions.text = "";
			cameraScript.enabled = false;
			mouvementPPscript.enabled = false;
            if(Input.GetKeyDown(parler))
            {
				instr = false;
                if (currentLine < textLines.Length)
                {
                    talktextGui.text = textLines[currentLine];
                    currentLine++;
                }
                else
                {
                    currentLine = 0;
                    talktextGui.text = "";
                    talking = false;
                    mouvementPPscript.enabled = true;
					cameraScript.enabled = true;
                }

            }
        }
	}

}
