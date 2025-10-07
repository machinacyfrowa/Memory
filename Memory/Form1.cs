namespace Memory
{
    public partial class Form1 : Form
    {
        //List<string> images = new List<string>();
        List<ImageTile> imageTiles;
        //flaga opisuj¹ czy to pierwszy czy drugi klik
        bool firstClick = true;
        //indeks pierwszego klikniêtego obrazka
        //-1 oznacza, ¿e jeszcze nie klikniêto
        int firstIndex = -1;
        //lista obrazków do ukrycia po czasie
        List<int> toHide = new List<int>();
        public Form1()
        {
            InitializeComponent();
            //for(int i = 1; i <= 8; i++)
            //{
            //    images.Add("m" + i);
            //    images.Add("m" + i);
            //}
            imageTiles = ImageTile.GenerateTiles();


            // Pobierz obrazek na podstawie nazwy string
            // Ustawianie obrazków w PictureBoxach za pomoc¹ pêtli
            //for (int i = 0; i < 16; i++)
            //{
            //    string pictureboxName = "pictureBox" + (i + 1);
            //    var pictureBox = Controls.Find(pictureboxName, true)
            //                                .FirstOrDefault() as PictureBox;
            //    if (pictureBox != null)
            //        pictureBox.Image = GetByIndex(i);
            //}
            //na koniec inicjalizacji formularza odœwie¿amy obrazki
            RefreshImages();
        }
        public void RefreshImages()
        {
            for (int i = 0; i < 16; i++)
            {
                string pictureboxName = "pictureBox" + (i + 1);
                var pictureBox = Controls.Find(pictureboxName, true)
                                            .FirstOrDefault() as PictureBox;
                if (pictureBox != null)
                    pictureBox.Image = imageTiles[i].GetImage();
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            //przyczytaj "nadawcê" zdarzenia jako PictureBox
            PictureBox pb = sender as PictureBox;
            //wyci¹gamy sobie jego nazwê
            string pictureboxName = pb.Name;
            //wyci¹gamy z nazwy numer obrazka
            //usuñ s³owo pictureBox
            pictureboxName = pictureboxName.Replace("pictureBox", "");
            //przekonwertuj na int i odejmij 1
            int index = int.Parse(pictureboxName) - 1;

            // w zale¿noœci od tego czy jest to pierwszy czy drugi klik
            // bêdziemy postêpowaæ inaczej
            if (firstClick)
            {
                //ods³oñ obrazek
                imageTiles[index].state = State.Shown;
                //ustaw indeks pierwszego klikniêtego obrazka
                firstIndex = index;
                //przerzuæ flagê
                firstClick = false;
            }
            else
            {
                //je¿eli jest to drugie klikniêcie
                //sprawdŸ czy obrazki s¹ takie same
                string firstImageName = imageTiles[firstIndex].name;
                string secondImageName = imageTiles[index].name;
                if (firstImageName == secondImageName)
                {
                    //jeœli obrazki s¹ takie same
                    //ustaw stan obu obrazków na Matched
                    imageTiles[firstIndex].state = State.Matched;
                    imageTiles[index].state = State.Matched;
                }
                else
                {
                    //jeœli obrazki s¹ ró¿ne
                    ////ukryj pierwszy obrazek
                    //imageTiles[firstIndex].state = State.Hidden;
                    ////ukryj drugi obrazek
                    //imageTiles[index].state = State.Hidden;
                    //zamiast ukrywaæ oba obrazki zapisz ich indeksy i odpal zegar
                    toHide.Add(firstIndex); //pierwszy obrazek do ukrycia
                    toHide.Add(index); //drugi obrazek do ukrycia
                    timer1.Stop();
                    timer1.Interval = 1000; //ustaw interwa³ na 1 sekundê
                    timer1.Start(); 
                }
                //przerzuæ flagê
                firstClick = true;
            }
            //prze³aduj obrazki
            RefreshImages();
        }
        /// <summary>
        /// Tick zegara - chowamy obrazki, jeœli s¹ ró¿ne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //ukryj wszystkie obrazki z listy toHide
            foreach (int index in toHide)
            {
                imageTiles[index].state = State.Hidden;
            }
            //wyczyœæ listê
            toHide.Clear();
            //wy³¹cz zegar
            timer1.Stop();
            //odœwie¿ obrazki
            RefreshImages();
        }
        //Image GetByIndex(int index)
        //{
        //    string imageName = images[index];
        //    return (Image)Properties.Resources.ResourceManager.GetObject(imageName);
        //}
    }
}
