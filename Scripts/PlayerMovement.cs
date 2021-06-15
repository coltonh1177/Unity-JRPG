using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	[SerializeField]
	private float speed;

	[SerializeField]
	private Animator animator;

    //so that we can use on screen buttons to move
    private bool upButtonPressed = false;
    private bool downButtonPressed = false;
    private bool leftButtonPressed = false;
    private bool rightButtonPressed = false;

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

        //for moving horizontally
        float newVelocityX = 0f;
        if ((moveHorizontal < 0 && currentVelocity.x <= 0) || leftButtonPressed) {
            newVelocityX = -speed;
            animator.SetInteger("DirectionX", -1);
        } else if ((moveHorizontal > 0 && currentVelocity.x >= 0) || rightButtonPressed) {
			newVelocityX = speed;
			animator.SetInteger ("DirectionX", 1);
		} else {
			animator.SetInteger ("DirectionX", 0);
		}
        
        //for moving vertically
		float newVelocityY = 0f;
		if ((moveVertical < 0 && currentVelocity.y <= 0) || downButtonPressed) {
			newVelocityY = -speed;
			animator.SetInteger ("DirectionY", -1);
		} else if ((moveVertical > 0 && currentVelocity.y >= 0) || upButtonPressed) {
			newVelocityY = speed;
			animator.SetInteger ("DirectionY", 1);
		} else {
			animator.SetInteger ("DirectionY", 0);
		}

		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (newVelocityX, newVelocityY);

        //reset the buttons
        resetButtons();
	}

    public void upButton()
    {
        upButtonPressed = true;
    }

    public void downButton()
    {
        downButtonPressed = true;
    }

    public void leftButton()
    {
        leftButtonPressed = true;
    }

    public void rightButton()
    {
        rightButtonPressed = true;
    }

    public void resetButtons()
    {
        upButtonPressed = false;
        downButtonPressed = false;
        leftButtonPressed = false;
        rightButtonPressed = false;
    }
}
