using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarColor : MonoBehaviour
{
    /*private Renderer _render;
    private int material;

    public Gradient Surce;
    private int gradiente;*/
    public GameObject[] cubos;
    Color[] colours = {Color.red, Color.blue, Color.green, Color.black};

    // Start is called before the first frame update
    void Start()
    {


        int i = Random.Range(0, colours.Length);
        cubos[0].GetComponent<Renderer>().material.color = colours[i]; ;

        /*_render= GetComponent<Renderer>();
        materialCount = _render.material.Length;
        gradiente = Surce.colorKeys.Length;
        RandomColor(); */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Nuevo(int i){
        int x = Random.Range(0, colours.Length);
        while(x =! i){
            int x = Random.Range(0, colours.Length);
        }
            return x;
        }
    }
    /*void RandomColor(){
        _render.material[i].color= Surce.Evaluate(Random.Range(0,1));
    }
    Color color = new Color(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1), 1);
    GameObject boxInit = Instantiate(box, transform.position, Quaternion.identity);
    boxInit.GetComponent<Renderer>().material.color = color;*/
}
