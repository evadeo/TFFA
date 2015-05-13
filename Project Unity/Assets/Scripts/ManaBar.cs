using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour {
	
	public RectTransform manaTransform;
	private float cachedY;
	private float minXValue;
	private float maxXValue;
	private float width;
	public int CurrentMana
	{
		get {return PersoPrincipal.Mana; }
		set{PersoPrincipal.Mana = value;}
	}
	private float currentXValue; 
	private int maxMana;
	public Text manaText;
	public Image visualMana;
	
	// Use this for initialization
	void Start () 
	{
		width = manaTransform.rect.width*0.91f;
		cachedY = manaTransform.position.y;
		maxXValue = manaTransform.position.x;
		minXValue = (manaTransform.position.x - width)/10;
	}
	
	// Update is called once per frame
	void Update () {
		maxMana = PersoPrincipal.Max_Mana;
		HandleMana ();
	}
	
	private void HandleMana(){
		manaText.text = "Mana: " + CurrentMana;
		currentXValue = MapValues (CurrentMana, 0, maxMana, minXValue, maxXValue);
		manaTransform.position = new Vector3 (currentXValue, cachedY);
		
		if (CurrentMana > maxMana / 2) 
			visualMana.color = new Color32 ((byte)MapValues(CurrentMana, maxMana / 2, maxMana, 255, 0), 0, 255, 255);
		else
			visualMana.color = new Color32(255, 0, (byte)MapValues(CurrentMana, 0, maxMana/2, 0, 255) , 255);
		
	}
	
	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax){
		return ((x-inMin)*(outMax-outMin) / (inMax - inMin) + outMin);
	}
}
