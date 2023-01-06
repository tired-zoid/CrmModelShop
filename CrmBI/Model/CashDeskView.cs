using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmBI.Model
{
    public class CashDeskView
    {
        CashDesk cashDesk { get; set; }
        public Label CashDeskName { get; set; }
        public NumericUpDown Price { get; set; }   
        public ProgressBar QueueLength { get; set; }
        public Label LeaveCutomers { get; set; }

        public CashDeskView(CashDesk cashDesk, int number, int x, int y)
        {
            CashDeskName = new Label();
            Price = new NumericUpDown();
            QueueLength = new ProgressBar();
            LeaveCutomers = new Label();

            this.cashDesk = cashDesk;
            CashDeskName.AutoSize = true;
            CashDeskName.Location = new System.Drawing.Point(x, y);
            CashDeskName.Name = "label1" + number;
            CashDeskName.Size = new System.Drawing.Size(35, 13);
            CashDeskName.TabIndex = number;
            CashDeskName.Text = cashDesk.ToString();

            Price.Location = new System.Drawing.Point(x + 70, y);
            Price.Name = "numericUpDown" + number;
            Price.Size = new System.Drawing.Size(120, 20);
            Price.TabIndex = number;
            Price.Maximum = 1000000000000000;

            QueueLength.Location = new System.Drawing.Point(x + 250, y);
            QueueLength.Maximum = cashDesk.MaxQueueLength;
            QueueLength.Name = "progressBar" + number;
            QueueLength.Size = new System.Drawing.Size(100, 23);
            QueueLength.TabIndex = number;
            QueueLength.Value = 0;

            LeaveCutomers.AutoSize = true;
            LeaveCutomers.Location = new System.Drawing.Point(x + 400, y);
            LeaveCutomers.Name = "label2" + number;
            LeaveCutomers.Size = new System.Drawing.Size(35, 13);
            LeaveCutomers.TabIndex = number;
            LeaveCutomers.Text = "";

            cashDesk.CheckClosed += CashDesk_CheckClosed;
        }

        private void CashDesk_CheckClosed(object sender, Check e)
        {
            
            Price.Invoke((Action)delegate 
            {
                Price.Value += e.Price;
                QueueLength.Value = cashDesk.Count;
                LeaveCutomers.Text = cashDesk.ExitCustomer.ToString();
            });
        }
    }
}
