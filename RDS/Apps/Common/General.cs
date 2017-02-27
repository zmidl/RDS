using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace RDS.Apps.Common
{
    static class General
    {
        public static void ExitView(object oldContent, Window newContent, IExitView iExitView)
        {
            iExitView.ExitView = new Action(() => { newContent.Content = oldContent; });
            newContent.Content = iExitView;
        }

        public static void ExitView(object oldContent, UserControl newContent, IExitView iExitView)
        {
            iExitView.ExitView = new Action(() => { newContent.Content = oldContent; });
            newContent.Content = iExitView;
        }
    }
}
