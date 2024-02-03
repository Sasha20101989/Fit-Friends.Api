using FitFriends.ServiceLibrary.Attributes;
using System.ComponentModel;

namespace FitFriends.ServiceLibrary.Enums
{
    /// <summary>
    /// Перечисление, представляющее собой набор хранимых процедур для схемы базы данных "Dbo".
    /// В Description написано название хранимой процедуры в базе данных без префикса
    /// </summary>
    [ProcedureName(DatabaseSchemas.Dbo)]
    public enum StoredProcedureImage
    {
        /// <summary>
        /// Добавление нового изображения.
        /// </summary>
        [Description("Insert_Image")]
        CreateImage, 
        /// <summary>
        /// Получение изображения по уникальному идентификатору.
        /// </summary>
        [Description("Get_Image_By_Id")]
        GetImageById,
        /// <summary>
        /// Удаление изображения по уникальному идентификатору.
        /// </summary>
        [Description("Delete_Image")]
        RemoveImage,
    }
}
