﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace XU3R7F_kliens
{
    internal class Helper
    {
        const string BACKGROUND_COLOR = "#EFE2D2";

        public static void AddInputCleaner(Border border)
        {
            StackPanel panel = border.Child as StackPanel;
            foreach (object parent in panel.Children)
            {
                if (parent is StackPanel)
                {
                    foreach (object child in (parent as StackPanel).Children)
                    {
                        if (child is TextBox) (child as TextBox).Clear();
                        if (child is CheckBox) (child as CheckBox).IsChecked = false;
                        if (child is ComboBox) (child as ComboBox).SelectedIndex = -1;
                        if (child is PasswordBox) (child as PasswordBox).Clear();
                    }
                }
            }


        }

    }
}
