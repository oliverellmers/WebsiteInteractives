using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Quaternion defaultAvatarRotation;

    [SerializeField]
    private float slowSpeedRotation = 0.03f;
    [SerializeField]
    private float speedRotation = 0.03f;

    private const string AVATAR_TAG = "SpinObject";

    private bool isRotating = false;

    private RaycastHit hit;

    private float time;

    void Awake()
    {
        time = Time.deltaTime;

        /*
        Camera[] cameraList = FindObjectsOfType(typeof(Camera)) as Camera[];
        foreach (Camera camObj in cameraList)
        {
            if (camObj && camObj.tag == "Untagged")
            {
                cam = camObj;
            }
        }
        */
        //defaultAvatarRotation.y = 180.0f;
    }

    // Update is called once per frame
    void Update()
    {
        MouseButtonDown();
        MouseButotnUp();
        if (Input.GetMouseButton(0) && isRotating)
        {
            RaycastHit dragingHit;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out dragingHit) && dragingHit.collider.gameObject == hit.collider.gameObject)
            {
                if (hit.collider.tag == AVATAR_TAG && transform == hit.collider.transform)
                {

                    float x = -Input.GetAxis("Mouse X");
                    float y = Input.GetAxis("Mouse Y");

                    //Quaternion rotX = (transform.rotation *= Quaternion.AngleAxis(x * speedRotation, Vector3.up));
                    Quaternion rotY = (transform.rotation *= Quaternion.AngleAxis(x * speedRotation, Vector3.forward));

                    //transform.DOLocalRotateQuaternion(rotX, 0.45f);
                    transform.DOLocalRotateQuaternion(rotY, 0.45f);
                }
            }

            
        }
        else
        {
            if (transform.rotation.y != defaultAvatarRotation.y)
            {
                SlowRotation();
            }
        }
    }

    private void MouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.tag == AVATAR_TAG && transform == hit.collider.transform)
                {

                    isRotating = true;
                }
            }
        }
    }

    private void MouseButotnUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
            hit = new RaycastHit();
        }
    }

    private void SlowRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              defaultAvatarRotation,
                                              slowSpeedRotation * Time.deltaTime);
    }
}