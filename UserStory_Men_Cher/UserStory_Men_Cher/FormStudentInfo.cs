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
    public partial class FormTovarInfo : Form
    {
        private readonly Tovar tovars;
        public FormTovarInfo()
        {
            InitializeComponent();
            FillMaterial();
            tovars = new Tovar
            {
                Material = Material.Med,
            };
            comboBoxMaterial.SelectedItem = tovars.Material;
        }

        public FormTovarInfo(Tovar source)
            :this()
        {
            textBoxName.Text = source.TovarName;
            comboBoxMaterial.SelectedItem = source.Material;
            numericUpDownAvg.Value = source.AvgRate;
            maskedTextBoxSize.Text = source.Size;
        }
        public Tovar Tovar => tovars;
        private void FillMaterial()
        {
            foreach (var item in Enum.GetValues(typeof(Models.Material)))
            {
                comboBoxMaterial.Items.Add(item);
            }
        }

        private void comboBoxDayB_DrawItem(object sender, DrawItemEventArgs e)
        {
            var parent = sender as ComboBox;
            if (parent != null)
            {
                e.DrawBackground();
                Brush brush = new SolidBrush(parent.ForeColor);
                if ((e.State & DrawItemState.Selected) ==  DrawItemState.Selected)
                {
                    brush = SystemBrushes.HighlightText;
                }

                if (e.Index >= 0)
                {
                    if (parent.Items[e.Index] is Material material)
                    {
                        //var text = gender == Material.Med //условие if сахар
                        //    ? "Мужской"
                        //    : "Женский";
                        var text = "";
                        switch (material)
                        {
                            case Material.Med:
                                text = "Медь";
                                break;
                            case Material.Stal:
                                text = "Сталь";
                                break;
                            case Material.Iron:
                                text = "Железо";
                                break;
                            case Material.Hrom:
                                text = "Хром";
                                break;
                        }

                        e.Graphics.DrawString(
                            text,
                            parent.Font,
                            brush,
                            e.Bounds);
                    }
                    else
                    {
                        e.Graphics.DrawString(
                            parent.Items[e.Index].ToString(),
                            parent.Font,
                            brush,
                            e.Bounds);
                    }
                }
            }
        }

        private void numericUpDownAvg_ValueChanged(object sender, EventArgs e)
        {
            tovars.AvgRate = numericUpDownAvg.Value;
        }

        public void Validate()
        {
            buttonSave.Enabled = !string.IsNullOrEmpty(tovars.TovarName);
        }

        private void comboBoxMaterial_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBoxMaterial.SelectedIndex >= 0)
            {
                tovars.Material = (Material)comboBoxMaterial.SelectedItem;
            }
        }

        private void maskedTextBoxSize_TextChanged(object sender, EventArgs e)
        {
            tovars.Size = maskedTextBoxSize.Text.Trim();
            Validate();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            textBoxName.Text = textBoxName.Text.ToUpper();
            tovars.TovarName = textBoxName.Text.Trim();
            Validate();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }
    }
}
