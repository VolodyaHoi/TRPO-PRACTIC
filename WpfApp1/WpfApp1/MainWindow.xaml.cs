using System;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int array_size = 0;
        int[] array;
        int[] sec_array;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btn_CreateArray(object sender, RoutedEventArgs e)
        {
            bool isNum;
            isNum = int.TryParse(tb_SizeInput.Text, out int size);
            if (isNum && size != 0 && size > 0) 
            {
                array_size = size;
                MessageBox.Show($"Размер массива установлен ({size})!", "Информация");
                tb_ElementsCountInfo.Text = $"Установленное кол-во элементов: {size}";
            }
            else
            {
                MessageBox.Show("Введено некорректное значение!", "Информация");
            }
        }

        public void get_Array()
        {
            DataView dataView = (DataView)dg_Array.ItemsSource;
            int[] allValuesInDataGrid = dataView.Table.AsEnumerable()
                .SelectMany(row => row.ItemArray.OfType<object>()
                .Where(value => int.TryParse(value.ToString(), out _))
                .Select(value => Convert.ToInt32(value)))
                .ToArray();

            string result = string.Join(", ", allValuesInDataGrid);
            array = allValuesInDataGrid;
        }
        private void btn_SaveArray(object sender, RoutedEventArgs e)
        {
            get_Array();
            MessageBox.Show("Изменения сохранены!", "Информация");
        }

        private void btn_DrawGraph(object sender, RoutedEventArgs e)
        {
            if (array != null && sec_array != null)
            {
                DrawingDisplayClass drawingDisplayClass = new DrawingDisplayClass();
                TextBlock el;
                Line[] axis_x;
                Line[] axis_y;
                Line[] intervals_x;
                Line[] intervals_y;
                Line[] graph1, graph2;
                TextBlock[] markers_x;
                TextBlock[] markers_y;
                int[,] coords;
                int iterator = 0;

                get_Array();


                // Graph 1 //

                el = drawingDisplayClass.add_Header("График №1");
                cv_Draw1.Children.Add(el);

                axis_y = drawingDisplayClass.add_Line(0);
                foreach (Line line in axis_y)
                {
                    cv_Draw1.Children.Add(line);
                }


                axis_x = drawingDisplayClass.add_Line(1);
                foreach (Line line in axis_x)
                {
                    cv_Draw1.Children.Add(line);
                }

                drawingDisplayClass.create_Interval(20, out intervals_x, out markers_x, out coords, 0); // x axis
                foreach (Line interval in intervals_x)
                {
                    cv_Draw1.Children.Add(interval);
                }
                foreach (TextBlock marker in markers_x)
                {
                    Canvas.SetLeft(marker, coords[iterator, 0]);
                    Canvas.SetTop(marker, coords[iterator, 1]);
                    cv_Draw1.Children.Add(marker);
                    iterator++;
                }
                iterator = 0;

                drawingDisplayClass.create_Interval(array_size, out intervals_y, out markers_y, out coords, 1); // y axis
                foreach (Line interval in intervals_y)
                {
                    cv_Draw1.Children.Add(interval);
                }
                foreach (TextBlock marker in markers_y)
                {
                    Canvas.SetLeft(marker, coords[iterator, 0]);
                    Canvas.SetTop(marker, coords[iterator, 1]);
                    cv_Draw1.Children.Add(marker);
                    iterator++;
                }
                iterator = 0;

                // Graph 2 // 

                el = drawingDisplayClass.add_Header("График №2");
                cv_Draw2.Children.Add(el);

                axis_y = drawingDisplayClass.add_Line(0);
                foreach (Line line in axis_y)
                {
                    cv_Draw2.Children.Add(line);
                }


                axis_x = drawingDisplayClass.add_Line(1);
                foreach (Line line in axis_x)
                {
                    cv_Draw2.Children.Add(line);
                }

                drawingDisplayClass.create_Interval(20, out intervals_x, out markers_x, out coords, 0); // x axis
                foreach (Line interval in intervals_x)
                {
                    cv_Draw2.Children.Add(interval);
                }
                foreach (TextBlock marker in markers_x)
                {
                    Canvas.SetLeft(marker, coords[iterator, 0]);
                    Canvas.SetTop(marker, coords[iterator, 1]);
                    cv_Draw2.Children.Add(marker);
                    iterator++;
                }
                iterator = 0;

                drawingDisplayClass.create_Interval(array_size, out intervals_y, out markers_y, out coords, 1); // y axis
                foreach (Line interval in intervals_y)
                {
                    cv_Draw2.Children.Add(interval);
                }
                foreach (TextBlock marker in markers_y)
                {
                    Canvas.SetLeft(marker, coords[iterator, 0]);
                    Canvas.SetTop(marker, coords[iterator, 1]);
                    cv_Draw2.Children.Add(marker);
                    iterator++;
                }
                iterator = 0;

                graph1 = drawingDisplayClass.drawGraph(array_size, array);
                foreach (Line line in graph1)
                {
                    try
                    {
                        cv_Draw1.Children.Add(line);
                    }
                    catch
                    {
                        // skip
                    }

                }

                graph2 = drawingDisplayClass.drawGraph(array_size, sec_array);
                foreach (Line line in graph2)
                {
                    try
                    {
                        cv_Draw2.Children.Add(line);
                    }
                    catch
                    {
                        // skip
                    }

                }
            }
            else { MessageBox.Show("Массив не сгенерирован! Будьте добры указать размер массива и нажать на кнопку <Сгенерировать массив> :)\nА так же" +
                "после генерации используйте функцию изменения позиций мин. и макс. элементов", "Информация"); }

        }
            

        private void btn_AllClear(object sender, RoutedEventArgs e)
        {
            var dg = new DataTable();
            dg_Array.ItemsSource = dg.DefaultView;
            dg_Array2.ItemsSource = dg.DefaultView;
            array_size = 0;
            Array.Clear(array);
            Array.Clear(sec_array);
            tb_SizeInput.Text = "Введите размер массива..";
            tb_ElementsCountInfo.Text = "Кол-во элементов не установлено";
            cv_Draw1.Children.Clear();
            cv_Draw2.Children.Clear();
        }

        private void btn_GenerateArray(object sender, RoutedEventArgs e)
        {
            if (array_size != 0)
            {
                array = new int[array_size];
                Random rnd = new Random();
                var dg = new DataTable();
                int rnd_value;
                var row = dg.NewRow();


                for (int i = 0; i < array_size; i++)
                {
                    dg.Columns.Add("col" + i);
                    rnd_value = rnd.Next(1, 20);
                    array[i] = rnd_value;
                    row[i] = array[i];

                }
                dg.Rows.Add(row);


                dg_Array.ItemsSource = dg.DefaultView;
                dg_Array.CanUserAddRows = false;
                dg_Array.Focusable = false;
                MessageBox.Show("Массив сгенерирован!", "Информация");

            } else
            {
                MessageBox.Show("Невозможно сгенерировать массив. Установите количество элементов!", "Информация");
            }
            
        }

        private void input_DC_Clear(object sender, MouseButtonEventArgs e)
        {
            tb_SizeInput.Text = string.Empty;
        }

        private void btn_ShowInfo(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Причиной для создания данной программы послужило задание на учебную практику.\nВыполнена студентом группы ИСП-41 Мансуровым Владимиром\n" +
                "Вариант №10", "Справка (?)");
        }

        private void btn_HowUseIt(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Как работает программа:\n1) Вам необходимо ввести размер массива в указанное поле ввода и нажать кнопку <Ввести>;\n2) Нажать кнопку <Сгенерировать массив>;\n3)Нажать" +
                "кнопку <Изменить позиции минимального и максимального элементов>;\n3) Нажать кнопку <Отрисовать графики>.\nВ ходе проделанных манипуляций вы получите 2 массива и два графика.", "Руководство пользователя");
        }

        private void cell_EditEnd(object sender, DataGridCellEditEndingEventArgs e)
        {
            Random rnd = new Random();
            if (e.EditingElement is TextBox textBox)
            {
                bool isNum = int.TryParse(textBox.Text, out int value);
                if (isNum == false || value == 0 || value < 0)
                {
                    MessageBox.Show("Введено некорректное значение! (текст, значение равно или меньше 0)\nУстановили случайное значение что бы вы не путались :)", "Информация");
                    textBox.Text = rnd.Next(1, 20).ToString();
                } 
            }
        }

        private void btn_Rework(object sender, RoutedEventArgs e)
        {
            Rework rework = new Rework();
            var dg = new DataTable();
            var row = dg.NewRow();

            sec_array = rework.ReworkArray(array);

            for (int i = 0; i < array_size; i++)
            {
                dg.Columns.Add("col" + i);
                row[i] = sec_array[i];

            }
            dg.Rows.Add(row);


            dg_Array2.ItemsSource = dg.DefaultView;
            dg_Array2.CanUserAddRows = false;
            dg_Array2.Focusable = false;
            dg_Array2.IsReadOnly= true;
            MessageBox.Show("Позиции мин. и макс. элемента изменены!", "Информация");
        }
    }
}