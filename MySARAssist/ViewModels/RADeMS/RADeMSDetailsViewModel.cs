using CommunityToolkit.Mvvm.ComponentModel;
using MySARAssist.Models.RADeMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.ViewModels.RADeMS
{
    public class RADeMSDetailsViewModel : ObservableObject
    {
        private RADeMSCategory? _rademsCateogry = null;
        private RADeMSScore _rademsScore = new RADeMSScore();
        public RADeMSCategory? rademsCateogry { get => _rademsCateogry; private set => _rademsCateogry = value; }
        public RADeMSScore rademsScore { get => _rademsScore; }


        public RADeMSDetailsViewModel()
        {
            //DEBUG
            rademsScore.ManualOpRisk = 10;
            rademsScore.ManualRespCap = 10;
            OnPropertyChanged(nameof(ScoreColor));

        }


        public void SetRademsType(string typeIdStr)
        {
            int typeId = 0;
            if (int.TryParse(typeIdStr, out typeId))
            {
                rademsCateogry = RADeMSTools.GetCategory(typeId);


                OnPropertyChanged(nameof(CategoryTitle));
                OnPropertyChanged(nameof(OperationalRiskQuestions));
                OnPropertyChanged(nameof(ResponseCapacityQuestions));
            }

        }


        public string CategoryTitle
        {
           get
            {
                if (_rademsCateogry == null) { return string.Empty; }
                return _rademsCateogry.Name;
            }
        }

        public List<RADeMSQuestion> OperationalRiskQuestions
        {
            get
            {
                if(rademsCateogry == null || rademsCateogry.Questions == null) { return new List<RADeMSQuestion>(); }
                return rademsCateogry.Questions.Where(o => o.IsOperationalRisk).ToList();
            }
        }
        public List<RADeMSQuestion> ResponseCapacityQuestions
        {
            get
            {
                if (rademsCateogry == null || rademsCateogry.Questions == null) { return new List<RADeMSQuestion>(); }
                return rademsCateogry.Questions.Where(o => !o.IsOperationalRisk).ToList();
            }
        }
        public string RademsResultText
        {
            get { return rademsScore.ShortText; }
        }

        public Color ScoreColor { get
            {
                //return Color.FromArgb("33CCFF");
                Color col = rademsScore.ScoreColor;
                return rademsScore.ScoreColor;

            } }
    }
}
