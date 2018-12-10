using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridScript : MonoBehaviour {
	public int NumberOfColumns = 10; // number of columns for the grid
	public int NumberOfRows = 10; // number of rows for the grid
	public GameObject prefab;

	// Use this for initialization
	void Start () {
		createGrid ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void createGrid()
	{
		for (int i = 0; i < NumberOfColumns; i++) {//loop 1 to loop through columns
			for (int j = 0; j < NumberOfRows; j++) {//loop 2 to loop through rows
				GameObject plane = Instantiate(prefab); //create a quad primitive as provided by unity
				plane.transform.position = new Vector3(i, j, 0); //position the newly created quad accordingly
			}
		}
	}

}
