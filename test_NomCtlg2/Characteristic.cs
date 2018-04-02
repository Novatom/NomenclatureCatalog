using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NomenclatureCatalog
{
    /// <summary>
    /// Класс характеристики
    /// </summary>
    public class Characteristic
    {
        /// <summary>
        /// Счетчик следующего отрицательного идентификатора характеристики
        /// </summary>
        private static int nextNewId;

        /// <summary>
        /// Указывает, что характеристика изменилась
        /// </summary>
        public bool IsChanged { get; private set; }

        /// <summary>
        /// Идентификатор характеристики
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор родителя (номенклатуры)
        /// </summary>
        public int ParentId { get; set; }

        private string name;
        /// <summary>
        /// Наименование характеристики
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    IsChanged = true;
                }
            }
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        private Characteristic()
        {
            Id = --nextNewId;
            name = string.Empty;
        }

        /// <summary>
        /// Конструктор характеристики по наименованию
        /// </summary>
        /// <param name="name"></param>
        public Characteristic(string name)
            : this()
        {
            Name = name;
        }
    }
}
