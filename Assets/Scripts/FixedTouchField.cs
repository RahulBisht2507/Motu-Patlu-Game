using UnityEngine;
using UnityEngine.EventSystems;




    public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        bool panel = false;
        public Vector2 Look;
        public float Sensitivity = 0.15f;
        int fingerid = -1;

        void Update()
        {
            pointerupdate();
        }

        private void pointerupdate()
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch t = Input.GetTouch(i);
                    if (panel && t.fingerId == fingerid)
                    {
                        if (t.phase == TouchPhase.Moved)
                        {
                            Look = new Vector2(t.deltaPosition.x, t.deltaPosition.y) * Sensitivity;
                            break;
                        }
                        else if (t.phase == TouchPhase.Stationary)
                        {
                            Look = new Vector2();
                        }
                    }
                    else
                    {
                        Look = new Vector2();
                    }
                }
            }
            else if (!panel) { Look = new Vector2(); }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            panel = true;
            fingerid = eventData.pointerId;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            panel = false;
            fingerid = -1;
        }
    }
