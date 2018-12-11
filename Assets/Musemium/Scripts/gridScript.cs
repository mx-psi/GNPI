using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridScript : MonoBehaviour {
	public int numberOfColumns = 50; // number of columns for the grid
	public int numberOfRows = 50; // number of rows for the grid
	public float gridWidth = 10.0f; // Witdh in distance units of the grid
	public float zPos = 0.0f; // Where the grid will be placed on Z plane
	public GameObject prefab;

	// Use this for initialization
	void Start () {
		createGrid ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void createGrid() {
		float columnProportion = gridWidth / numberOfColumns; // Each quad should have this amount of space
		float rowProportion = gridWidth / numberOfRows;


		for (int i = 0; i < numberOfColumns; i++) {//loop 1 to loop through columns
			for (int j = 0; j < numberOfRows; j++) {//loop 2 to loop through rows
				GameObject plane = Instantiate(prefab); //create a quad primitive as provided by unity
				plane.transform.position = new Vector3(i * columnProportion - gridWidth/2, j * rowProportion, zPos); //position the newly created quad accordingly
				Vector3 proportion = new Vector3(columnProportion, rowProportion, 1);
				plane.transform.localScale = proportion;
				//Debug.Log(plane.transform.position);

			}
		}
	}

}
