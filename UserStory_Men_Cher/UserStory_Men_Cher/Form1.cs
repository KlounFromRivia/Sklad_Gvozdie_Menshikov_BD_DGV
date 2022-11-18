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
        private readonly List<Tovar> tovars;
        private readonly BindingSource bindingSource;

        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            tovars = new List<Tovar>();
            bindingSource = new BindingSource();
            bindingSource.DataSource = tovars;
            dataGridView1.DataSource = bindingSource;
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

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var tvrInfoForm = new FormTovarInfo();
            tvrInfoForm.Text = "Добавление студента";
            if (tvrInfoForm.ShowDialog(this) == DialogResult.OK)
            {
                tvrInfoForm.Tovar.Id = Guid.NewGuid();
                //MessageBox.Show($"Name: {stdInfoForm.Student.FullName}\n\rGender: {stdInfoForm.Student.Gender}\n\rBirthDay: {stdInfoForm.Student.BirthDay:dd.MM.yyyy}\n\rAvgRate: {stdInfoForm.Student.AvgRate}\n\rDeduct: {stdInfoForm.Student.Deduct}\n\rDebtor: {stdInfoForm.Student.Debtor}", "", MessageBoxButtons.OK);
                tovars.Add(tvrInfoForm.Tovar);
                bindingSource.ResetBindings(false);
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
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 &&
                e.RowIndex >= 0 &&
                dataGridView1.Columns[e.ColumnIndex].Name == "AvgRColumn")
            {
                e.PaintBackground(e.ClipBounds, true);
                var val = decimal.Parse(e.Value.ToString());
                var resultW = (float)(val * e.CellBounds.Width) / 5.0f;
                if (val <= 2)
                {
                    e.Graphics.FillRectangle(Brushes.Red, e.CellBounds.X, e.CellBounds.Y, resultW, e.CellBounds.Height);
                }
                else if (2 < val && val <=4)
                {
                    e.Graphics.FillRectangle(Brushes.Yellow, e.CellBounds.X, e.CellBounds.Y, resultW, e.CellBounds.Height);
                }
                else if (val > 4)
                {
                    e.Graphics.FillRectangle(Brushes.Green, e.CellBounds.X, e.CellBounds.Y, resultW -1, e.CellBounds.Height-1);
                }
                //e.Graphics.DrawRectangle(Pens.Red, e.CellBounds.X, e.CellBounds.Y, resultW, e.CellBounds.Height);
                e.Handled = true;
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
                tovars.Remove(data);
                bindingSource.ResetBindings(false);
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
                data.Size = infoForm.Tovar.Size;
                data.AvgRate = infoForm.Tovar.AvgRate;
                bindingSource.ResetBindings(false);
            }
        }

        private void ToolStripMenuItemChange_Click(object sender, EventArgs e)
        {
            var data = (Tovar)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            var infoForm = new FormTovarInfo(data);
            infoForm.Text = "Редактирование студента";
            if (infoForm.ShowDialog(this) == DialogResult.OK)
            {
                data.TovarName = infoForm.Tovar.TovarName;
                data.Material = infoForm.Tovar.Material;
                data.Size = infoForm.Tovar.Size;
                data.AvgRate = infoForm.Tovar.AvgRate;
                bindingSource.ResetBindings(false);
            }
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            var data = (Tovar)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            if (MessageBox.Show($"Вы действительно хотите удалить '{data.TovarName}'?",
                    "Удаление записи",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                tovars.Remove(data);
                bindingSource.ResetBindings(false);
            }
        }
    }
}
