using System;
using System.Drawing;

namespace BoolPgia
{
    public class BoolPgiaLogic
    {
        public enum eColor
        {
            MediumPurple,
            Red,
            LightGreen,
            LightSkyBlue,
            Blue,
            Yellow,
            DarkRed,
            White
        }

        private const byte k_NumOfRandomColor = 4;
        private eColor[] m_Colors = new eColor[k_NumOfRandomColor];

        public void DrawRandomColors()
        {
            const byte k_NumOfColorsOptions = 8;
            int index = 0;
            int chosenColorInt;
            int numOfColorsOptions = k_NumOfColorsOptions - 1;
            Random randomColorGenerator = new Random();
            eColor chosenColor;
            bool isColorNotInArray;

            // randomly generate colors and enter into eColor array
            while (index < k_NumOfRandomColor) 
            {
                chosenColorInt = randomColorGenerator.Next(0, numOfColorsOptions);
                chosenColor = (eColor)Enum.ToObject(typeof(eColor), chosenColorInt);
                isColorNotInArray = Array.IndexOf(this.m_Colors, chosenColor) < 0; // check if color is not in array

                if (isColorNotInArray) 
                {
                    this.m_Colors[index] = chosenColor;
                    index++;
                }
            }
        }
        
        public void CheckGuess(eColor[] i_ChosenColor, out byte io_NumOfV, out byte io_NumOfX)
        {
            int currentColorIndex = 0;
            int indexOfColorInArray; 
            io_NumOfV = 0;
            io_NumOfX = 0;
            foreach (eColor color in i_ChosenColor)
            {
                indexOfColorInArray = Array.IndexOf(this.m_Colors, color);
                
                if (indexOfColorInArray == currentColorIndex)
                { // guessed correct location of the color
                    io_NumOfV++;
                }
                else if (indexOfColorInArray >= 0)
                { // guessed correct color, but not in correct location
                    io_NumOfX++;
                }

                currentColorIndex++;
            }
        }

        public eColor ConvertColorToEColor(Color i_ColorToConvert)
        {
            eColor color = new eColor();
            
            foreach (eColor ecolor in Enum.GetValues(typeof(eColor)))
            {
                if (ecolor.ToString() == i_ColorToConvert.Name)
                {
                    color = ecolor;
                    break;
                }
            }

            return color;
        }
    }
}
