using UnityEngine;
using System.Collections;

public class CreateWalls : MonoBehaviour {

    [SerializeField]
	private GenerateGrid grid;

    bool isWall;

	void OnMouseDown()
	{
		string [] splitter = this.gameObject.name.Split (',');
		if(!isWall)
		{
			grid.AddWall(int.Parse(splitter[0]),int.Parse(splitter[1]));
			isWall = true;
			this.GetComponent<Renderer>().material.color = Color.red;
            this.gameObject.layer = 8;
            this.tag = "Wall";
            this.GetComponent<Collider2D>().isTrigger = false;
		}
		else
		{
            grid.RemoveWall(int.Parse(splitter[0]),int.Parse(splitter[1]));
			isWall = false;
			this.GetComponent<Renderer>().material.color = Color.white;
            this.tag = "GridBox";
        }
		

	}
}
