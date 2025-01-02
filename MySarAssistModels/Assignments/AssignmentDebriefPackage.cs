using MySarAssistModels.Clues;
using MySarAssistModels.RADeMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySarAssistModels.Assignments
{
    public class AssignmentDebriefPackage
    {
        private AssignmentDebrief? _debrief;
        private List<RADeMSScore>? _RademsScores;
        private List<Clue>? _Clues;

        public AssignmentDebrief? Debrief { get => _debrief; set => _debrief = value; }
        public List<RADeMSScore>? RademsScores { get => _RademsScores; set => _RademsScores = value; }
        public List<Clue>? Clues { get => _Clues; set => _Clues = value; }
    }
}
