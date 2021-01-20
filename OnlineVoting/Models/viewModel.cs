using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onlineVoting.Models
{
    public class viewModel
    {
        public IEnumerable<Voting> Votings { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Gender> Genders { get; set; }
        public Voter Voters { get; set; }
        public IEnumerable<Vote> Vote { get; set; }



    }

}