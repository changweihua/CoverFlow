using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Media3D;
using _3DTools;
using System.Windows.Media.Animation;


namespace CoverFlowDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {
        #region CurrentMidIndexProperty
        public static readonly DependencyProperty CurrentMidIndexProperty = DependencyProperty.Register(
            "CurrentMidIndex", typeof(double), typeof(Window1),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(CurrentMidIndexPropertyChangedCallback)));

        private static void CurrentMidIndexPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Window1 win = sender as Window1;
            if (win != null)
            {
                win.ReLayoutInteractiveVisual3D();
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ�м������
        /// </summary>
        public double CurrentMidIndex
        {
            get
            {
                return (double)this.GetValue(CurrentMidIndexProperty);
            }
            set
            {
                this.SetValue(CurrentMidIndexProperty, value);
            }
        }
        #endregion

        #region ModelAngleProperty
        public static readonly DependencyProperty ModelAngleProperty = DependencyProperty.Register(
            "ModelAngle", typeof(double), typeof(Window1),
            new FrameworkPropertyMetadata(70.0, new PropertyChangedCallback(ModelAnglePropertyChangedCallback)));


        private static void ModelAnglePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Window1 win = sender as Window1;
            if (win != null)
            {
                win.ReLayoutInteractiveVisual3D();
            }
        }

        /// <summary>
        /// ��ȡ������ģ����Y�����ת�Ƕ�
        /// </summary>
        public double ModelAngle
        {
            get
            {
                return (double)this.GetValue(ModelAngleProperty);
            }
            set
            {
                this.SetValue(ModelAngleProperty, value);
            }
        }
        #endregion

        #region XDistanceBetweenModelsProperty
        public static readonly DependencyProperty XDistanceBetweenModelsProperty = DependencyProperty.Register(
            "XDistanceBetweenModels", typeof(double), typeof(Window1),
            new FrameworkPropertyMetadata(0.5, XDistanceBetweenModelsPropertyChangedCallback));

        private static void XDistanceBetweenModelsPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Window1 win = sender as Window1;
            if (win != null)
            {
                win.ReLayoutInteractiveVisual3D();
            }
        }

        /// <summary>
        /// ��ȡ������X����������ģ�ͼ�ľ���
        /// </summary>
        public double XDistanceBetweenModels
        {
            get
            {
                return (double)this.GetValue(XDistanceBetweenModelsProperty);
            }
            set
            {
                this.SetValue(XDistanceBetweenModelsProperty, value);
            }
        }
        #endregion

        #region ZDistanceBetweenModelsProperty
        public static readonly DependencyProperty ZDistanceBetweenModelsProperty = DependencyProperty.Register(
            "ZDistanceBetweenModels", typeof(double), typeof(Window1),
            new FrameworkPropertyMetadata(0.5, ZDistanceBetweenModelsPropertyChangedCallback));

        private static void ZDistanceBetweenModelsPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Window1 win = sender as Window1;
            if (win != null)
            {
                win.ReLayoutInteractiveVisual3D();
            }
        }

        /// <summary>
        /// ��ȡ������Z����������ģ�ͼ�ľ���
        /// </summary>
        public double ZDistanceBetweenModels
        {
            get
            {
                return (double)this.GetValue(ZDistanceBetweenModelsProperty);
            }
            set
            {
                this.SetValue(ZDistanceBetweenModelsProperty, value);
            }
        }
        #endregion


        #region MidModelDistanceProperty
        public static readonly DependencyProperty MidModelDistanceProperty = DependencyProperty.Register(
            "MidModelDistance", typeof(double), typeof(Window1),
            new FrameworkPropertyMetadata(1.5, MidModelDistancePropertyChangedCallback));

        private static void MidModelDistancePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Window1 win = sender as Window1;
            if (win != null)
            {
                win.ReLayoutInteractiveVisual3D();
            }
        }

        /// <summary>
        /// ��ȡ�������м��ģ�;�������ģ�͵ľ���
        /// </summary>
        public double MidModelDistance
        {
            get
            {
                return (double)this.GetValue(MidModelDistanceProperty);
            }
            set
            {
                this.SetValue(MidModelDistanceProperty, value);
            }
        }
        #endregion

        public Window1()
        {
            InitializeComponent();
            IniComponent();

        }

        private void IniComponent()
        {
            this.LoadImageToViewport3D(this.GetUserImages());

            //test
            this.MouseDown += new MouseButtonEventHandler(Window1_MouseDown);
        }

        void Window1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.CurrentMidIndex++;
            }
            else
            {
                this.CurrentMidIndex--;
            }
        }

        

        /// <summary>
        /// ���ͼƬ���ӿ�
        /// </summary>
        /// <param name="images"></param>
        private void LoadImageToViewport3D(List<string> images)
        {
            if (images == null)
            {
                return;
            }

            for(int i=0; i<images.Count; i++)
            {
                string imageFile = images[i];

                InteractiveVisual3D iv3d = this.CreateInteractiveVisual3D(imageFile, i);
                
                this.viewport3D.Children.Add(iv3d);
            }

            this.ReLayoutInteractiveVisual3D();
        }

        /// <summary>
        /// ��ȡ��ǰ�û���ͼƬ�ļ����е�ͼƬ(���������ļ���)
        /// </summary>
        /// <returns>����ͼƬ·���б�</returns>
        private List<string> GetUserImages()
        {
            List<string> images = new List<string>();

            string path = @"C:\Users\Public\Pictures\Sample Pictures\";
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles("*.jpg", SearchOption.AllDirectories);
            if (files != null)
            {
                foreach (FileInfo file in files)
                {
                    images.Add(file.FullName);
                }
            }

            return images;
        }

       
        /// <summary>
        ///  ��ָ����ͼƬ·������һ�����Ӷ���
        /// </summary>
        /// <param name="imageFile">ͼƬ·��</param>
        /// <returns>�����Ŀ��Ӷ���</returns>
        private Visual CreateVisual(string imageFile, int index)
        {
            BitmapImage bmp = null;

            try
            {
                bmp = new BitmapImage(new Uri(imageFile));
            }
            catch
            {
            }

            Image img = new Image();
            img.Width = 50;
            img.Source = bmp;

            Border outBordre = new Border();
            outBordre.BorderBrush = Brushes.White;
            outBordre.BorderThickness = new Thickness(0.5);
            outBordre.Child = img;

            outBordre.MouseDown += delegate(object sender, MouseButtonEventArgs e)
            {
                this.CurrentMidIndex = index;
                e.Handled = true;
            };
           
            return outBordre;
            
        }

      

        

        /// <summary>
        /// ����3Dͼ��
        /// </summary>
        /// <returns>������3Dͼ��</returns>
        private Geometry3D CreateGeometry3D()
        {
            MeshGeometry3D geometry = new MeshGeometry3D();

            geometry.Positions = new Point3DCollection();
            geometry.Positions.Add(new Point3D(-1, 1, 0));
            geometry.Positions.Add(new Point3D(-1, -1, 0));
            geometry.Positions.Add(new Point3D(1, -1, 0));
            geometry.Positions.Add(new Point3D(1, 1, 0));

            geometry.TriangleIndices = new Int32Collection();
            geometry.TriangleIndices.Add(0);
            geometry.TriangleIndices.Add(1);
            geometry.TriangleIndices.Add(2);
            geometry.TriangleIndices.Add(0);
            geometry.TriangleIndices.Add(2);
            geometry.TriangleIndices.Add(3);

            geometry.TextureCoordinates = new PointCollection();
            geometry.TextureCoordinates.Add(new Point(0, 0));
            geometry.TextureCoordinates.Add(new Point(0, 1));
            geometry.TextureCoordinates.Add(new Point(1, 1));
            geometry.TextureCoordinates.Add(new Point(1, 0));

            return geometry;
        }

        /// <summary>
        /// Ϊָ��ͼƬ·������һ��3D�Ӿ�����
        /// </summary>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        private InteractiveVisual3D CreateInteractiveVisual3D(string imageFile, int index)
        {
            InteractiveVisual3D iv3d = new InteractiveVisual3D();
            iv3d.Visual = this.CreateVisual(imageFile, index);
            iv3d.Geometry = this.CreateGeometry3D();
            iv3d.Transform = this.CreateEmptyTransform3DGroup();

            return iv3d;
        }

        /// <summary>
        /// ����һ���յ�Transform3DGroup
        /// </summary>
        /// <returns></returns>
        private Transform3DGroup CreateEmptyTransform3DGroup()
        {
            Transform3DGroup group = new Transform3DGroup();
            group.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0)));
            group.Children.Add(new TranslateTransform3D(new Vector3D()));
            group.Children.Add(new ScaleTransform3D());

            return group;
        }

        /// <summary>
        /// ����InteractiveVisual3D���б��е�������任��λ�õ�
        /// </summary>
        /// <param name="index">���б��е����</param>
        /// <param name="midIndex">�б��б���Ϊ�м�������</param>
        private void GetTransformOfInteractiveVisual3D(int index, double midIndex, out double angle, out double offsetX, out double offsetZ)
        {
            double disToMidIndex = index - midIndex;


            //��ת,�����ͼƬ����תһ���Ķ���
            angle = 0;
            if (disToMidIndex < 0)
            {
                angle = this.ModelAngle;//��ߵ���תN��
            }
            else if (disToMidIndex > 0)
            {
                angle = (-this.ModelAngle);//�ұߵ���ת-N��
            }
            
         

            //ƽ��,�����ͼƬ����X�Ḻ������������չ��
            offsetX = 0;//�м�Ĳ�ƽ��
            if (Math.Abs(disToMidIndex) <= 1)
            {
                offsetX = disToMidIndex * this.MidModelDistance;
            }
            else if (disToMidIndex != 0)
            {
                offsetX = disToMidIndex * this.XDistanceBetweenModels + (disToMidIndex > 0 ? this.MidModelDistance : -this.MidModelDistance);
            }
            

            //�����ͼƬ����Z�Ḻ�����ƶ�һ��,����м�ͻ��(����ڽϽ���Ч��)
            offsetZ = Math.Abs(disToMidIndex) * -this.ZDistanceBetweenModels;
          
        }

        /// <summary>
        /// ���²���3D����
        /// </summary>
        private void ReLayoutInteractiveVisual3D()
        {
            int j=0;
            for (int i = 0; i < this.viewport3D.Children.Count; i++)
            {
                InteractiveVisual3D iv3d =  this.viewport3D.Children[i] as InteractiveVisual3D;
                if(iv3d != null)
                {
                    double angle = 0;
                    double offsetX = 0;
                    double offsetZ = 0;
                    this.GetTransformOfInteractiveVisual3D(j++, this.CurrentMidIndex,out angle,out offsetX,out offsetZ);


                    NameScope.SetNameScope(this, new NameScope());
                    this.RegisterName("iv3d", iv3d);
                    Duration time = new Duration(TimeSpan.FromSeconds(0.3));

                    DoubleAnimation angleAnimation = new DoubleAnimation(angle, time);
                    DoubleAnimation xAnimation = new DoubleAnimation(offsetX, time);
                    DoubleAnimation zAnimation = new DoubleAnimation(offsetZ, time);

                    Storyboard story = new Storyboard();
                    story.Children.Add(angleAnimation);
                    story.Children.Add(xAnimation);
                    story.Children.Add(zAnimation);

                    Storyboard.SetTargetName(angleAnimation, "iv3d");
                    Storyboard.SetTargetName(xAnimation, "iv3d");
                    Storyboard.SetTargetName(zAnimation, "iv3d");

                    Storyboard.SetTargetProperty(
                        angleAnimation,
                        new PropertyPath("(ModelVisual3D.Transform).(Transform3DGroup.Children)[0].(RotateTransform3D.Rotation).(AxisAngleRotation3D.Angle)"));

                    Storyboard.SetTargetProperty(
                        xAnimation,
                        new PropertyPath("(ModelVisual3D.Transform).(Transform3DGroup.Children)[1].(TranslateTransform3D.OffsetX)"));
                    Storyboard.SetTargetProperty(
                        zAnimation,
                        new PropertyPath("(ModelVisual3D.Transform).(Transform3DGroup.Children)[1].(TranslateTransform3D.OffsetZ)"));

                    story.Begin(this);

                }
            }
        }


    }
}