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

    public class HelloARController : MonoBehaviour
    {
     
        public Button BR, BL,BS,Set,Back,MLeft,MRight;
        public Text hint,rotate,move;
        int count = 0,c=0,setcount=0,item;
        public Camera FirstPersonCamera;
        public GameObject DetectedPlanePrefab;
        public GameObject offchair, chandler, table, bed, bench, archair, archair2, stand, bcase, sink, sofa, couch, stove, fridge, couch2;
        public GameObject prefab;
        public GameObject SearchingForPlaneUI;
        private const float k_ModelRotation = 180.0f;
        public TrackableHit hit;
        public TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;
        private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();
        private bool m_IsQuitting = false;
        public GameObject andyObject;
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
                rotate.gameObject.SetActive(true);
                move.gameObject.SetActive(true);

                setcount = 1;
            }
            else
            {
                BL.gameObject.SetActive(false);
                BR.gameObject.SetActive(false);
                MLeft.gameObject.SetActive(false);
                MRight.gameObject.SetActive(false);
                rotate.gameObject.SetActive(false);
                move.gameObject.SetActive(false);
                setcount = 0;
            }
        }
        public void PlaceObject()
        {
            andyObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);
            DetectedPlanePrefab.SetActive(false);
            andyObject.transform.Rotate(0, k_ModelRotation, 0, Space.Self);
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);
            BS.gameObject.SetActive(false);
            Set.gameObject.SetActive(true);           
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
            Session.GetTrackables<DetectedPlane>(m_AllPlanes);
            bool showSearchingUI = true;
            for (int i = 0; i < m_AllPlanes.Count; i++)
            {
                if (m_AllPlanes[i].TrackingState == TrackingState.Tracking)
                {
                    showSearchingUI = false;

                    if (c == 0)
                    {
                        hint.gameObject.SetActive(true);
                        c++;
                    }
                    break;
                }
            }
            SearchingForPlaneUI.SetActive(showSearchingUI);
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                
                return;
            }

            if (count == 0)
            { 
               
                if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
                {
                   
                    count++;
                    if ((hit.Trackable is DetectedPlane) &&
                        Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                            hit.Pose.rotation * Vector3.up) < 0)
                    {
                        Debug.Log("Hit at back of the current DetectedPlane");

                    }
                    else
                    {
   
                        item = PlayerPrefs.GetInt("item", 1);
                        if (hit.Trackable is FeaturePoint)
                        {
                            hint.gameObject.SetActive(false);
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
                            hint.gameObject.SetActive(false);
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
                        
                    }
                }
            }
        }
        private void _UpdateApplicationLifecycle()
        {          
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("SelectionMenu");
            }
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
        private void _DoQuit()
        {
            Application.Quit();
        }
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
