using Eventmi.Core.Models;

namespace Eventmi.Core.Contracts
{
    /// <summary>
    /// Услуга за управление на събития
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Добавяне на събитие
        /// </summary>
        /// <param name="model">Данни за събитие</param>
        /// <returns></returns>
        Task AddAsync(EventModel model);

        /// <summary>
        /// Изтриване на събитие
        /// </summary>
        /// <param name="id">Идентификатор на събитие</param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Промяна на събитие
        /// </summary>
        /// <param name="model">Данни за събитие</param>
        /// <returns></returns>
        Task UpdateAsync(EventModel model);

        /// <summary>
        /// Преглед на всички събития
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EventModel>> GetAllAsync();

        /// <summary>
        /// Преглед на събитие
        /// </summary>
        /// <param name="id">Идентификатор на събитие</param>
        /// <returns></returns>
        Task<EventModel> GetEventAsync(int id);
    }
}