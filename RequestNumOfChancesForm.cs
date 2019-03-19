using System;
using System.Windows.Forms;
using System.Drawing;

namespace BoolPgia
{
    public class RequestNumOfChancesForm : Form
    {
        private const byte k_MaxNumOfChances = 10;
        private const byte k_MinNumOfChances = 4;
        private const byte k_FixedSpace = 10;
        private byte m_CurrentNumOfChances;
        private Button m_NumOfChancesButton = new Button();
        private Button m_StartButton = new Button();

        public RequestNumOfChancesForm(Size i_Size, string i_Text)
        {
            InitializeComponent(i_Size, i_Text);
            initializeButtons();
        }

        public byte CurrentNumOfChances
        {
            get { return m_CurrentNumOfChances; }
        }

        #region Initialize

        private void InitializeComponent(Size i_Size, string i_Text)
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Size = i_Size;
            this.Text = i_Text; 
        }

        private void initializeButtons()
        {
            m_CurrentNumOfChances = k_MinNumOfChances;
            initializeNumOfChancesButton();
            initializeStartButton();
        }

        private void initializeNumOfChancesButton()
        {
            const byte k_Space = 4;

            updateNumOfChancesButtonText();
            m_NumOfChancesButton.Click += new EventHandler(NumOfChancesButton_OnClick);
            m_NumOfChancesButton.Width = Width - (k_Space * k_FixedSpace);
            m_NumOfChancesButton.Left = k_FixedSpace;
            m_NumOfChancesButton.Top = k_FixedSpace;
            Controls.Add(m_NumOfChancesButton);
        }

        private void updateNumOfChancesButtonText()
        {
            m_NumOfChancesButton.Text = string.Format("Number Of Chances: {0}", m_CurrentNumOfChances);
        }

        private void initializeStartButton()
        {
            const byte k_Space = 5;

            m_StartButton.Text = BoolPgiaFormConfig.StartButtonStr;
            m_StartButton.Click += new EventHandler(StartButton_OnClick);
            m_StartButton.Width = m_NumOfChancesButton.Width / 3;
            m_StartButton.Left = m_NumOfChancesButton.Right - m_StartButton.Width;
            m_StartButton.Top = Height - (k_Space * k_FixedSpace) - m_StartButton.Height;
            Controls.Add(m_StartButton);
        }
#endregion

        private void NumOfChancesButton_OnClick(object sender, EventArgs e)
        {
            m_CurrentNumOfChances++;

            if (m_CurrentNumOfChances > k_MaxNumOfChances)
            {
                m_CurrentNumOfChances = k_MinNumOfChances;
            }

            updateNumOfChancesButtonText();
        }

        private void StartButton_OnClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}