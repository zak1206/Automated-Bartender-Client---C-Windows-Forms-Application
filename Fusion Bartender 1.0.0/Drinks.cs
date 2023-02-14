using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fusion_Bartender_1._0
{
    [XmlRoot("Drinks")]
    public class Drinks
    {
        [XmlElement]
        public String DrinkName { get; set; }

        [XmlElement]
        public double AmountInFlOz { get; set; }
    }
}
