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
           public int[] CoordianteXY = new int[2];
           public int[] Dimentions = new int[2];

            //whatever the fuck you save an image as
            public Image internalSprite;
            
            string name;

        }


        [Serializable]
        public class TextureAtlas
        {
            //save load functionality
            //array of sprites?
            List<Sprite> m_spritelist;

        }

        Sprite Img1 = new Sprite();

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
            openFileDialog1.ShowDialog();
            
        }


//###################################################################################
//EndButton
//###################################################################################







        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            Img1.internalSprite = new Bitmap(openFileDialog1.OpenFile());
            pictureBox1.Image = Img1.internalSprite;
        }



        

    }
}


