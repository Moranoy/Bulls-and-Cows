using System;
using System.Windows.Forms;

namespace BoolPgia
{
    public static class ButtonUtils
    {
        public static void SetButtonArray(Button[] i_ButtonArray, Button i_ToCopy, EventHandler i_ClickHandler, bool i_IsEnable)
        {
            for (int i = 0; i < i_ButtonArray.Length; i++)
            {
                i_ButtonArray[i] = new Button();
                SetButton(i_ButtonArray[i], i_ToCopy, i_ClickHandler, i_IsEnable);
            }
        }

        public static void SetButton(Button i_Button, Button i_ToCopy, EventHandler i_ClickHandler, bool i_IsEnable)
        {
            cloneImportantProperties(i_Button, i_ToCopy);
            i_Button.Click += i_ClickHandler;
            i_Button.Enabled = i_IsEnable;
        }

        private static void cloneImportantProperties(Button button, Button i_ToCopy)
        {
            button.BackColor = i_ToCopy.BackColor;
            button.Size = i_ToCopy.Size;
            button.Text = i_ToCopy.Text;
            button.Enabled = i_ToCopy.Enabled;
        }
    }
}
