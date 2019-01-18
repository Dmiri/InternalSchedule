using Hnatob.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hnatob.WebUI.Models
{
    public class ChoiceEmployee
    {
        public ChoiceEmployee()
        {

        }

        List<EPosition> Producer { get; set; }
        List<EPosition> Conductor { get; set; }
        List<EPosition> LightingDesigner { get; set; }
        List<EPosition> SoundEngineer { get; set; }
        List<EPosition> Accompanist { get; set; }
        List<EPosition> Actors { get; set; }
    }
}