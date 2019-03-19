using System.Windows.Forms;
using System.Drawing;

namespace BoolPgia
{
    public static class BoolPgiaFormConfig
    {
        #region Data Members
        private const byte k_NumOfColorsToGuess = 4;
        private const byte k_ColorButtonWidth = 44;
        private const byte k_SpaceBetweenUserGuessesAndRandomColorButtons = 16;
        private const byte k_SpaceBetweenGuessResultButtons = 18;
        private const byte k_GuessResultButtonWidth = 14;
        private const byte k_ColorButtonHeight = k_ColorButtonWidth;
        private const byte k_GuessResultButtonHeight = k_GuessResultButtonWidth;
        private const byte k_SpaceBetweenGuessButtons = 6;
        private const byte k_SpaceBetweenGuessButtonsAndFormBorder = 12;
        private const int k_ArrowBottunHeight = 22;
        private const int k_ArrowBottunWidth = 30;
        private const int FormHeight = 430;
        private const int k_StartAndFinishFormHeight = 140;
        private const int k_StartAndFinishFormWidth = 270;
        private const int FormWidth = 340;
        private const string k_ArrowButtonSign = "-->>";
        private const string k_StartButtonStr = "Start";
        private const string k_FormText = "Bool Pgia";
        private const string k_FinishFormText = "Finish BoolPgia Game";
        
        private static readonly Size r_FormSize = new Size(FormWidth, FormHeight);
        private static readonly Size r_GuessResultButtonSize = new Size(k_GuessResultButtonWidth, k_GuessResultButtonHeight); 
        private static readonly Size r_StartAndFinishFormSize = new Size(k_StartAndFinishFormWidth, k_StartAndFinishFormHeight);
        private static readonly Size r_ArrowButtonSize = new Size(k_ArrowBottunWidth, k_ArrowBottunHeight);
        private static readonly Size r_GuessButtonSize = new Size(k_ColorButtonWidth, k_ColorButtonHeight);
        private static Button s_UserGuessButton = new Button();
        private static Button s_ArrowButton = new Button();
        private static Button s_GuessResultButton = new Button();
        private static Button s_HiddenRandomButton = new Button();
        #endregion

        static BoolPgiaFormConfig()
        {
            configButtons();
        }

        private static void configButtons()
        {
            s_UserGuessButton.Size = ColorButtonSize;
            s_UserGuessButton.Enabled = false;

            s_ArrowButton.Size = ArrowButtonSize;
            s_ArrowButton.Text = ArrowButtonSign;
            s_ArrowButton.Enabled = false;

            s_HiddenRandomButton.Size = ColorButtonSize;
            s_HiddenRandomButton.Enabled = false;
            s_HiddenRandomButton.BackColor = Color.Black;

            s_GuessResultButton.Size = GuessResultButtonSize;
            s_GuessResultButton.Enabled = false;
        }

        public static Button UserGuessButtonConfig
        {
            get { return s_UserGuessButton; }
        }

        public static Button ArrowButtonConfig
        {
            get { return s_ArrowButton; }
        }

        public static Button GuessResultButtonConfig
        {
            get { return s_GuessResultButton; }
        }

        public static Button HiddenRandomButtonConfig
        {
            get { return s_HiddenRandomButton; }
        }

        public static byte NumColorsToGuess
        {
            get { return k_NumOfColorsToGuess; }
        }

        public static string ArrowButtonSign
        {
            get { return k_ArrowButtonSign; }
        }

        public static Size ArrowButtonSize
        {
            get { return r_ArrowButtonSize; }
        }

        public static Size ColorButtonSize
        {
            get { return r_GuessButtonSize; }
        }

        public static Size GuessResultButtonSize
        {
            get { return r_GuessResultButtonSize; }
        }

        public static Size FormSize
        {
            get { return r_FormSize; }
        }

        public static string FormText
        {
            get { return k_FormText; }
        }

        public static byte SpaceBetweenColorButtons
        {
            get { return k_SpaceBetweenGuessButtons; }
        }

        public static byte SpaceBetweenFormBorderAndColorButtons
        {
            get { return k_SpaceBetweenGuessButtonsAndFormBorder; }
        }

        public static byte SpaceBetweenUserGuessesAndRandomColorButtons
        {
            get { return k_SpaceBetweenUserGuessesAndRandomColorButtons; }
        }

        public static byte SpaceBetweenGuessResultButtons
        {
            get { return k_SpaceBetweenGuessResultButtons; }
        }

        public static string StartButtonStr
        {
            get
            {
                return k_StartButtonStr;
            }
        }

        public static Size StartAndFinishFormSize
        {
            get { return r_StartAndFinishFormSize; }
        }

        public static string FinishFormText
        {
            get { return k_FinishFormText; }
        }
    }
}
