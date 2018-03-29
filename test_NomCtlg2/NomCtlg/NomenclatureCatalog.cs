using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_NomCtlg2.NomCtlg
{
    /// <summary>
    /// Класс каталога номенклатуры
    /// </summary>
    public class NomenclatureCatalog
    {
        private List<Folder> folders;
        /// <summary>
        /// Коллекция корневых папок каталога
        /// </summary>
        public IList<Folder> Folders
        {
            get { return folders; }
        }

        private List<Nomenclature> nomenclatures;
        /// <summary>
        /// Коллекция всех номенклатур каталога
        /// </summary>
        public IList<Nomenclature> Nomenclatures
        {
            get { return nomenclatures; }
        }

        /// <summary>
        /// Коллекция всех характеристик всех номенклатур каталога
        /// </summary>
        private IEnumerable<Characteristic> characteristics
        {
            get { return Nomenclatures.SelectMany(n => n.Characteristics); }
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public NomenclatureCatalog()
        {
            folders = new List<Folder>();
            nomenclatures = new List<Nomenclature>();
        }

        /// <summary>
        /// Метод добавления папки
        /// </summary>
        /// <param name="name">Наименование добавляемой папки</param>
        /// <returns></returns>
        public Folder AddFolder(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (IsFolderNameExists(name))
            {
                return null;
            }

            var folder = new Folder(name)
            {
                catalogue = this
            };
            Folders.Add(folder);

            return folder;
        }

        /// <summary>
        /// Метод добавления папки
        /// </summary>
        /// <param name="folder">Добавляемая папка</param>
        public void AddFolder(Folder folder)
        {
            if (folder == null)
            {
                throw new ArgumentNullException("folder");
            }

            if (IsFolderNameExists(folder.Name))
            {
                return;
            }

            folder.catalogue = this;
            Folders.Add(folder);
        }

        /// <summary>
        /// Метод проверки дубликата имени папки
        /// </summary>
        /// <param name="name">Проверяемое имя</param>
        /// <returns></returns>
        internal bool IsFolderNameExists(string name)
        {
            return Folders.Any(f => f.Name.Equals(name));
        }

        /// <summary>
        /// Метод проверки дубликата наименования номенклатуры
        /// </summary>
        /// <param name="name">Проверяемое имя</param>
        /// <returns></returns>
        internal bool IsNomenclatureNameExists(string name)
        {
            return Nomenclatures.Any(n => n.Name.Equals(name));
        }

        /// <summary>
        /// Метод получения номенклатуры по идентификатору
        /// </summary>
        /// <param name="id">Искомый идентификатор номенклатуры</param>
        /// <returns></returns>
        public Nomenclature GetNomenclatureById(int id)
        {
            return Nomenclatures.SingleOrDefault(n => n.Id == id);
        }

        /// <summary>
        /// Метод получения характеристики по идентификатору
        /// </summary>
        /// <param name="id">Искомый идентификатор характеристики</param>
        /// <returns></returns>
        public Characteristic GetCharacteristicById(int id)
        {
            return characteristics.SingleOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Поиск номенклатур по имени
        /// </summary>
        /// <param name="name">Искомое имя</param>
        /// <param name="option">Опции поиска</param>
        /// <returns>Коллекция найденных номенклатур</returns>
        public IEnumerable<Nomenclature> GetNomenclaturesByName(string name, CatalogueSearchOption option)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            var nomenclatures = Enumerable.Empty<Nomenclature>();

            switch (option)
            {
                case CatalogueSearchOption.Equals:
                    nomenclatures = Nomenclatures.Where(n => n.Name.Equals(name));
                    break;

                case CatalogueSearchOption.Contains:
                    nomenclatures = Nomenclatures.Where(n => n.Name.Contains(name));
                    break;

                case CatalogueSearchOption.StartsWith:
                    nomenclatures = Nomenclatures.Where(n => n.Name.StartsWith(name));
                    break;

                case CatalogueSearchOption.EndsWith:
                    nomenclatures = Nomenclatures.Where(n => n.Name.EndsWith(name));
                    break;

                default:
                    break;
            }

            return nomenclatures;
        }

        /// <summary>
        /// Поиск характеристик по имени
        /// </summary>
        /// <param name="name">Искомое имя</param>
        /// <param name="option">Опции поиска</param>
        /// <returns>Коллекция найденных характеристик</returns>
        public IEnumerable<Characteristic> GetCharecteristicsByName(string name, CatalogueSearchOption option)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            var result = Enumerable.Empty<Characteristic>();

            switch (option)
            {
                case CatalogueSearchOption.Equals:
                    result = characteristics.Where(c => c.Name.Equals(name));
                    break;

                case CatalogueSearchOption.Contains:
                    result = characteristics.Where(c => c.Name.Contains(name));
                    break;

                case CatalogueSearchOption.StartsWith:
                    result = characteristics.Where(c => c.Name.StartsWith(name));
                    break;

                case CatalogueSearchOption.EndsWith:
                    result = characteristics.Where(c => c.Name.EndsWith(name));
                    break;

                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Поиск номенклатуры по имени
        /// </summary>
        /// <param name="name">Искомое имя</param>
        /// <returns>Найденная номенклатура</returns>
        public Nomenclature GetNomenclatureByName(string name)
        {
            var nomenclature = Nomenclatures.SingleOrDefault(n => n.Name.Equals(name));
            return nomenclature;
        }

        /// <summary>
        /// Поиск характеристики по имени
        /// </summary>
        /// <param name="name">Искомое имя</param>
        /// <returns>Найденная характеристика</returns>
        public Characteristic GetCharacteristicByName(string name)
        {
            var characteristic = characteristics.SingleOrDefault(c => c.Name.Equals(name));
            return characteristic;
        }

        /// <summary>
        /// Поиск папок по имени
        /// </summary>
        /// <param name="name">Искомое имя</param>
        /// <param name="option">Опции поиска</param>
        /// <returns>Коллекция найденных папок</returns>
        public IEnumerable<Folder> GetFoldersByName(string name, CatalogueSearchOption option)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            var folders = new List<Folder>();

            foreach (var folder in Folders)
            {
                folders.AddRange(folder.GetFoldersByName(name, option));
            }

            switch (option)
            {
                case CatalogueSearchOption.Equals:
                    folders.AddRange(Folders.Where(f => f.Name.Equals(name)));
                    break;

                case CatalogueSearchOption.Contains:
                    folders.AddRange(Folders.Where(f => f.Name.Contains(name)));
                    break;

                case CatalogueSearchOption.StartsWith:
                    folders.AddRange(Folders.Where(f => f.Name.StartsWith(name)));
                    break;

                case CatalogueSearchOption.EndsWith:
                    folders.AddRange(Folders.Where(f => f.Name.EndsWith(name)));
                    break;

                default:
                    break;
            }

            return folders.ToArray();
        }
    }

    /// <summary>
    /// Перечисление опций поиска элементов в каталоге номенклатуры
    /// </summary>
    public enum CatalogueSearchOption
    {
        /// <summary>
        /// Элемент соответствует
        /// </summary>
        Equals,
        /// <summary>
        /// Элемент содержит
        /// </summary>
        Contains,
        /// <summary>
        /// Элемент начинается с
        /// </summary>
        StartsWith,
        /// <summary>
        /// Элемент заканчивается на
        /// </summary>
        EndsWith
    }
}
