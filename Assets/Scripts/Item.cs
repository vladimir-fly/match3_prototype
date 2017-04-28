using System;
using UnityEngine;

namespace m3Prototype
{
    public class Item : MonoBehaviour
    {
        public Func<Item, Item, bool> OnTrySwap;
        public Texture Texture;
        public int Id;
        public EItemType Type;

        private Vector3 screenPoint;
        private Vector3 currentPosition;

        void Start()
        {
            GetComponent<Renderer>().material.mainTexture = Texture;
        }

        void OnMouseDown()
        {
            currentPosition = transform.position;
            transform.position += new Vector3(0f, 0f, 1f);
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        }

        void OnMouseUp()
        {
            transform.position -= new Vector3(0f, 0f, 1f);

            if (OnTrySwap != null)
            {
                if (Input.GetMouseButtonDown(0)) return;
                RaycastHit hit;
                Physics.Raycast(transform.position, new Vector3(0f, -1f, 0f), out hit);

                var selectedItem = hit.transform != null ? hit.transform.gameObject.GetComponent<Item>() : null;


                print(selectedItem);

                var canSwap = OnTrySwap(this, selectedItem);
                if (!canSwap) transform.position = currentPosition;
            }
        }

        void OnMouseDrag()
        {
            var point = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            var position = Camera.main.ScreenToWorldPoint(point);

            var x = position.x >= currentPosition.x - 1 && position.x <= currentPosition.x + 1 ? position.x :
                    position.x >= currentPosition.x - 1 ? currentPosition.x + 1 : currentPosition.x - 1;

            var y = position.y >= currentPosition.y - 1 && position.y <= currentPosition.y + 1 ? position.y :
                    position.y >= currentPosition.y - 1 ? currentPosition.y + 1 : currentPosition.y - 1;

            transform.position =  new Vector3(x, y, position.z);
        }
    }
}