using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NomenclatureCatalog

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
        /// Добавление папки
        /// </summary>
        /// <param name="name">Наименование добавляемой папки</param>
        /// <returns></returns>
        public Folder AddFolder(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            name = GetNextAvailableFolderName(name);

            var folder = new Folder(name)
            {
                catalog = this
            };
            Folders.Add(folder);

            return folder;
        }

        /// <summary>
        /// Добавление папки
        /// </summary>
        /// <param name="folder">Добавляемая папка</param>
        public void AddFolder(Folder folder)
        {
            if (folder == null)
            {
                throw new ArgumentNullException("folder");
            }

            folder.Name = GetNextAvailableFolderName(folder.Name);
            folder.ParentId = 0;
            folder.catalog = this;

            Folders.Add(folder);
        }

        /// <summary>
        /// Возвращает следующее доступное имя папки
        /// </summary>
        /// <param name="name">Проверяемое первоначальное имя</param>
        /// <returns>Уникальное имя папки</returns>
        private string GetNextAvailableFolderName(string name)
        {
            if (IsFolderNameExists(name))
            {
                var number = 2;
                var originalName = name;

                do
                {
                    name = originalName + " (" + (number++) + ")";
                } while (IsFolderNameExists(name));
            }

            return name;
        }

        /// <summary>
        /// Проверка имени папки на дубликат
        /// </summary>
        /// <param name="name">Проверяемое имя</param>
        /// <returns></returns>
        private bool IsFolderNameExists(string name)
        {
            return Folders.Any(f => f.Name.Equals(name));
        }

        /// <summary>
        /// Возвращает следующее доступное имя номенклатуры
        /// </summary>
        /// <param name="p">Проверяемое первоначальное имя</param>
        /// <returns>Уникальное имя номенклатуры</returns>
        internal string GetNextAvailableNomenclatureName(string name)
        {
            if (IsNomenclatureNameExists(name))
            {
                var number = 2;
                var originalName = name;

                do
                {
                    name = originalName + " (" + (number++) + ")";
                } while (IsNomenclatureNameExists(name));
            }

            return name;
        }


        /// <summary>
        /// Проверки наименования номенклатуры на дубликат
        /// </summary>
        /// <param name="name">Проверяемое имя</param>
        /// <returns></returns>
        internal bool IsNomenclatureNameExists(string name)
        {
            return Nomenclatures.Any(n => n.Name.Equals(name));
        }

        /// <summary>
        /// Получение номенклатуры по идентификатору
        /// </summary>
        /// <param name="id">Искомый идентификатор номенклатуры</param>
        /// <returns></returns>
        public Nomenclature GetNomenclatureById(int id)
        {
            return Nomenclatures.SingleOrDefault(n => n.Id == id);
        }

        /// <summary>
        /// Получение характеристики по идентификатору
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
        public IEnumerable<Characteristic> GetCharacteristicsByName(string name, CatalogueSearchOption option)
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

        /// <summary>
        /// Перемещение папки в другую папку
        /// </summary>
        /// <param name="folder">Перемещаемая папка</param>
        /// <param name="toFolder">Папка назначения</param>
        public void RelocateFolder(Folder folder, Folder toFolder)
        {
            if (folder == null)
            {
                throw new ArgumentNullException("folder");
            }

            var parentFolder = GetFolderById(folder.ParentId);
            if (parentFolder != null)
            {
                parentFolder.RemoveFolder(folder);

                if (toFolder == null)
                {
                    AddFolder(folder);
                }
                else
                {
                    toFolder.AddFolder(folder);
                }
            }
        }

        /// <summary>
        /// Перемещение номенклатуры в другую папку
        /// </summary>
        /// <param name="nomenclature">Перемещаемая номенклатура</param>
        /// <param name="toFolder">Папка назначения</param>
        public void RelocateNomenclature(Nomenclature nomenclature, Folder toFolder)
        {
            if (nomenclature == null)
            {
                throw new ArgumentNullException("nomenclature");
            }

            if (toFolder == null)
            {
                throw new ArgumentNullException("toFolder");
            }

            nomenclature.ParentId = toFolder.Id;
        }

        /// <summary>
        /// Поиск папки по её идентификатору
        /// </summary>
        /// <param name="id">Искомый идентификатор</param>
        /// <returns></returns>
        public Folder GetFolderById(int id)
        {
            Folder result = Folders.SingleOrDefault(f => f.Id == id);

            if (result == null)
            {
                foreach (var folder in Folders)
                {
                    result = folder.GetFolderById(id);

                    if (result != null)
                    {
                        break;
                    }
                }
            }

            return result;
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
