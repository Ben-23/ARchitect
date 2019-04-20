namespace GoogleARCore.Examples.HelloAR
{
    using System.Collections;
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = InstantPreviewInput;
#endif
   
    /// <summary>
    /// Controls the HelloAR example.
    /// </summary>
    public class HelloARController : MonoBehaviour
    {
     
        public Button BR, BL,BS,Set,Back,MLeft,MRight;
        public Text hint;
        int count = 0,setcount=0,item;
        /// <summary>
        /// The first-person camera being used to render the passthrough camera image (i.e. AR background).
        /// </summary>
        public Camera FirstPersonCamera;

        /// <summary>
        /// A prefab for tracking and visualizing detected planes.
        /// </summary>
        public GameObject DetectedPlanePrefab;

        /// <summary>
        /// A model to place when a raycast from a user touch hits a plane.
        /// </summary>
        public GameObject AndyPlanePrefab, offchair, chandler, table, bed, bench, archair, archair2, stand, bcase, sink, sofa, couch, stove, fridge, couch2;

        /// <summary>
        /// A model to place when a raycast from a user touch hits a feature point.
        /// </summary>
        //public GameObject AndyPointPrefab;
        public GameObject prefab;
        /// <summary>
        /// A gameobject parenting UI for displaying the "searching for planes" snackbar.
        /// </summary>
        public GameObject SearchingForPlaneUI;

        /// <summary>
        /// The rotation in degrees need to apply to model when the Andy model is placed.
        /// </summary>
        private const float k_ModelRotation = 180.0f;
        public TrackableHit hit;
        public TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;
        /// <summary>
        /// A list to hold all planes ARCore is tracking in the current frame. This object is used across
        /// the application to avoid per-frame allocations.
        /// </summary>
        private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();

        /// <summary>
        /// True if the app is in the process of quitting due to an ARCore connection error, otherwise false.
        /// </summary>
        private bool m_IsQuitting = false;
        public Vector3 moveVector;
        /// <summary>
        /// The Unity Update() method.
        /// </summary>
        GameObject andyObject;
        public void Scrnbck()
        {
            SceneManager.LoadScene("SelectionMenu");
        }
        public void Setter()
        {
            if (setcount == 0)
            {
                BL.gameObject.SetActive(true);
                BR.gameObject.SetActive(true);
                MLeft.gameObject.SetActive(true);
                MRight.gameObject.SetActive(true);
                setcount = 1;
            }
            else
            {
                BL.gameObject.SetActive(false);
                BR.gameObject.SetActive(false);
                MLeft.gameObject.SetActive(false);
                MRight.gameObject.SetActive(false);
                setcount = 0;
            }
        }
        public void PlaceObject()
        {
            // Instantiate Andy model at the hit pose.
            andyObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);
            DetectedPlanePrefab.SetActive(false);
            // Compensate for the hitPose rotation facing away from the raycast (i.e. camera).
            andyObject.transform.Rotate(0, k_ModelRotation, 0, Space.Self);

            // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
            // world evolves.
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);
            BS.gameObject.SetActive(false);
            Set.gameObject.SetActive(true);           
            // Make Andy model a child of the anchor.
            andyObject.transform.parent = anchor.transform;

        }
        public void  MoveL()
        {
            andyObject.transform.Translate(0.1f, 0, 0, Space.Self);
        }
        public void MoveR()
        {
            andyObject.transform.Translate(-0.1f, 0, 0, Space.Self);
        }
        public void RotateLeft()
        {
            andyObject.transform.Rotate(0,-1,0,Space.Self);

        }
        public void RotateRight()
        {
            andyObject.transform.Rotate(0,1, 0, Space.Self);

        }
        public void Update()
        {
            _UpdateApplicationLifecycle();

            // Hide snackbar when currently tracking at least one plane.
            Session.GetTrackables<DetectedPlane>(m_AllPlanes);
            bool showSearchingUI = true;
            for (int i = 0; i < m_AllPlanes.Count; i++)
            {
                if (m_AllPlanes[i].TrackingState == TrackingState.Tracking)
                {
                    showSearchingUI = false;
                    break;
                }
            }

            SearchingForPlaneUI.SetActive(showSearchingUI);
            hint.gameObject.SetActive(true);
            // If the player has not touched the screen, we are done with this update.
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }
            if (count == 0)
            {
                
                if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
                {
                    hint.gameObject.SetActive(false);
                    count++;
                    // Use hit pose and camera pose to check if hittest is from the
                    // back of the plane, if it is, no need to create the anchor.
                    if ((hit.Trackable is DetectedPlane) &&
                        Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                            hit.Pose.rotation * Vector3.up) < 0)
                    {
                        Debug.Log("Hit at back of the current DetectedPlane");
                    }
                    else
                    {
                        hint.gameObject.SetActive(false);
                        // Choose the Andy model for the Trackable that got hit.
                        //GameObject prefab;
                        item = PlayerPrefs.GetInt("item", 1);
                        if (hit.Trackable is FeaturePoint)
                        {
                            switch (item)
                            {
                                case 1:
                                    prefab = offchair;
                                    break;
                                case 2:
                                    prefab = chandler;
                                    break;
                                case 3:
                                    prefab = table;
                                    break;
                                case 4:
                                    prefab = bed;
                                    break;
                                case 5:
                                    prefab = bench;
                                    break;
                                case 6:
                                    prefab = archair;
                                    break;
                                case 7:
                                    prefab = archair2;
                                    break;
                                case 8:
                                    prefab = stand;
                                    break;
                                case 9:
                                    prefab = bcase;
                                    break;
                                case 10:
                                    prefab = sink;
                                    break;
                                case 11:
                                    prefab = sofa;
                                    break;
                                case 12:
                                    prefab = couch;
                                    break;
                                case 13:
                                    prefab = stove;
                                    break;
                                case 14:
                                    prefab = fridge;
                                    break;
                                case 15:
                                    prefab = couch2;
                                    break;
                                default:
                                    _ShowAndroidToastMessage("No Furniture Selected");
                                    prefab = null;
                                    break;
                            }

                        }
                        else
                        {
                            switch (item)
                            {
                                case 1:
                                    prefab = offchair;
                                    break;
                                case 2:
                                    prefab = chandler;
                                    break;
                                case 3:
                                    prefab = table;
                                    break;
                                case 4:
                                    prefab = bed;
                                    break;
                                case 5:
                                    prefab = bench;
                                    break;
                                case 6:
                                    prefab = archair;
                                    break;
                                case 7:
                                    prefab = archair2;
                                    break;
                                case 8:
                                    prefab = stand;
                                    break;
                                case 9:
                                    prefab = bcase;
                                    break;
                                case 10:
                                    prefab = sink;
                                    break;
                                case 11:
                                    prefab = sofa;
                                    break;
                                case 12:
                                    prefab = couch;
                                    break;
                                case 13:
                                    prefab = stove;
                                    break;
                                case 14:
                                    prefab = fridge;
                                    break;
                                case 15:
                                    prefab = couch2;
                                    break;
                                default:
                                    _ShowAndroidToastMessage("No Furniture Selected");
                                    prefab = null;
                                    break;
                            }

                            

                        }
                    
                        /*// Instantiate Andy model at the hit pose.
                        var andyObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);
                        DetectedPlanePrefab.SetActive(false);
                        // Compensate for the hitPose rotation facing away from the raycast (i.e. camera).
                        andyObject.transform.Rotate(0, k_ModelRotation, 0, Space.Self);

                        // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
                        // world evolves.
                        var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                        BL.gameObject.SetActive(true);
                        BR.gameObject.SetActive(true);
                        // Make Andy model a child of the anchor.
                        andyObject.transform.parent = anchor.transform;*/
                        
                    }
                }
            }
        }

        /// <summary>
        /// Check and update the application lifecycle.
        /// </summary>
        private void _UpdateApplicationLifecycle()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("SelectionMenu");
            }

            // Only allow the screen to sleep when not tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                const int lostTrackingSleepTimeout = 15;
                Screen.sleepTimeout = lostTrackingSleepTimeout;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            if (m_IsQuitting)
            {
                return;
            }

            // Quit if ARCore was unable to connect and give Unity some time for the toast to appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                _ShowAndroidToastMessage("Camera permission is needed to run this application.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                _ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
        }

        /// <summary>
        /// Actually quit the application.
        /// </summary>
        private void _DoQuit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Show an Android toast message.
        /// </summary>
        /// <param name="message">Message string to show in the toast.</param>
        private void _ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                        message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }
}
