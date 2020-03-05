using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErrorInPrograms
{
    public class Error
    {
        // Ошибка 4хх
        public partial class ClientError
        {
            private const string _windowTitte = "Ошибка на стороне клиента / Error on the client side";
            public static void BadRequest() => MessageBox.Show("Плохой, неверный запрос\nBad, wrong request", _windowTitte, MessageBoxButtons.OK, MessageBoxIcon.Error);
            public static void Unauthorized() => MessageBox.Show("Не авторизирован (не представился)\nNot authorized (not introduced)", _windowTitte, MessageBoxButtons.OK, MessageBoxIcon.Error);
            public static void NotFound() => MessageBox.Show("Не найдено\nNot found", _windowTitte, MessageBoxButtons.OK, MessageBoxIcon.Error);
            public static void Locked() => MessageBox.Show("Доступ заблокирован\nAccess blocked", _windowTitte, MessageBoxButtons.OK, MessageBoxIcon.Error);
            public static void UserExits() => MessageBox.Show("Такой пользователь уже существует\nThis user already exists", _windowTitte, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        // Ошибка 5хх
        public partial class ServerError
        {
            private const string _windowTittle = "Ошибка на стороне сервера / Error on the server side";
            public static void InternalServerError() => MessageBox.Show("Внутренняя ошибка сервера\nInternal Server Error", _windowTittle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            public static void UnknowError() => MessageBox.Show("Неизвестная ошибка\nUnknown Error", _windowTittle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            public static void RequestTimeOut() => MessageBox.Show("Истекло время ожидания\nHas timed out", _windowTittle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public class Warning
    {
        private const string _windowTitle = "Успешный запрос / Successful request";
        public static void SaveDataUser() => MessageBox.Show("Все данные были успешно сохранены\nAll data has been successfully saved.", _windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        public static DialogResult AllDataWillBeDeleted()
        {
            return MessageBox.Show("Все внесенные изменения не будут сохранены.\nAny changes you make will not be saved.", _windowTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        public static DialogResult DeleteFile()
        {
            return MessageBox.Show("Вы уверены, что хотиде удалить выбранный элемент?\nAre you sure you want to delete the selected item?", _windowTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        
    }
}
