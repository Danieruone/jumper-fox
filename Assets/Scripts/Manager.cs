using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{
    int Columns, Rows;
    int level = 0;

    public GameObject[] TilesPrefabs;
    public GameObject BgPrefab;
    public Sprite[] BgSPrites;

    public GameObject cam;
    public GameObject player;

    int[] prefabsorder;

    Canvas seleccion;

    private void Start()
    {
        //para cargar el canvas con el menu dentro del manager
        seleccion = (Canvas)FindObjectOfType(typeof(Canvas));
    }

    private void Update()
    {
        //para reiniciar y cargar nuevamente el menú de selección
        if (Input.GetKeyDown("r"))
        {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
    }
   

    //método para cargar los prefabs en el escenario
    void SetupScene() {
        int c = 0;
        for (int i = 0;i < Columns; i++) { 
                for( int j = 0; j < Rows; j++)
                {
                    if (prefabsorder[c] != 0) { 
                        Instantiate(TilesPrefabs[prefabsorder[c]], new Vector3(i,j,0), Quaternion.identity, GameObject.Find("Map").transform);
                    }
                    c++;
                }
        }
    }

    void ReadFile() {

        //Para leer el archivo dependiendo del nivel seleccionado.
        string path = "Assets/GameManagers/" + level.ToString() + ".txt";
        string linea = "";
        StreamReader reader = new StreamReader(path);

        // Para leer el background de cada nivel
        linea = reader.ReadLine();
        int Bgpos = (int)char.GetNumericValue(linea[0]);
        linea = reader.ReadLine();

        //Para definir el numero de columnas y filas de cada nivel
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
        Columns = int.Parse(columnas);
        Rows = int.Parse(filas);
        int c = 0;
        prefabsorder = new int[Columns * Rows];


        //Para leer el arreglo de configuración del nivel
        linea = reader.ReadLine();
        linea = reader.ReadLine();
        while (linea != null) { 
            for(int i = 0; i < linea.Length; i++) {
                prefabsorder[c] = (int)char.GetNumericValue(linea[i]);
                c++;
            }
            linea = reader.ReadLine();
        }

        //para cerrar el archivo y cargar los objetos en el escenario
        reader.Close();
        SetupScene();

        //para cargar el background en el escenario
        //BgPrefab.GetComponent<SpriteRenderer>().sprite = BgSPrites[Bgpos];
        Instantiate(BgPrefab, new Vector3(0, 2.4f, -5), Quaternion.identity, GameObject.Find("Map").transform);
    }

    //método public para correr con cada uno de los botones del menu de selección
    public void LoadLevel(int value) {
        level = value;
        if(seleccion.enabled == true) ReadFile();
        player.SetActive(true);
        cam.transform.SetParent(player.transform);
        seleccion.enabled = false;
    }
}
