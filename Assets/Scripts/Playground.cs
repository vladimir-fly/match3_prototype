using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace m3Prototype
{
	public enum EItemType : int { None = 0, Dotted = 1, Lined = 2, Filled = 3, Celled = 4 }

	public class Playground : MonoBehaviour
	{
	    [SerializeField] public Item Item;

	    private int _width;
	    private int _height;
        private List<GameObject> _items;
        private List<Texture> _textures;

        public void Init(int width, int height)
	    {
	        _width = width;
	        _height = height;

            CreateTextures();
	        InitItems();
        }

        void CreateTextures()
        {
            _textures = new List<Texture>();

            //Dotted
            var texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            texture.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f, 0.5f));
            texture.SetPixel(1, 0, Color.black);
            texture.SetPixel(0, 1, Color.black);
            texture.SetPixel(1, 1, Color.black);
            texture.Apply();
            _textures.Add(texture);

            //Lined
            texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            texture.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f, 0.5f));
            texture.SetPixel(1, 0, Color.red);
            texture.SetPixel(0, 1, Color.red);
            texture.SetPixel(1, 1, Color.red);
            texture.Apply();
            _textures.Add(texture);

            //Filled
            texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            texture.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f, 0.5f));
            texture.SetPixel(1, 0, Color.cyan);
            texture.SetPixel(0, 1, Color.cyan);
            texture.SetPixel(1, 1, Color.cyan);
            texture.Apply();
            _textures.Add(texture);

            //Celled
            texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            texture.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f, 0.5f));
            texture.SetPixel(1, 0, Color.yellow);
            texture.SetPixel(0, 1, Color.yellow);
            texture.SetPixel(1, 1, Color.yellow);
            texture.Apply();
            _textures.Add(texture);
        }

	    public void InitItems()
	    {
            _items = new List<GameObject>();
	        for (var i = 0; i < _width; i++)
	        {
	            for (var j = 0; j < _height; j++)
	            {
	                var position = new Vector3(i - _width/2 + .5f, j - _height/2 + 1.5f, 1f);
	                var item = Instantiate(Item, position, new Quaternion(1f, 0f, 0f, 1f));
	                item.transform.SetParent(transform);
	                item.Id = (i * 10 + j);
	                item.OnTrySwap += TrySwap;
	                item.Type = (EItemType) UnityEngine.Random.Range(1, 4);
                    item.Texture = _textures[(int) item.Type];
                    item.gameObject.SetActive(true);
	                _items.Add(item.gameObject);
	            }
	        }
	    }

        bool TrySwap(Item from, Item to)
        {
            //IEnumerable itemsToRemove;// = new List<GameObject>();
            var result = false;

            //Checking item neighbourhood first distance

            //var neighbourhoodFromByX =
            print(from == null);
            print(to == null);

              var itemsToRemove =
                  _items.Where(item =>
                      (item.GetComponent<Item>().Type == from.Type &&
                       (item.GetComponent<Item>().Id == to.Id + 10 || item.GetComponent<Item>().Id == to.Id - 10)) ||
                  Item.GetComponent<Item>().Id == from.Id);


            //print(itemsToRemove.Count());
            //print("tst");
            //var neighbourhoodFromByY =
            if (itemsToRemove != null && !itemsToRemove.Any())
              itemsToRemove =  _items.Where(item => (item.GetComponent<Item>().Type == from.Type && (item.GetComponent<Item>().Id == to.Id + 1 || item.GetComponent<Item>().Id == to.Id - 1)));

            //var neighbourhoodToByX =
            if (!itemsToRemove.Any())
                itemsToRemove = _items.Where(item => (item.GetComponent<Item>().Type == to.Type && (item.GetComponent<Item>().Id == from.Id + 10 || item.GetComponent<Item>().Id == from.Id - 10)));

            //var neighbourhoodToByY =

            if (!itemsToRemove.Any())
                itemsToRemove = _items.Where(item => (item.GetComponent<Item>().Type == to.Type && (item.GetComponent<Item>().Id == from.Id + 1 || item.GetComponent<Item>().Id == from.Id - 1)));


            //_items.FirstOrDefault(item => item.Id == itemId - 1); //y

            //x -10\+10
            //y -1\+1

            //Check additional right
            foreach (var item in itemsToRemove)
            {
                Destroy(item);
            }

            //check additionl left

            //check additionl right

            result = itemsToRemove.Any();
            //itemsToRemove.ForEach(item => DestroyImmediate(item));

            //Move items
            OffsetItems();

            //add new items
            ReinitItems();

            return result;
        }

	    void OffsetItems()
	    {

	    }

	    void ReinitItems()
        {

        }
	}
}