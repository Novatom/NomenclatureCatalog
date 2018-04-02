using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NomenclatureCatalog
{
    /// <summary>
    /// Класс папки каталога номенклатуры
    /// </summary>
    public class Folder
    {
        /// <summary>
        /// Счетчик следующего отрицательного идентификатора папки
        /// </summary>
        private static int nextNewId;

        /// <summary>
        /// Каталог папки
        /// </summary>
        internal NomenclatureCatalog catalog;

        /// <summary>
        /// Идентификатор папки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор родителя (надпапки)
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Наименование папки
        /// </summary>
        public string Name { get; set; }

        private List<Folder> folders;
        /// <summary>
        /// Коллекция подпапок (детей)
        /// </summary>
        public IList<Folder> Folders
        {
            get { return folders; }
        }

        /// <summary>
        /// Коллекция номенклатур текущей папки
        /// </summary>
        public IEnumerable<Nomenclature> Nomenclatures
        {
            get { return catalog.Nomenclatures.Where(n => n.ParentId == this.Id); }
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        private Folder()
        {
            Id = --nextNewId;
            Name = string.Empty;
            folders = new List<Folder>();
        }

        /// <summary>
        /// Конструктор папки по имени
        /// </summary>
        /// <param name="name"></param>
        public Folder(string name)
            : this ()
        {
            Name = name;
        }

        /// <summary>
        /// Добавление подпапки
        /// </summary>
        /// <param name="name">Наименование добавляемой подпапки</param>
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
                ParentId = this.Id,
                catalog = this.catalog
            };
            Folders.Add(folder);

            return folder;
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
        /// Добавление подпапки
        /// </summary>
        /// <param name="folder">Добавляемая подпапка</param>
        public void AddFolder(Folder folder)
        {
            if (folder == null)
            {
                throw new ArgumentNullException("folder");
            }

            folder.Name = GetNextAvailableFolderName(folder.Name);
            folder.ParentId = this.Id;
            folder.catalog = this.catalog;

            Folders.Add(folder);
        }

        /// <summary>
        /// Проверка имени подпапки на дубликат
        /// </summary>
        /// <param name="name">Проверяемое имя</param>
        /// <returns></returns>
        private bool IsFolderNameExists(string name)
        {
            return Folders.Any(f => f.Name.Equals(name));
        }

        /// <summary>
        /// Добавление номенклатуры
        /// </summary>
        /// <param name="name">Наименование добавляемой номенклатуры</param>
        /// <returns>Добавленная номенклатура</returns>
        public Nomenclature AddNomenclature(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            name = catalog.GetNextAvailableNomenclatureName(name);

            var nomenclature = new Nomenclature(name)
            {
                ParentId = this.Id
            };

            catalog.Nomenclatures.Add(nomenclature);
            return nomenclature;
        }

        /// <summary>
        /// Добавление номенклатуры
        /// </summary>
        /// <param name="nomenclature">Добавляемая номенклатура</param>
        public void AddNomenclature(Nomenclature nomenclature)
        {
            if (nomenclature == null)
            {
                throw new ArgumentNullException("nomenclature");
            }

            nomenclature.Name = catalog.GetNextAvailableNomenclatureName(nomenclature.Name);

            nomenclature.ParentId = this.Id;
            catalog.Nomenclatures.Add(nomenclature);
        }

        /// <summary>
        /// Поиск папок по имени
        /// </summary>
        /// <param name="name">Искомое имя</param>
        /// <param name="option">Опции поиска</param>
        /// <returns>Коллекция найденных папок</returns>
        internal IEnumerable<Folder> GetFoldersByName(string name, CatalogueSearchOption option)
        {
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

            return folders;
        }

        /// <summary>
        /// Поиск папки по её идентификатору
        /// </summary>
        /// <param name="id">Искомый идентифактор</param>
        /// <returns></returns>
        internal Folder GetFolderById(int id)
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

        /// <summary>
        /// Удаление подпапки
        /// </summary>
        /// <param name="folder">Удаляемая подпапка</param>
        internal bool RemoveFolder(Folder folder)
        {
            if (folder == null)
            {
                throw new ArgumentNullException();
            }

            return folders.Remove(folder);
        }
    }
}
