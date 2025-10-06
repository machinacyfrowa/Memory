using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    enum State     
    {
        Hidden, //obrazek jest ukryty - pleckami do góry
        Shown, //obrazek jest pokazany - widoczny na planszy
        Matched //obrazek jest dopasowany - niewidoczny na planszy
    }
    internal class ImageTile
    {
        string name;
        State state;
        /// <summary>
        /// Metoda tworzy nową listę obrazków
        /// </summary>
        static List<ImageTile> GenerateTiles()
        {
            //stwórz pustą listę
            List<ImageTile> tiles = new List<ImageTile>();
            //dodajemy 8 par obrazków do listy
            for (int i = 1; i <= 8; i++)
            {
                //nowy obrazek
                ImageTile tile1 = new ImageTile();
                //ustaw nazwę zgodnie z nazwą w zasobach
                tile1.name = "m" + i;
                //ustaw stan na ukryty
                tile1.state = State.Hidden;
                tiles.Add(tile1);
                ImageTile tile2 = new ImageTile();
                tile2.name = "m" + i;
                tile2.state = State.Hidden;
                tiles.Add(tile2);
            }
            //zwróć listę
            return tiles;
        }
        /// <summary>
        /// Metoda zwraca obrazek na podstawie nazwy
        /// </summary>
        /// <returns>obrazek z zasobów</returns>
        Image? GetImage()
        {
            if(state == State.Shown)
                return (Image)Properties.Resources.ResourceManager.GetObject(name);
            else if(state == State.Hidden)
                //tu zwraca obrazek plecków
                return (Image)Properties.Resources.ResourceManager.GetObject("mb");
            else //(state == State.Matched)
                //tu powinno zwracać puste tło
                return null;
        }
    }
}
