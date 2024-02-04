using FitFriends.ServiceLibrary.Attributes;
using System.ComponentModel;

namespace FitFriends.ServiceLibrary.Enums
{
    /// <summary>
    /// Перечисление, представляющее собой набор хранимых процедур для схемы базы данных "User".
    /// В Description написано название хранимой процедуры в базе данных без префикса
    /// </summary>
    [ProcedureName(DatabaseSchemas.User)]
    public enum StoredProcedureUser
    {
        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        [Description("Get_All_Users")]
        GetAllUsers,
        /// <summary>
        /// Получение пользователя по уникальному идентификатору.
        /// </summary>
        [Description("Get_User_By_Id")]
        GetUserById,
        /// <summary>
        /// Удаление пользователя по уникальному идентификатору.
        /// </summary>
        [Description("Delete_User")]
        DeleteUser,
        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        [Description("Insert_User")]
        InsertUser,
        /// <summary>
        /// Обновление пользователя по уникальному идентификатору.
        /// </summary>
        [Description("Update_User")]
        UpdateUser,
        /// <summary>
        /// Получение пользователя по электонной почте.
        /// </summary>
        [Description("Get_User_By_Email")]
        GetUserByEmail,
    }
}
