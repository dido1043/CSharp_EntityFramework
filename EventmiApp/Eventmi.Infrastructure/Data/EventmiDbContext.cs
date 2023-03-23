using Eventmi.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventmi.Infrastructure.Data
{
    /// <summary>
    /// Контекст описващ базата данни 
    /// </summary>
    public class EventmiDbContext : DbContext
    {
        /// <summary>
        /// Създава контекст без настройки
        /// </summary>
        public EventmiDbContext()
        {

        }
        /// <summary>
        /// Създава контекст с предварителни настройки
        /// </summary>
        /// <param name="options"></param>
        public EventmiDbContext(DbContextOptions<EventmiDbContext> options) 
            :base(options)
        {

        }


        /// <summary>
        /// Таблица със събития
        /// </summary>
        public DbSet<Event> Event { get; set; }


    }
}
