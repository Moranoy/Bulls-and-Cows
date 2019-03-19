using System.Windows.Forms;

namespace BoolPgia
{
    public class BoolPgiaGameManager
    {
        private RequestNumOfChancesForm m_RequestNumOfChancesForm; 
        private BoolPgiaForm m_BoolPgiaForm;

        public void Run()
        {
            this.m_RequestNumOfChancesForm = new RequestNumOfChancesForm(BoolPgiaFormConfig.StartAndFinishFormSize, BoolPgiaFormConfig.FormText);
            
            if (this.m_RequestNumOfChancesForm.ShowDialog() == DialogResult.OK)
            {
                byte numOfChances = this.m_RequestNumOfChancesForm.CurrentNumOfChances;

                this.m_BoolPgiaForm = new BoolPgiaForm(numOfChances);
                this.m_BoolPgiaForm.ShowDialog();
            }
        }
    }
}