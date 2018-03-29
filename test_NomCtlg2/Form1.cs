using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using test_NomCtlg2.NomCtlg;

namespace test_NomCtlg2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var nomCtlg = new NomenclatureCatalog();
            var folder = nomCtlg.AddFolder("Root folder #1");
            nomCtlg.AddFolder("One more root folder #2");

            folder.AddFolder("Folder #1.1");

            var nom = new Nomenclature("Nom #1.1.1");

            string nomName = "Nom #1.1.2";

            folder.AddNomenclature(nom);
            folder.AddNomenclature(nomName);

            nom.AddCharacteristic(new Characteristic("Charact #1.1.1.1"));
            nom.AddCharacteristic("Charact #1.1.1.2");

            XmlSerializer xmlSer = new XmlSerializer(typeof(NomenclatureCatalog));
            var fileStream = new FileStream("ctlg.xml", FileMode.Create);
            xmlSer.Serialize(fileStream, nomCtlg);
            fileStream.Close();
        }
    }
}
