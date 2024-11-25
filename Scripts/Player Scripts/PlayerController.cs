using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; //speed at which is moves
    public float min_X, max_X; //constraints so it doesnt go off screen


    void Start()
    {
        
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f) //checking the left and right arrow keys, so if you press left it'll be -1 and right it'll be 1
        { //right
            Vector3 temp = transform.position; //grab the temp value for transforming
            temp.x += speed * Time.deltaTime; //grab the speed value with real time adding toward the transform position
            transform.position = temp;

            if (temp.x > max_X) //keeps the position stuck at max_X
            {
                temp.x = max_X;
            }
            transform.position = temp;
        }

        else if (Input.GetAxisRaw("Horizontal") < 0f)
        { //left
            Vector3 temp = transform.position; //grab the temp value for transforming
            temp.x -= speed * Time.deltaTime; //grab the speed value with real time adding toward the transform position

            if (temp.x < min_X) //keeps the position stuck at max_X
            {
                temp.x = min_X;
            }
            transform.position = temp;
        }
    }

} //class
