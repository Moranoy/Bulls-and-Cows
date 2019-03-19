using System;
using System.Windows.Forms;
using System.Drawing;

namespace BoolPgia
{
    public class ColorPickForm : Form
    {
        private const byte k_NumOfColorsOptions = 8;
        private const byte k_SpaceBetweenButtonAndFormBorder = 8;
        private const byte k_SpaceBetweenButtons = 6;
        private const byte k_NumberOfButtonsInRow = 4;

        private readonly Button[] r_ColorOptionButtons;
        private Color[] m_AvailableColors = new Color[k_NumOfColorsOptions];
        private Color m_ChosenColor;

        public Color ChosenColor
        {
            get
            {
                return m_ChosenColor;
            }
        }

        public ColorPickForm()
        {
            Text = "Pick A Color:";
            Width = 230;
            Height = 150;
            r_ColorOptionButtons = new Button[k_NumOfColorsOptions];
            this.CenterToParent(); // set location of window
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            initializeColors();
            initializeButtons();
        }

        private void initializeColors() // set available colors
        {
            m_AvailableColors[0] = Color.MediumPurple;
            m_AvailableColors[1] = Color.Red;
            m_AvailableColors[2] = Color.LightGreen;
            m_AvailableColors[3] = Color.LightSkyBlue;
            m_AvailableColors[4] = Color.Blue;
            m_AvailableColors[5] = Color.Yellow;
            m_AvailableColors[6] = Color.DarkRed;
            m_AvailableColors[7] = Color.White;
        }

        private void initializeButtons() 
        {
            for (int i = 0; i < this.r_ColorOptionButtons.Length; i++)
            {
                this.r_ColorOptionButtons[i] = new Button();
                this.r_ColorOptionButtons[i].Click += new EventHandler(this.colorOptionButtons_OnClick);
            }

            Size ButtonSize = new Size(44, 44);
            Point currentLocation = new Point(k_SpaceBetweenButtonAndFormBorder, k_SpaceBetweenButtonAndFormBorder);
            bool startNewRow;

            for (int i = 0; i < r_ColorOptionButtons.Length; i++)
            {
                setButton(r_ColorOptionButtons[i], currentLocation, m_AvailableColors[i], ButtonSize);
                currentLocation.X += k_SpaceBetweenButtons + ButtonSize.Width;

                startNewRow = (i + 1) % k_NumberOfButtonsInRow == 0;

                // set location to new row
                if (startNewRow) 
                {
                    currentLocation.Y = r_ColorOptionButtons[0].Bottom + k_SpaceBetweenButtons;
                    currentLocation.X = k_SpaceBetweenButtonAndFormBorder;
                }
            }
            
            Controls.AddRange(r_ColorOptionButtons);
        }

        private void setButton(Button i_Button, Point i_Location, Color i_Color, Size i_ButtonSize) 
        {
            i_Button.Size = i_ButtonSize;
            i_Button.Location = i_Location;
            i_Button.BackColor = i_Color;
        }

        private void colorOptionButtons_OnClick(object sender, EventArgs e)
        {
            this.m_ChosenColor = (sender as Button).BackColor;
            this.Close();
        }
    }
}
