using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace WpfApp1
{
    public class DrawingDisplayClass
    {
        int[,] points_x = new int[20, 2];
        int[] points_y;
        public Line[] drawGraph(int count, int[] array)
        {
            SolidColorBrush color = new SolidColorBrush(Colors.Black);
            Line[] graph = new Line[30];
            Line line = new Line();
            

            for (int i = 0; i < 20; i++)
            {
                int j = 0;
                for (int k = 0; k < count - 1; k++)
                {
                    if (points_x[i, 0] == array[k])
                    {
                        
                        line = create_Line(points_x[i, 1], points_x[(array[j + 1]) - 1, 1], points_y[j], points_y[j + 1], color);
                        graph[i] = line;
                        if (array[k] == array[k+1])
                        {
                            line = create_Line(points_x[i, 1], points_x[(array[j + 1]) - 1, 1], points_y[j], points_y[j + 1], color);
                            graph[29] = line;
                        }
                    }
                    j++;
                }
                


            }
            
            

            return graph;
        }
        
        public TextBlock add_Header(String text)
        {
            TextBlock tb = new TextBlock();
            tb.Text = text;

            return tb;
        }

        public TextBlock add_Marker(int point)
        {
            TextBlock tb = new TextBlock();
            tb.Text = point.ToString();
            return tb;
        }

        public Line[] add_Line(int axis)
        {
            Line[] lines = new Line[3];
            Line line, arrow_part_left, arrow_part_right;
            SolidColorBrush color = new SolidColorBrush(Colors.Black);

            if (axis == 0) // Y - axis
            {
                line = create_Line(20, 20, 20, 260, color);
                // arrows
                arrow_part_left = create_Line(20, 10, 20, 30, color);
                arrow_part_right = create_Line(20, 30, 20, 30, color);
                // add parts in array
                lines[0] = line;
                lines[1] = arrow_part_left;
                lines[2] = arrow_part_right;

                // intervals



            }
            else if (axis == 1) // X - axis
            {
                line = create_Line(10, 710, 250, 250, color);

                // arrows
                arrow_part_left = create_Line(710, 700, 250, 240, color);
                arrow_part_right = create_Line(710, 700, 250, 260, color);
                // add parts in array
                lines[0] = line;
                lines[1] = arrow_part_left;
                lines[2] = arrow_part_right;


            }



            return lines;
        }

        public Line create_Line(int x1, int x2, int y1, int y2, SolidColorBrush color)
        {
            Line line = new Line();
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            line.StrokeThickness = 1;
            line.Stroke = color;



            return line;
        }

        public void create_Interval(int count, out Line[] intervals, out TextBlock[] markers, out int[,] coords, int axis)
        {
            points_y = new int[count];
            SolidColorBrush color = new SolidColorBrush(Colors.Black);
            int width = 710;
            int height = 250;
            Line line;
            TextBlock marker;

            intervals = new Line[count];
            markers = new TextBlock[count];
            coords = new int[count, 2];

            if (axis == 0) // x axis
            {
                int first_x1_x_axis = width / (count + 1);
                int first_y1_x_axis = height - 5;
                int first_x2_x_axis = width / (count + 1);
                int first_y2_x_axis = height + 5;

                

                int x1 = first_x1_x_axis + 20;
                int x2 = first_x2_x_axis + 20;

                for (int i = 0; i < count; i++)
                {
                    points_x[i, 1] = x1;
                    points_x[i, 0] = i + 1;
                    line = create_Line(x1, x2, first_y1_x_axis, first_y2_x_axis, color);

                    marker = add_Marker(i + 1);

                    for (int j = 0; j < 2; j++)
                    {
                        if (j == 0)
                        {
                            coords[i, j] = x1 - 2;
                        }
                        else
                        {
                            coords[i, j] = first_y1_x_axis - 14;
                        }
                    }

                    markers[i] = marker;
                    intervals[i] = line;

                    x1 += first_x1_x_axis;
                    x2 += first_x2_x_axis;
                }
            } 
            else if (axis == 1) // y axis
            {
                int first_x1_y_axis = 15;
                int first_y1_y_axis = (height) / (count + 1);
                int first_x2_y_axis = 25;
                int first_y2_y_axis = (height) / (count + 1);

                int y1 = (height - first_y1_y_axis) + 5;
                int y2 = (height - first_y2_y_axis) + 5;


                for (int i = 0; i < count; i++)
                {
                    points_y[i] = y1;
                    

                    line = create_Line(first_x1_y_axis, first_x2_y_axis, y1, y2, color);

                    marker = add_Marker(i + 1);

                    for (int j = 0; j < 2; j++)
                    {
                        if (j == 0)
                        {
                            coords[i, j] = first_x2_y_axis + 10;
                        }
                        else
                        {
                            coords[i, j] = y2 - 10;
                        }

                        
                    }

                    markers[i] = marker;
                    intervals[i] = line;

                    y1 -= first_y1_y_axis;
                    y2 -= first_y2_y_axis;
                }
            }
            
        }
    }
}
