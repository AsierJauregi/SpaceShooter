using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed; 
    [SerializeField] private Vector3 direction;
    [SerializeField] private float widthImage;
    private Vector3 positionStart;

    // Start is called before the first frame update
    void Start()
    {
        positionStart = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //dif : tells how much is left until new cicle
        float difference = (speed * Time.time) % widthImage;

        //Position refreshes startin from the initialPosition adding as much difference is left with the image
        transform.position = positionStart + difference * direction;

    }
}
