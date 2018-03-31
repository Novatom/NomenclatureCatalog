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
        /// Идентификатор характеристики
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор родителя (номенклатуры)
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Наименование характеристики
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        private Characteristic()
        {
            Id = --nextNewId;
            Name = string.Empty;
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
