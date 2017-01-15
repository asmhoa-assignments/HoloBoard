using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class writingScript : MonoBehaviour {

	public Texture2D texture;
	public int count = 0, secondCount = 0;
	public List<List<int[]>> strokeArray = new List<List<int[]>>();

	// Use this for initialization
	void Start () {
		Screen.SetResolution(1024, 1024, false);
		GetComponent<Renderer>().material.mainTexture = new Texture2D(1024, 1024);
		texture = GetComponent<Renderer>().material.mainTexture as Texture2D;
		strokeArray.Add(new List<int[]>());
	}

	// void onMouseDrag() {
	// 	InvokeRepeating("Draw", 0.1f, 0.3f);
	// }

	void OnMouseUp() {
		count = count + 1;
		strokeArray.Add(new List<int[]>());
		secondCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		int valx;
		int valy;
       	valx = (int)Input.mousePosition.x;
        valy = (int)Input.mousePosition.y;

		if (Input.GetMouseButton(1)) {
			if(Input.GetKey(KeyCode.S)) {
				string text = "[";
				// Send
				foreach(List<int[]> listElem in strokeArray) { //for each stroke
					text += "[";
					if(listElem.Count != 0) { // foreach coordinate
						foreach(int[] arrElm in listElem) {
							text += "["+arrElm[0] + ", " + arrElm[1] + "], ";
						}
					}
					text = text.Substring(0, text.Length - 2);
					text += "], ";
				}
				//Cut off last comma
				text = text.Substring(0, text.Length - 4) + "]";
				System.IO.File.WriteAllText(@"Resources/strokes.txt", text);
				
				Process proc = new System.Diagnostics.Process();
				proc.StartInfo.FileName = "/usr/local/bin/node";
				proc.StartInfo.Arguments = "'/Users/Amol/Final Math/Resources/index.js'";
				proc.StartInfo.UseShellExecute = false; 
				proc.StartInfo.RedirectStandardOutput = true;
				proc.Start();
				proc.WaitForExit();

				//Read file
				string wolframLink = System.IO.File.ReadAllText(@"/Users/Amol/Final Math/Resources/log.txt");
				UnityEngine.Debug.Log(wolframLink);


			} // Clear
				Texture2D texture2 = new Texture2D(texture.width, texture.height);
				GetComponent<Renderer>().material.mainTexture = texture2;
				texture2.Apply();
				texture = GetComponent<Renderer>().material.mainTexture as Texture2D;
		} else if ((Input.GetMouseButton(0)) && Time.frameCount % 1 == 0) {
			Texture2D texture2 = new Texture2D(texture.width, texture.height);
			for (int j = 0; j <= texture.height; j++) {
            	for (int i = 0; i <= texture.width; i++) {
            		texture2.SetPixel(-i, -j, texture.GetPixel(-i,-j));
            	}
        	}
        	
			GetComponent<Renderer>().material.mainTexture = texture2;
			for (int j = (int)valy - 20; j <= (int)valy + 20; j++) {
            	for (int i = (int)valx - 20; i <= (int)valx + 20; i++) {
            		texture2.SetPixel(-i, -j, Color.black);
            	}
        	}
        	strokeArray[count].Add(new int[2]);
        	strokeArray[count][secondCount][0] = (int)valx;
        	strokeArray[count][secondCount][1] = (int)valy;
        	secondCount++;

        	texture2.Apply();
        	texture = GetComponent<Renderer>().material.mainTexture as Texture2D;
		}
	}
}
