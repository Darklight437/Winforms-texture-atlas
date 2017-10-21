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
            if(e.GetType() == typeof(MouseEventArgs))
            {
                MouseEventArgs me = e as MouseEventArgs;
                TextOutput.Text = me.Location.ToString();
            }
        }

        private void AtlasGen_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void AtlasGen_DragDrop(object sender, DragEventArgs e)
        {
            //pictureBox1.Image = e.Data.GetData(DataFormats.FileDrop);
        }

        //###################################################################################
        //button zone
        //###################################################################################

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            //want to activate saving in here
            XmlSerializer MYXML = new XmlSerializer(typeof(SaveObject));

            StreamWriter SS = new StreamWriter("test.xml");

            SaveObject woahBoi = new SaveObject();

            woahBoi.randomData = "Woah Woah";
            MYXML.Serialize(SS, woahBoi);


            SS.Close();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            XmlSerializer MYXML = new XmlSerializer(typeof(SaveObject));

            StreamReader SS = new StreamReader("test.xml");
            SaveObject woahBoi = MYXML.Deserialize(SS) as SaveObject;
            SS.Close();

            TextOutput.Text = woahBoi.randomData;
        }


        [Serializable]
        public class SaveObject
        {

          public string randomData = "Woah";
            
            
        }

        private void Import_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
