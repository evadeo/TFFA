using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class XpBar : MonoBehaviour 
{
		public RectTransform xpTransform;
		private float cachedY;
		private float minXValue;
		private float maxXValue;
		private float width;

		public int CurrentXp
	    {
			get {return PersoPrincipal.Current_Xp; }
		}

		private float currentXValue; 
		private int max_Xp;
		public Text xpText;
		public Image visualXp;
		
		// Use this for initialization
		void Start () 
		{
			width = xpTransform.rect.width*0.91f;
			cachedY = xpTransform.position.y;
			maxXValue = xpTransform.position.x;
			minXValue = (xpTransform.position.x - width)/10;
		}
		
		// Update is called once per frame
		void Update () {
			max_Xp = PersoPrincipal.Max_Xp;
			HandleXp ();
		}
		
		private void HandleXp(){
			xpText.text = " level: " + PersoPrincipal.level;
			currentXValue = MapValues (CurrentXp, 0, max_Xp, minXValue, maxXValue);
			xpTransform.position = new Vector3 (currentXValue , cachedY);
			 
			visualXp.color = new Color32 (255, 255, 0, (byte)MapValues(CurrentXp, 0, CurrentXp, 0, 255));
		}

		private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
	    {
			return ((x-inMin)*(outMax-outMin) / (inMax - inMin) + outMin);
		}
}
