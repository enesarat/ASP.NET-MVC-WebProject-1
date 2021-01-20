using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onlineVoting.Models
{
    public class answerModel
    {

        public string Voting { get; set; }
        public string AnswerY { get; set; }
        public int AmountY { get; set; }

        public string AnswerN { get; set; }
        public int AmountN { get; set; }


        public int Amount { get; set; }


    }
}