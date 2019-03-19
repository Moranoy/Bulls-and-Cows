using System.Windows.Forms;
using System.Drawing;
using System;

namespace BoolPgia
{
    public class BoolPgiaForm : Form
    {
        private readonly byte r_NumOfGuesses;
        private BoolPgiaLogic m_BoolPgiaLogic = new BoolPgiaLogic();
        private byte m_CurrentNumOfGuess = 0;
        private Button[] m_HiddenRandomColorButtons = new Button[BoolPgiaFormConfig.NumColorsToGuess];
        private RowOfButtons[] m_RowsOfButtons;
        private FinishForm m_FinishForm;

        public BoolPgiaForm(byte i_NumOfGuesses)
        {
            r_NumOfGuesses = i_NumOfGuesses;           
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            initialize();
        }

        #region Initialize
        private void initialize()
        {
            const byte k_SpaceFromBottom = 50;
            const bool v_Enable = true;
            int numOFGuessMinosOne = r_NumOfGuesses - 1;

            initializeForm();
            initializeButtons();
            this.m_BoolPgiaLogic.DrawRandomColors();
            this.m_RowsOfButtons[m_CurrentNumOfGuess].SetColorButtonsEnable(v_Enable); // enable first row of buttons
            this.Height = m_RowsOfButtons[numOFGuessMinosOne].Bottom + k_SpaceFromBottom;
            this.CenterToScreen();
        }

        private void initializeButtons()
        {
            for (int i = 0; i < BoolPgiaFormConfig.NumColorsToGuess; i++)
            {
                m_HiddenRandomColorButtons[i] = new Button();
            }

            initializeButtonsLocation();
        }

        protected void initializeForm()
        { // protected for injection point
            Size = BoolPgiaFormConfig.FormSize;
            Text = BoolPgiaFormConfig.FormText;
        }

        protected void initializeButtonsLocation()
        { // protected for injection point
            Point currentPosition = new Point(BoolPgiaFormConfig.SpaceBetweenFormBorderAndColorButtons, BoolPgiaFormConfig.SpaceBetweenFormBorderAndColorButtons);

            setHiddenButtonsAppearance(currentPosition);
            Controls.AddRange(m_HiddenRandomColorButtons);
            setRowButtonsLocation(currentPosition);
        }

        private void setRowButtonsLocation(Point i_CurrentPosition)
        {
            m_RowsOfButtons = new RowOfButtons[r_NumOfGuesses];

            int bottom = m_HiddenRandomColorButtons[0].Bottom + BoolPgiaFormConfig.SpaceBetweenUserGuessesAndRandomColorButtons;

            for (byte i = 0; i < r_NumOfGuesses; i++)
            {
                m_RowsOfButtons[i] = new RowOfButtons(this, BoolPgiaFormConfig.NumColorsToGuess);
                m_RowsOfButtons[i].InitializeButtons(BoolPgiaFormConfig.UserGuessButtonConfig, BoolPgiaFormConfig.ArrowButtonConfig, BoolPgiaFormConfig.GuessResultButtonConfig); // initialize button configs
                i_CurrentPosition.Y = bottom;
                m_RowsOfButtons[i].SetRowButtonsLocation(i_CurrentPosition, BoolPgiaFormConfig.SpaceBetweenFormBorderAndColorButtons, BoolPgiaFormConfig.ColorButtonSize, BoolPgiaFormConfig.SpaceBetweenColorButtons, BoolPgiaFormConfig.SpaceBetweenGuessResultButtons); // set row button pos
                bottom = m_RowsOfButtons[i].Bottom + BoolPgiaFormConfig.SpaceBetweenColorButtons;
                m_RowsOfButtons[i].ArrowButtonClickedHandler += new EventHandler(ArrowButton_OnClick);
            }
        }
        #endregion
        
        private void ArrowButton_OnClick(object sender, EventArgs e) 
        {
            RowOfButtons currentRowOfButtons = sender as RowOfButtons;
            const bool v_EnableButton = true;
            bool isNotLastGuess = m_CurrentNumOfGuess != m_RowsOfButtons.Length - 1;
            
            if (isNotLastGuess)
            {
                m_RowsOfButtons[m_CurrentNumOfGuess].SetColorButtonsEnable(!v_EnableButton); // disable current row
                m_RowsOfButtons[m_CurrentNumOfGuess].SetArrowButtonsEnable(!v_EnableButton); // disable current arrow
                m_RowsOfButtons[m_CurrentNumOfGuess + 1].SetColorButtonsEnable(v_EnableButton); // enable next row
            }

            checkGuess();
            this.m_CurrentNumOfGuess++;
        }

        private void setHiddenButtonsAppearance(Point i_CurrentPosition)
        {
            // set hidden random color buttons location
            foreach (Button hiddenRandomColorButton in m_HiddenRandomColorButtons)
            {
                hiddenRandomColorButton.Location = i_CurrentPosition;
                hiddenRandomColorButton.Size = BoolPgiaFormConfig.ColorButtonSize;
                hiddenRandomColorButton.BackColor = BoolPgiaFormConfig.HiddenRandomButtonConfig.BackColor;
                hiddenRandomColorButton.Enabled = false;
                i_CurrentPosition.X += BoolPgiaFormConfig.SpaceBetweenColorButtons +
                                                 BoolPgiaFormConfig.ColorButtonSize.Width;
            }
        }
        
        /// <summary>
        /// After user guessed four colors, the method checks the result
        /// </summary>
        private void checkGuess()
        {
            const bool v_GameWon = true;
            byte numOfX, numOfV;
            BoolPgiaLogic.eColor[] userGuess;
         
            userGuess = getAndConvertChosenColorToEColor(m_RowsOfButtons[m_CurrentNumOfGuess]);

            this.m_BoolPgiaLogic.CheckGuess(userGuess, out numOfV, out numOfX); // check users guess
            this.m_RowsOfButtons[m_CurrentNumOfGuess].SetResultGuessButtonColors(numOfV, numOfX); // set results

            bool isGuessCurrect = numOfV == BoolPgiaFormConfig.NumColorsToGuess;

            if (isGuessCurrect)
            {
                this.exposeCorrectColors();
                this.m_FinishForm = new FinishForm(v_GameWon, BoolPgiaFormConfig.StartAndFinishFormSize, BoolPgiaFormConfig.FinishFormText);
                this.m_FinishForm.ShowDialog();
            }
            else if (this.m_CurrentNumOfGuess == this.r_NumOfGuesses - 1)
            { // at the final guess
                this.m_FinishForm = new FinishForm(!v_GameWon, BoolPgiaFormConfig.StartAndFinishFormSize, BoolPgiaFormConfig.FinishFormText);
                this.m_FinishForm.ShowDialog();
                this.Close();
            }
        }

        private void exposeCorrectColors()
        {
            byte index = 0;
            Color[] colors = m_RowsOfButtons[m_CurrentNumOfGuess].GetChosenColors();

            foreach (Button button in m_HiddenRandomColorButtons)
            {
                button.BackColor = colors[index];
                index++;
            }
        }

        /// <summary>
        /// Method get User's guess and convert it to Ecolor
        /// </summary>
        /// <param name="i_CurrentRowOfButtons">Param is the current row</param>
        /// <returns></returns>
        private BoolPgiaLogic.eColor[] getAndConvertChosenColorToEColor(RowOfButtons i_CurrentRowOfButtons)
        {
            BoolPgiaLogic.eColor[] convertedColors = new BoolPgiaLogic.eColor[BoolPgiaFormConfig.NumColorsToGuess]; // dummy initialize
            Color[] colorsToConvert = new Color[BoolPgiaFormConfig.NumColorsToGuess];
            int index = 0;

            colorsToConvert = i_CurrentRowOfButtons.GetChosenColors();

            foreach (Color chosenColor in colorsToConvert)
            {
                convertedColors[index] = m_BoolPgiaLogic.ConvertColorToEColor(chosenColor);
                index++;
            }

            return convertedColors;
        }                
    }
}
