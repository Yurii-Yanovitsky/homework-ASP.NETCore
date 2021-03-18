using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebSurveyApp
{
    public class Option
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public List<Response> Responses { get; set; }
        public Option()
        {
            Responses = new List<Response>();
        }
    }
}
