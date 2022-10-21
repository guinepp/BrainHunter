using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileBuilder : MonoBehaviour {

	[Range(0, 100)]
	public int iniChance;
	[Range(1, 8)]
	public int birthLimit;
	[Range(1, 8)]
	public int deathLimit;
	[Range(1, 3)]
	public int maxChests;

	[Range(1, 10)]
	public int numR;

	private int[,] terrainMap;
	public Vector3Int tmpSize;
	public Tilemap topMap;
	public Tilemap botMap;
	public Tile grama;
	public Tile montanha;
	public Tile caminho;
	public Tile bau;

	int width;
	int height;

	public void doSim(int nu) {
		clearMap(false);
		width = tmpSize.x;
		height = tmpSize.y;

		if (terrainMap == null) {
			terrainMap = new int[width, height];
			initPos();
		}


		for (int i = 0; i < nu; i++) {
			terrainMap = genTilePos(terrainMap);
		}

		genChests();

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (terrainMap[x, y] == 0)
					botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), montanha);
				if (terrainMap[x, y] == 1)
					topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), grama);
				if (terrainMap[x, y] == 2)
					botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), bau);
				if (terrainMap[x, y] == 3)
					topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), caminho);
			}
		}


	}

	public void genChests() {
		for (int i = 0; i < maxChests;) {
			int col = Random.Range(1, width);
			int row = Random.Range(1, height);

			if (terrainMap[col, row] != 0 && terrainMap[col, row] != 2 && terrainMap[col, row] != 3) {
				terrainMap[col, row] = 2;
				i++;
			}
		}
	}

	public void initPos() {
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				terrainMap[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0;
			}
		}
	}

	public int[,] genTilePos(int[,] oldMap) {
		int[,] newMap = new int[width, height];
		int neighb;
		BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				neighb = 0;
				foreach (var b in myB.allPositionsWithin) {
					if (b.x == 0 && b.y == 0) continue;
					if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height) {
						neighb += oldMap[x + b.x, y + b.y];
					} else {
						neighb++;
					}
				}

				if (oldMap[x, y] == 1) {
					if (neighb < deathLimit) newMap[x, y] = 0;

					else {
						newMap[x, y] = 1;
					}
				}

				if (oldMap[x, y] == 0) {
					if (neighb > birthLimit) newMap[x, y] = 1;

					else {
						newMap[x, y] = 0;
					}
				}
			}
		}
		return newMap;
	}

	private void Start() {
		doSim(numR);
	}

	public void clearMap(bool complete) {
		topMap.ClearAllTiles();
		botMap.ClearAllTiles();
	}
}