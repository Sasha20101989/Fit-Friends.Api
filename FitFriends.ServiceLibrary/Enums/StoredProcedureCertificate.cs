using FitFriends.ServiceLibrary.Attributes;
using System.ComponentModel;

namespace FitFriends.ServiceLibrary.Enums
{
    /// <summary>
    /// Перечисление, представляющее собой набор хранимых процедур для схемы базы данных "User".
    /// В Description написано название хранимой процедуры в базе данных без префикса
    /// </summary>
    [ProcedureName(DatabaseSchemas.User)]
    public enum StoredProcedureCertificate
    {
        /// <summary>
        /// Получение всех сертификатов для пользователя.
        /// </summary>
        [Description("Get_All_Certificates_By_User")]
        GetAllCertificatesByUser,
        /// <summary>
        /// Получение сертификата по уникальному идентификатору.
        /// </summary>
        [Description("Get_Certificate_By_Id")]
        GetCertificateById,
        /// <summary>
        /// Удаление сертификата по уникальному идентификатору.
        /// </summary>
        [Description("Delete_Certificate")]
        DeleteCertificate,
        /// <summary>
        /// Добавление нового сертификата.
        /// </summary>
        [Description("Insert_Certificate")]
        InsertCertificate,
        /// <summary>
        /// Обновление сертификата по уникальному идентификатору.
        /// </summary>
        [Description("Update_Certificate")]
        UpdateCertificate,
    }
}
