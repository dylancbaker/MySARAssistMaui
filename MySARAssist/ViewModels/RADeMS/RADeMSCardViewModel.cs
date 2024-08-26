using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySARAssist.ViewModels.RADeMS
{
    public class RADeMSCardViewModel : ObservableObject
    {
        public RADeMSCardViewModel()
        {
            Back_Command = Back_Command = new Command(Back); ;
            OperationalRiskScore = 0;
            ResponseCapacityScore = 0;
        }

        public ICommand Back_Command { get; set; }

        public int OperationalRiskScore { get; set; }
        public int ResponseCapacityScore { get; set; }

        public double ImageWidth { get; set; }
        public double ImageHeight { get; set; }

        public string ScoreText
        {
            get
            {
                return $"Operational Risk: {OperationalRiskScore} | Response Capacity: {ResponseCapacityScore}";
            }
        }
        public void SetOpRiskScore(int score)
        {
            OperationalRiskScore = score;
            UpdateScoreImage();
            
            
            
        }

        public void SetResponseCapacityScore(int score)
        {
            ResponseCapacityScore = score;
            UpdateScoreImage();

        }

        private void UpdateScoreImage()
        {
            //this removes the white areas on the top bottom left and right from the equation
            double widthBuffer = ImageWidth * .11;
            double heightBuffer = ImageHeight * .143;
            
            double gridWidth = ImageWidth - widthBuffer * 2;
            double gridHeight = ImageHeight - heightBuffer * 2;

            double x = gridWidth * ResponseCapacityScore / 10 + widthBuffer;
            double y =ImageHeight - ( gridHeight * OperationalRiskScore / 10 + heightBuffer);

            MyDrawable.CenterX = Convert.ToInt32(x);
            MyDrawable.Centery = Convert.ToInt32(y);
            OnPropertyChanged(nameof(OperationalRiskScore));
            OnPropertyChanged(nameof(ResponseCapacityScore));
            OnPropertyChanged(nameof(MyDrawable));
            OnPropertyChanged(nameof(ScoreText));
        }

        public void SetImageSize(double width, double height)
        {
            ImageWidth = width;
            ImageHeight = height;
            UpdateScoreImage();
        }

        private async void Back()
        {
            await Shell.Current.GoToAsync("..");
        }

        public MyFirstDrawing MyDrawable { get; set; } = new MyFirstDrawing();
    }

    public class MyFirstDrawing : IDrawable
    {
        public int CenterX { get; set; } = 100;
        public int Centery { get; set; } = 100;
  
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeSize = 6;
            PointF center = new PointF(CenterX, Centery);
            canvas.DrawCircle(center, 10);

            canvas.StrokeColor = Colors.White;
            canvas.StrokeSize = 2;
            canvas.DrawCircle(center, 10);

        }
    }

}
