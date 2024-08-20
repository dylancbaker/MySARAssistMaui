using CommunityToolkit.Mvvm.ComponentModel;
using MySARAssist.Models.Events;
using MySarAssistModels.RADeMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.ViewModels.RADeMS
{
    public class RADeMSQuestionViewModel :  ObservableObject
    {
        public RADeMSQuestionViewModel()
        {
            SetOption1 = new Command(() => SetOption(0));
            SetOption2 = new Command(() => SetOption(1));
            SetOption3 = new Command(() => SetOption(2));
            ToggleDetailsCommand = new Command(() => OnToggleDetails());

        }
        public event RADeMSEventHandler ScoreChanged;

        private void HandleScoreChanged(object sender, RADeMSEventArgs e)
        {
            // we'll explain this in a minute
            this.OnScoreChanged(new RADeMSEventArgs(question, SelectedValue));
        }

        protected virtual void OnScoreChanged(RADeMSEventArgs e)
        {
            RADeMSEventHandler handler = this.ScoreChanged;
            if (handler != null)
            {
                handler(e);
            }
        }
        public bool ShowDetails { get; set; } = false;
        public bool ShowSummary { get => !ShowDetails; set => ShowDetails = !value; }


        public RADeMSQuestion question { get; set; } = new RADeMSQuestion();
        public int SelectedValue { get; set; }  = 0;

        public Color Option1ButtonColor
        {
            get
            {
                if(SelectedValue == 0)
                {
                    return Colors.Green;
                }
                return Colors.Gray;
            }
        }

        public Color Option2ButtonColor
        {
            get
            {
                if (SelectedValue == 1)
                {
                    return Colors.Orange;
                }
                return Colors.Gray;


            }
        }

        public Color Option3ButtonColor
        {
            get
            {
                if (SelectedValue == 2)
                {
                    return Colors.Red;
                }
                return Colors.Gray;

            }
        }

        public Command SetOption1 { get; }
        public Command SetOption2 { get; }
        public Command SetOption3 { get; }
        public Command ToggleDetailsCommand { get; }
        public void SetOption(int optionValue)
        {
            SelectedValue = optionValue;
            HandleScoreChanged(this, new RADeMSEventArgs(question, SelectedValue));
            OnPropertyChanged(nameof(Option1ButtonColor));
            OnPropertyChanged(nameof(Option2ButtonColor));
            OnPropertyChanged(nameof(Option3ButtonColor));

        }

        public void OnToggleDetails()
        {
            ShowDetails = !ShowDetails;
            OnPropertyChanged(nameof(ShowDetails));
            OnPropertyChanged(nameof(ShowSummary));

        }



    }
}
