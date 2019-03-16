using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models
{
    public class SurveyOption
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int OptionId { get; set; }
        public int? Point { get; set; }

        public virtual Survey Survey { get; set; }
        public virtual Option Option { get; set; }
    }
}
