using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapController : MonoBehaviour
{
    int columns, rows;
    int level = 0;

    [Header("Prefabs setup")]
    public GameObject[] TilesPrefabs;
    public GameObject BgPrefab;
    public Sprite[] BgSPrites;

    [Header("Interface and player")]
    public GameObject menu;
    public GameObject HUD;
    public GameObject player;

    int[] prefabsOrder;

    // Get the prefabs into scene
    void SetupScene() {
        int c = 0;
        for (int i = 0;i < columns; i++) { 
                for( int j = 0; j < rows; j++)
                {
                    if (prefabsOrder[c] != 0) { 
                        Instantiate(TilesPrefabs[prefabsOrder[c]], new Vector3(i,j,0), Quaternion.identity, GameObject.Find("Map").transform);
                    }
                    c++;
                }
        }
    }

    void ReadFile() {
        // Read the file into Android systems
        string linea = "";
        TextAsset path = Resources.Load<TextAsset>(level.ToString());
        string textContent = path.text;
        StringReader reader = new StringReader(textContent);

        // Read the background for each level
        linea = reader.ReadLine();
        int Bgpos = (int)char.GetNumericValue(linea[0]);
        linea = reader.ReadLine();

        // Define the numbers of columns and rows
        string columnas = "";
        int j = 0;
        linea = reader.ReadLine();
        string digito = linea[j].ToString();
        while (digito != " ")
        {
            columnas = columnas + digito;
            j++;
            digito = linea[j].ToString();
        }

        string filas = "";
        j = 0;
        linea = reader.ReadLine();
        digito = linea[j].ToString();
        while (digito != " ")
        {
            filas = filas + digito;
            j++;
            digito = linea[j].ToString();
        }
        columns = int.Parse(columnas);
        rows = int.Parse(filas);
        int c = 0;
        prefabsOrder = new int[columns * rows];


        // Read the setup file for the level
        linea = reader.ReadLine();
        linea = reader.ReadLine();
        while (linea != null) { 
            for(int i = 0; i < linea.Length; i++) {
                prefabsOrder[c] = (int)char.GetNumericValue(linea[i]);
                c++;
            }
            linea = reader.ReadLine();
        }

        // Close the file and setup the scene
        reader.Close();
        SetupScene();

        Instantiate(BgPrefab, new Vector3(0, 2.4f, -5), Quaternion.identity, GameObject.Find("Map").transform);
    }

    // Load the level from method
    public void LoadLevel(int value) {
        level = value;
        if(menu.activeSelf == true) ReadFile();
        player.SetActive(true);
        menu.SetActive(false);
        HUD.SetActive(true);
    }
}