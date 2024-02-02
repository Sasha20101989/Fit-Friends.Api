using FitFriends.ServiceLibrary.Enums;
using System.Data;

namespace FitFriends.ServiceLibrary.DataAccess.Contracts
{
    /// <summary>
    /// Методы определения интерфейса для доступа к данным SQL.
    /// </summary>
    public interface ISqlDataAccess
    {
        /// <summary>
        /// Загружает данные из базы данных.
        /// </summary>
        /// <typeparam name="T">Тип возвращаемых данных.</typeparam>
        /// <param name="storedProcedure">Перечисление, представляющее хранимую процедуру для выполнения.</param>
        /// <param name="parameters">Необязательные параметры хранимой процедуры.</param>
        /// <param name="connectionId">Идентификатор соединения из appsettings.json.</param>
        /// <returns>Задача, представляющая асинхронную операцию, возвращающую загруженные данные.</returns>
        Task<IEnumerable<T>> LoadData<T>(Enum storedProcedure, object? parameters = null, ConnectionId connectionId = ConnectionId.Default);

        /// <summary>
        /// Сохраняет данные в базу данных.
        /// </summary>
        /// <param name="storedProcedure">Перечисление, представляющее хранимую процедуру для выполнения.</param>
        /// <param name="parameters">Необязательные параметры хранимой процедуры.</param>
        /// <param name="connectionId">Идентификатор соединения из appsettings.json.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task SaveData(Enum storedProcedure, object? parameters = null, ConnectionId connectionId = ConnectionId.Default);
    }
}