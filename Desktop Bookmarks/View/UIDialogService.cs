using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopBookmarks.View
{
    class UIDialogService : IDialogService
    {
        public DialogResult ShowMessageBox(string caption, string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(text, caption, buttons, icon);
        }
    }
}
