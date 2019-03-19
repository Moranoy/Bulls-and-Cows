using System.Windows.Forms;
using System.Drawing;

namespace BoolPgia
{
    public class FinishForm : Form
    {
        private const byte k_SpaceFromTopBorder = 30;
        private const byte k_SpaceFromLeftBorder = 90;
        private Label m_Label;

        public FinishForm(bool i_IsGameFinishSuccessefuly, Size i_Size, string i_Text)
        {
            const bool v_Disable = false;

            initializeLabel(i_IsGameFinishSuccessefuly);
            Size = i_Size;
            this.CenterToParent();
            MinimizeBox = v_Disable;
            MaximizeBox = v_Disable;
            Text = i_Text;
            Controls.Add(m_Label);
        }

        private void initializeLabel(bool i_IsGameFinishSuccessefuly)
        {
            m_Label = new Label();
            m_Label.Top = Top + k_SpaceFromTopBorder;
            m_Label.Left = Left + k_SpaceFromLeftBorder;

            if (i_IsGameFinishSuccessefuly)
            {
                m_Label.Text = "Congrats, You Won!! :)";
            }
            else
            {
                m_Label.Text = "Game Over, You Lost :(";
            }
        }
    }
}
