using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace RPGRules.UI
{
    public class Tooltip : MonoBehaviour
    {
        [Tooltip("This is the parent object to turn on and off")]
        public GameObject tooltipObject;
        [Tooltip("The text to change")]
        public TextMeshProUGUI toolTipText;
        public Image toolTipImage;
        public IToolTip current;
        RectTransform toolTipTransform;
        Vector2 lastMousePos;

        // this is used for set items, where to give a correct tooltip message we need to know what collection we're a part of.
        public static UnityEngine.Object[] context;
        public static Tooltip instance;

        // Use this for initialization
        void Start()
        {
            toolTipTransform = tooltipObject.GetComponent<RectTransform>();
            tooltipObject.SetActive(false);
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 pos = Input.mousePosition;
            if (pos != lastMousePos)
            {
                // look under the mouse for a tooltip source
                current = null;

                GameObject go = GetObjectUnderPos(pos);
                if (go)
                {
                    // found an object...
                    current = go.GetComponent<IToolTip>();
                    if (current == null && go.transform.parent)
                        current = go.transform.parent.GetComponent<IToolTip>();
                }
                lastMousePos = pos;
                SetCurrent(go, true);
            }
        }

        public void SetCurrent(GameObject go, bool useMouse)
        { 
            if (current != null)
            {
                //ObjectContainerArray contextHolder = Utilities.FindParent<ObjectContainerArray>(go.transform);
                //context = contextHolder && contextHolder.provideContext ? contextHolder.GetObjects() : null;

                string tip = current.getToolTipMessage();

                if (tip != "")
                {
                    Vector2 pos = useMouse? lastMousePos : Vector2.right * go.transform.position.x + Vector2.up * go.transform.position.y;

                    tooltipObject.SetActive(true);

                    // we need this to force a recalculation of the text size to propagate up to the parent frame
                    toolTipText.text = "";
                    // set the text to the tooltip one
                    toolTipText.text = tip;
                    float width = Mathf.Min(240, toolTipText.GetPreferredValues().x + 16);
                    //float width = (toolTipText.transform as RectTransform).rect.width;

                    // positon at the mouse
                    float offsetX = current.getOffsetX();
                    pos.x += offsetX;
                    toolTipTransform.position = pos;

                    // make it point inwards to the centre of the screen
                    bool goLeft = (offsetX == 0 && pos.x > Screen.width / 2) || offsetX < 0;
                    toolTipTransform.pivot = new Vector2(goLeft ? 1 : 0, pos.y > Screen.height / 2 ? 1 : 0);

                    if (toolTipImage)
                    {
                        Sprite image = current.getToolTipImage();
                        toolTipImage.sprite = image;
                        toolTipImage.gameObject.SetActive(image != null);
                    }
                }
                else
                    current = null;
            }

            // turn the tooltip on or off based on whether we have a thing under the mouse
            tooltipObject.SetActive(current != null);
            if (current != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipObject.transform as RectTransform);
        }

        // helper function for getting the UI pbject at a point
        List<RaycastResult> hitObjects = new List<RaycastResult>();
        GameObject GetObjectUnderPos(Vector3 position)
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = position;
            EventSystem.current.RaycastAll(pointer, hitObjects);
            return (hitObjects.Count <= 0) ? null : hitObjects[0].gameObject;
        }
    }
}
