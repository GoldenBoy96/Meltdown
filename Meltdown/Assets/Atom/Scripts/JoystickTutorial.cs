using UnityEngine;

public class JoystickTutorial : MonoBehaviour
{
    public GameObject tutorialUI;
    public GameObject fingerPointer;
    public Joystick joystick;
    private bool _hasMoved = false;


    void Update()
    {
        if (!_hasMoved && (Mathf.Abs(joystick.Horizontal) > 0.2f || Mathf.Abs(joystick.Vertical) > 0.2f))
        {
            _hasMoved = true;
            tutorialUI.SetActive(false);
            fingerPointer.SetActive(false);
        }
    }
}
