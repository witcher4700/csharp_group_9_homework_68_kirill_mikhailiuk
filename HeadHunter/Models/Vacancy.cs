using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ExpirienceFrom { get; set; }
        public int ExpirienceTo { get; set; }
        public DateTime RefreshDate { get; set; }
        public Status Status { get; set; }
        public Category Category { get; set; }
    }
    public enum Status
    {
        InStock,
        InPublic
    }
    public enum Category
    {
        РаботаИзДома,
        Подработка,
        Продавец,
        Кассир,
        Водитель,
        Курьер,
        Администратор,
        Оператор,
        Программист,
        Менеджер
    }
}
