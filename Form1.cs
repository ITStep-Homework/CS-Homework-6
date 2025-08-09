using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework6
{
    public partial class FormStudents : Form
    {
        private List<Student> students = new List<Student>();
        private int editingIndex = -1;
        private bool isEditing = false;

        public FormStudents()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFIO.Text) ||
                    string.IsNullOrWhiteSpace(txtGroup.Text) ||
                    string.IsNullOrWhiteSpace(txtAgerMark.Text))
                {
                    MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double avgMark = double.Parse(txtAgerMark.Text.Replace('.', ','));

                if (isEditing)
                {
                    // Режим редактирования
                    Student editedStudent = students[editingIndex];
                    editedStudent.FIO = txtFIO.Text;
                    editedStudent.Group = txtGroup.Text;
                    editedStudent.AvrgMark = avgMark;

                    ExitEditMode();
                    MessageBox.Show("Данные студента обновлены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Режим добавления
                    students.Add(new Student(txtFIO.Text, txtGroup.Text, avgMark));
                    MessageBox.Show("Студент добавлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ClearFields();
                UpdateStudents();
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректное значение среднего балла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStudents()
        {
            lstStudents.Items.Clear();
            foreach (Student student in students)
            {
                lstStudents.Items.Add(student.ToString());
            }
            lstStudents.Invalidate();
            lstStudents.Update();
        }

        private void bntRemove_Click(object sender, EventArgs e)
        {
            if (lstStudents.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите студента для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить этого студента?",
                                                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                students.RemoveAt(lstStudents.SelectedIndex);
                UpdateStudents();
                MessageBox.Show("Студент удален", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormStudents_Load(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstStudents.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите студента для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Переходим в режим редактирования
            editingIndex = lstStudents.SelectedIndex;
            isEditing = true;

            Student st = students[editingIndex];
            txtFIO.Text = st.FIO;
            txtGroup.Text = st.Group;
            txtAgerMark.Text = st.AvrgMark.ToString();

            // Меняем интерфейс для режима редактирования
            btnAdd.Text = "Сохранить";
            btnAdd.BackColor = Color.Orange;
            groupBox1.Text = " Редактирование студента ";

            MessageBox.Show("Режим редактирования активирован. Измените данные и нажмите 'Сохранить'",
                          "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lstStudents.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите студента для просмотра", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Student st = students[lstStudents.SelectedIndex];
            string info = $"ID: {st.id}\n" +
                         $"ФИО: {st.FIO}\n" +
                         $"Группа: {st.Group}\n" +
                         $"Средний балл: {st.AvrgMark:F2}";

            MessageBox.Show(info, "Информация о студенте", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateStudents();
            MessageBox.Show("Список обновлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearFields()
        {
            txtFIO.Clear();
            txtGroup.Clear();
            txtAgerMark.Clear();
        }

        private void ExitEditMode()
        {
            isEditing = false;
            editingIndex = -1;
            btnAdd.Text = "Добавить";
            btnAdd.BackColor = SystemColors.Control;
            groupBox1.Text = " Ввод информации о студенте ";
        }
    }
}