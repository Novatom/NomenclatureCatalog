using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using test_NomCtlg2.NomCtlg;
using test_NomCtlgMVVM.Models;

namespace test_NomCtlgMVVM.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService dataService;

        private readonly NomenclatureCatalog nomCtlg = new NomenclatureCatalog();

        public ObservableCollection<Folder> folders;
        public ObservableCollection<Folder> Folders
        {
            get { return folders; }
            set
            {
                Set(ref folders, value);
            }
        }

        private Folder selectedFolder;
        public Folder SelectedFolder
        {
            get { return selectedFolder; }
            set
            {
                Set(ref selectedFolder, value);
                Nomenclatures = new ObservableCollection<Nomenclature>(selectedFolder.Nomenclatures);
            }
        }

        private ObservableCollection<Nomenclature> nomenclatures;
        public ObservableCollection<Nomenclature> Nomenclatures
        {
            get { return nomenclatures; }
            set
            {
                Set(ref nomenclatures, value);
            }
        }

        private Nomenclature selectedNomenclature;
        public Nomenclature SelectedNomenclature
        {
            get { return selectedNomenclature; }
            set
            {
                Set(ref selectedNomenclature, value);
                Characteristics = new ObservableCollection<Characteristic>(selectedNomenclature.Characteristics);
            }
        }

        private ObservableCollection<Characteristic> characteristics;
        public ObservableCollection<Characteristic> Characteristics
        {
            get { return characteristics; }
            set
            {
                Set(ref characteristics, value);
            }
        }

        private Characteristic selectedCharacteristic;
        public Characteristic SelectedCharacteristic
        {
            get { return selectedCharacteristic; }
            set
            {
                Set(ref selectedCharacteristic, value);
            }
        }

        public RelayCommand AddFolderCommand { get; set; }
        public RelayCommand AddNomenclatureCommand { get; set; }
        public RelayCommand AddCharacteristicCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            this.dataService = dataService;

            var folder = nomCtlg.AddFolder("TEST 1");
            folder.AddFolder("TEST 2");

            var nom = folder.AddNomenclature("TEST NOM 1");
            nom.AddCharacteristic("TEST CHAR 1.1");
            nom.AddCharacteristic("TEST CHAR 1.2");

            nom = folder.AddNomenclature("TEST NOM 2");
            nom.AddCharacteristic("TEST CHAR 2.1");

            nom = folder.AddNomenclature("TEST NOM 3");
            nom.AddCharacteristic("TEST CHAR 3.1");
            nom.AddCharacteristic("TEST CHAR 3.2");
            nom.AddCharacteristic("TEST CHAR 3.3");

            Folders = new ObservableCollection<Folder>(nomCtlg.Folders);
            Nomenclatures = new ObservableCollection<Nomenclature>(nomCtlg.Nomenclatures);

            AddFolderCommand = new RelayCommand(AddFolder);
            AddNomenclatureCommand = new RelayCommand(AddNomenclature);
            AddCharacteristicCommand = new RelayCommand(AddCharacteristic);
        }

        private void AddFolder()
        {
            nomCtlg.AddFolder("Новая папка");
        }

        private void AddNomenclature()
        {
            selectedFolder.AddNomenclature("Новая номенклатура");
        }

        private void AddCharacteristic()
        {
            selectedNomenclature.AddCharacteristic("Новая характеристика");
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}