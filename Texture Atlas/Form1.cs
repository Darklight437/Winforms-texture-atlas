using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace Texture_Atlas
{
    

    public partial class AtlasGen : Form
    {

        /*
        * Classes
        *
        *contains a class for storing each image on the sheet
        * & class for the sheet itself
        *
        *
        */

        public class Sprite
        {
           
            public Image internalSprite;
            
            string name;

        }

        public class Position
        {
            public int coOrdinateX;
            public int coOrdinateY;
        }

        [Serializable]
        public class TextureAtlas
        {
            
            //one master image that is saved here and an array of positions that represent each image
            //i need to "work on my public image"
            public Image Masterimage;
            private List<Position> m_spritePos;

        }

        Sprite Img1 = new Sprite();
        TextureAtlas Master = new TextureAtlas();

        public AtlasGen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TextOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (e.GetType() == typeof(MouseEventArgs))
            {
                MouseEventArgs me = e as MouseEventArgs;
                TextOutput.Text = me.Location.ToString();
            }
        }

        private void AtlasGen_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;

            }
        }



        //###################################################################################
        //button zone
        //###################################################################################



        private void Import_Click(object sender, EventArgs e)
        {
            previewPictureBox.Image = null;
            openFileDialog1.ShowDialog();
            if (previewPictureBox.SizeMode != PictureBoxSizeMode.Zoom)
            {
                previewPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.SizeMode != PictureBoxSizeMode.Zoom)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            Master.Masterimage = reGenerateImage(Master.Masterimage, Img1.internalSprite);
            pictureBox1.Image = Master.Masterimage;
            Img1.internalSprite = null;
            
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            
            
            finalImageSaver.ShowDialog();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            finalImageLoader.ShowDialog();
        }

        //########################################################################################
        //########################################################################################
        //EndButton
        //########################################################################################
        //########################################################################################
        //returns an image that combines the position of each old image

        private Image reGenerateImage(Image original, Image secondary)
        {

            //returns this
            Bitmap finalImage = null;
            if (original != null && secondary != null)
            {
                finalImage = new Bitmap(original.Width + secondary.Width, Math.Max(original.Height, secondary.Height));
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    g.DrawImage(original, 0, 0);
                    g.DrawImage(secondary, original.Width, 0, secondary.Width, secondary.Height);
                }
            }
            else
            {
                if (secondary != null)
                {
                    finalImage = new Bitmap(secondary.Width,  secondary.Height);
                    
                    using (Graphics g = Graphics.FromImage(finalImage))
                    {
                        g.DrawImage(secondary, 0, 0, secondary.Width, secondary.Height);
                    }
                }
                
            }



            return finalImage;
           
        }

        private void serialise(string targetPath)
        {
            
            BinaryFormatter SerialiseTool = new BinaryFormatter();
            FileStream stream = new FileStream(targetPath + ".dat", FileMode.Create);

            SerialiseTool.Serialize(stream, Master);
            stream.Close();
        }

        private void deSerialise(Stream loadfilestr)
        {
            BinaryFormatter serialiseTool = new BinaryFormatter();
            try
            {
                Master = (TextureAtlas)serialiseTool.Deserialize(loadfilestr);
                pictureBox1.Image = Master.Masterimage;
                if (pictureBox1.SizeMode != PictureBoxSizeMode.Zoom)
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch(SerializationException e)
            {
                Console.WriteLine("failed to serialize. reason:" + e.Message);
            }
            
        }
       


        //########################################################################################
        //########################################################################################
        //Dialog boxes
        //########################################################################################
        //########################################################################################

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            Img1.internalSprite = new Bitmap(openFileDialog1.OpenFile());
            previewPictureBox.Image = Img1.internalSprite;
            
        }
        
        private void finalImageLoader_FileOk(object sender, CancelEventArgs e)
        {
            deSerialise(finalImageLoader.OpenFile());
        }

        private void finalImageSaver_FileOk(object sender, CancelEventArgs e)
        {
            serialise(Path.GetFullPath(finalImageSaver.FileName));
        }

      
    }
}


