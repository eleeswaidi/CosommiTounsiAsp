using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsommiTounsii.Models
{
    class Line
    {

        public int LineId { get; set; }
        public String name_line { get; set; }

        public int nbr_place_line { get; set; }

        public typeE typee {get;set;}

public Line(int lineId, string name_line, int nbr_place_line, typeE type)
        {
            LineId = lineId;
            this.name_line = name_line;
            this.nbr_place_line = nbr_place_line;
            typee = type;
        }

        public override bool Equals(object obj)
        {
            return obj is Line line &&
                   LineId == line.LineId &&
                   name_line == line.name_line &&
                   nbr_place_line == line.nbr_place_line &&
                   EqualityComparer<Enum>.Default.Equals(typee, line.typee);
        }

        public override int GetHashCode()
        {
            int hashCode = -96987508;
            hashCode = hashCode * -1521134295 + LineId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name_line);
            hashCode = hashCode * -1521134295 + nbr_place_line.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Enum>.Default.GetHashCode(typee);
            return hashCode;
        }

        public override string ToString()
        {
            return ("id:"+LineId+ "name"+name_line+"number_places"+nbr_place_line);
        }
    }

    public enum typeE
    {
        a,b
    }
}
