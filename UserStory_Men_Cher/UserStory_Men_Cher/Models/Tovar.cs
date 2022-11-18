using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStory_Men_Cher.Models
{
    /// <summary>
    /// Сущность студента
    /// </summary>
    public class Tovar
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string TovarName { get; set; }
        /// <summary>
        /// Пол
        /// </summary>
        public Material Material { get; set; }
        /// <summary>
        /// Размер
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// Средняя оценка
        /// </summary>
        public decimal AvgRate { get; set; }

    }
}
