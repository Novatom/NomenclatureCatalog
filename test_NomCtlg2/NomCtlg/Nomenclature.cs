using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_NomCtlg2.NomCtlg
{
    /// <summary>
    /// Класс номенклатуры
    /// </summary>
    public class Nomenclature
    {
        /// <summary>
        /// Счетчик следующего отрицательного идентификатора номенклатуры
        /// </summary>
        private static int nextNewId;

        /// <summary>
        /// Идентификатор номенклатуры
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор родителя (папки)
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Наименование номенклатуры
        /// </summary>
        public string Name { get; set; }

        private List<Characteristic> characteristics;
        /// <summary>
        /// Коллекция характеристик номенклатуры
        /// </summary>
        public IList<Characteristic> Characteristics
        {
            get { return characteristics; }
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        private Nomenclature()
        {
            Id = --nextNewId;
            Name = string.Empty;
            characteristics = new List<Characteristic>();
        }

        /// <summary>
        /// Конструктор номенклатуры по наименованию
        /// </summary>
        /// <param name="name">Наименование номенклатуры</param>
        public Nomenclature(string name)
            : this()
        {
            Name = name;
        }

        /// <summary>
        /// Добавление характеристики к номенклатуре
        /// </summary>
        /// <param name="name">Наименование добавляемой характеристики</param>
        /// <returns></returns>
        public Characteristic AddCharacteristic(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (IsCharacteristicNameExists(name))
            {
                return null;
            }

            var characteristic = new Characteristic(name)
            {
                ParentId = this.Id
            };
            Characteristics.Add(characteristic);

            return characteristic;
        }

        /// <summary>
        /// Добавление характеристики к номенклатуре
        /// </summary>
        /// <param name="characteristic">Добавляемая характеристика</param>
        public void AddCharacteristic(Characteristic characteristic)
        {
            if (characteristic == null)
            {
                throw new ArgumentNullException("characteristic");
            }

            if (IsCharacteristicNameExists(characteristic.Name))
            {
                return;
            }

            characteristic.ParentId = this.Id;
            Characteristics.Add(characteristic);
        }

        /// <summary>
        /// Проверка дубликата наименования характеристики
        /// </summary>
        /// <param name="name">Проверяемое имя</param>
        /// <returns></returns>
        private bool IsCharacteristicNameExists(string name)
        {
            return Characteristics.Any(c => c.Name.Equals(name));
        }
    }
}
