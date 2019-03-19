using System;
using System.Windows.Forms;
using System.Drawing;

namespace BoolPgia
{
    internal class RowOfButtons
    {
        private readonly byte r_NumColorsToGuesses;
        private Button[] m_UserGuessButtons;
        private Button m_Arrows;
        private Button[] m_GuessResultButtons;
        private Form m_ParentForm;

        public event EventHandler ArrowButtonClickedHandler;

        public RowOfButtons(Form i_ParentForm, byte i_NumOfColorsToGuess)
        {
            this.m_ParentForm = i_ParentForm;
            this.r_NumColorsToGuesses = i_NumOfColorsToGuess;
        }

        public void InitializeButtons(Button i_UserGuessButtonConfig, Button i_ArrowButtonConfig, Button i_GuessResultButtonConfig)
        { 
            const bool v_Disabled = false;

            this.m_UserGuessButtons = new Button[this.r_NumColorsToGuesses];
            EventHandler buttonClickEventHandler = new EventHandler(this.userGuessesButton_OnClick);
            ButtonUtils.SetButtonArray(this.m_UserGuessButtons, i_UserGuessButtonConfig, buttonClickEventHandler, v_Disabled);

            this.m_Arrows = new Button();
            buttonClickEventHandler = new EventHandler(this.arrowButton_OnClick);
            ButtonUtils.SetButton(this.m_Arrows, i_ArrowButtonConfig, buttonClickEventHandler, v_Disabled);

            const EventHandler k_NoEvent = null;

            this.m_GuessResultButtons = new Button[this.r_NumColorsToGuesses];
            ButtonUtils.SetButtonArray(this.m_GuessResultButtons, i_GuessResultButtonConfig, k_NoEvent, v_Disabled);

            this.addButtonsToControls();
        }
        
        private void addButtonsToControls()
        {
            for (int i = 0; i < this.r_NumColorsToGuesses; i++)
            {
                this.m_ParentForm.Controls.AddRange(this.m_UserGuessButtons);
                this.m_ParentForm.Controls.AddRange(this.m_GuessResultButtons);
            }

            this.m_ParentForm.Controls.Add(this.m_Arrows);
        }

        public void SetRowButtonsLocation(Point i_CurrentPosition, byte i_SpaceBetweenBorderAndColorButton, Size i_ColorButtonSize, byte i_SpaceBetweenColorButtons, byte i_SpaceBetweenGuessResultButtons)
        {
            Point guessResultPosition = new Point(i_SpaceBetweenBorderAndColorButton, i_SpaceBetweenBorderAndColorButton);

            this.setGuessButtonRow(ref i_CurrentPosition, i_SpaceBetweenColorButtons + i_ColorButtonSize.Width); // set a row of user guess buttons
            this.setArrowButtonInRow(ref i_CurrentPosition, i_SpaceBetweenColorButtons + i_ColorButtonSize.Width);

            guessResultPosition.X = i_CurrentPosition.X += i_SpaceBetweenColorButtons;
            guessResultPosition.Y = i_CurrentPosition.Y + i_SpaceBetweenGuessResultButtons;
            this.setGuessResultInRow(ref guessResultPosition, i_SpaceBetweenGuessResultButtons);

            i_CurrentPosition.Y += this.m_UserGuessButtons[0].Height + i_SpaceBetweenGuessResultButtons; // get the Y position of the next row of buttons
            i_CurrentPosition.X = i_SpaceBetweenBorderAndColorButton;
        }

        private void setGuessButtonRow(ref Point io_CurrentPosition, int i_SpacesBuffer)
        {
            for (byte i = 0; i < this.r_NumColorsToGuesses; i++)
            {
                this.m_UserGuessButtons[i].Location = io_CurrentPosition;
                io_CurrentPosition.X += i_SpacesBuffer;
            }
        }

        private void setGuessResultInRow(ref Point io_CurrentPosition, int i_SpacesBuffer)
        {
            int startPositionX = io_CurrentPosition.X;
            bool isFirstRow = true;

            for (byte i = 0; i < this.r_NumColorsToGuesses; i++)
            {
                if (i >= 2 && isFirstRow)
                {
                    io_CurrentPosition.X = startPositionX;
                    io_CurrentPosition.Y += i_SpacesBuffer;
                    isFirstRow = false;
                }

                this.m_GuessResultButtons[i].Location = io_CurrentPosition;
                io_CurrentPosition.X += i_SpacesBuffer;
            }

            io_CurrentPosition.X = startPositionX;
        }

        private void setArrowButtonInRow(ref Point io_CurrentPosition, int i_SpacesBuffer)
        {
            int hightExtraSpace = BoolPgiaFormConfig.ColorButtonSize.Height / 2;

            io_CurrentPosition.Y += hightExtraSpace - (BoolPgiaFormConfig.ArrowButtonSize.Height / 2);
            this.m_Arrows.Location = io_CurrentPosition;
            io_CurrentPosition.X += i_SpacesBuffer;
            io_CurrentPosition.Y -= hightExtraSpace;
        }

        private void arrowButton_OnClick(object sender, EventArgs e)
        {
            this.ArrowButtonClickedHandler(this, e);
        }

        public int Bottom
        {
            get { return this.m_UserGuessButtons[0].Bottom; }
        }

        private void userGuessesButton_OnClick(object sender, EventArgs e)
        {
            ColorPickForm colorPickForm = new ColorPickForm();

            colorPickForm.ShowDialog();

            (sender as Button).BackColor = colorPickForm.ChosenColor;

            if (this.isGuessComplete())
            {
                this.m_Arrows.Enabled = true;
            }
        }

        private bool isGuessComplete()
        {
            int counter = 0;
            Button userGuessButton = BoolPgiaFormConfig.GuessResultButtonConfig;
            Color defaultBackColor = userGuessButton.BackColor;

            foreach (Button guess in this.m_UserGuessButtons)
            {
                if (!(guess.BackColor == defaultBackColor))
                {
                    counter++;
                }
            }

            return counter == BoolPgiaFormConfig.NumColorsToGuess;
        }

        public void SetColorButtonsEnable(bool i_IsEnable)
        {
            foreach (Button button in this.m_UserGuessButtons)
            {
                button.Enabled = i_IsEnable;
            }
        }

        public void SetArrowButtonsEnable(bool i_IsEnable)
        {
            this.m_Arrows.Enabled = i_IsEnable;
        }

        public Color[] GetChosenColors()
        {
            Color[] chosenColor = new Color[this.r_NumColorsToGuesses];
            byte index = 0;

            foreach (Button button in this.m_UserGuessButtons)
            {
                chosenColor[index] = button.BackColor;
                index++;
            }

            return chosenColor;
        }

        /// <summary>
        /// Method set resultGuessButtonArray colors 
        /// </summary>
        /// <param name="i_NumOfV"> Num of "Bool"</param>
        /// <param name="i_NumOfX">Num of "Pgia"</param>
        public void SetResultGuessButtonColors(byte i_NumOfV, byte i_NumOfX)
        {
            bool isDrawBlack, isDrawYellow;

            for (int i = 0; i < BoolPgiaFormConfig.NumColorsToGuess; i++)
            {
                isDrawBlack = i < i_NumOfV;
                isDrawYellow = i >= i_NumOfV && i < i_NumOfX + i_NumOfV;

                if (isDrawBlack)
                {
                    this.m_GuessResultButtons[i].BackColor = Color.Black;
                }
                else if (isDrawYellow)
                {
                    this.m_GuessResultButtons[i].BackColor = Color.Yellow;
                }
            }
        }
    }
}