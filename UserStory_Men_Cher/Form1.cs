using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserStory_Men_Cher.Models;

namespace UserStory_Men_Cher
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = ReadDb();
            provLabel();
        }

        private void CreateDb(FormTovarInfo infoForm)
        {
            using (ApplicationContext db = new ApplicationContext(DataBaseHellper.Options()))
            {
                infoForm.Tovar.Id = Guid.NewGuid();
                db.TovarBD.Add(infoForm.Tovar);
                db.SaveChanges();
            }
        }
        private static List<Tovar> ReadDb()
        {
            using (ApplicationContext db = new ApplicationContext(DataBaseHellper.Options()))
            {
                return db.TovarBD.ToList();
            }
        }
        private static void UpDateDb(Tovar tvr)
        {
            using (ApplicationContext db = new ApplicationContext(DataBaseHellper.Options()))
            {
                var Tvr = db.TovarBD.FirstOrDefault(u => u.Id == tvr.Id);
                if (Tvr != null)
                {
                    Tvr.TovarName = tvr.TovarName;
                    Tvr.Size = tvr.Size;
                    Tvr.Material = tvr.Material;
                    Tvr.Count = tvr.Count;
                    Tvr.MinCount = tvr.MinCount;
                    Tvr.Price = tvr.Price;
                    Tvr.AllPrice = tvr.Price * tvr.Count;
                    db.SaveChanges();
                }
            }
        }
        private static void RemoveDb(Tovar tvr)
        {
            using (ApplicationContext db = new ApplicationContext(DataBaseHellper.Options()))
            {
                var Tvr = db.TovarBD.FirstOrDefault(u => u.Id == tvr.Id);
                if (tvr != null)
                {
                    db.TovarBD.Remove(Tvr);
                    db.SaveChanges();
                }
            }
        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolStripMenuItemProg_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа 'Реестр товаров' ", "О программе", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void provLabel()
        {
            using (ApplicationContext db = new ApplicationContext(DataBaseHellper.Options()))
            {
                toolStripStatuslblAllKolvo.Text = "Колличество товаров на складе: " + Convert.ToString(db.TovarBD.ToList().Count());
                toolStripStatuslblAllPrice.Text = "Общая сумма товаров без НДС: " + Convert.ToString(db.TovarBD.ToList().Sum(x => x.AllPrice));
                toolStripStatuslblPriceNDS.Text = "Общая сумма товаров с НДС 20%: " + Convert.ToString(db.TovarBD.ToList().Sum(x => x.AllPrice + (x.AllPrice * 0.2)));
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var tvrInfoForm = new FormTovarInfo();
            tvrInfoForm.Text = "Добавление студента";
            if (tvrInfoForm.ShowDialog(this) == DialogResult.OK)
            {
                CreateDb(tvrInfoForm);
                dataGridView1.DataSource = ReadDb();
                provLabel();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "MaterialColumn")
            {
                var val = (Material)e.Value;
                switch (val)
                {
                    case Material.Med:
                        e.Value = "Медь";
                        break;
                    case Material.Stal:
                        e.Value = "Сталь";
                        break;
                    case Material.Iron:
                        e.Value = "Железо";
                        break;
                    case Material.Hrom:
                        e.Value = "Хром";
                        break;
                }
            }
            if (e.ColumnIndex >= 0 &&
                e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "CountColumn")
            {
                var val = int.Parse(e.Value.ToString());
                var minVal = int.Parse(dataGridView1[e.ColumnIndex+1,e.RowIndex].Value.ToString());
                if (val < minVal)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.Red;
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            toolStripButtonChange.Enabled =
                toolStripButtonDelete.Enabled = ToolStripMenuItemChange.Enabled = ToolStripMenuItemDelete.Enabled =
                dataGridView1.SelectedRows.Count > 0;
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var data = (Tovar)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            if(MessageBox.Show($"Вы действительно хотите удалить '{data.TovarName}'?",
                    "Удаление записи",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RemoveDb(data);
                dataGridView1.DataSource = ReadDb();
                provLabel();
            }
        }

        private void toolStripButtonChange_Click(object sender, EventArgs e)
        {
            var data = (Tovar)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            var infoForm = new FormTovarInfo(data);
            infoForm.Text = "Редактирование студента";
            if (infoForm.ShowDialog(this)== DialogResult.OK)
            {
                data.TovarName = infoForm.Tovar.TovarName;
                data.Material = infoForm.Tovar.Material;
                data.Size  = infoForm.Tovar.Size;
                data.Count = infoForm.Tovar.Count;
                data.MinCount = infoForm.Tovar.MinCount;
                data.Price = infoForm.Tovar.Price;
                data.AllPrice = infoForm.Tovar.Price * infoForm.Tovar.Count;
                UpDateDb(data);
                dataGridView1.DataSource = ReadDb();
                provLabel();
            }
        }
    }
}
