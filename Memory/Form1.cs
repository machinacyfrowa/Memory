namespace Memory
{
    public partial class Form1 : Form
    {
        List<string> images = new List<string>();
        public Form1()
        {
            InitializeComponent();
            for(int i = 1; i <= 8; i++)
            {
                images.Add("m" + i);
                images.Add("m" + i);
            }
            // Pobierz obrazek na podstawie nazwy string
            // Ustawianie obrazk�w w PictureBoxach za pomoc� p�tli
            for (int i = 0; i < 16; i++)
            {
                string pictureboxName = "pictureBox" + (i + 1);
                var pictureBox = Controls.Find(pictureboxName, true)
                                            .FirstOrDefault() as PictureBox;
                if (pictureBox != null)
                    pictureBox.Image = GetByIndex(i);
            }
            

        }
        Image GetByIndex(int index)
        {
            string imageName = images[index];
            return (Image)Properties.Resources.ResourceManager.GetObject(imageName);
        }
    }
}
