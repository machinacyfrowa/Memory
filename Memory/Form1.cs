namespace Memory
{
    public partial class Form1 : Form
    {
        //List<string> images = new List<string>();
        List<ImageTile> imageTiles;
        //flaga opisuj� czy to pierwszy czy drugi klik
        bool firstClick = true;
        //indeks pierwszego klikni�tego obrazka
        //-1 oznacza, �e jeszcze nie klikni�to
        int firstIndex = -1;
        //lista obrazk�w do ukrycia po czasie
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
            // Ustawianie obrazk�w w PictureBoxach za pomoc� p�tli
            //for (int i = 0; i < 16; i++)
            //{
            //    string pictureboxName = "pictureBox" + (i + 1);
            //    var pictureBox = Controls.Find(pictureboxName, true)
            //                                .FirstOrDefault() as PictureBox;
            //    if (pictureBox != null)
            //        pictureBox.Image = GetByIndex(i);
            //}
            //na koniec inicjalizacji formularza od�wie�amy obrazki
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
            //przyczytaj "nadawc�" zdarzenia jako PictureBox
            PictureBox pb = sender as PictureBox;
            //wyci�gamy sobie jego nazw�
            string pictureboxName = pb.Name;
            //wyci�gamy z nazwy numer obrazka
            //usu� s�owo pictureBox
            pictureboxName = pictureboxName.Replace("pictureBox", "");
            //przekonwertuj na int i odejmij 1
            int index = int.Parse(pictureboxName) - 1;

            // w zale�no�ci od tego czy jest to pierwszy czy drugi klik
            // b�dziemy post�powa� inaczej
            if (firstClick)
            {
                //ods�o� obrazek
                imageTiles[index].state = State.Shown;
                //ustaw indeks pierwszego klikni�tego obrazka
                firstIndex = index;
                //przerzu� flag�
                firstClick = false;
            }
            else
            {
                //je�eli jest to drugie klikni�cie
                //sprawd� czy obrazki s� takie same
                string firstImageName = imageTiles[firstIndex].name;
                string secondImageName = imageTiles[index].name;
                if (firstImageName == secondImageName)
                {
                    //je�li obrazki s� takie same
                    //ustaw stan obu obrazk�w na Matched
                    imageTiles[firstIndex].state = State.Matched;
                    imageTiles[index].state = State.Matched;
                }
                else
                {
                    //je�li obrazki s� r�ne
                    ////ukryj pierwszy obrazek
                    //imageTiles[firstIndex].state = State.Hidden;
                    ////ukryj drugi obrazek
                    //imageTiles[index].state = State.Hidden;
                    //zamiast ukrywa� oba obrazki zapisz ich indeksy i odpal zegar
                    toHide.Add(firstIndex); //pierwszy obrazek do ukrycia
                    toHide.Add(index); //drugi obrazek do ukrycia
                    timer1.Stop();
                    timer1.Interval = 1000; //ustaw interwa� na 1 sekund�
                    timer1.Start(); 
                }
                //przerzu� flag�
                firstClick = true;
            }
            //prze�aduj obrazki
            RefreshImages();
        }
        /// <summary>
        /// Tick zegara - chowamy obrazki, je�li s� r�ne
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
            //wyczy�� list�
            toHide.Clear();
            //wy��cz zegar
            timer1.Stop();
            //od�wie� obrazki
            RefreshImages();
        }
        //Image GetByIndex(int index)
        //{
        //    string imageName = images[index];
        //    return (Image)Properties.Resources.ResourceManager.GetObject(imageName);
        //}
    }
}
