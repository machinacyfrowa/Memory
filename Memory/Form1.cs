namespace Memory
{
    public partial class Form1 : Form
    {
        //List<string> images = new List<string>();
        List<ImageTile> imageTiles;
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
            //mamy indeks w liœcie obrazków - zmieñ stan obrazka na pokazany
            imageTiles[index].state = State.Shown;
            //prze³aduj obrazki
            RefreshImages();
        }
        //Image GetByIndex(int index)
        //{
        //    string imageName = images[index];
        //    return (Image)Properties.Resources.ResourceManager.GetObject(imageName);
        //}
    }
}
