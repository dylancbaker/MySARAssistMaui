using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.Models.Personnel
{
    public class Qualification
    {
        private string _Code = string.Empty;
        private string _FullName = string.Empty;
        private int _QualificationListIndex = 0;
        private int? _QRIndex;
        private int? _D4HDefaultColumn;
        private string? _PDFFieldName;
        private bool _PersonHas;

        public string Code { get => _Code; set => _Code = value; }
        public string FullName { get => _FullName; set => _FullName = value; }
        public int QualificationListIndex { get => _QualificationListIndex; set => _QualificationListIndex = value; }
        public int? QRIndex { get => _QRIndex; set => _QRIndex = value; }
        public int? D4HDefaultColumn { get => _D4HDefaultColumn; set => _D4HDefaultColumn = value; }
        public string? PDFFieldName { get => _PDFFieldName; set => _PDFFieldName = value; }
        public bool PersonHas { get => _PersonHas; set => _PersonHas = value; }
        public Qualification() { _Code = string.Empty; _FullName = string.Empty; }
        public Qualification(string code, string name, int index, int d4hindex, int qrindex, string pdf)
        {
            Code = code; FullName = name; QualificationListIndex = index; D4HDefaultColumn = d4hindex; QRIndex = qrindex; PDFFieldName = pdf;
        }
    }



}
